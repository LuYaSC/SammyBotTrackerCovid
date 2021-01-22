using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.BotWsp.SendMessageWsp;
using TC.Core.Connectors.Models;

namespace TC.Connectors.BotWsp
{
    public interface IBotWspManager
    {
        BaseResponse<SendMessageWspResponse> SendMessageWsp(SendMessageWspRequest request);
    }
}
