using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using TC.Core.JwtAuthServer.Entities;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using TC.Core.JwtAuthServer.Models;
using Core.JwtAuthServer.Providers;
using System.Configuration;
using System.Linq;

namespace TC.Core.JwtAuthServer
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store)
            : base(store)
        {
        }

        public int UserId { get; set; }

        public string ClientId { get; set; }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var userStore = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(context.Get<AuthContext>());

            var manager = new ApplicationUserManager(userStore);
            //var manager = new ApplicationUserManager(new UserStore());

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                //RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = manager.MaxFailedAccessAttemptsBeforeLockout = Convert.ToInt32(ConfigurationManager.AppSettings["maxFailedAccess"]);
            manager.PasswordHasher = new PasswordHasherManager(ConfigurationManager.AppSettings["encryptionSalt"], PasswordHasherManager.OptionsHashNameAlgorithm.SHA512, int.Parse(ConfigurationManager.AppSettings["encryptionIterations"]));

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    TokenLifespan = TimeSpan.FromHours(3)
                };
            }

            return manager;
        }

        public override async Task<User> FindAsync(string userName, string password)
        {
            var user = await base.FindAsync(userName, password);
            return user;
        }

        public override Task<IdentityResult> AddToRolesAsync(int userId, params string[] roles)
        {
            return base.AddToRolesAsync(userId, roles);
        }

        public override async Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType)
        {
            var identityClaims = await base.CreateIdentityAsync(user, authenticationType);
            List<Claim> claimsDelete = new List<Claim>();
            foreach (var claim in identityClaims.Claims)
            {
                if (claim.Type == ClaimTypes.Role)
                {
                    claimsDelete.Add(claim);
                }
            }

            foreach (var claim in claimsDelete)
            {
                identityClaims.TryRemoveClaim(claim);
            }

            var roleStore = new RoleStore();
            var roles = roleStore.GetAll();

            //for (int i = 0; i < user.Roles.Count; i++)
            //{
            //    if ((user.Roles)[i].UserId == this.UserId)
            //    {
            //        identityClaims.AddClaim(new Claim(ClaimTypes.Role, roles[(user.Roles)[i].Role.Code]));
            //    }
            //}

            var tempRoles = (from x in user.Roles
                             where x.IsDeleted == false
                             select new UserRole
                             {
                                 RoleId = x.RoleId,
                                 UserId = x.UserId
                             }).GroupBy(x => x.RoleId).Select(grp => grp.First());

            ////for (int i = 0; i < tempRoles.ToList().Count; i++)
            ////{
            ////    if (((UserRole)((List<UserRole>)user.Roles)[i]).UserId == this.UserId)
            ////    {
            ////        identityClaims.AddClaim(new Claim(ClaimTypes.Role, roles[((List<UserRole>)user.Roles)[i].RoleId]));
            ////    }

            ////}

            foreach (var item in tempRoles)
            {
                if (item.UserId == this.UserId)
                {
                    identityClaims.AddClaim(new Claim(ClaimTypes.Role, roles[item.RoleId]));
                }
            }

            return identityClaims;
        }
    }
}
