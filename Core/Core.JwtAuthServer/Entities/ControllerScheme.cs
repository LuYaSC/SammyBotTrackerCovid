namespace TC.Core.JwtAuthServer.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ControllerScheme : BaseBackOfficeTrace
    {
        [Key, ForeignKey("Company"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new int Id { get; set; }

        public virtual Company Company { get; set; }
    }
}