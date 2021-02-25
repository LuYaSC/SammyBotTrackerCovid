using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.IntHealthInsurance.GetHealthIn;
using TC.Core.Connectors.Models;

namespace TC.Connectors.IntHealthInsurance
{
    public class ManagerIntHeIn : IManagerIntHeIn
    {
        private string url;
        private const string METHOD_IN = "GetInsurancePatient";


        public ManagerIntHeIn(string url) => this.url = url;

        public BaseResponse<ResultContract<GetHealthInResponse>> GetInsurancePerson(GetHealthInRequest request)
        {
            var manager = new GetHealthInConnector(request, url + METHOD_IN);
            return manager.Response;
        }

    }
}
