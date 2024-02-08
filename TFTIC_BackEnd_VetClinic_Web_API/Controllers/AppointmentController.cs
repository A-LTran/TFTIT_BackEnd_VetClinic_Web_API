using BLL.Entities.AppointmentForms;

namespace TFTIC_BackEnd_VetClinic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository_BLL _appointmentService;
        private readonly Func<string> _getMessage;
        public AppointmentController(IAppointmentRepository_BLL appointmentService)
        {
            _appointmentService = appointmentService;
            _getMessage += _appointmentService.GetMessage;
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
        [HttpGet("GetById/{appId}")]
        public IActionResult GetById([FromRoute] Guid appId)
        {
            Appointment? a = _appointmentService.GetById(appId);
            return (a is not null) ? Ok(a) : BadRequest("Invalid Request");
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
                return BadRequest("Invalid Form");

            return (_appointmentService.Create(form)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        //******************************************************//
        //                         PATCH                        //   
        //******************************************************//

        [Authorize("veterinaryPolicy")]
        [HttpPatch("EditAppointment/{appId}")]
        public IActionResult Update([FromRoute] Guid appId, [FromBody] AppointmentEditForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            return (_appointmentService.Update(appId, form)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        //******************************************************//
        //                        DELETE                        //   
        //******************************************************//

        [Authorize("veterinaryPolicy")]
        [HttpDelete("DeleteAppointment/{appId}")]
        public IActionResult Delete([FromRoute] Guid appId)
        {
            return (_appointmentService.Delete(appId)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        #region TESTING
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
        #endregion
    }
}
