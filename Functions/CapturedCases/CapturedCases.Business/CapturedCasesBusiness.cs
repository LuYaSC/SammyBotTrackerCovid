using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.TwilioRooms;
using TC.Connectors.TwilioRooms.GenerateRoom;
using TC.Core.Business;
using TC.Core.Data;
using TC.Functions.CapturedCases.Business.Models;

namespace TC.Functions.CapturedCases.Business
{
    public class CapturedCasesBusiness : BaseBusiness<CasosCaptados, CapturedCasesContext>, ICapturedCasesBusiness
    {
        IMapper mapper;
        int userId;
        bool isIntern;
        string userName;
        ITwilioRoomsManager service;
        P_Pacientes patient = new P_Pacientes();

        public CapturedCasesBusiness(CapturedCasesContext context, IPrincipal userInfo, IConfiguration configuration, ITwilioRoomsManager service) : base(context, userInfo, configuration)
        {
            userId = UserInfo.Identity.GetUserId<int>();
            isIntern = UserInfo.IsInRole("INTERNO");
            userName = UserInfo.Identity.Name;
            this.service = service;
        }

        public Result<List<GetCapturedCasesResult>> GetCapturedCases()
        {
            var listCases = Context.CasosCaptados.Where(x => x.InternoId == userId).ToList();
            return listCases.Any() ? Result<List<GetCapturedCasesResult>>.SetOk(mapper.Map<List<GetCapturedCasesResult>>(listCases))
                : Result<List<GetCapturedCasesResult>>.SetError("No hay registros para mostrar");
        }

        public Result<string> CreateCase(GetDataDto dto)
        {
            dto.NumeroContacto = dto.NumeroContacto.Substring(0, 3) == "591" ? dto.NumeroContacto : $"591{dto.NumeroContacto}";
            patient = Context.Pacientes.Where(x => x.NumeroContacto == dto.NumeroContacto).FirstOrDefault();
            if (patient == null)
            {
                patient = Context.Save(mapper.Map<P_Pacientes>(dto));
            }
            var newCase = Context.Save(mapper.Map<CasosAgenda>(dto));
            var searchControls = Context.Controles.Where(x => x.P_Paciente.Id == patient.Id).OrderByDescending(x => x.FechaControl).FirstOrDefault();
            if(searchControls == null)
            {

            }
            return newCase.Id == 0 ? Result<string>.SetError("Hubo un error intente nuevamente") : Result<string>.SetOk("El caso fue creado correctamente");
        }

        

        private string GenerateRoom(string nroCto, int nivel)
        {
            var room = service.GenerateRoom(new GenerateRoomRequest
            {
                CodigoSala = nroCto,
                NumeroContacto = nroCto,
                Nivel = nivel
            });

            if (!room.Header.IsOk)
            {
                return "Hubo un problema al generar la sala intente nuevamente";
            }
            //caseData.CodigoSala = "";
            //caseData.Url = $"{configuration.GetSection("UrlSala").Value}{caseData.CodigoSala}&UserName={userName.Trim()}";
            return null;
        }
    }
}
