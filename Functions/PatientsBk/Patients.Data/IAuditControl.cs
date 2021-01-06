using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Data
{
    public interface IAuditControl
    {
        int TimeStampCreacion { get; set; }

        int TimeStampModificacion { get; set; }

        string UsuarioCreacion { get; set; }

        string UsuarioModificacion { get; set; }
    }
}
