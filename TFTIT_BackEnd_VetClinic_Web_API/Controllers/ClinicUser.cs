using Microsoft.AspNetCore.Mvc;

namespace TFTIT_BackEnd_VetClinic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicUser : ControllerBase
    {
        [HttpGet("Users")]
        public IActionResult Get()
        {
            return Ok();
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
