using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.ViewModels
{
    public class ValidateUserViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }

        public static implicit operator ValidateUserViewModel(ValidateUserModel model)
        {
            return new ValidateUserViewModel()
            {
                IsSuccess = model.IsSuccess,
                Message = model.ResultMessage,
                UserId = model.UserId
            };
        }
    }
}
