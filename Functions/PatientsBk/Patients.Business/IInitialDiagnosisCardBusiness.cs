using SBTC.Functions.Patients.Business.BaseBusiness;
using SBTC.Functions.Patients.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Business
{
    public interface IInitialDiagnosisCardBusiness
    {
        Result<FormSaveResult> SaveForm(FormDiagDto dto);

        Result<FormDiagResult> GetFormById(GetFormById dto);

        Result<List<GetFormPrevious>> GetFormPrevious(GetFormPreviousDto dto);
    }
}
