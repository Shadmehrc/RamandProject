using System.Collections.Generic;
using Core.ViewModels;
using MediatR;

namespace Application.Query.GetUserList.GetUserListQuery
{
    public class GetUserListQuery : IRequest<IEnumerable<UserViewModel>>
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
    }
}
