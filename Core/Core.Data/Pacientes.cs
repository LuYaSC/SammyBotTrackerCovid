using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;

namespace TC.Core.Data
{
    public class Pacientes : Base, IAuditControl
    {
        public string Matricula { get; set; }

        public int Documento { get; set; }

        public string TipoDocumento { get; set; }

        public string Extension { get; set; }

        public string Complemento { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Ocupacion { get; set; }

        public string OcupacionDescripcion { get; set; }

        public string AreaLaboral { get; set; }

        public string EmpresaTrabaja { get; set; }

        public string Direccion { get; set; }
       
        public string Email { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int Edad { get; set; }

        public string Genero { get; set; }

        public int Telefono { get; set; }

        public string Celular { get; set; }

        public int TimeStampCreacion { get; set; }

        public int TimeStampModificacion { get; set; }

        public string UsuarioCreacion { get; set; }

        public string UsuarioModificacion { get; set; }
    }
}
