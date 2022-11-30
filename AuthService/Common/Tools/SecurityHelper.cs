using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Tools
{
    public class SecurityHelper
    {
        public string Getsha256Hash(string value)
        {
            var algorithm = new SHA256CryptoServiceProvider();
            var byteValue = Encoding.UTF8.GetBytes(value);
            var byteHash = algorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }
    }
}
