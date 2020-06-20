using AutoMapper;
using Microsoft.Extensions.Configuration;
using SBTC.Core.Business;
using SBTC.Core.Data;
using SBTC.Functions.GetDataCovid.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.GetDataCovid.Business
{
    public class GetDataCovidBusiness : BaseBusiness<CasesByCountry, GetDataCovidContext>, IGetDataCovidBusiness
    {
        IMapper mapper;

        public GetDataCovidBusiness(GetDataCovidContext context, IConfiguration configuration = null) : base(context, null, configuration)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CasesByCountry, TotalCases>();
                cfg.CreateMap<CasesByState, TotalCases>();
                cfg.CreateMap<UpdateDatesDto, CasesByCountry>()
                .ForMember(d => d.DateCreation, o => o.MapFrom(s => DateTime.Now));
                cfg.CreateMap<UpdateDatesDto, CasesByState>()
                .ForMember(d => d.DateCreation, o => o.MapFrom(s => DateTime.Now));
                cfg.CreateMap<RegisterQueriesDto, AppQuery>()
                .ForMember(d => d.DateCreation, o => o.MapFrom(s => DateTime.Now));
            });
            mapper = new Mapper(config);
        }

        public Result<GetDateCasesResult> GetCasesCovid(GetDateCasesDto dto)
        {
            var resultState = Context.CasesByStates.Where(x => x.StateId == dto.State).OrderByDescending(x => x.DateCreation).FirstOrDefault();
            var resultCountry = Context.CasesByCountries.Where(x => x.CountryId == dto.Country).OrderByDescending(x => x.DateCreation).FirstOrDefault();
            return (resultState == null && resultCountry == null) ? Result<GetDateCasesResult>.SetError("Los datos no se encuentran disponibles, intente nuevamente porfavor") :
                Result<GetDateCasesResult>.SetOk(new GetDateCasesResult { TotalCasesCountry = mapper.Map<TotalCases>(resultCountry), TotalCasesState = mapper.Map<TotalCases>(resultState) });
        }

        public Result<bool> UpdateManualDates(UpdateDatesDto dto)
        {
            if (dto.StateId == 0)
            {
                updatePreviousData(dto.StateId);
                Context.Save(mapper.Map<CasesByCountry>(dto));
            }
            else
            {
                updatePreviousData(dto.StateId);
                Context.Save(mapper.Map<CasesByState>(dto));
            }
            return Result<bool>.SetOk(true);
        }

        private void updatePreviousData(int stateId)
        {
            if (stateId == 0)
            {
                var previousData = Context.CasesByCountries.OrderByDescending(x => x.DateCreation).FirstOrDefault();
                if (previousData != null)
                {
                    previousData.DateModification = DateTime.Now;
                    Context.Save(previousData);
                }
            }
            else
            {
                var previousData = Context.CasesByStates.Where(x => x.StateId == stateId).OrderByDescending(x => x.DateCreation).FirstOrDefault();
                if (previousData != null)
                {
                    previousData.DateModification = DateTime.Now;
                    Context.Save(previousData);
                }
            }
        }

        public Result<bool> RegisterQueries(RegisterQueriesDto dto)
        {
            Context.Save(mapper.Map<AppQuery>(dto));
            return Result<bool>.SetOk(true);
        }
    }
}
