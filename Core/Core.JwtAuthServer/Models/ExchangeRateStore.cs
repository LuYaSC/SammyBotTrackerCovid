using TC.Core.JwtAuthServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC.Core.JwtAuthServer.Models
{
    public class ExchangeRateStore
    {
        private AuthContext ctx;

        public ExchangeRateStore()
        {
            this.ctx = new AuthContext();
        }

        public void Dispose()
        {
            this.ctx.Dispose();
        }

        public ExchangeRate FindCurrentExchangeRate()
        {
            return this.ctx.ExchangeRates.Where(f => f.Date == this.ctx.ExchangeRates.Max(f2 => f2.Date)).FirstOrDefault();
        }
    }
}