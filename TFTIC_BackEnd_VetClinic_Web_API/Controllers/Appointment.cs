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

        [HttpGet("GetAppointments")]
        public IActionResult Get()
        {
            return Ok(_appointmentService.Get());
        }

        [HttpGet("GetAppointmentsByVeterinaryId/{vetId}")]
        public IActionResult Get([FromRoute] Guid vetId)
        {
            return Ok(_appointmentService.GetByVeterinaryId(vetId));
        }

        [HttpGet("GetAppointmentsByDogName/{name}")]
        public IActionResult Get([FromRoute] string name)
        {
            return Ok(_appointmentService.GetByDogName(name));
        }

        [HttpPost("AddAppointment")]
        public IActionResult Create([FromBody] AppointmentRegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_appointmentService.GetByAppointmentAvailability(form))
                return BadRequest("Cette plage horaire n'est pas disponible.");

            return Ok(_appointmentService.Create(form));
        }
        [HttpPut("EditAppointment")]
        public IActionResult Update()
        {
            return Ok();

        }
        [HttpDelete("DeleteAppointment")]
        public IActionResult Delete()
        {
            return Ok();
        }

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
