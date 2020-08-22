using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class Parameter : BaseBackOfficeTrace
    {
        [Required, MaxLength(6)]
        public string Groups { get; set; }
        [Required, MaxLength(6)]
        public string Code { get; set; }
        [Required, MaxLength(8)]
        public string Value { get; set; }
        [Required, MaxLength(80)]
        public string Description { get; set; }
    }
}
