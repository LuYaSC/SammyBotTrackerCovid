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
        private const string URL_CREATE_ROOM = "Salas";
        private const string URL_CREATE_ROOM_URL = "AsignarCita";

        public TwilioRoomsManager(string url) => this.url = url;

        public BaseResponse<GenerateRoomResponse> GenerateRoom(GenerateRoomRequest request)
        {
            var manager = new GenerateRoomConnector(request, $"{url}{URL_CREATE_ROOM}");
            return manager.Response;
        }

        public BaseResponse<GenerateRoomUrlResponse> GenerateRoomUrl(GenerateRoomUrlRequest request)
        {
            var manager = new GenerateRoomUrlConnector(request, $"{url}{URL_CREATE_ROOM_URL}");
            return manager.Response;
        }
    }
}
