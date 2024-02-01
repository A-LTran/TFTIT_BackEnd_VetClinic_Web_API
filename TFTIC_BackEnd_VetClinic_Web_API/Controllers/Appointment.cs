using Microsoft.AspNetCore.Authorization;

namespace TFTIC_BackEnd_VetClinic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Appointment : ControllerBase
    {
        private readonly IAppointmentRepository_BLL _appointmentService;
        public Appointment(IAppointmentRepository_BLL appointmentService)
        {
            _appointmentService = appointmentService;
        }

        //******************************************************//
        //                          GET                         //   
        //******************************************************//

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetAppointments")]
        public IActionResult Get()
        {
            return Ok(_appointmentService.Get(1));
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetHistory")]
        public IActionResult GetHistory()
        {
            return Ok(_appointmentService.Get(-1));
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetAppointmentsByVeterinaryId/{vetId}")]
        public IActionResult GetByVet([FromRoute] Guid vetId)
        {
            return Ok(_appointmentService.GetByVeterinaryId(vetId, 1));
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetHistoryByVeterinaryId/{vetId}")]
        public IActionResult GetHistoryByVet([FromRoute] Guid vetId)
        {
            return Ok(_appointmentService.GetByVeterinaryId(vetId, -1));
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetAppointmentsByOwnerId/{ownerId}")]
        public IActionResult GetByOwner([FromRoute] Guid ownerId)
        {
            return Ok(_appointmentService.GetByOwnerId(ownerId, 1));
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetHistoryByOwnerId/{ownerId}")]
        public IActionResult GetHistoryByOwner([FromRoute] Guid ownerId)
        {
            return Ok(_appointmentService.GetByOwnerId(ownerId, -1));
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetAppointmentsByAnimalName/{name}")]
        public IActionResult GetByAnimal([FromRoute] string name)
        {
            return Ok(_appointmentService.GetByAnimalName(name, 1));
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetHistoryByAnimalName/{name}")]
        public IActionResult GetHistoryByAnimal([FromRoute] string name)
        {
            return Ok(_appointmentService.GetByAnimalName(name, -1));
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetAppointmentsByAnimalId/{animalId}")]
        public IActionResult GetByAnimal([FromRoute] Guid animalId)
        {
            return Ok(_appointmentService.GetByAnimalId(animalId, 1));
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetHistoryByAnimalId/{animalId}")]
        public IActionResult GetHistoryByAnimal([FromRoute] Guid animalId)
        {
            return Ok(_appointmentService.GetByAnimalId(animalId, -1));
        }

        //******************************************************//
        //                          POST                        //   
        //******************************************************//

        [Authorize("veterinaryPolicy")]
        [HttpPost("AddAppointment")]
        public IActionResult Create([FromBody] AppointmentRegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_appointmentService.GetByAppointmentAvailability(form))
                return BadRequest("Cette plage horaire n'est pas disponible.");

            return Ok(_appointmentService.Create(form));
        }

        //******************************************************//
        //                         PATCH                        //   
        //******************************************************//

        [Authorize("veterinaryPolicy")]
        [HttpPatch("EditAppointment")]
        public IActionResult Update()
        {
            return Ok();

        }

        //******************************************************//
        //                        DELETE                        //   
        //******************************************************//

        [Authorize("veterinaryPolicy")]
        [HttpDelete("DeleteAppointment")]
        public IActionResult Delete()
        {
            return Ok();
        }

        //******************************************************//
        //                        TESTING                       //   
        //******************************************************//

        [Authorize("adminPolicy")]
        [HttpPost("GenerateSomeAppointments")]
        public IActionResult GenerateSomeAppointments()
        {
            AppointmentRegisterForm appointmentRegisterForm = new AppointmentRegisterForm()
            {
                AppointmentDate = DateTime.Now.AddDays(1),
                DurationMinutes = 30,
                Reason = "test",
                AnimalId = new Guid("5671c043-84d5-4da6-9863-c1a5710fca70"),
                VeterinaryId = new Guid("5671c043-84d5-4da6-9863-c1a5710fca59")
            };
            _appointmentService.Create(appointmentRegisterForm);
            return Ok();
        }
    }
}
