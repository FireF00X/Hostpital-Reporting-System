using HospitalReporting.Application.DTOs.Identity;
using HospitalReporting.Application.Services.ServicesInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostpitalReporting.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAuthenticationService _auth) : ControllerBase
    {
        [HttpPost("CreateUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(CreateUser user)
        {
            var result= await _auth.CreateUser(user);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            var result = await _auth.LoginUser(user);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost("CompeletData")]
        public async Task<IActionResult> CompeletData(LoginForFirstTime user)
        {
            var result = await _auth.LoginNewCommer(user);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

    }
}
