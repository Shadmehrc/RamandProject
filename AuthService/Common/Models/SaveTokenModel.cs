using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class SaveTokenModel
    {
        public string HashedToken { get; set; }
        public string RefreshToken { get; set; }
        public string HashedRefreshToken{ get; set; }
        public int UserId{ get; set; }
        public DateTime TokenExp { get; set; }
        public DateTime RefreshTokenExp { get; set; }
    }
}
