using System.Threading.Tasks;
using Core.ViewModels;

namespace Application.IFacades
{
    public interface IAuthenticationFacade
    {
        Task<TokenResultViewModel> GetToken(LoginUserViewModel VModel);
        Task<TokenResultViewModel> CreateToken(TokenViewModel VModel);
        Task<TokenResultViewModel> GetRefreshToken(string refreshToken);
    }
}
