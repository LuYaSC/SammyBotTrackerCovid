using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.HealthInsurance.GetInsurancePerson;
using TC.Core.Connectors.Models;

namespace TC.Connectors.HealthInsurance
{
    public class HealthInsuranceManager : IHealthInsuranceManager
    {
        private string url;

        public HealthInsuranceManager(string url) => this.url = url;

        public BaseResponse<GetInsurancePersonResponse> GetInsurancePerson(GetInsurancePersonRequest request)
        {
            var manager = new GetInsurancePersonConnector(request, url);
            return manager.Response;
        }
    }
}
