using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class TokenResultViewModel
    {
        public string HashedToken{ get; set; }
        public string RefreshToken{ get; set; }
        public string Message{ get; set; }
        public bool IsSuccess { get; set; }

    }
}
