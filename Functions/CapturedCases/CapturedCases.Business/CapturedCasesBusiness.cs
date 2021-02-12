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
        string codeRoom;
        string urlRoom;

        public CapturedCasesBusiness(CapturedCasesContext context, IPrincipal userInfo, IConfiguration configuration, ITwilioRoomsManager service) : base(context, userInfo, configuration)
        {
            userId = UserInfo.Identity.GetUserId<int>();
            isIntern = UserInfo.IsInRole("INTERNO");
            userName = UserInfo.Identity.Name;
            this.service = service;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<string, string>()
                    .ConvertUsing(str => (str ?? "").Trim());

                cfg.CreateMap<GetDataDto, P_Pacientes>()
                    .ForMember(d => d.CI, o => o.MapFrom(s => s.DocumentNumber))
                    .ForMember(d => d.NumeroContacto, o => o.MapFrom(s => s.PhoneNumber))
                    .ForMember(d => d.FechaRegistro, o => o.MapFrom(s => DateTime.Now))
                    .ForMember(d => d.UsuarioWpp, o => o.MapFrom(s => 1));

                cfg.CreateMap<GetDataDto, P_Pacientes>()
                    .ForMember(d => d.CI, o => o.MapFrom(s => s.DocumentNumber))
                    .ForMember(d => d.NumeroContacto, o => o.MapFrom(s => s.PhoneNumber))
                    .ForMember(d => d.FechaRegistro, o => o.MapFrom(s => DateTime.Now))
                    .ForMember(d => d.UsuarioWpp, o => o.MapFrom(s => 1));

                cfg.CreateMap<GetDataDto, CasosAgenda>()
                    .ForMember(d => d.PacienteId, o => o.MapFrom(s => patient.Id))
                    .ForMember(d => d.InternoId, o => o.MapFrom(s => userId))
                    .ForMember(d => d.DoctorId, o => o.MapFrom(s => 34))
                    .ForMember(d => d.DescripcionNivel, o => o.MapFrom(s => s.Level))
                    .ForMember(d => d.NombrePaciente, o => o.MapFrom(s => s.NamePatient))
                    .ForMember(d => d.HoraInicio, o => o.MapFrom(s => DateTime.Now.ToString("HH:mm")))
                    .ForMember(d => d.HoraFin, o => o.MapFrom(s => DateTime.Now.AddMinutes(15).ToString("HH:mm")))
                    .ForMember(d => d.FechaCreacion, o => o.MapFrom(s => DateTime.Now))
                    .ForMember(d => d.CodigoSala, o => o.MapFrom(s => codeRoom))
                    .ForMember(d => d.UrlSala, o => o.MapFrom(s => urlRoom));
            });
            mapper = new Mapper(config);
        }

        public Result<List<GetCapturedCasesResult>> GetCapturedCases()
        {
            var listCases = Context.CasosCaptados.Where(x => x.InternoId == userId).ToList();
            return listCases.Any() ? Result<List<GetCapturedCasesResult>>.SetOk(mapper.Map<List<GetCapturedCasesResult>>(listCases))
                : Result<List<GetCapturedCasesResult>>.SetError("No hay registros para mostrar");
        }

        public Result<string> CreateCase(GetDataDto dto)
        {
            dto.PhoneNumber = dto.PhoneNumber.Substring(0, 3) == "591" ? dto.PhoneNumber : $"591{dto.PhoneNumber}";
            patient = Context.Pacientes.Where(x => x.NumeroContacto == dto.PhoneNumber).FirstOrDefault();
            if (patient == null)
            {
                patient = Context.Save(mapper.Map<P_Pacientes>(dto));
            }
            var isValidRoom = GenerateRoom(dto);
            if(!isValidRoom)
            {
                return Result<string>.SetError("La sala no fue generada con exito comuniquese con el administrador");
            }
            var newCase = Context.Save(mapper.Map<CasosAgenda>(dto));
            var searchControls = Context.Controles.Where(x => x.P_Paciente.Id == patient.Id).OrderByDescending(x => x.FechaControl).FirstOrDefault();
            if (searchControls == null)
            {

            }
            return newCase.Id == 0 ? Result<string>.SetError("Hubo un error intente nuevamente") : Result<string>.SetOk("El caso fue creado correctamente");
        }

        private bool GenerateRoom(GetDataDto dto)
        {
            codeRoom = $"{dto.PhoneNumber}{GetTimeStamp()}";
            var room = service.GenerateRoomUrl(new GenerateRoomUrlRequest
            {
                codigoSala = codeRoom,
                numeroContacto = dto.PhoneNumber,
                nivel = dto.Level,
                fechaHoraCreacion = DateTime.Now.ToString("yyyy-MM-dd"),
                fechaHoraProgramada = DateTime.Now.ToString("yyyy-MM-dd")
            });
            if (!room.Header.IsOk)
            {
                return false;
            }
            urlRoom = $"{configuration.GetSection("UrlSala").Value}{codeRoom}&UserName={userName.Trim()}";
            return true;
        }
    }
}
