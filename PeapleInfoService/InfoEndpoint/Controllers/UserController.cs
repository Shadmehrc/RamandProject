using System.Threading.Tasks;
using Application.Commands.CreateUserCommand;
using Application.Query.GetUserList.GetUserListQuery;
using Core.ViewModels;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserEndpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiVersion("1")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        public UserController( IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUsersList([FromQuery] UserFilterViewModel userVModel)
        {
            return Ok(await _mediator.Send(userVModel.Adapt<GetUserListQuery>()) );
        }


        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserViewModel userVModel)
        {
            var result = await _mediator.Send(userVModel.Adapt<CreateUserCommand>());

            return Ok(result);
        }

    }
}
