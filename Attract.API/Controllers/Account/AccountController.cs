using Attract.Common.BaseResponse;
using Attract.Common.DTOs.User;
using Attract.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attract.API.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;

        public AccountController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<BaseCommandResponse>> Register(UserDTO userDTO)
        {
            return Ok(await authService.Register(userDTO));
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseCommandResponse>> Login(LoginUserDTO loginUserDTO)
        {
            return Ok(await authService.Login(loginUserDTO));
        }
    }
}
