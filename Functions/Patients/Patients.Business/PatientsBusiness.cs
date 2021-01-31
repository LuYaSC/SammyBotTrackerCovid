using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Core.Data;
using TC.Functions.Patients.Business.Models;

namespace TC.Functions.Patients.Business
{
    public class PatientsBusiness : BaseBusiness<P_Controles, PatientsContext>, IPatientsBusiness
    {
        IMapper mapper;
        string categoryName;

        public PatientsBusiness(PatientsContext context) : base(context, null)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<P_Controles, GetPatientResult>()
                    .ForMember(d => d.ControlDate, o => o.MapFrom(s => s.FechaControl));
                cfg.CreateMap<P_TablaControl, TechnicalSheetResult>()
                  .ForMember(d => d.Factor, o => o.MapFrom(s => getFactor(s.IdFactor)))
                  .ForMember(d => d.CategoryName, o => o.MapFrom(s => categoryName))
                  .ForMember(d => d.PresentFactor, o => o.MapFrom(s => s.FactorPresente ? "SI" : "NO"))
                  .ForMember(d => d.AlterResponse, o => o.MapFrom(s => s.RespuestaAlternativa))
                  .ForMember(d => d.RegisterDate, o => o.MapFrom(s => s.FechaRegistro))
                  .ForMember(d => d.Confirmed, o => o.MapFrom(s => s.Confirmado ? "SI" : "NO"));
            });
            mapper = new Mapper(config);
        }

        private string getFactor(int IdFactor)
        {
            var result = Context.Factores.Where(x => x.Id == IdFactor).FirstOrDefault();
            if (result == null)
            {
                categoryName = "Sin Categoria";
                return "Sin Factor";
            }
            else
            {
                var pat = Context.FactorPatologico.Where(x => x.Id == result.IdCategoriaFactor).FirstOrDefault();
                if (pat == null)
                {
                    categoryName = "Sin Categoria";
                }
                else
                {
                    categoryName = pat.NombreCategoria;
                }

                return result.Factor;
            }
        }

        public Result<List<GetPatientResult>> GetPatients(GetPatientsDto dto)
        {
            P_Pacientes patients = new P_Pacientes();
            if (string.IsNullOrEmpty(dto.CI))
            {
                patients = Context.Pacientes.Where(x => x.NumeroContacto.Contains(dto.Phone)).FirstOrDefault();
            }
            else
            {
                patients = Context.Pacientes.Where(x => x.CI.Contains(dto.CI)).FirstOrDefault();
            }

            return patients != null ? Result<List<GetPatientResult>>.SetOk(mapper.Map<List<GetPatientResult>>(patients.Controles.Where(x => x.ControlFinalizado).ToList())) :
                Result<List<GetPatientResult>>.SetError("El dato buscado no tiene resultados");
        }

        public Result<List<TechnicalSheetResult>> GetTechnicalSheet(TechnicalSheetDto dto)
        {
            var control = Context.Controles.Where(x => x.Id == dto.Id).FirstOrDefault();
            return control != null ? Result<List<TechnicalSheetResult>>.SetOk(mapper.Map<List<TechnicalSheetResult>>(control.TablaControl.OrderByDescending(x => x.FechaRegistro))) :
               Result<List<TechnicalSheetResult>>.SetError("No existen datos que mostrar");
        }


        public Result<string> ConfirmedTest(TechnicalSheetDto dto)
        {
            var control = Context.Controles.Where(x => x.Id == dto.Id).FirstOrDefault();
            if (control != null)
            {
                control.Atendido = true;
                control.FechaAtendido = DateTime.Now;
                control.Observaciones = dto.Observations;
                control.ControlCancelado = dto.IsCancel;
                Context.SaveChanges();
            }
            return Result<string>.SetOk("El test fue actualizado con exito");
        }
    }
}
