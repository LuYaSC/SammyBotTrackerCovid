using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public partial class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, IBase<int>
    {
        [MaxLength(16)]
        [Column(TypeName = "nchar")]
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        public virtual UserDetail UserDetail { get; set; }

        public int AvailableDays { get; set; } = 0;

        public bool IsActive { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        public DateTime? DateModification { get; set; }

        public DateTime? DateLastPasswordChange { get; set; }

        [MaxLength(1), Column(TypeName = "nchar")]
        public string State { get; set; }

        public virtual List<UserRole> UserRoles { get; set; } 

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            var sessionDate = DateTime.UtcNow;
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            //userIdentity.AddClaim(new Claim("company_id", this.CompanyId.ToString()));
            //userIdentity.AddClaim(new Claim("company_state", this.Company.IsDeleted ? BOOL_VALUE_FALSE : BOOL_VALUE_TRUE, ClaimValueTypes.Boolean));
            //userIdentity.AddClaim(new Claim("company_name", this.Company.Name));
            //userIdentity.AddClaim(new Claim("controller_scheme", this.Company.ControllerScheme != null ? (this.Company.ControllerScheme.IsDeleted ? BOOL_VALUE_FALSE : BOOL_VALUE_TRUE) : BOOL_VALUE_FALSE, ClaimValueTypes.Boolean));
            //userIdentity.AddClaim(new Claim("user_type", this.UserDetail.Type));
            userIdentity.AddClaim(new Claim("user_name", string.Format("{0} {1} {2}", this.UserDetail.Name, this.UserDetail.FirstLastName, this.UserDetail.SecondLastName)));
            //userIdentity.AddClaim(new Claim("user_document_number", this.UserDetail.Idc.ToString()));
            //userIdentity.AddClaim(new Claim("user_document_extension", this.UserDetail.IdcExtension != null ? this.UserDetail.IdcExtension.ToString() : string.Empty));
            //userIdentity.AddClaim(new Claim("user_document_type", this.UserDetail.IdcType != null ? this.UserDetail.IdcType.ToString() : string.Empty));
            //userIdentity.AddClaim(new Claim("exchange_buy", this.ExchangeRate.Purchase.ToString()));
            //userIdentity.AddClaim(new Claim("exchange_sale", this.ExchangeRate.Sale.ToString()));
            //userIdentity.AddClaim(new Claim("is_signature", this.Company.IsSignature.ToString(), ClaimValueTypes.Boolean));
            //userIdentity.AddClaim(new Claim("authorize_operation", this.Company.AuthorizeOperation.ToString(), ClaimValueTypes.Boolean)); // comentar
            //userIdentity.AddClaim(new Claim("authorize_pin", this.Company.AuthorizePin.ToString(), ClaimValueTypes.Boolean));
            //userIdentity.AddClaim(new Claim("authorize_ftp", this.Company.AuthorizeFtp.ToString(), ClaimValueTypes.Boolean));
            //userIdentity.AddClaim(new Claim("is_validbatchtoken", this.Company.IsValidBatchToken.ToString(), ClaimValueTypes.Boolean));
            return userIdentity;
        }

        //private string BOOL_VALUE_TRUE = "True";

        //private string BOOL_VALUE_FALSE = "False";
    }

    public class UserLogin : IdentityUserLogin<int>
    { }

    public class UserClaim : IdentityUserClaim<int>
    { }
}
