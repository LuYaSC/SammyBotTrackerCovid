using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.GetDataCovid.Business.Models
{
    public class RegisterQueriesDto
    {
        public string DeviceId { get; set; }

        public string DeviceType { get; set; }

        public string DeviceModel { get; set; }

        public string AndroidId { get; set; }

        public string Ip { get; set; }

        public string Qr { get; set; }

        public string DocumentNumber { get; set; }

        public bool IsTest { get; set; }

        public double Latitude { get; set; }

        public double Length { get; set; }
    }
}
