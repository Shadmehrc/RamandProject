using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ConfigModel
    {
        public string Key { get; set; } 
        public string Issuer { get; set; } 
        public string Audience { get; set; } 
        public string ExpiresInHour { get; set; } 
        public string RefreshTokenExpiresInHour { get; set; }

    }
}
