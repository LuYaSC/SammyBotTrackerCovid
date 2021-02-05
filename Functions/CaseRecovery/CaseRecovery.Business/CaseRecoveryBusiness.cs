using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using TC.Connectors.TwilioRooms;
using TC.Connectors.TwilioRooms.GenerateRoom;
using TC.Core.Business;
using TC.Core.Data;
using TC.Functions.CaseRecovery.Business.Models;

namespace CaseRecovery.Business
{
    public class CaseRecoveryBusiness : BaseBusiness<CasosRecuperados, CaseRecoveryContext>, ICaseRecoveryBusiness
    {
        IMapper mapper;
        int userId;
        bool isIntern;
        string userName;
        ITwilioRoomsManager service;
        private const string ROOM_OK = "La Sala se generó con exito, favor verificar los datos del paciente para iniciar la videollamada";
        int totalItems;

        public CaseRecoveryBusiness(CaseRecoveryContext context, IPrincipal userInfo, IConfiguration configuration, ITwilioRoomsManager service) : base(context, userInfo, configuration)
        {
            userId = UserInfo.Identity.GetUserId<int>();
            isIntern = UserInfo.IsInRole("INTERNO");
            userName = UserInfo.Identity.Name;
            this.service = service;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CasosAgenda, CasesForRecoverResult>()
                    .ForMember(d => d.CasoId, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.FechaAtencion, o => o.MapFrom(s => s.FechaModificacion))
                    .ForMember(d => d.NumeroContacto, o => o.MapFrom(s => s.P_Pacientes.NumeroContacto.Substring(3, s.P_Pacientes.NumeroContacto.Length - 3)))
                    .ForMember(d => d.Nivel, o => o.MapFrom(s => s.DescripcionNivel));

                //cfg.CreateMap<UserRole, DoctorResult>()
                //    .ForMember(d => d.DoctorId, o => o.MapFrom(s => s.UserId))
                //    .ForMember(d => d.Name, o => o.MapFrom(s => $"{s.User.UserDetail.Name} {s.User.UserDetail.FirstLastName} {s.User.UserDetail.SecondLastName}"));
            });
            mapper = new Mapper(config);
        }

        public Result<List<CasesForRecoverResult>> GetCasesForRecovers(GetDataDto dto)
        {
            var listCases = Context.CasosAgendas.Where(x => x.NombrePaciente.Contains("SN") && x.Inconcluso && !x.CasoRecuperado).OrderByDescending(x => x.DescripcionNivel).ToList();
            if (dto.Nivel != 0)
            {
                listCases = listCases.Where(x => x.DescripcionNivel == dto.Nivel).ToList();
            }
            totalItems = listCases.Count;
            var listpag = listCases.OrderBy(x => x.Id).Skip(dto.PageSize * (dto.CurPage - 1)).Take(dto.PageSize).ToList();
            var result = mapper.Map<List<CasesForRecoverResult>>(listpag);
            foreach (var date in result)
            {
                var verify = Context.CasosRecuperados.Where(x => x.CasoId == date.CasoId).FirstOrDefault();
                if (verify != null && verify.InternoId == userId) date.Retomar = true;
                if (verify != null && verify.InternoId != userId) date.Eliminar = true;
                if (verify != null && verify.Finalizado) date.Eliminar = true;
                if (verify != null && verify.Inconcluso) date.Eliminar = true;
            }
            return result.Any() ? Result<List<CasesForRecoverResult>>.SetOk(result.Where(x => !x.Eliminar).ToList()) : Result<List<CasesForRecoverResult>>.SetError("No hay registros para mostrar");
        }

        public Result<GenerateRoomResult> RecoverCase(GetDataDto dto)
        {
            var caseData = Context.CasosAgendas.Where(x => x.Id == dto.CasoId).FirstOrDefault();
            if (caseData == null)
            {
                return Result<GenerateRoomResult>.SetError("El caso no existe");
            }
            var phone = caseData.P_Pacientes.NumeroContacto;
            phone = phone.Substring(0, 3) == "591" ? phone.Substring(3, phone.Length - 3) : phone;
            var patientControl = Context.Controles.Where(x => x.P_Paciente.NumeroContacto.Contains(phone)).OrderByDescending(x => x.FechaControl).FirstOrDefault()?.Id;
            var validCase = Context.CasosRecuperados.Where(x => x.CasoId == dto.CasoId).FirstOrDefault();
            if (validCase != null)
            {
                if (validCase.InternoId == userId)
                {

                    return Result<GenerateRoomResult>.SetOk(new GenerateRoomResult
                    {
                        CasoId = validCase.CasoId,
                        Message = "Caso retomado con exito",
                        Url = validCase.Url,
                        ControlId = patientControl,
                        CasoRetomado = true
                    });
                }
                else
                {
                    return Result<GenerateRoomResult>.SetError("El caso ya esta siendo recuperado por otro doctor, favor buscar otro caso");
                }
            }
            var recCase = Context.Save(new CasosRecuperados
            {
                CasoId = dto.CasoId,
                FechaAtencion = DateTime.Now,
                EnvioBrigada = false,
                Finalizado = false,
                Inconcluso = false,
                InternoId = userId,
                Observaciones = string.Empty,
                Url = string.Empty,
                CodigoSala = string.Empty,
            });
            return recCase.Id == 0 ? Result<GenerateRoomResult>.SetError("Hubo un error intente nuevamente")
                : Result<GenerateRoomResult>.SetOk(new GenerateRoomResult
                {
                    CasoId = recCase.CasoId,
                    Message = "Caso Asignado con exito",
                    Url = string.Empty,
                    ControlId = patientControl,
                    CasoRetomado = false
                });
        }

        public Result<GenerateRoomResult> GenerateRoom(GetDataDto dto)
        {
            var oldCase = Context.CasosAgendas.Where(x => x.Id == dto.CasoId).FirstOrDefault();
            if (oldCase == null)
            {
                return Result<GenerateRoomResult>.SetError("El caso no existe");
            }
            var caseData = Context.CasosRecuperados.Where(x => x.CasoId == dto.CasoId).FirstOrDefault();
            if (caseData == null)
            {
                return Result<GenerateRoomResult>.SetError("El caso no existe");
            }
            if(!string.IsNullOrEmpty(caseData.Url))
            {
                return Result<GenerateRoomResult>.SetOk(new GenerateRoomResult { CasoId = caseData.CasoId, Url = caseData.Url, Message = ROOM_OK });
            }
            var codeRoom = $"{caseData.CasosAgenda.P_Pacientes.NumeroContacto}{GetTimeStamp()}";
            var room = service.GenerateRoomUrl(new GenerateRoomUrlRequest
            {
                codigoSala = codeRoom,
                numeroContacto = caseData.CasosAgenda.P_Pacientes.NumeroContacto,
                nivel = caseData.CasosAgenda.DescripcionNivel,
                fechaHoraCreacion = DateTime.Now.ToString("yyyy-MM-dd"),
                fechaHoraProgramada = DateTime.Now.ToString("yyyy-MM-dd")
            });
            if (!room.Header.IsOk)
            {
                return Result<GenerateRoomResult>.SetError("Hubo un problema al generar la sala intente nuevamente");
            }
            caseData.CodigoSala = codeRoom;
            caseData.Url =  $"{configuration.GetSection("UrlSala").Value}{codeRoom}&UserName={userName.Trim()}";
            Context.Save(caseData);
            return Result<GenerateRoomResult>.SetOk(new GenerateRoomResult { CasoId = caseData.CasoId, Url = caseData.Url, Message = ROOM_OK });
        }

        private string GetTimeStamp() => ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();

        public Result<string> FinalizeCase(GetDataDto dto)
        {
            var caseData = Context.CasosRecuperados.Where(x => x.CasoId == dto.CasoId).FirstOrDefault();
            if (caseData == null)
            {
                return Result<string>.SetError("El caso no existe");
            }
            caseData.FechaFinalizacion = DateTime.Now;
            if (dto.Finalizar) caseData.Finalizado = true;
            else caseData.Inconcluso = true;
            caseData.Observaciones = dto.Observaciones;
            caseData.RecetaMedica = dto.RecetaMedica;
            caseData.NombrePaciente = dto.NombrePaciente;
            if (dto.EnvioBrigada)
            {
                caseData.EnvioBrigada = true;
                Context.Save(new CasosGrupoRescate
                {
                    CasoId = dto.CasoId,
                    DireccionExplicita = dto.DireccionExplicita,
                    Observaciones = string.Empty,
                    FechaPriorizacion = DateTime.Now
                });
            }
            Context.Save(caseData);
            return Result<string>.SetOk("Caso atendido con exito");
        }
    }
}
