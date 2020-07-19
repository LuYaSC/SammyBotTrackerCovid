using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Connectors;
using TC.Core.Connectors.Models;

namespace TC.Connectors.TwilioRooms.GenerateRoom
{
    public class GenerateRoomConnector : RestFulPostConnector<GenerateRoomRequest, GenerateRoomResponse>
    {
        public GenerateRoomConnector(GenerateRoomRequest request, string url) :
            base(request, url, new GenerateRoomValidator()) => Execute();
    }
}
