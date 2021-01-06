using AutoMapper;
using Microsoft.Extensions.Configuration;
using SBTC.Functions.Patients.Business.BaseBusiness;
using SBTC.Functions.Patients.Business.Models;
using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Business
{
    public class InitialDiagnosisCardBusiness : BaseBusiness<FormDiagInicials, PatientContext>, IInitialDiagnosisCardBusiness
    {
        IMapper mapper;
        public InitialDiagnosisCardBusiness(PatientContext context, IConfiguration configuration) : base(context, null, configuration)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FormDiagDto, FormDiagInicials>()
                    .ForMember(d => d.Paciente, o => o.MapFrom(s => s));
                cfg.CreateMap<FormDiagDto, Pacientes>();
                cfg.CreateMap<FormDiagDto, HistoriaClinicas>();
                cfg.CreateMap<FormDiagInicials, GetFormPrevious>()
                    .ForMember(d => d.Description, o => o.MapFrom(s => $"Paciente: {s.Paciente.Nombre} {s.Paciente.ApellidoPaterno} HC.{s.Paciente.HistoriaClinica.NumeroHistoria} Mat.{s.Paciente.Matricula}"));
                cfg.CreateMap<FormDiagInicials, FormDiagResult>()
                    .ForMember(d => d.NumeroHistoria, o => o.MapFrom(s => s.Paciente.HistoriaClinica.NumeroHistoria));
                cfg.CreateMap<Pacientes, FormDiagResult>();
            });
            mapper = new Mapper(config);
        }

        public Result<string> SaveForm(FormDiagDto dto)
        {
            var form = mapper.Map<FormDiagInicials>(dto);
            form.Paciente.HistoriaClinica = mapper.Map<HistoriaClinicas>(dto);
            var result = Context.Save(form);
            return result.Id == 0 ? Result<string>.SetError("el formulario no se pudo guardar correctamente")
                : Result<string>.SetOk($"el formulario se guardo correctamente el nro de formulario es: {result.Id}");
        }

        public Result<FormDiagResult> GetFormById(GetFormById dto)
        {
            var result = Context.formDiagInicials.Where(x => x.Id == dto.FormId).FirstOrDefault();
            var aux = mapper.Map<FormDiagResult>(result);
            var aux2 = mapper.Map(result, mapper.Map<FormDiagResult>(result.Paciente));
            return result != null ? Result<FormDiagResult>.SetOk(aux2) : Result<FormDiagResult>.SetError("No se encontraron registros");
        }

        public Result<List<GetFormPrevious>> GetFormPrevious()
        {
            var listForms = Context.formDiagInicials.ToList();
            return listForms.Any() ? Result<List<GetFormPrevious>>.SetOk(mapper.Map<List<GetFormPrevious>>(listForms)) :
                Result<List<GetFormPrevious>>.SetError("No se encontraron registros guardados");
        }
    }
}
