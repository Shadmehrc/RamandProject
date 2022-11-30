using System.Threading.Tasks;
using Application.Facades;
using Application.IFacades;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationEndpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationFacade _authenticationFacade;

        public AuthenticationController(IAuthenticationFacade authenticationFacade)
        {
            _authenticationFacade = authenticationFacade;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserViewModel vModel)
        {
            return Ok(await _authenticationFacade.GetToken(vModel));
        }
        [HttpGet]
        [Route("RefreshToken")]
        public async Task<IActionResult> Login([FromQuery] string refreshToken)
        {
            var result = await _authenticationFacade.GetRefreshToken(refreshToken);
            if (!result.IsSuccess)
            {
                return Unauthorized(result.Message);
            }
            return Ok(result);
        }
    }
}
