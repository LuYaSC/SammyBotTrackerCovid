using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Connectors;
using TC.Core.Connectors.Models;

namespace TC.Connectors.IntHealthInsurance.GetHealthIn
{
    public class GetHealthInConnector : RestFulPostConnector<GetHealthInRequest, ResultContract<GetHealthInResponse>>
    {
        public GetHealthInConnector(GetHealthInRequest request, string url) :
            base(request, url, new GetHealthInValidator()) => Execute();
    }

}
