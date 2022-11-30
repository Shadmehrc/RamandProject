using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.IRepository;
using Core.ViewModels;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Query.GetUserList.GetUserListHandlers
{
    public class GetUserListHandlers : IRequestHandler<GetUserListQuery.GetUserListQuery, IEnumerable<UserViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetUserListHandlers> _logger;

        public GetUserListHandlers(IUserRepository userRepository, ILogger<GetUserListHandlers> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<UserViewModel>> Handle(GetUserListQuery.GetUserListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userRepository.GetUsersList(request);
                return result.Adapt<List<UserViewModel>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new List<UserViewModel>();
            }
        }
    }
}
