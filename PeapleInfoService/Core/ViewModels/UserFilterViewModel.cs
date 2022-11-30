using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.ViewModels
{
    public class UserFilterViewModel
    {
        public int? Id { get; set; }
        public string UserName { get; set; }

        public static implicit operator UserFilterModel(UserFilterViewModel vmodel)
        {
            return new UserFilterModel()
            {
                Id = vmodel.Id,
                UserName = vmodel.UserName,
            };
        }
    }
}
