namespace TC.Core.JwtAuthServer.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExchangeRate : Base, IDateCreation
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Purchase { get; set; }

        [Required]
        public decimal Sale { get; set; }

        public DateTime DateCreation { get; set; }
    }
}