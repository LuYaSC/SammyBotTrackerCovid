namespace TC.Core.JwtAuthServer.Providers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    
    using TC.Core.JwtAuthServer;
    using TC.Core.JwtAuthServer.Entities;


    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<User, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return this.GenerateUserIdentityAsync(user, (ApplicationUserManager)UserManager);
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User user, ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
          
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("user", user.UserDetail.FirstLastName));
            userIdentity.AddClaim(new Claim("userId", user.Id.ToString()));
            return userIdentity;
        }
    }
}