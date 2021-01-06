using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Data
{
    public class NivelesAlertas
    {
        public int Id { get; set; }

        public string NivelAlerta { get; set; }

        public decimal PorcentajeMinimo { get; set; }

        public decimal PorcentajeMaximo { get; set; }
    }
}
