using CodePulseAPI.Models.DTO.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        //tao costructor
        public AuthController(UserManager<IdentityUser> userManager) 
        {
            this.userManager = userManager;
        }

        //POST: {apibaseurl}/api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> register([FromBody] RegisterRequestDto request)
        {
            // tao doi tuong danh tinh nguoi dung

            var user = new IdentityUser
            {
                Email = request.Email?.Trim(),
                UserName = request.Email?.Trim(),
            };
            //tao nguoi dung
            var identityResult = await userManager.CreateAsync(user, request.Password);
            if (identityResult.Succeeded)
            {
                // them vai tro cho user (reader)
                identityResult = await userManager.AddToRoleAsync(user, "Reader");
                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                ;
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }
        //POST: {apibaseurl}/api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login([FromBody] LoginRequestDto request)
        {
            // kiem tra email
         var identityUser =    await userManager.FindByEmailAsync(request.Email);
            if(identityUser is not null)
            {
                // kiem tra mat khau
             var checkPasswordResult =    await userManager.CheckPasswordAsync(identityUser, request.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);
                    //tao 1 token va response (phan hoi)
                    var response = new LoginResponseDto
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = "TOKEN",

                    };
                    return Ok();
                }
            }
            ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
            return ValidationProblem(ModelState);
        }
    }
}
