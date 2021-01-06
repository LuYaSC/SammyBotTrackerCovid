using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace TC.Core.JwtAuthServer.Extensions
//namespace Microsoft.AspNet.Identity
{
    public static class CustomClaimTypes
    {
        //userIdentity.AddClaim(new Claim("company_id", this.CompanyId.ToString()));
        //    userIdentity.AddClaim(new Claim("company_state", this.Company.IsDeleted? BOOL_VALUE_FALSE : BOOL_VALUE_TRUE));
        //    userIdentity.AddClaim(new Claim("company_name", this.Company.Name));
        //    userIdentity.AddClaim(new Claim("controller_scheme", this.Company.ControllerScheme.IsDeleted? BOOL_VALUE_FALSE : BOOL_VALUE_TRUE));
        //    userIdentity.AddClaim(new Claim("user_type", this.UserDetail.Type));
        //    userIdentity.AddClaim(new Claim("user_name", string.Format("{0} {1} {2}", this.UserDetail.Name, this.UserDetail.FirstLastName, this.UserDetail.SecondLastName)));
        public const string CompanyId = "company_id";
        public const string CompanyName = "company_name";
        public const string CompanyState = "company_state";
        public const string ControllerScheme = "controller_scheme";
        public const string UserType = "user_type";
        public const string UserName = "user_name";
        public const string IsSignatureSismac = "is_signature_sismac";
    }

    public static class IdentityExtensions
    {
        public static int GetCompanyId(this IIdentity identity)
        {
            var claimValue = GetClaim(identity, CustomClaimTypes.CompanyId);
            if (claimValue == null)
                return -1;

            return claimValue == null ? 0 : int.Parse(claimValue);
        }

        public static string GetCompanyName(this IIdentity identity)
        {
            var claimValue = GetClaim(identity, CustomClaimTypes.CompanyName);
            return claimValue;
        }

        public static bool GetCompanyState(this IIdentity identity)
        {
            var claimValue = GetClaim(identity, CustomClaimTypes.CompanyState);
            
            return int.Parse(claimValue) == 1;
        }

        public static int GetControllerScheme(this IIdentity identity)
        {
            var claimValue = GetClaim(identity, CustomClaimTypes.ControllerScheme);
            if (claimValue == null)
                return -1;
            return int.Parse(claimValue);
        }

        public static string GetUserType(this IIdentity identity)
        {
            var claimValue = GetClaim(identity, CustomClaimTypes.UserType);
            return claimValue;
        }

        public static string GetFullUserName(this IIdentity identity)
        {
            var claimValue = GetClaim(identity, CustomClaimTypes.UserName);
            return claimValue;
        }

        public static bool GetIsSignatureSismac(this IIdentity identity)
        {
            var claimValue = GetClaim(identity, CustomClaimTypes.IsSignatureSismac);
            return bool.Parse(claimValue);
        }

        private static string GetClaim(this IIdentity identity, string customClaim)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(customClaim);
            return claim.Value;
        }
    }
}