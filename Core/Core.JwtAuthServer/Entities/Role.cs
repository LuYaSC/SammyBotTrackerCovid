namespace TC.Core.JwtAuthServer.Entities
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class Role : IdentityRole<int, UserRole>
    {
    }
}