using System.Collections.Generic;
using Core.ViewModels;
using MediatR;

namespace Application.Commands.CreateUserCommand
{
    public class CreateUserCommand : IRequest<RegisterUserResultViewModel>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string ProvinceTitle { get; set; }
        public int Age { get; set; }

    }
}
