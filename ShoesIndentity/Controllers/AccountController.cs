using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormatApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoesIndentity.Models;
using ShoesIndentity.ViewModels;

namespace ShoesIndentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserManager<User> UserManager { get; }
        public AccountController(UserManager<User> userManager)
        {
            UserManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            var user = new User() { Email = model.Email, UserName = model.Email };
            var result = await UserManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                return this.OkResult();
            }
            else
            {
                if(result.Errors.Any(x=>x.Code == "DuplicateUserName"))
                {
                    return this.ErrorResult(400, "User exist");
                }
                return this.ErrorResult(400,"Fail register");
            }
        }
    }
}