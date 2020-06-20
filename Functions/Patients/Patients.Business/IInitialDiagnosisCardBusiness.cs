using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Functions.Patients.Business.Models;

namespace TC.Functions.Patients.Business
{
    public interface IInitialDiagnosisCardBusiness
    {
        Result<FormSaveResult> SaveForm(FormDiagDto dto);

        Result<FormDiagResult> GetFormById(GetFormById dto);

        Result<List<GetFormPrevious>> GetFormPrevious(GetFormPreviousDto dto);
    }
}
