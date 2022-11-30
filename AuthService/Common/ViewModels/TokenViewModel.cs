using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.ViewModels
{
    public class TokenViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string HashedToken { get; set; }
        public string HashedRefreshToken { get; set; }
        public DateTime TokenExp { get; set; }
        public DateTime RefreshTokenExp { get; set; }

        public static implicit operator TokenViewModel(TokenModel model)
        {
            return new TokenViewModel()
            {
                UserId = model.UserId,
                UserName = model.UserName,
                HashedToken = model.HashedToken,
                HashedRefreshToken = model.HashedRefreshToken,
                TokenExp = model.TokenExp,
                RefreshTokenExp = model.RefreshTokenExp
            };
        }
    }
}
