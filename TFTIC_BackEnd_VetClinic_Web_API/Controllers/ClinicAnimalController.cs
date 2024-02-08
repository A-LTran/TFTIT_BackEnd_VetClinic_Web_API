namespace TFTIC_BackEnd_VetClinic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicAnimalController : ControllerBase
    {
        private readonly IAnimalRepository_BLL _animalService;
        private Func<string> _getMessage;
        public ClinicAnimalController(IAnimalRepository_BLL animalService)
        {
            _animalService = animalService;
            _getMessage += _animalService.GetMessage;
        }

        //******************************************************//
        //                          GET                         //   
        //******************************************************//

        /// <summary>
        /// Get all animals
        /// </summary>
        /// <returns>IEnumerable&lt;Animal&gt;</returns>
        [Authorize("veterinaryPolicy")]
        [HttpGet("GetAnimals")]
        public IActionResult GetAnimals()
        {
            return Ok(_animalService.Get());
        }

        /// <summary>
        /// Get animals by owner
        /// </summary>
        /// <param name="ownerId">Guid - PersonId</param>
        /// <returns>IEnumerable&lt;Address&gt;</returns>
        [Authorize("veterinaryPolicy")]
        [HttpGet("GetAnimalsByOwner/{ownerId}")]
        public IActionResult GetAnimals([FromRoute] Guid ownerId)
        {
            return Ok(_animalService.GetByOwner(ownerId));
        }

        /// <summary>
        /// Get animal by Id
        /// </summary>
        /// <param name="animalId">Guid - AnimalId</param>
        /// <returns>Animal</returns>
        [Authorize("veterinaryPolicy")]
        [HttpGet("GetAnimalById/{animalId}")]
        public IActionResult GetAnimalById([FromRoute] Guid animalId)
        {
            Animal? a = _animalService.GetAnimal(animalId);
            return (a is not null) ? Ok(a) : BadRequest(_getMessage?.Invoke());
        }

        //******************************************************//
        //                         POST                         //   
        //******************************************************//

        /// <summary>
        /// Create an animal
        /// </summary>
        /// <param name="form">AnimalRegisterForm</param>
        /// <returns>void</returns>
        [Authorize("veterinaryPolicy")]
        [HttpPost("AddAnimal")]
        public IActionResult Create([FromBody] AnimalRegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Model");

            return (_animalService.Create(form)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        //******************************************************//
        //                         PATCH                        //   
        //******************************************************//

        /// <summary>
        /// Update an animal
        /// </summary>
        /// <param name="animalId">Guid - AnimalId</param>
        /// <param name="form">AnimalEditForm</param>
        /// <returns>void</returns>
        [Authorize("veterinaryPolicy")]
        [HttpPatch("EditAnimal/{animalId}")]
        public IActionResult Update([FromRoute] Guid animalId, [FromBody] AnimalEditForm form)
        {
            return (_animalService.Update(form, animalId)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        //******************************************************//
        //                        DELETE                        //   
        //******************************************************//

        /// <summary>
        /// Delete an animal
        /// </summary>
        /// <param name="animalId">Guid - AnimalId</param>
        /// <returns>void</returns>
        [Authorize("veterinaryPolicy")]
        [HttpDelete("DeleteAnimal/{animalId}")]
        public IActionResult Delete([FromRoute] Guid animalId)
        {
            return (_animalService.Delete(animalId)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        #region TESTING
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
        #endregion
    }
}
