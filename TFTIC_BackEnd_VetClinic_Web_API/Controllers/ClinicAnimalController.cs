namespace TFTIC_BackEnd_VetClinic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicAnimalController : ControllerBase
    {
        private readonly IAnimalRepository_BLL _animalService;
        public ClinicAnimalController(IAnimalRepository_BLL animalService)
        {
            _animalService = animalService;
        }

        //******************************************************//
        //                          GET                         //   
        //******************************************************//

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetAnimals")]
        public IActionResult GetAnimals()
        {
            return Ok(_animalService.Get());
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetAnimalsByOwner/{ownerId}")]
        public IActionResult GetAnimals([FromRoute] Guid ownerId)
        {
            return Ok(_animalService.GetByOwner(ownerId));
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetAnimalById/{animalId}")]
        public IActionResult GetAnimalById([FromRoute] Guid AnimalId)
        {
            Animal? a = _animalService.GetAnimal(AnimalId);
            return (a is not null) ? Ok(a) : BadRequest(ToolSet.Message);
        }

        //******************************************************//
        //                         POST                         //   
        //******************************************************//

        [Authorize("veterinaryPolicy")]
        [HttpPost("AddAnimal")]
        public IActionResult Create([FromBody] AnimalRegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Model");

            return (_animalService.Create(form)) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        //******************************************************//
        //                         PATCH                        //   
        //******************************************************//

        [Authorize("veterinaryPolicy")]
        [HttpPatch("EditAnimal/{animalId}")]
        public IActionResult Update([FromRoute] Guid animalId, [FromBody] AnimalEditForm form)
        {
            return (_animalService.Update(form, animalId)) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        //******************************************************//
        //                        DELETE                        //   
        //******************************************************//

        [Authorize("veterinaryPolicy")]
        [HttpDelete("DeleteAnimal/{animalId}")]
        public IActionResult Delete([FromRoute] Guid animalId)
        {
            return (_animalService.Delete(animalId)) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        //******************************************************//
        //                        TESTING                       //   
        //******************************************************//

        [Authorize("adminPolicy")]
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
    }
}
