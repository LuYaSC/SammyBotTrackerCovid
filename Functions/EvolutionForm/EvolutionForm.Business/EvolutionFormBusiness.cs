using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Core.Data;
using TC.Functions.EvolutionForm.Business.Models;

namespace TC.Functions.EvolutionForm.Business
{
    public class EvolutionFormBusiness : BaseBusiness<FormDiagInicials, EvolutionFormContext>, IEvolutionFormBusiness
    {
        IMapper mapper;
        int UserId = 12;
        int sgmDiaDonacion;
        int IdFormInitial;

        public EvolutionFormBusiness(EvolutionFormContext context, IPrincipal userInfo, IConfiguration configuration) : base(context, userInfo, configuration)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HistoriaClinicas, GetHistoryClinicResult>()
                    .ForMember(d => d.TieneDiagInicial, o => o.MapFrom(s => GetHaveInitialForm(s.NumeroHistoria)))
                    .ForMember(d => d.TieneDiagEvolucion, o => o.MapFrom(s => GetHaveEvolutionForm(s.NumeroHistoria)))
                    .ForMember(d => d.IdForm, o => o.MapFrom(s => IdFormInitial))
                    .ForMember(d => d.Paciente, o => o.MapFrom(s => $"{s.Paciente.Nombre} {s.Paciente.ApellidoPaterno} {s.Paciente.ApellidoMaterno}"));
                cfg.CreateMap<SaveFormDto, FormEvolucion>();
                cfg.CreateMap<FormDiagInicials, GetBasicDatesFormResult>()
                    .ForMember(d => d.IdFormularioInicial, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.NombreMedico, o => o.MapFrom(s => $"{s.HistoriaClinicas.User.UserDetail.Name} {s.HistoriaClinicas.User.UserDetail.FirstLastName} " +
                                                                    $"{s.HistoriaClinicas.User.UserDetail.SecondLastName}"))
                    .ForMember(d => d.Paciente, o => o.MapFrom(s => $"{s.HistoriaClinicas.Paciente.Nombre} {s.HistoriaClinicas.Paciente.ApellidoPaterno} " +
                                                                    $"{s.HistoriaClinicas.Paciente.ApellidoMaterno}"))
                    .ForMember(d => d.Dia, o => o.MapFrom(s => 1))
                    .ForMember(d => d.NumeroHistoria, o => o.MapFrom(s => s.HistoriaClinicas.NumeroHistoria))
                    .ForMember(d => d.AntEstadoSalud, o => o.MapFrom(s => s.EstadoSaludActual))
                    .ForMember(d => d.AntEstadoSalud, o => o.MapFrom(s => s.EstadoSaludActual))
                    .ForMember(d => d.AntDolorGarganta, o => o.MapFrom(s => s.DolorGarganta))
                    .ForMember(d => d.AntCuantoDolorGarganta, o => o.MapFrom(s => "Sin registro anterior"))
                    .ForMember(d => d.AntDolorCabeza, o => o.MapFrom(s => s.DolorCabeza))
                    .ForMember(d => d.AntCuantoDolorCabeza, o => o.MapFrom(s => "Sin registro anterior"))
                    .ForMember(d => d.AntTos, o => o.MapFrom(s => s.Tos))
                    .ForMember(d => d.AntFiebre, o => o.MapFrom(s => s.Fiebre))
                    .ForMember(d => d.AntTemperatura, o => o.MapFrom(s => s.Temperatura))
                    .ForMember(d => d.RealizarPreguntaDonacion, o => o.MapFrom(s => 1 == sgmDiaDonacion ? true : false))
                    .ForMember(d => d.AntDificultadRespirar, o => o.MapFrom(s => s.DificultadRespirar))
                    .ForMember(d => d.AntDificultadTerminarOraciones, o => o.MapFrom(s => s.DificultadTerminarFrases))
                    .ForMember(d => d.AntCansancio, o => o.MapFrom(s => "Sin registro anterior"));
                cfg.CreateMap<FormEvolucion, GetBasicDatesFormResult>()
                    .ForMember(d => d.IdFormularioInicial, o => o.MapFrom(s => s.FormDiagInicials.Id))
                    .ForMember(d => d.IdUser, o => o.MapFrom(s => UserId))
                    .ForMember(d => d.NombreMedico, o => o.MapFrom(s => $"{s.FormDiagInicials.HistoriaClinicas.User.UserDetail.Name} {s.FormDiagInicials.HistoriaClinicas.User.UserDetail.FirstLastName} " +
                                                                    $"{s.FormDiagInicials.HistoriaClinicas.User.UserDetail.SecondLastName}"))
                    .ForMember(d => d.Paciente, o => o.MapFrom(s => $"{s.FormDiagInicials.HistoriaClinicas.Paciente.Nombre} {s.FormDiagInicials.HistoriaClinicas.Paciente.ApellidoPaterno} " +
                                                                    $"{s.FormDiagInicials.HistoriaClinicas.Paciente.ApellidoMaterno}"))
                    .ForMember(d => d.Dia, o => o.MapFrom(s => s.Dia + 1))
                    .ForMember(d => d.NumeroHistoria, o => o.MapFrom(s => s.FormDiagInicials.HistoriaClinicas.NumeroHistoria))
                    .ForMember(d => d.AntEstadoSalud, o => o.MapFrom(s => s.DescripcionSalud))
                    .ForMember(d => d.AntDolorGarganta, o => o.MapFrom(s => s.DolorGarganta))
                    .ForMember(d => d.AntCuantoDolorGarganta, o => o.MapFrom(s => s.CuantoDolorGarganta))
                    .ForMember(d => d.AntDolorCabeza, o => o.MapFrom(s => s.DolorCabeza))
                    .ForMember(d => d.AntCuantoDolorCabeza, o => o.MapFrom(s => s.CuantoDolorCabeza))
                    .ForMember(d => d.AntTos, o => o.MapFrom(s => s.Tos))
                    .ForMember(d => d.AntFiebre, o => o.MapFrom(s => s.Fiebre))
                    .ForMember(d => d.RealizarPreguntaDonacion, o => o.MapFrom(s => (s.Dia + 1 == sgmDiaDonacion) ? true : false))
                    .ForMember(d => d.AntTemperatura, o => o.MapFrom(s => s.CuantaTemperatura))
                    .ForMember(d => d.AntDificultadRespirar, o => o.MapFrom(s => s.DificultadRespirar))
                    .ForMember(d => d.AntDificultadTerminarOraciones, o => o.MapFrom(s => s.DificultadTerminarOraciones))
                    .ForMember(d => d.AntCansancio, o => o.MapFrom(s => s.SienteCansancio));
            });
            mapper = new Mapper(config);
            //this.UserId = UserInfo.Identity.GetUserId<int>();
        }

        private string GetHaveInitialForm(string NumberHistory)
        {
            var result = Context.FormDiagInicials.Where(x => x.HistoriaClinicas.NumeroHistoria == NumberHistory).FirstOrDefault();
            IdFormInitial = result != null ? result.Id : 0;
            return result == null ? "No tiene formulario Inicial" : "Si tiene registrado formulario inicial";
        }

        private string GetHaveEvolutionForm(string NumberHistory)
        {
            var result = Context.FormEvolucions.Where(x => x.FormDiagInicials.HistoriaClinicas.NumeroHistoria == NumberHistory).ToList();
            return !result.Any() ? "No tiene formularios de evolucion" : $"Si Contiene {result.Count} formularios de evolucion";
        }

        public Result<List<GetHistoryClinicResult>> GetHistoryClinics()
        {
            var forms = Context.HistoriaClinicas.Where(x => x.IdUser == UserId).ToList();
            return forms.Any() ? Result<List<GetHistoryClinicResult>>.SetOk(mapper.Map<List<GetHistoryClinicResult>>(forms))
                : Result<List<GetHistoryClinicResult>>.SetError("No se encontraron resultados");
        }

        public Result<string> SaveEvolutionForm(SaveFormDto dto)
        {
            var result = Context.Save(mapper.Map<FormEvolucion>(dto));
            return result.Id != 0 ? Result<string>.SetOk("Se Registro Exitosamente") : Result<string>.SetError("No se puedo guardar, intente nuevamente");
        }

        public Result<GetBasicDatesFormResult> GetBasicData(GetBasicDatesFormDto dto)
        {
            GetBasicDatesFormResult response = new GetBasicDatesFormResult();
            var param = Context.Parametros.Where(x => x.Groups == "DONSAN" && x.Code == "TPS").FirstOrDefault();
            sgmDiaDonacion = param != null ? int.Parse(param.Value) : 7;
            var result = Context.FormEvolucions.Where(x => x.FormDiagInicials.HistoriaClinicas.NumeroHistoria == dto.NumeroHistoria).OrderByDescending(x => x.Dia).FirstOrDefault();
            if (result == null)
            {
                var res = Context.FormDiagInicials.Where(x => x.HistoriaClinicas.NumeroHistoria == dto.NumeroHistoria).FirstOrDefault();
                response = res == null ? null : mapper.Map<GetBasicDatesFormResult>(res);
            }
            else
            {
                response = mapper.Map<GetBasicDatesFormResult>(result);
            }
            return response == null ? Result<GetBasicDatesFormResult>.SetError("No se encontraron resultados, favor crear un diagnóstico inicial") :
                Result<GetBasicDatesFormResult>.SetOk(response);
        }
    }
}
