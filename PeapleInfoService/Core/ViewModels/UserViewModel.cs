using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string ProvinceTitle { get; set; }
        public DateTime CreateDate { get; set; }

        public static implicit operator UserModel(UserViewModel vModel)
        {
            return new UserModel()
            {
                Id = vModel.Id,
                Age = vModel.Age,
                FullName = vModel.FullName,
                ProvinceTitle = vModel.ProvinceTitle,
                UserName = vModel.UserName,
                CreateDate = vModel.CreateDate
            };
        }
    }
}
