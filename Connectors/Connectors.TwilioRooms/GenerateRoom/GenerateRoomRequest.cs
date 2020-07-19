using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Connectors.TwilioRooms.GenerateRoom
{
    public class GenerateRoomRequest
    {
        public string CodigoSala { get; set; }

        public string NumeroContacto { get; set; }

        public int Nivel { get; set; }

        public DateTime FechaHoraProgramada { get; set; }

        public DateTime FechaHoraCreacion { get; set; }
    }
}
