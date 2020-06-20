using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Functions.EvolutionForm.Business.Models;

namespace TC.Functions.EvolutionForm.Business
{
    public interface IEvolutionFormBusiness
    {
        Result<List<GetHistoryClinicResult>> GetHistoryClinics();

        Result<string> SaveEvolutionForm(SaveFormDto dto);

        Result<GetBasicDatesFormResult> GetBasicData(GetBasicDatesFormDto dto);
    }
}
