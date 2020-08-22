namespace TC.Core.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserRole : IdentityUserRole<int>, /*IUserCreation<string>, IUserModification<string>,*/ IDateCreation, IDateModification, ILogicalDelete
    {
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        //public string UserCreation { get; set; }

        //public string UserModification { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime? DateModification { get; set; }

        public bool IsDeleted { get; set; }
    }
}
