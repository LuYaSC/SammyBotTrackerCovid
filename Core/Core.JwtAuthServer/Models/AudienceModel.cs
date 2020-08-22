using System.ComponentModel.DataAnnotations;

namespace TC.Core.JwtAuthServer.Models
{
    public class AudienceModel
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}