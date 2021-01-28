using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class SendNotification : Base, IDateCreation
    {
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int UserId { get; set; }

        public string SendPhone { get; set; }

        public string UID { get; set; }

        public DateTime DateCreation { get; set; }

        public bool Status { get; set; }

        public string MessageStatus { get; set; }
    }
}
