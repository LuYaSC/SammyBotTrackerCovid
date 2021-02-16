using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.HealthInsurance.GetInsurancePerson;
using TC.Core.Connectors.Models;

namespace TC.Connectors.HealthInsurance
{
    public interface IHealthInsuranceManager
    {
        BaseResponse<GetInsurancePersonResponse> GetInsurancePerson(GetInsurancePersonRequest request);
    }
}
