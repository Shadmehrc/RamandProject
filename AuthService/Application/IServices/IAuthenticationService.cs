using System.Threading.Tasks;
using Core.Models;
using Core.ViewModels;
using Microsoft.Extensions.Primitives;

namespace Application.IServices
{
    public interface IAuthenticationService
    {
        Task<ValidateUserModel> ValidateUser(LoginUserViewModel vModel);
        Task<string> CreateToken(string UserName);

        Task<bool> DeleteToken(int userId);
        Task<bool> SaveToken(SaveTokenViewModel vModel);
        Task<string> GenerateRefreshToken();
        Task<TokenViewModel> FindRefreshToken(string refreshToken);

    }
}
