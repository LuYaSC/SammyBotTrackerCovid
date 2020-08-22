using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class Audit : Base
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Action { get; set; }

        public int AuditGroupId { get; set; }

        [ForeignKey("AuditGroupId")]
        public virtual AuditGroup AuditGroup { get; set; }
    }
}
