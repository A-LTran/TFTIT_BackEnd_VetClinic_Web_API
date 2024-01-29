using Microsoft.AspNetCore.Mvc;

namespace TFTIC_BackEnd_VetClinic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicUser : ControllerBase
    {
        private IUserRepository_BLL _userService;
        public ClinicUser(IUserRepository_BLL userService)
        {
            _userService = userService;
        }
        [HttpGet("Users")]
        public IActionResult Get()
        {
            return Ok(_userService.Get());
        }
        [HttpGet("UsersByRole/{role}")]
        public IActionResult GetByRole([FromRoute] int role)
        {
            return Ok(_userService.GetUsersByRole(role));
        }
        [HttpPost("AddUser")]
        public IActionResult CreateUser()
        {
            return Ok();
        }
        [HttpPatch("EditUser")]
        public IActionResult UpdateUser()
        {
            return Ok();
        }
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser()
        {
            return Ok();
        }
    }
}
