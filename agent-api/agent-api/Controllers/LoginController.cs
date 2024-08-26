using agent_api.Dto;
using agent_api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace agent_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController(ILoginService loginService) : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                TokenDto token = new()
                {
                    token =
                loginService.Login(loginDto.id)
                };
                return Ok(token);
            }
            catch 
            {
                return Unauthorized();
            }
        }

    }
}
