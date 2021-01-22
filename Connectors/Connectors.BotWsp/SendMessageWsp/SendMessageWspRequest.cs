using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Connectors.BotWsp.SendMessageWsp
{
    public class SendMessageWspRequest
    {
        public string Token { get; set; }

        public string Uid { get; set; }

        public string To { get; set; }

        public string Custom_uid { get; set; }

        public string Text { get; set; }
    }
}
