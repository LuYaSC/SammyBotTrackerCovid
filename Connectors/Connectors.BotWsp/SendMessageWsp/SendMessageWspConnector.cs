using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Connectors;

namespace TC.Connectors.BotWsp.SendMessageWsp
{
    public class SendMessageWspConnector : RestFulPostConnector<SendMessageWspRequest, SendMessageWspResponse>
    {
        public SendMessageWspConnector(SendMessageWspRequest request, string url) :
            base(request, url, new SendMessageWspValidator()) => Execute();
    }
}
