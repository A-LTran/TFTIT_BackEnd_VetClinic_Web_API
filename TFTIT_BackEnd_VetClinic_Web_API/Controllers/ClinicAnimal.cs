using Microsoft.AspNetCore.Mvc;

namespace TFTIC_BackEnd_VetClinic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicAnimal : ControllerBase
    {
        [HttpGet("Animals")]
        public IActionResult Get()
        {
            return Ok();
        }
        [HttpPost("AddAnimal")]
        public IActionResult Create()
        {
            return Ok();
        }
        [HttpPatch("EditAnimal")]
        public IActionResult Update()
        {
            return Ok();

        }
        [HttpDelete("DeleteAnimal")]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}
