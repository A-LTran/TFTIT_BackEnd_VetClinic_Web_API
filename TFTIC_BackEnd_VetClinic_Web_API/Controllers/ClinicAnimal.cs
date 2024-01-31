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
        [HttpPost("GenerateAnimal")]
        public IActionResult GenerateAnimal()
        {
            AnimalRegisterForm animalRegisterForm = new AnimalRegisterForm()
            {
                AnimalName = "Boxy",
                Breed = "Boxer",
                BirthDate = DateTime.Now.AddYears(-1),
                OwnerId = new Guid("5671c043-84d5-4da6-9863-c1a5710fca60")
            };
            _animalService.Create(animalRegisterForm);
            return Ok();
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
            if (!ModelState.IsValid)
                return BadRequest();

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
