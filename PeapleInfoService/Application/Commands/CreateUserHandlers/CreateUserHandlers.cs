using Application.IRepository;
using Core.Models;
using Core.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Tools;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Application.Commands.CreateUserHandlers
{
    public class CreateUserHandlers : IRequestHandler<CreateUserCommand.CreateUserCommand, RegisterUserResultViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CreateUserHandlers> _logger;

        public CreateUserHandlers(IUserRepository userRepository, ILogger<CreateUserHandlers> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<RegisterUserResultViewModel> Handle(CreateUserCommand.CreateUserCommand userModel, CancellationToken cancellationToken)
        {
            try
            {
                var secHelper = new SecurityHelper();
                RegisterUserModel model = userModel.Adapt<RegisterUserModel>();
                model.HashedPassword = secHelper.Getsha256Hash(userModel.Password);
                var result = await _userRepository.CreateUser(model);
                
                return result.Adapt<RegisterUserResultViewModel>();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return new RegisterUserResultViewModel()
                {
                    IsSuccess=false,
                    ResultMessage = "Error"
                };

            }
        }
    }
}
