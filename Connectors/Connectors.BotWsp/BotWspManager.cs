using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.BotWsp.SendMessageWsp;
using TC.Core.Connectors.Models;

namespace TC.Connectors.BotWsp
{
    public class BotWspManager : IBotWspManager
    {
        private string url;
        private const string METHOD_SEND_TEXT = "EnviarMsgWpp";

        public BotWspManager(string url) => this.url = url;

        public BaseResponse<SendMessageWspResponse> SendMessageWsp(SendMessageWspRequest request)
        {
            var manager = new SendMessageWspConnector(request, $"{url}{METHOD_SEND_TEXT}");
            return manager.Response;
        }
    }
}
