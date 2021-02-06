using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Functions.GetInformationPatient.Business.Models;

namespace TC.Functions.GetInformationPatient.Business
{
    public interface IGetInformationPatientBusiness
    {
        Result<GetInsurancePatientResult> GetInsurancePatient(GetInsurancePatientDto dto);

        Result<GetRiskCovidResult> GetRiskCovid(GetRiskCovidDto dto);
    }
}
