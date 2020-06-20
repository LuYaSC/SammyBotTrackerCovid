namespace Core.JwtAuthServer.Providers
{
    using System;
    using System.Web;
    using System.Linq;
    using System.Collections.Generic;
    using System.Security.Cryptography;

    using Microsoft.AspNet.Identity;

    public class SHA512PasswordHasher : PasswordHasher
    //IPasswordHasher
    {
        public SHA512PasswordHasher(string salt, int passwordIterations)
        {
            this.salt = salt;
            this.passwordIterations = passwordIterations;
        }

        private string salt;

        private int passwordIterations;

        private string optionsNameAlgorithm;

        public override string HashPassword(string password)
        {
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(password, System.Text.Encoding.UTF8.GetBytes(this.salt), OptionsNameAlgorithm.SHA512, this.passwordIterations);
            return Convert.ToBase64String(passwordDeriveBytes.GetBytes(64));
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var providerHasPassword = this.HashPassword(providedPassword);
            if (hashedPassword != providerHasPassword)
            {
                return PasswordVerificationResult.Failed;
            }

            return PasswordVerificationResult.Success;
        }

        public class OptionsNameAlgorithm
        {
            public const string MD5 = "MD5";

            public const string SHA1 = "SHA1";

            public const string SHA256 = "SHA256";

            public const string SHA384 = "SHA384";

            public const string SHA512 = "SHA512";

            public const string HMACSHA1 = "HMACSHA1";            
        }
    }
}