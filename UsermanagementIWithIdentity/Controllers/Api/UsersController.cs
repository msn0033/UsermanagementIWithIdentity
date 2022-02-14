using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UsermanagementIWithIdentity.Models;

namespace UsermanagementIWithIdentity.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
       

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string Id)
        {
            var user=await _userManager.FindByIdAsync(Id);
            if (user == null)return NotFound("not user");
            var result=await _userManager.DeleteAsync(user);
            if(!result.Succeeded) 
                return BadRequest(result);

            return Ok(200);
           
        }
    }
}
