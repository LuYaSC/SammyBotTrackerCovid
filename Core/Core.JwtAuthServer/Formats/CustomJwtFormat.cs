
namespace TC.Core.JwtAuthServer.Formats
{
    using System;
    using System.IdentityModel.Tokens;

    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataHandler.Encoder;

    using TC.Core.JwtAuthServer.Models;
    using TC.Core.JwtAuthServer.Entities;

    using Thinktecture.IdentityModel.Tokens;

    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private const string AudiencePropertyKey = "audience";

        private readonly string pissuer = string.Empty;

        public CustomJwtFormat(string issuer)
        {
            this.pissuer = issuer;
        }

        /// <summary>
        /// JWT generation
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            // Reading the audience (client id) from the Authentication Ticket properties
            string audienceId = data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) ? data.Properties.Dictionary[AudiencePropertyKey] : null;

            if (string.IsNullOrWhiteSpace(audienceId))
            {
                throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");
            }
            
            // Getting this audience from the In-memory store.
            AudienceStore store = new AudienceStore();
            Audience audience = store.FindAudience(audienceId);
        
            // Reading the Symmetric key for this audience and Base64 decode it to byte array 
            string symmetricKeyAsBase64 = audience.Base64Secret;
            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            // Create a HMAC265 signing key.
            var signingKey = new HmacSigningCredentials(keyByteArray);

            // Preparing the raw data for the JSON Web Token
            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            // Issue token
            var token = new JwtSecurityToken(this.pissuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            // Serialize the JSON Web Token to a string 
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(token);
            store.Dispose();
            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}