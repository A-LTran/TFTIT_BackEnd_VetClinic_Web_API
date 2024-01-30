using BLL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace TFTIC_BackEnd_VetClinic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicAnimal : ControllerBase
    {
        private readonly IAnimalRepository_BLL _animalService;
        public ClinicAnimal(IAnimalRepository_BLL animalService)
        {
            _animalService = animalService;
        }

        [HttpGet("GetAnimals")]
        public IActionResult GetAnimals()
        {
            return Ok(_animalService.Get());
        }
        [HttpGet("GetAnimalById/{animalId}")]
        public IActionResult GetAnimalById([FromRoute] Guid AnimalId)
        {
            return Ok(_animalService.GetAnimal(AnimalId));
        }
        [HttpPost("AddAnimal")]
        public IActionResult Create([FromBody] AnimalRegisterForm form)
        {
            return Ok(_animalService.Create(form));
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
