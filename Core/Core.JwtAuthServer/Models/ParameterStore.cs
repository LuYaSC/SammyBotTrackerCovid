using TC.Core.JwtAuthServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC.Core.JwtAuthServer.Models
{
    public class ParameterStore
    {
        private AuthContext ctx;
        public ParameterStore()
        {
            this.ctx = new AuthContext();
        }

        public void Dispose() {
            this.ctx.Dispose();
        }

        public int GetNumberPasswordVerifyHistory() {
            int numberPasswordVerify = 5;
            return numberPasswordVerify;
        }
    }
}