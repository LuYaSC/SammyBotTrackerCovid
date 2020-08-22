using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class Session : BaseBackOfficeOnlyTrace<int>
    {
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [MaxLength(1), Column(TypeName = "nchar")]
        public string Action { get; set; }

        [Required, MaxLength(16)]
        public string CardNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(15)]
        public string Ip { get; set; }
    }
}
