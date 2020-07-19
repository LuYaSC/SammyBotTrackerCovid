using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public CaseRecoveryBusiness(CaseRecoveryContext context, IPrincipal userInfo, IConfiguration configuration, ITwilioRoomsManager service) : base(context, userInfo, configuration)
        {
            userId = UserInfo.Identity.GetUserId<int>();
            isIntern = UserInfo.IsInRole("INTERNO");
            userName = UserInfo.Identity.Name;
            this.service = service;
        }

        public Result<List<CasesForRecoverResult>> GetCasesForRecovers()
        {
            var listCases = Context.CasosAgendas.Where(x => x.NombrePaciente == "SN" && !x.Inconcluso && x.CasoRecuperado).ToList();
            return listCases.Any() ? Result<List<CasesForRecoverResult>>.SetOk(mapper.Map<List<CasesForRecoverResult>>(listCases))
                : Result<List<CasesForRecoverResult>>.SetError("No hay registros para mostrar");
        }

        public Result<string> RecoverCase(GetDataDto dto)
        {
            var caseData = Context.CasosAgendas.Where(x => x.Id == dto.CasoId).FirstOrDefault();
            if (caseData == null)
            {
                return Result<string>.SetError("El caso no existe");
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
            return recCase.CasoId == 0 ? Result<string>.SetError("Hubo un error intente nuevamente") : Result<string>.SetOk("El caso fue creado correctamente");
        }

        public Result<string> GenerateRoom(GetDataDto dto)
        {
            var caseData = Context.CasosRecuperados.Where(x => x.Id == dto.CasoId).FirstOrDefault();
            if (caseData == null)
            {
                return Result<string>.SetError("El caso no existe");
            }
            var room = service.GenerateRoom(new GenerateRoomRequest
            {
                CodigoSala = caseData.CasosAgenda.P_Pacientes.NumeroContacto,
                NumeroContacto = caseData.CasosAgenda.P_Pacientes.NumeroContacto,
                Nivel = caseData.CasosAgenda.DescripcionNivel
            });

            if (!room.Header.IsOk)
            {
                return Result<string>.SetError("Hubo un problema al generar la sala intente nuevamente");
            }
            caseData.CodigoSala = "";
            caseData.Url = $"{configuration.GetSection("UrlSala").Value}{caseData.CodigoSala}&UserName={userName.Trim()}";
            return null;
        }

        public Result<string> FinalizeCase(GetDataDto dto)
        {
            var caseData = Context.CasosRecuperados.Where(x => x.Id == dto.CasoId).FirstOrDefault();
            if (caseData == null)
            {
                return Result<string>.SetError("El caso no existe");
            }
            caseData.FechaFinalizacion = DateTime.Now;
            if (dto.Finalizar)
            {
                caseData.Finalizado = true;
            }
            else
            {
                caseData.Inconcluso = true;
            }
            caseData.Observaciones = dto.Observaciones;
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
            return Result<string>.SetOk("Caso atendido con exito");
        }
    }
}
