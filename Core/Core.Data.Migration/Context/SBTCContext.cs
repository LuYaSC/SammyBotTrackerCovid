namespace TC.Core.Data.Migrations.Context
{
    using TC.Core.Data.Attributes;
    using TC.Core.Data.Migration;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class SBTCContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public SBTCContext() : base("SBTCContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("doctor");
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<UserRole>().HasKey(m => new { m.RoleId, m.UserId }).ToTable("UserRoles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().Property(m => m.UserName).HasMaxLength(16).IsFixedLength();
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<User>().Property(m => m.PhoneNumber).HasMaxLength(12).IsFixedLength();
            modelBuilder.Entity<User>().Property(m => m.Email).HasMaxLength(50).IsFixedLength();
            Precision.ConfigureModelBuilder(modelBuilder);
            UserDetailMap.Map(modelBuilder);
        }

        public DbSet<Audience> Audiences { get; set; }
        public DbSet<Audit> Audit { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AuditGroup> AuditGroups { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserCardHistory> UserCardsHistory { get; set; }
    }
}
