namespace TC.Core.Data.Migration
{
    using System.Data.Entity;

    public class UserDetailMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<UserDetail>().Property(prop => prop.IdcComplement).HasColumnType("nvarchar").HasMaxLength(10);
            modelBuilder.Entity<UserDetail>().Property(prop => prop.Type).HasColumnType("nchar").HasMaxLength(1);
            modelBuilder.Entity<UserDetail>().Property(prop => prop.IdcExtension).HasColumnType("nvarchar").HasMaxLength(3);
            modelBuilder.Entity<UserDetail>().Property(prop => prop.IdcType).HasColumnType("nchar").HasMaxLength(1);
            modelBuilder.Entity<UserDetail>().Property(prop => prop.Idc).HasColumnType("nvarchar").HasMaxLength(12).IsRequired();*/
            modelBuilder.Entity<UserDetail>().Property(prop => prop.Name).HasColumnType("nvarchar").HasMaxLength(25).IsRequired();
            modelBuilder.Entity<UserDetail>().Property(prop => prop.FirstLastName).HasColumnType("nvarchar").HasMaxLength(25).IsRequired();
            modelBuilder.Entity<UserDetail>().Property(prop => prop.SecondLastName).HasColumnType("nvarchar").HasMaxLength(25).IsRequired();
        }
    }
}
