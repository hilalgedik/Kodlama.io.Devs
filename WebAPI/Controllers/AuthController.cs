using Application.Features.Users.Commands.UserLogin;
using Application.Features.Users.Commands.UserRegister;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        /// <summary>
        /// Kullanıcı kayıt işlemini yapar.
        /// </summary>
        /// <param name="userRegisterCommand">Kullanıcı kayıt komutu.</param>
        /// <returns>Kullanıcı kayıt işleminin sonucunu döndürür.</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterCommand userRegisterCommand)
        {
            var result = await Mediator!.Send(userRegisterCommand);
            return Ok(result);
        }

        /// <summary>
        /// Kullanıcı giriş işlemini yapar.
        /// </summary>
        /// <param name="UserLoginCommand">Kullanıcı giriş komutu.</param>
        /// <returns>Kullanıcı giriş işleminin sonucunu döndürür.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand userLoginCommand)
        {
            var result = await Mediator!.Send(userLoginCommand);
            return Ok(result);
        }
    }
}
