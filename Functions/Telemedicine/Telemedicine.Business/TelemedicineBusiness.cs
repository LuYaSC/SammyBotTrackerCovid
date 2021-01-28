using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using TC.Connectors.BotWsp;
using TC.Connectors.BotWsp.SendMessageWsp;
using TC.Core.AuthConfig;
using TC.Core.Business;
using TC.Core.Data;
using TC.Functions.Telemedicine.Business.Models;

namespace TC.Functions.Telemedicine.Business
{
    public class TelemedicineBusiness : BaseBusiness<CasosAgenda, TelemedicineContext>, ITelemedicineBusiness
    {
        IBotWspManager serviceBot;
        IMapper mapper;
        int userId;
        bool isDoctor;
        bool isIntern;
        string userName;
        string messageNotification;
        string completeUserName;

        public TelemedicineBusiness(TelemedicineContext context, IPrincipal userInfo, IConfiguration configuration, IBotWspManager serviceBot) : base(context, userInfo, configuration)
        {
            this.serviceBot = serviceBot;
            userId = UserInfo.Identity.GetUserId<int>();
            completeUserName = UserInfo.Identity.GetFullUserName();
            isDoctor = UserInfo.IsInRole("MEDICO");
            isIntern = UserInfo.IsInRole("INTERNO");
            userName = UserInfo.Identity.Name;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CasosAgenda, PendingAppointmentsResult>()
                    .ForMember(d => d.CasoId, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.Finalizado, o => o.MapFrom(s => s.Finalizado ? "SI" : "NO"))
                    .ForMember(d => d.Inconcluso, o => o.MapFrom(s => s.Inconcluso ? "SI" : "NO"))
                    .ForMember(d => d.Identificacion, o => o.MapFrom(s => "AGENDADO"))
                    .ForMember(d => d.NumeroContacto, o => o.MapFrom(s => s.P_Pacientes.NumeroContacto.Substring(0, 3) == "591" ? s.P_Pacientes.NumeroContacto.Substring(3, s.P_Pacientes.NumeroContacto.Length - 3)
                    : s.P_Pacientes.NumeroContacto))
                    .ForMember(d => d.NombreInterno, o => o.MapFrom(s => GetNameUser(s.UserInterno)))
                    .ForMember(d => d.NombreDoctor, o => o.MapFrom(s => GetNameUser(s.UserDoctor)));

                cfg.CreateMap<CasosRecuperados, PendingAppointmentsResult>()
                  .ForMember(d => d.CasoId, o => o.MapFrom(s => s.CasoId))
                   .ForMember(d => d.FechaCreacion, o => o.MapFrom(s => s.FechaAtencion))
                  .ForMember(d => d.Finalizado, o => o.MapFrom(s => s.Finalizado ? "SI" : "NO"))
                  .ForMember(d => d.Inconcluso, o => o.MapFrom(s => s.Inconcluso ? "SI" : "NO"))
                  .ForMember(d => d.Identificacion, o => o.MapFrom(s => "RECUPERADO"))
                  .ForMember(d => d.DescripcionNivel, o => o.MapFrom(s => s.CasosAgenda.DescripcionNivel))
                  .ForMember(d => d.NumeroContacto, o => o.MapFrom(s => s.CasosAgenda.P_Pacientes.NumeroContacto.Substring(0, 3) == "591"
                  ? s.CasosAgenda.P_Pacientes.NumeroContacto.Substring(3, s.CasosAgenda.P_Pacientes.NumeroContacto.Length - 3) : s.CasosAgenda.P_Pacientes.NumeroContacto))
                  .ForMember(d => d.NombreInterno, o => o.MapFrom(s => GetNameUser(s.UserInterno)))
                  .ForMember(d => d.NombreDoctor, o => o.MapFrom(s => "No asignado"));

                cfg.CreateMap<UserRole, DoctorResult>()
                    .ForMember(d => d.DoctorId, o => o.MapFrom(s => s.UserId))
                    .ForMember(d => d.Name, o => o.MapFrom(s => $"{s.User.UserDetail.Name} {s.User.UserDetail.FirstLastName} {s.User.UserDetail.SecondLastName}"));
            });
            mapper = new Mapper(config);
        }

        private string GetNameUser(User user) => user.Id == 34 ? user.UserDetail.Name : $"{user.UserDetail.Name} {user.UserDetail.FirstLastName} {user.UserDetail.SecondLastName}";

        public Result<List<PendingAppointmentsResult>> GetPendingAppointments(GetDataDto dto)
        {
            if (!isDoctor && !isIntern)
            {
                return Result<List<PendingAppointmentsResult>>.SetError("Esta consulta no esta permitida para su Rol, porfavor comuniquese con el Administrador del sistema");
            }
            var OnAttended = Context.CasosAgendas.Where(x => !x.Finalizado && !x.Inconcluso && DbFunctions.TruncateTime(x.FechaCreacion) == DbFunctions.TruncateTime(DateTime.Now)).ToList();
            if (isIntern)
            {
                OnAttended = OnAttended.Where(x => x.InternoId == userId && (x.UrlSala != null || x.UrlSala != "") && (x.NombrePaciente == string.Empty || x.NombrePaciente == null) && (x.Observaciones == string.Empty || x.Observaciones == null)).ToList();
            }
            if (isDoctor)
            {
                OnAttended = OnAttended.Where(x => x.DoctorId == userId && (x.UrlSala != null || x.UrlSala != "") && (x.NombrePaciente == string.Empty || x.NombrePaciente == null) && (x.Observaciones == string.Empty || x.Observaciones == null)).ToList();
            }
            var resultOnAttended = mapper.Map<List<PendingAppointmentsResult>>(OnAttended);
            resultOnAttended.ForEach(x => x.EnAtencion = true);
            var pendings = Context.CasosAgendas.Where(x => !x.Finalizado && !x.Inconcluso && x.InternoId == 34 && x.DoctorId == 34 && DbFunctions.TruncateTime(x.FechaCreacion) == DbFunctions.TruncateTime(DateTime.Now)).ToList();
            var resultPendings = mapper.Map<List<PendingAppointmentsResult>>(pendings);
            var result = resultOnAttended.Concat(resultPendings).OrderBy(y => y.FechaCreacion).OrderByDescending(x => x.DescripcionNivel == 4).ToList();
            if (dto.Nivel != 0)
            {
                result = result.Where(x => x.DescripcionNivel == dto.Nivel).ToList();
            }
            return result.Any() ? Result<List<PendingAppointmentsResult>>.SetOk(result) : Result<List<PendingAppointmentsResult>>.SetError("No existen datos para mostrar");
        }

        public Result<List<PendingAppointmentsResult>> GetPatientsAttended(GetDataDto dto)
        {
            if (!isDoctor && !isIntern)
            {
                return Result<List<PendingAppointmentsResult>>.SetError("Esta consulta no esta permitida para su Rol, porfavor comuniquese con el Administrador del sistema");
            }
            var pendings = Context.CasosAgendas.Where(x => x.Finalizado || x.Inconcluso).OrderByDescending(x => x.FechaModificacion).ToList();
            if (dto.Nivel != 0)
            {
                pendings = pendings.Where(x => x.DescripcionNivel == dto.Nivel).ToList();
            }
            if (isIntern)
            {
                pendings = pendings.Where(x => x.InternoId == userId).ToList();
            }
            if (isDoctor)
            {
                pendings = pendings.Where(x => x.DoctorId == userId).ToList();
            }
            var result = mapper.Map<List<PendingAppointmentsResult>>(pendings);

            var recoverCases = Context.CasosRecuperados.Where(x => (x.Finalizado || x.Inconcluso) && x.InternoId == userId).OrderByDescending(x => x.FechaAtencion).ToList();
            if (dto.Nivel != 0)
            {
                recoverCases = recoverCases.Where(x => x.CasosAgenda.DescripcionNivel == dto.Nivel).ToList();
            }
            var endResult = result.Concat(mapper.Map<List<PendingAppointmentsResult>>(recoverCases));

            return endResult.Any() ? Result<List<PendingAppointmentsResult>>.SetOk(endResult.OrderByDescending(x => x.DescripcionNivel).ToList())
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
            if (isIntern && casePending.InternoId != 34 && casePending.InternoId != userId)
            {
                return Result<AssingCaseResult>.SetError("El caso ya esta asignado, favor busque y asignese otro caso");
            }
            if (isDoctor && casePending.InternoId != 34 && casePending.DoctorId != 34 && casePending.DoctorId != userId)
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
            phone = phone.Substring(0, 3) == "591" ? phone.Substring(3, phone.Length - 3) : phone;
            var patientControl = Context.Controles.Where(x => x.P_Paciente.NumeroContacto.Contains(phone)).OrderByDescending(x => x.FechaControl).FirstOrDefault();
            if (patientControl == null)
            {
                return Result<AssingCaseResult>.SetError("El paciente no tiene fichas que mostrar");
            }
            result.IdControl = patientControl.Id;
            var parameter = GetParameter("NOTSB", "TXUSNU");
            if (parameter == null)
            {
                return Result<AssingCaseResult>.SetOk(result);
            }
            var text = parameter.Description.Replace("<url>", casePending.UrlSala).Replace("<Date>", casePending.FechaCreacion.ToString("yyyy-MM-dd")).Replace("<hour>", casePending.HoraInicio)
                .Replace("<doctor>", $"{casePending.UserInterno.UserDetail.Name} {casePending.UserInterno.UserDetail.FirstLastName} {casePending.UserInterno.UserDetail.SecondLastName}");
            messageNotification = SendNotification(text, "59168216880");
            //messageNotification = SendNotification(text, casePending.P_Pacientes.NumeroContacto);
            return Result<AssingCaseResult>.SetOk(result);
        }

        private string SendNotification(string text, string phoneNumber)
        {
            string result = string.Empty;
            var uid = $"{DateTime.Now.ToString("yyyymmddhhmmss")}{userId}";
            var notifyUser = serviceBot.SendMessageWsp(new SendMessageWspRequest
            {
                Uid = uid,
                To = phoneNumber,
                Text = text
            });
            result = notifyUser.Header.IsOk ? "Notificación enviada correctamente"
                : string.IsNullOrEmpty(notifyUser.Header.FirstError) ? notifyUser.Header.Exception.Message : notifyUser.Header.FirstError;
            Context.Save(new SendNotification
            {
                SendPhone = phoneNumber,
                UserId = userId,
                Status = notifyUser.Header.IsOk,
                UID = uid,
                MessageStatus = result,
                DateCreation = DateTime.Now
            });
            return result;
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
            endCase.RecetaMedica = dto.RecetaMedica;
            endCase.FechaModificacion = DateTime.Now;
            if (dto.EnvioBrigada)
            {
                endCase.EnviadoBrigada = true;
                endCase.FechaEmisionBrigada = DateTime.Now;
                Context.Save(new CasosGrupoRescate
                {
                    CasoId = dto.CasoId,
                    DireccionExplicita = dto.DireccionExplicita,
                    Observaciones = string.Empty,
                    FechaPriorizacion = DateTime.Now
                });
            }
            Context.Save(endCase);
            var parameter = GetParameter("NOTSB", "TXUSCF");
            if (parameter == null)
            {
                return Result<string>.SetOk($"Caso Atendido, favor iniciar otro caso, Notificación no enviada");
            }
            var text = parameter.Description.Replace("<status>", dto.Finalizar ? "a finalizado" : "no ha finalizado").Replace("<text>", endCase.RecetaMedica)
                .Replace("<doctor>", completeUserName);
            messageNotification = SendNotification(text, "59168216880");
            //messageNotification = SendNotification(text, endCase.P_Pacientes.NumeroContacto);
            return Result<string>.SetOk($"Caso Atendido, favor iniciar otro caso, {messageNotification}");
        }

        public Result<List<DoctorResult>> GetListDoctor()
        {
            if (!isDoctor && !isIntern)
            {
                return Result<List<DoctorResult>>.SetError("Esta consulta no esta permitida para su Rol, porfavor comuniquese con el Administrador del sistema");
            }
            var listDoctor = Context.UserRoles.Where(x => x.RoleId == 2 && x.UserId != 34).ToList();
            return listDoctor.Any() ? Result<List<DoctorResult>>.SetOk(mapper.Map<List<DoctorResult>>(listDoctor)) : Result<List<DoctorResult>>.SetError("No se encontraron Doctores");
        }
    }
}
