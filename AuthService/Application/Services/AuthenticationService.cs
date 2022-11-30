using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.IRepositories;
using Application.IServices;
using Core.Models;
using Core.Tools;
using Core.ViewModels;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Application.Helper;
using Microsoft.Extensions.Logging;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly  IAuthenticationRepository _authenticationRepository;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly ConfigModel _configs;

        public AuthenticationService(IAuthenticationRepository authenticationRepository, ILogger<AuthenticationService> logger, ConfigService configService)
        {
            _authenticationRepository = authenticationRepository;
            _logger = logger;
            _configs = configService.GetConfigs().ConfigureAwait(true).GetAwaiter().GetResult();
        }

        public async Task<ValidateUserModel> ValidateUser(LoginUserViewModel vModel)
        {
            try
            {
                var secHelper = new SecurityHelper();
                LoginUserModel model = vModel;
                model.HashedPassword = secHelper.Getsha256Hash(vModel.Password);
                return await _authenticationRepository.ValidateUser(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ValidateUserModel(){IsSuccess = false, ResultMessage = "عملیات با خطا مواجه شد!"};
            }


        }

        public async Task<string> CreateToken(string userName)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim("UserName", userName),
                    new Claim("CreateDate", new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim("ExpireDate", new DateTimeOffset(DateTime.Now.AddHours((Convert.ToInt32(_configs.ExpiresInHour)))).ToUnixTimeSeconds().ToString()),
                };

                var key = _configs.Key;
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenExpire = DateTime.Now.AddHours(Convert.ToInt32(_configs.ExpiresInHour));

                var token = new JwtSecurityToken(
                    issuer: _configs.Issuer,
                    audience: _configs.Audience,
                    claims: claims,
                    expires: tokenExpire,
                    notBefore: DateTime.Now,
                    signingCredentials: credentials
                );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                return jwtToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ( "عملیات با خطا مواجه شد!");

            }
        }

        public async Task<bool> SaveToken(SaveTokenViewModel vModel)
        {
            try
            {
                SaveTokenModel saveTokenModel = vModel;
                saveTokenModel.RefreshTokenExp = DateTime.Now.AddHours(Convert.ToInt32(_configs.RefreshTokenExpiresInHour));
                saveTokenModel.TokenExp = DateTime.Now.AddHours(Convert.ToInt32(_configs.ExpiresInHour));
                return await _authenticationRepository.SaveToken(saveTokenModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public Task<string> GenerateRefreshToken()
        {
            try
            {
                var refreshToken = Guid.NewGuid().ToString();
                return Task.FromResult(refreshToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Task.FromResult("عملیات با خطا مواجه شد!");
            }
        }

        public async Task<TokenViewModel> FindRefreshToken(string refreshToken)
        {
            var helper = new SecurityHelper();
                return await _authenticationRepository.FindRefreshToken(helper.Getsha256Hash(refreshToken));
        }

        public async Task<bool> DeleteToken(int userId)
        {
            try
            {
                return await _authenticationRepository.DeleteToken(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }


    }
}
