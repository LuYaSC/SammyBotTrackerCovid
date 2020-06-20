using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.GetDataCovid.Business.Models
{
    public class UpdateDatesDto
    {
        public int TotalInfecteds { get; set; }

        public int TotalWishes { get; set; }

        public int TotalRecovered { get; set; }

        public int TotalDiscarded { get; set; }

        public int TotalObserved { get; set; }

        public int CountryId { get; set; }

        public int StateId { get; set; }
    }
}
