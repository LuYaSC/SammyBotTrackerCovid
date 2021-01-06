using SBTC.Functions.Patients.Business.BaseBusiness;
using SBTC.Functions.Patients.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Business
{
    public interface IPatientBusiness
    {
        Result<List<GetPatientResult>> GetPatients(GetPatientsDto dto);

        Result<List<TechnicalSheetResult>> GetTechnicalSheet(TechnicalSheetDto dto);

        Result<string> ConfirmedTest(TechnicalSheetDto dto);
    }
}
