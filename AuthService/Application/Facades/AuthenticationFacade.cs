using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IFacades;
using Application.IServices;
using Core.Tools;
using Core.ViewModels;
using Microsoft.Extensions.Logging;

namespace Application.Facades
{
    public class AuthenticationFacade : IAuthenticationFacade
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationFacade> _logger;


        public AuthenticationFacade(IAuthenticationService authenticationService, ILogger<AuthenticationFacade> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        public async Task<TokenResultViewModel> GetToken(LoginUserViewModel vModel)
        {
            try
            {
                var checkUser = await _authenticationService.ValidateUser(vModel);
                if (checkUser.IsSuccess)
                {
                    var createTokenModel = new TokenViewModel()
                    {
                        UserId = checkUser.UserId.Value,
                        UserName = vModel.UserName
                    };
                    return await CreateToken(createTokenModel);
                }
                return new TokenResultViewModel()
                {
                    HashedToken = "",
                    IsSuccess = false,
                    Message = checkUser.ResultMessage,
                    RefreshToken = ""
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new TokenResultViewModel()
                {
                    HashedToken = "",
                    IsSuccess = false,
                    Message = "Error",
                    RefreshToken = ""
                };

            }

        }

        public async Task<TokenResultViewModel> GetRefreshToken(string refreshToken)
        {
            try
            {
                var token = await _authenticationService.FindRefreshToken(refreshToken);
                if (token.HashedToken == null)
                {
                    return new TokenResultViewModel()
                    {
                        IsSuccess = false
                    };

                }

                if (token.RefreshTokenExp < DateTime.Now)
                {
                    return new TokenResultViewModel()
                    {
                        IsSuccess = false,
                        Message = "RefreshToken Expired"
                    };
                }
                var model = new TokenViewModel()
                {
                    UserId = token.UserId,
                    UserName = token.UserName
                };
                return await CreateToken(model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new TokenResultViewModel()
                {
                    HashedToken = "",
                    IsSuccess = false,
                    Message = "Error",
                    RefreshToken = ""
                };

            }
        }

        public async Task<TokenResultViewModel> CreateToken(TokenViewModel vModel)
        {
            var deleteToken = await _authenticationService.DeleteToken(vModel.UserId);
            var token = await _authenticationService.CreateToken(vModel.UserName);
            var refreshToken = await _authenticationService.GenerateRefreshToken();

            var helper = new SecurityHelper();
            var saveTokenModel = new SaveTokenViewModel()
            {
                HashedToken = token,
                RefreshToken = refreshToken,
                UserId = vModel.UserId,
                HashedRefreshToken = helper.Getsha256Hash(refreshToken)
            };
            var saveToken = await _authenticationService.SaveToken(saveTokenModel);
            if (saveToken)
            {
                return new TokenResultViewModel()
                {
                    HashedToken = token,
                    IsSuccess = true,
                    Message = "",
                    RefreshToken = refreshToken
                };

            }
            return new TokenResultViewModel()
            {
                HashedToken = "",
                IsSuccess = false,
                Message = "Save Token Failed",
                RefreshToken = ""
            };
        }

    }

}
