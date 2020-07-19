using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.TwilioRooms.GenerateRoom;
using TC.Core.Connectors.Models;

namespace TC.Connectors.TwilioRooms
{
    public class TwilioRoomsManager : ITwilioRoomsManager
    {
        private string url;
       // private const string URL_COMPLEMENT_IS_OPERATION_AVAILABLE_METHOD = "IsOperationAvailable";

        public TwilioRoomsManager(string url) => this.url = url;

        public BaseResponse<GenerateRoomResponse> GenerateRoom(GenerateRoomRequest request)
        {
            var manager = new GenerateRoomConnector(request, url);
            return manager.Response;
        }
    }
}
