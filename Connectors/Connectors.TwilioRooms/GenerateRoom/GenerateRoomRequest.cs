using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Connectors.TwilioRooms.GenerateRoom
{
    public class GenerateRoomRequest
    {
        public string codigoSala { get; set; }

        public string numeroContacto { get; set; }

        public int nivel { get; set; }

        public string fechaHoraProgramada { get; set; }

        public string fechaHoraCreacion { get; set; }
    }
}
