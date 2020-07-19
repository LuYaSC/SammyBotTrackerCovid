using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.TwilioRooms.GenerateRoom;
using TC.Core.Connectors.Models;

namespace TC.Connectors.TwilioRooms
{
    public interface ITwilioRoomsManager
    {
        BaseResponse<GenerateRoomResponse> GenerateRoom(GenerateRoomRequest request);
    }
}
