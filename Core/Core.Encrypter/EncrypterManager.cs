namespace TC.Core.Encrypter
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    public class EncrypterManager
    {
        public static string DecryptConnectionString(string cnn, string key)
        {
            Match match = Regex.Match(cnn, @"(?<=assword=)(.*)(?===;)",
            RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string password = Encoding.UTF8.GetString(CriptographyUtils.Decrypt(Convert.FromBase64String(match.Value + "=="), key));
                return cnn.Replace(match.Value + "==", password);
            }
            return cnn;
        }

        public static string DecryptString(string encryptedString, string key)
        {
            return Encoding.UTF8.GetString(CriptographyUtils.Decrypt(Convert.FromBase64String(encryptedString), key));
        }
    }
}
