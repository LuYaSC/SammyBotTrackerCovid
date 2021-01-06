using TC.Core.JwtAuthServer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace TC.Core.JwtAuthServer.Models
{
    public class RoleStore
    { 
        private AuthContext ctx;

        public RoleStore()
        {
            this.ctx = new AuthContext();
        }

        public Dictionary<int, string> GetAll()
        {
            return this.ctx.Roles.ToDictionary(r => r.Id, r => r.Name);
        }
    }
}