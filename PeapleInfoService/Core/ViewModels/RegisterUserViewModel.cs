using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.ViewModels
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string ProvinceTitle { get; set; }
        public int Age { get; set; }

        public static implicit operator RegisterUserModel(RegisterUserViewModel VModel)
        {
            return new RegisterUserModel()
            {
                UserName = VModel.UserName,
                FullName = VModel.FullName,
                ProvinceTitle = VModel.ProvinceTitle,
                Age = VModel.Age,
            };
        }
    }
}
