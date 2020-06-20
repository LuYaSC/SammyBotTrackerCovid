using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class AuditGroup : BaseTrace
    {
        public AuditGroup()
        {
            this.Audits = new HashSet<Audit>();
        }

        [Required]
        public string Entity { get; set; }

        public virtual ICollection<Audit> Audits { get; set; }
    }
}
