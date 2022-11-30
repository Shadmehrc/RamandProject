using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Query.GetUserList.GetUserListQuery;
using Core.Models;
using Core.ViewModels;

namespace Application.IRepository
{
    public interface IUserRepository
    {
        public Task<List<UserModel>> GetUsersList(GetUserListQuery model);
        public Task<RegisterUserResultModel> CreateUser(RegisterUserModel model);
    }
}
