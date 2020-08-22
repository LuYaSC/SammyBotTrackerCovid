namespace Core.JwtAuthServer.Providers
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Web;

    public class PasswordHasherManager : IPasswordHasher
    {
        public PasswordHasherManager(string salt, OptionsHashNameAlgorithm optionsHashNameAlgorithm, int passwordIterations)
        {
            this.salt = salt;
            this.optionHashNameAlgorithm = optionsHashNameAlgorithm;
            this.passwordIterations = passwordIterations;
        }

        private string salt;

        private OptionsHashNameAlgorithm optionHashNameAlgorithm;

        private int passwordIterations;

        public string HashPassword(string password)
        {
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(password, System.Text.Encoding.UTF8.GetBytes(this.salt), this.optionHashNameAlgorithm.ToString(), this.passwordIterations);
            return Convert.ToBase64String(passwordDeriveBytes.GetBytes(64));
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var providerHasPassword = this.HashPassword(providedPassword);
            if (hashedPassword != providerHasPassword)
            {
                return PasswordVerificationResult.Failed;
            }

            return PasswordVerificationResult.Success;
        }

        public enum OptionsHashNameAlgorithm
        {
            MD5 = 0,
            SHA1 = 1,
            SHA256 = 2,
            SHA384 = 3,
            SHA512 = 4,
            HMACSHA1 = 5
        }
    }
}