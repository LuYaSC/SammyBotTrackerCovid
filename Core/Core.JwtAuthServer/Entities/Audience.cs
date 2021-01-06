namespace TC.Core.JwtAuthServer.Entities
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Audience
    {
        [Key]
        [MaxLength(32)]
        public string ClientId { get; set; }

        [MaxLength(80)]
        [Required]
        public string Base64Secret { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public int RefreshTokenLifeTime { get; set; }
    }
}