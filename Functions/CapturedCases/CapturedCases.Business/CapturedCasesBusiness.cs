using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.BotWsp;
using TC.Connectors.BotWsp.SendMessageWsp;
using TC.Connectors.HealthInsurance;
using TC.Connectors.HealthInsurance.GetInsurancePerson;
using TC.Connectors.TwilioRooms;
using TC.Connectors.TwilioRooms.GenerateRoom;
using TC.Core.Business;
using TC.Core.Data;
using TC.Functions.CapturedCases.Business.Models;
using TC.Core.AuthConfig;

namespace TC.Functions.CapturedCases.Business
{
    public class CapturedCasesBusiness : BaseBusiness<CasosCaptados, CapturedCasesContext>, ICapturedCasesBusiness
    {
        IMapper mapper;
        int userId;
        bool isIntern;
        string userName = string.Empty;
        string codeRoom = string.Empty;
        string urlRoom = string.Empty;
        string patientName = string.Empty;
        string doctorName = string.Empty;
        P_Pacientes patient = new P_Pacientes();

        //Connectors
        ITwilioRoomsManager serviceRooms;
        IHealthInsuranceManager serviceInsurance;
        IBotWspManager serviceBot;

        public CapturedCasesBusiness(CapturedCasesContext context, IPrincipal userInfo, IConfiguration configuration, ITwilioRoomsManager serviceRooms, IHealthInsuranceManager serviceInsurance,
            IBotWspManager serviceBot) : base(context, userInfo, configuration)
        {
            userId = UserInfo.Identity.GetUserId<int>();
            isIntern = UserInfo.IsInRole("INTERNO");
            userName = UserInfo.Identity.Name;
            doctorName = UserInfo.Identity.GetFullUserName();


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
                    .ForMember(d => d.FechaActualizacion, o => o.MapFrom(s => DateTime.Now))
                    .ForMember(d => d.UsuarioWpp, o => o.MapFrom(s => 1));

                cfg.CreateMap<GetDataDto, CasosAgenda>()
                    .ForMember(d => d.PacienteId, o => o.MapFrom(s => patient.Id))
                    .ForMember(d => d.InternoId, o => o.MapFrom(s => userId))
                    .ForMember(d => d.DoctorId, o => o.MapFrom(s => 34))
                    .ForMember(d => d.DescripcionNivel, o => o.MapFrom(s => s.Level))
                    .ForMember(d => d.NombrePaciente, o => o.MapFrom(s => patientName))
                    .ForMember(d => d.HoraInicio, o => o.MapFrom(s => DateTime.Now.ToString("HH:mm")))
                    .ForMember(d => d.HoraFin, o => o.MapFrom(s => DateTime.Now.AddMinutes(15).ToString("HH:mm")))
                    .ForMember(d => d.FechaCreacion, o => o.MapFrom(s => DateTime.Now))
                    .ForMember(d => d.CodigoSala, o => o.MapFrom(s => codeRoom))
                    .ForMember(d => d.UrlSala, o => o.MapFrom(s => urlRoom));

                cfg.CreateMap<GetDataDto, GetInsurancePersonRequest>();

                cfg.CreateMap<CasosAgenda, PreviousAttention>()
                    .ForMember(d => d.CaseId, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.AssignedDoctor, o => o.MapFrom(s => $"{s.UserDoctor.UserDetail.Name} {s.UserDoctor.UserDetail.FirstLastName}"))
                    .ForMember(d => d.DateAttention, o => o.MapFrom(s => s.FechaModificacion));

                cfg.CreateMap<CasosAgenda, PreviousCaseResult>()
                    .ForMember(d => d.DoctorAttended, o => o.MapFrom(s => $"{s.UserDoctor.UserDetail.Name} {s.UserDoctor.UserDetail.FirstLastName} {s.UserDoctor.UserDetail.SecondLastName}"))
                    .ForMember(d => d.DateAttention, o => o.MapFrom(s => s.FechaModificacion))
                    .ForMember(d => d.Observations, o => o.MapFrom(s => s.Observaciones))
                    .ForMember(d => d.Prescription, o => o.MapFrom(s => s.RecetaMedica))
                    .ForMember(d => d.IsBrigade, o => o.MapFrom(s => s.EnviadoBrigada));

            });
            mapper = new Mapper(config);
            this.serviceRooms = serviceRooms;
            this.serviceInsurance = serviceInsurance;
            this.serviceBot = serviceBot;
        }

        public Result<List<GetCapturedCasesResult>> GetCapturedCases()
        {
            var listCases = Context.CasosCaptados.Where(x => x.InternoId == userId).ToList();
            return listCases.Any() ? Result<List<GetCapturedCasesResult>>.SetOk(mapper.Map<List<GetCapturedCasesResult>>(listCases))
                : Result<List<GetCapturedCasesResult>>.SetError("No hay registros para mostrar");
        }

        public Result<CreateCaseResult> CreateCase(GetDataDto dto)
        {
            CreateCaseResult result = new CreateCaseResult();
            dto.PhoneNumber = dto.PhoneNumber.Substring(0, 3) == "591" ? dto.PhoneNumber : $"591{dto.PhoneNumber}";
            //Validate Patient
            patient = Context.Pacientes.Where(x => x.NumeroContacto == dto.PhoneNumber).FirstOrDefault();
            if (patient == null)
            {
                result.IsNewPatient = true;
                patient = Context.Save(mapper.Map<P_Pacientes>(dto));
            }
            //Validate Insurance
            var personInsurance = serviceInsurance.GetInsurancePerson(mapper.Map<GetInsurancePersonRequest>(dto));
            if (personInsurance.Header.IsOk)
            {
                if (personInsurance.Body != null)
                {
                    result.IsInsurance = true;
                    patientName = result.NamePatient = personInsurance.Body.Name;
                    result.InsuranceName = personInsurance.Body.Insurance;
                    result.Municipality = personInsurance.Body.Municipality;
                    result.Departament = personInsurance.Body.Departament;
                    result.BornDate = personInsurance.Body.BornDate;
                }
            }
            //Get Dates Patient if exists
            if (!result.IsNewPatient)
            {
                result.PreviousAttentions = mapper.Map<List<PreviousAttention>>(Context.CasosAgendas.Where(x => x.PacienteId == patient.Id && x.Finalizado).ToList());
                var searchControls = Context.Controles.Where(x => x.P_Paciente.Id == patient.Id).OrderByDescending(x => x.FechaControl).FirstOrDefault();
                if (searchControls != null)
                {
                    result.LastControlId = searchControls.Id;
                }
            }
            //Room Generation y Generate Case
            var isValidRoom = GenerateRoom(dto);
            if (!isValidRoom)
            {
                return Result<CreateCaseResult>.SetError("La sala no fue generada, comuniquese con el administrador");
            }
            result.UrlRoom = urlRoom;
            var newCase = Context.Save(mapper.Map<CasosAgenda>(dto));
            result.CaseId = newCase.Id;

            //Notifiation Patient
            var parameter = GetParameter("NOTSB", "TXUSNU");
            if (parameter == null)
            {
                return Result<CreateCaseResult>.SetOk(result);
            }
            var text = parameter.Description.Replace("<url>", urlRoom).Replace("<Date>", DateTime.Now.ToString("yyyy-MM-dd")).Replace("<hour>", newCase.HoraInicio)
                .Replace("<doctor>", doctorName);
            var messageNotification = SendNotification(text, "59168216880");
            //var messageNotification = SendNotification(text, dto.PhoneNumber);
            return newCase.Id == 0 ? Result<CreateCaseResult>.SetError("Hubo un error intente nuevamente") : Result<CreateCaseResult>.SetOk(result);
        }

        public Result<PreviousCaseResult> GetPreviousCase(PreviousCaseDto dto)
        {
            var prevCase = Context.CasosAgendas.Where(x => x.Id == dto.CaseId).FirstOrDefault();
            return prevCase == null ? Result<PreviousCaseResult>.SetError("El caso no existe verifique porfavor") 
                : Result<PreviousCaseResult>.SetOk(mapper.Map<PreviousCaseResult>(prevCase));
        }

        private bool GenerateRoom(GetDataDto dto)
        {
            codeRoom = $"{dto.PhoneNumber}{GetTimeStamp()}";
            var room = serviceRooms.GenerateRoomUrl(new GenerateRoomUrlRequest
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
    }
}
