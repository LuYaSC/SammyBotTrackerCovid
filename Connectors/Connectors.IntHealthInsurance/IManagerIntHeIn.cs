using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.IntHealthInsurance.GetHealthIn;
using TC.Core.Connectors.Models;

namespace TC.Connectors.IntHealthInsurance
{
    public interface IManagerIntHeIn
    {
        BaseResponse<ResultContract<GetHealthInResponse>> GetInsurancePerson(GetHealthInRequest request);
    }
}
