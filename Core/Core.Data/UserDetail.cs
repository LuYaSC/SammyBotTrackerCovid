namespace TC.Core.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserDetail : BaseBackOfficeTrace
    {
        [Key, ForeignKey("User"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new int Id { get; set; }

        public string Name { get; set; }

        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }

       /* public string Idc { get; set; }

        public string IdcType { get; set; }

        public string IdcExtension { get; set; }*/

        public virtual User User { get; set; }

        //public string Type { get; set; }

        //public string IdcComplement { get; set; }
    }
}
