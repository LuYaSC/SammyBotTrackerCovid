﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using TC.Core.Business;
using TC.Core.Data;
using TC.Functions.Telemedicine.Business.Models;

namespace TC.Functions.Telemedicine.Business
{
    public class TelemedicineBusiness : BaseBusiness<CasosAgenda, TelemedicineContext>, ITelemedicineBusiness
    {
        IMapper mapper;
        int userId;
        bool isDoctor;
        bool isIntern;
        string userName;

        public TelemedicineBusiness(TelemedicineContext context, IPrincipal userInfo, IConfiguration configuration) : base(context, userInfo, configuration)
        {
            userId = UserInfo.Identity.GetUserId<int>();
            isDoctor = UserInfo.IsInRole("MEDICO");
            isIntern = UserInfo.IsInRole("INTERNO");
            userName = UserInfo.Identity.Name;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CasosAgenda, PendingAppointmentsResult>()
                    .ForMember(d => d.CasoId, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.Finalizado, o => o.MapFrom(s => s.Finalizado ? "SI" : "NO"))
                    .ForMember(d => d.Inconcluso, o => o.MapFrom(s => s.Inconcluso ? "SI" : "NO"))
                    .ForMember(d => d.NumeroContacto, o => o.MapFrom(s => s.P_Pacientes.NumeroContacto))
                    .ForMember(d => d.NombreInterno, o => o.MapFrom(s => GetNameUser(s.UserInterno)))
                    .ForMember(d => d.NombreDoctor, o => o.MapFrom(s => GetNameUser(s.UserDoctor)));

                cfg.CreateMap<UserRole, DoctorResult>()
                    .ForMember(d => d.DoctorId, o => o.MapFrom(s => s.UserId))
                    .ForMember(d => d.Name, o => o.MapFrom(s => $"{s.User.UserDetail.Name} {s.User.UserDetail.FirstLastName} {s.User.UserDetail.SecondLastName}"));
            });
            mapper = new Mapper(config);
        }

        private string GetNameUser(User user) => user.Id == 34 ? user.UserDetail.Name : $"{user.UserDetail.Name} {user.UserDetail.FirstLastName} {user.UserDetail.SecondLastName}";

        public Result<List<PendingAppointmentsResult>> GetPendingAppointments()
        {
            if (!isDoctor && !isIntern)
            {
                return Result<List<PendingAppointmentsResult>>.SetError("Esta consulta no esta permitida para su Rol, porfavor comuniquese con el Administrador del sistema");
            }
            var pendings = Context.CasosAgendas.Where(x => !x.Finalizado && !x.Inconcluso && x.InternoId == 34 && x.DoctorId == 34 && DbFunctions.TruncateTime(x.FechaCreacion) == DbFunctions.TruncateTime(DateTime.Now)).ToList();
            return pendings.Any() ? Result<List<PendingAppointmentsResult>>.SetOk(mapper.Map<List<PendingAppointmentsResult>>(pendings))
                : Result<List<PendingAppointmentsResult>>.SetError("No existen datos para mostrar");
        }

        public Result<List<PendingAppointmentsResult>> GetPatientsAttended(GetDataDto dto)
        {
            if (!isDoctor && !isIntern)
            {
                return Result<List<PendingAppointmentsResult>>.SetError("Esta consulta no esta permitida para su Rol, porfavor comuniquese con el Administrador del sistema");
            }
            var pendings = Context.CasosAgendas.Where(x => x.Finalizado || x.Inconcluso && (dto.Nivel == 0 || x.DescripcionNivel == dto.Nivel) && (x.InternoId == userId || x.DoctorId == userId))
                                               .OrderByDescending(x => x.FechaModificacion).ToList();
            return pendings.Any() ? Result<List<PendingAppointmentsResult>>.SetOk(mapper.Map<List<PendingAppointmentsResult>>(pendings))
                : Result<List<PendingAppointmentsResult>>.SetError("No existen datos para mostrar");
        }

        public Result<AssingCaseResult> AssingCase(GetDataDto dto)
        {
            if (!isDoctor && !isIntern)
            {
                return Result<AssingCaseResult>.SetError("Esta consulta no esta permitida para su Rol, porfavor comuniquese con el Administrador del sistema");
            }
            AssingCaseResult result = new AssingCaseResult();
            var casePending = Context.CasosAgendas.Where(x => x.Id == dto.CasoId).FirstOrDefault();
            if (casePending == null)
            {
                return Result<AssingCaseResult>.SetError("El caso no existe, verifique porfavor");
            }
            if (isIntern && casePending.InternoId != 34)
            {
                return Result<AssingCaseResult>.SetError("El caso ya esta asignado, favor busque y asignese otro caso");
            }
            if (isDoctor && casePending.InternoId != 34 && casePending.DoctorId != 34)
            {
                return Result<AssingCaseResult>.SetError("El caso ya esta asignado, favor busque y asignese otro caso");
            }
            if (casePending.Finalizado || casePending.Inconcluso)
            {
                return Result<AssingCaseResult>.SetError("El caso ya fue atendido, favor busque y asignese otro caso");
            }
            if (isDoctor)
            {
                casePending.DoctorId = userId;
            }
            if (isIntern)
            {
                casePending.InternoId = userId;
            }
            casePending.UrlSala = result.UrlSala = $"{configuration.GetSection("UrlSala").Value}{casePending.CodigoSala}&UserName={userName.Trim()}";
            Context.Save(casePending);
            result.Message = "Caso Asignado Correctamente favor, verifique los datos del paciente antes de iniciar la consulta";
            var phone = casePending.P_Pacientes.NumeroContacto;
            phone = phone.Substring(0, 3) == "591" ? phone.Substring(3, phone.Length) : phone;
            var patientControl = Context.Controles.Where(x => x.P_Paciente.NumeroContacto.Contains(phone)).OrderByDescending(x => x.FechaControl).FirstOrDefault();
            if (patientControl == null)
            {
                return Result<AssingCaseResult>.SetError("El paciente no tiene fichas que mostrar");
            }
            result.IdControl = patientControl.Id;
            return Result<AssingCaseResult>.SetOk(result);
        }

        public Result<string> AddingDoctor(GetDataDto dto)
        {
            if (!isDoctor && !isIntern)
            {
                return Result<string>.SetError("Esta consulta no esta permitida para su Rol, porfavor comuniquese con el Administrador del sistema");
            }
            var endCase = Context.CasosAgendas.Where(x => x.Id == dto.CasoId).FirstOrDefault();
            if (endCase == null)
            {
                return Result<string>.SetError("El caso no fue encontrado, favor intentar nuevamente");
            }
            if (endCase.DoctorId != 34)
            {
                return Result<string>.SetError($"El doctor {endCase.UserDoctor.UserDetail.Name} {endCase.UserDoctor.UserDetail.FirstLastName} {endCase.UserDoctor.UserDetail.SecondLastName}," +
                    $"ya fue asignado al caso");
            }
            endCase.DoctorId = dto.DoctorId;
            Context.Save(endCase);
            return Result<string>.SetOk("Doctor Adicionado correctamente al caso");
        }

        public Result<string> UpdateCase(GetDataDto dto)
        {
            if (!isDoctor && !isIntern)
            {
                return Result<string>.SetError("Esta consulta no esta permitida para su Rol, porfavor comuniquese con el Administrador del sistema");
            }
            var endCase = Context.CasosAgendas.Where(x => x.Id == dto.CasoId).FirstOrDefault();
            if (endCase == null)
            {
                return Result<string>.SetError("El caso no fue encontrado, favor intentar nuevamente");
            }
            if (dto.Finalizar)
            {
                endCase.Finalizado = true;
            }
            else
            {
                endCase.Inconcluso = true;
            }
            endCase.NombrePaciente = dto.NombrePaciente;
            endCase.Observaciones = dto.Observaciones;
            endCase.FechaModificacion = DateTime.Now;
            Context.Save(endCase);
            return Result<string>.SetOk("Caso Atendido, favor iniciar otro caso");
        }

        public Result<List<DoctorResult>> GetListDoctor()
        {
            if (!isDoctor && !isIntern)
            {
                return Result<List<DoctorResult>>.SetError("Esta consulta no esta permitida para su Rol, porfavor comuniquese con el Administrador del sistema");
            }
            var listDoctor = Context.UserRoles.Where(x => x.RoleId == 3 && x.UserId != 34).ToList();
            return listDoctor.Any() ? Result<List<DoctorResult>>.SetOk(mapper.Map<List<DoctorResult>>(listDoctor)) : Result<List<DoctorResult>>.SetError("No se encontraron Doctores");
        }
    }
}