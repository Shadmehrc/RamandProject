using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.ViewModels
{
    public class SaveTokenViewModel
    {
        public string HashedToken { get; set; }
        public string RefreshToken { get; set; }
        public string HashedRefreshToken { get; set; }
        public int UserId { get; set; }

        public static implicit operator SaveTokenModel(SaveTokenViewModel model)
        {
            return new SaveTokenModel()
            {
                HashedRefreshToken = model.HashedRefreshToken,
                UserId = model.UserId,
                HashedToken = model.HashedToken,
            };
        }
    }
}
