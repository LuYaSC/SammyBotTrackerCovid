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
                    .ForMember(d => d.HistoriaClinicas, o => o.MapFrom(s => s));
                cfg.CreateMap<FormDiagDto, Pacientes>()
                    .ForMember(d => d.Matricula, o => o.MapFrom(s => s.Matricula.ToUpper()));
                cfg.CreateMap<FormDiagDto, HistoriaClinicas>()
                    .ForMember(d => d.NumeroHistoria, o => o.MapFrom(s => s.NumeroHistoria.ToUpper()));
                cfg.CreateMap<FormDiagInicials, GetFormPrevious>()
                    .ForMember(d => d.Description, o => o.MapFrom(s => $"Paciente: {s.HistoriaClinicas.Paciente.Nombre} {s.HistoriaClinicas.Paciente.ApellidoPaterno} " +
                    $"HistoriaClinica. {s.HistoriaClinicas.NumeroHistoria} Matrícula.{s.HistoriaClinicas.Paciente.Matricula} Médico Asignado. {s.MedicoAsignado}"));
                cfg.CreateMap<FormDiagInicials, FormDiagResult>()
                    .ForMember(d => d.NumeroHistoria, o => o.MapFrom(s => s.HistoriaClinicas.NumeroHistoria));
                cfg.CreateMap<Pacientes, FormDiagResult>();
                cfg.CreateMap<FormDiagInicials, FormSaveResult>()
                    .ForMember(d => d.NumeroDiagnostico, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.Matricula, o => o.MapFrom(s => s.HistoriaClinicas.Paciente.Matricula))
                    .ForMember(d => d.Paciente, o => o.MapFrom(s => $"{s.HistoriaClinicas.Paciente.Nombre} {s.HistoriaClinicas.Paciente.ApellidoPaterno} {s.HistoriaClinicas.Paciente.ApellidoMaterno}"))
                    .ForMember(d => d.HistoriaClinica, o => o.MapFrom(s => s.HistoriaClinicas.NumeroHistoria));
            });
            mapper = new Mapper(config);
        }

        public Result<FormSaveResult> SaveForm(FormDiagDto dto)
        {
            var form = mapper.Map<FormDiagInicials>(dto);
            form.HistoriaClinicas.Paciente = mapper.Map<Pacientes>(dto);
            var result = Context.Save(form);
            return result.Id == 0 ? Result<FormSaveResult>.SetError("el formulario no se pudo guardar correctamente")
                : Result<FormSaveResult>.SetOk(mapper.Map<FormSaveResult>(result));
        }

        public Result<FormDiagResult> GetFormById(GetFormById dto)
        {
            var result = Context.formDiagInicials.Where(x => x.Id == dto.FormId).FirstOrDefault();
            if (result == null)
            {
                Result<FormDiagResult>.SetError("No se encontraron registros");
            }
            var aux = mapper.Map<FormDiagResult>(result);
            var aux2 = mapper.Map(result, mapper.Map<FormDiagResult>(result.HistoriaClinicas.Paciente));
            return Result<FormDiagResult>.SetOk(aux2);
        }

        public Result<List<GetFormPrevious>> GetFormPrevious(GetFormPreviousDto dto)
        {
            List<FormDiagInicials> listResult = new List<FormDiagInicials>();
            dto.Parametros = dto.Parametros != null ? dto.Parametros.ToUpper() : string.Empty; 
            if (dto.EsMatricula)
            {
                listResult = Context.formDiagInicials.Where(x => x.HistoriaClinicas.Paciente.Matricula.Contains(dto.Parametros)).ToList();
            }
            else
            {
                listResult = Context.formDiagInicials.Where(x => x.HistoriaClinicas.NumeroHistoria.Contains(dto.Parametros)).ToList();
            }
            return listResult.Any() ? Result<List<GetFormPrevious>>.SetOk(mapper.Map<List<GetFormPrevious>>(listResult)) :
                Result<List<GetFormPrevious>>.SetError("No se encontraron registros guardados");
        }
    }
}
