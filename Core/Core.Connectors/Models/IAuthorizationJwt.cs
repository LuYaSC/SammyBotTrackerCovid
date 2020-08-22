using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Connectors.Models
{
    public interface IAuthorizationJwt
    {
        string Jwt { get; set; }
    }
}
