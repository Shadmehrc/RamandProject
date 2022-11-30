using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
