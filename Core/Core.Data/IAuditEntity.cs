using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public interface IAuditEntity
    {
        int GrupoDeAuditoriaId { get; set; }

        AuditGroup Auditoria { get; set; }
    }
}
