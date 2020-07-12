using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.DataMigrations
{
    public class PacienteMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pacientes>().Property(prop => prop.Matricula).HasColumnType("text");
            modelBuilder.Entity<Pacientes>().Property(prop => prop.Email).HasColumnType("text");
            modelBuilder.Entity<Pacientes>().Property(prop => prop.Ocupacion).HasColumnType("text");
            modelBuilder.Entity<Pacientes>().Property(prop => prop.OcupacionDescripcion).HasColumnType("text");
            modelBuilder.Entity<Pacientes>().Property(prop => prop.AreaLaboral).HasColumnType("text");
            modelBuilder.Entity<Pacientes>().Property(prop => prop.Nombre).HasColumnType("text");
            modelBuilder.Entity<Pacientes>().Property(prop => prop.ApellidoPaterno).HasColumnType("text");
            modelBuilder.Entity<Pacientes>().Property(prop => prop.ApellidoMaterno).HasColumnType("text");
            modelBuilder.Entity<Pacientes>().Property(prop => prop.Direccion).HasColumnType("text");
            modelBuilder.Entity<Pacientes>().Property(prop => prop.TipoDocumento).HasColumnType("text");
            modelBuilder.Entity<Pacientes>().Property(prop => prop.Extension).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<Pacientes>().Property(prop => prop.Complemento).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<Pacientes>().Property(prop => prop.Genero).HasColumnType("char").HasMaxLength(1);
            modelBuilder.Entity<Pacientes>().Property(prop => prop.Celular).HasColumnType("text");
            modelBuilder.Entity<Pacientes>().Property(prop => prop.UsuarioCreacion).HasColumnType("char").HasMaxLength(15);
            modelBuilder.Entity<Pacientes>().Property(prop => prop.UsuarioModificacion).HasColumnType("char").HasMaxLength(15);
            modelBuilder.Entity<Pacientes>().Property(prop => prop.EmpresaTrabaja).HasColumnType("text");
        }
    }
}
