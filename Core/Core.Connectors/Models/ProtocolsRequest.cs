using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Connectors.Models
{
    public class ProtocolsRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Type { get; set; }

        public bool IsSpecial { get; set; }
    }
}
