using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Owin.Security.DataHandler.Encoder;
using TC.Core.JwtAuthServer.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;

namespace TC.Core.JwtAuthServer.Models
{
    public class AudienceStore
    {
        private AuthContext ctx;

        public AudienceStore()
        {
            this.ctx = new AuthContext();
        }

        public void Dispose()
        {
            this.ctx.Dispose();
        }

        public Audience AddAudience(string name)
        {
            // Generating random string of 32 characters as an identifier for the audience (client id).
            var clientId = Guid.NewGuid().ToString("N");

            // Generating 256 bit random key using the “RNGCryptoServiceProvider” class then base 64 URL encode it, this key will be shared between the Authorization server and the Resource server only.
            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            // Add the newly generated audience to the in-memory “AudiencesList”.
            Audience newAudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = name, RefreshTokenLifeTime = 14400 };
            this.ctx.Audiences.Add(newAudience);
            this.ctx.SaveChanges();
            this.Dispose();
            return newAudience;
        }

        /// <summary>
        /// The “FindAudience” method is responsible for finding an audience based on the client id.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Audience FindAudience(string clientId)
        {
            Audience audience = this.ctx.Audiences.Find(clientId);
            this.Dispose();
            return audience;
        }

        /// <summary>
        /// The “FindAudience” method is responsible for finding an audience based on the client id.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<Audience> ListAudiences()
        {
            var audiences = this.ctx.Audiences.ToList();
            this.Dispose();
            return audiences;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            this.ctx.RefreshTokens.RemoveRange(this.ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId));
            try
            {
                this.ctx.RefreshTokens.Add(token);
                return await this.ctx.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                return await Task<bool>.FromResult(false);
            }
            catch (OptimisticConcurrencyException)
            {
                return await Task<bool>.FromResult(false);
            }
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await this.ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                this.ctx.RefreshTokens.Remove(refreshToken);
                return await this.ctx.SaveChangesAsync() > 0;
            }
            this.Dispose();
            return false;
        }

        public async Task<bool> RemoveAllRefreshTokenForUser(string user)
        {
            var refreshTokens = await this.ctx.RefreshTokens.Where(r => r.Subject == user).ToListAsync();

            if (refreshTokens != null && refreshTokens.Count() > 0)
            {
                this.ctx.RefreshTokens.RemoveRange(refreshTokens);
                return await this.ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            this.ctx.RefreshTokens.Remove(refreshToken);
            return await this.ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await this.ctx.RefreshTokens.FindAsync(refreshTokenId);
            this.Dispose();
            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return this.ctx.RefreshTokens.ToList();
        }
    }
}