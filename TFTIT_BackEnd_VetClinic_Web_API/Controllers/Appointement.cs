using Microsoft.AspNetCore.Mvc;

namespace TFTIT_BackEnd_VetClinic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Appointement : ControllerBase
    {
        [HttpGet("Appointments")]
        public IActionResult Get()
        {
            return Ok();
        }
        [HttpPost("AddAppointments")]
        public IActionResult Create()
        {
            return Ok();
        }
        [HttpPut("EditAppointments")]
        public IActionResult Update()
        {
            return Ok();

        }
        [HttpDelete("DeleteAppointments")]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}
