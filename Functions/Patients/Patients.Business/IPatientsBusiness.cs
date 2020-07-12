using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Functions.Patients.Business.Models;

namespace TC.Functions.Patients.Business
{
    public interface IPatientsBusiness
    {
        Result<List<GetPatientResult>> GetPatients(GetPatientsDto dto);

        Result<List<TechnicalSheetResult>> GetTechnicalSheet(TechnicalSheetDto dto);

        Result<string> ConfirmedTest(TechnicalSheetDto dto);
    }
}
