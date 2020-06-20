namespace TC.Core.JwtAuthServer.Entities
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public class Company : BaseBackOfficeTrace
    {
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }

        [Required]
        [MaxLength(12)]
        public string Idc { get; set; }

        [Required]
        [MaxLength(1)]
        [Column(TypeName = "nchar")]
        public string TypeIdc { get; set; }

        [MaxLength(3)]
        [Column(TypeName = "nchar")]
        public string ExtensionIdc { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "nchar")]
        public string ComplementIdc { get; set; }

        public decimal DailyLimit { get; set; }

        public bool DebitOrder { get; set; }

        public bool AuthorizePin { get; set; }

        public bool AuthorizeFtp { get; set; }

        public bool AuthorizeOperation { get; set; }

        public bool IsSignature { get; set; }

        public int EconomicGroupId { get; set; }

        public bool IsValidBatchToken { get; set; }

        public virtual ControllerScheme ControllerScheme { get; set; }
    }
}