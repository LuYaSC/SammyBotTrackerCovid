namespace TC.Core.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserCardHistory : BaseBackOfficeOnlyTrace<int>
    {
        public string CardNumber { get; set; }

        public bool IsActive { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
