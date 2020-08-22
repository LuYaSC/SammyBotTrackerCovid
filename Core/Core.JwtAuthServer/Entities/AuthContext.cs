using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TC.Core.JwtAuthServer.Entities
{
    public class AuthContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public AuthContext()
            : base("AuthContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("doctor");
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<UserRole>().HasKey(m => new { m.RoleId, m.UserId}).ToTable("UserRoles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
        }

        public DbSet<UserDetail> UserDetails { get; set; }

        public DbSet<Audience> Audiences { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<ControllerScheme> ControllerSchemes { get; set; }

        public static AuthContext Create()
        {
            return new AuthContext();
        }
    }
}