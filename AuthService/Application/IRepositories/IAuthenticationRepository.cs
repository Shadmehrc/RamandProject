using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Application.IRepositories
{
    public interface IAuthenticationRepository
    {
        Task<ValidateUserModel> ValidateUser(LoginUserModel model);
        Task<bool> DeleteToken(int userId);
        Task<bool> SaveToken(SaveTokenModel model);
        Task<TokenModel> FindRefreshToken(string refreshToken);
        Task<List<ConfigKeyValueList>> GetAppConfig( );
    }
}
