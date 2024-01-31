namespace TFTIC_BackEnd_VetClinic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicUser : ControllerBase
    {
        private IUserRepository_BLL _userService;
        public ClinicUser(IUserRepository_BLL userService)
        {
            _userService = userService;
        }

        //**************************************************************************************//
        //                                       GET                                            //
        //**************************************************************************************//

        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            return Ok(_userService.Get());
        }
        [HttpGet("GetUsersByRole/{role}")]
        public IActionResult GetByRole([FromRoute] int role)
        {
            return Ok(_userService.GetPersonsByRole(role));
        }

        //**************************************************************************************//
        //                                       POST                                           //
        //**************************************************************************************//

        [HttpPost("AddAdministrator/{addressId}")]
        public IActionResult CreateAdmin([FromBody] UserRegisterForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            form.PersonRole = Role.Administrator;
            return Ok(_userService.Create(form, addressId));
        }

        [HttpPost("AddVeterinary/{addressId}")]
        public IActionResult CreateVeterinary([FromBody] UserRegisterForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            form.PersonRole = Role.Veterinary;
            return Ok(_userService.Create(form, addressId));
        }

        [HttpPost("AddOwner/{addressId}")]
        public IActionResult CreateOwner([FromBody] OwnerRegisterForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            form.PersonRole = Role.Owner;
            return Ok(_userService.Create(form, addressId));
        }

        [HttpPost("AddAddress")]
        public IActionResult CreateAddress([FromBody] AddressRegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_userService.Create(form));
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User? connectedUser = _userService.GetUserByMail(form.Email);

            if (connectedUser == null)
            {
                return BadRequest("Utilisateur inexistant.");
            }

            TokenManager tokenManager = new TokenManager();

            return Ok(tokenManager.GenerateToken(connectedUser));
        }

        [HttpPost("GenerateSomePersons")]
        public IActionResult GenerateSomePersons()
        {
            AddressRegisterForm addressForm = new AddressRegisterForm()
            {
                Country = "FR",
                City = "Paris",
                Address1 = "rue de la paix, 1",
                PostalCode = "75000"
            };
            _userService.Create(addressForm);

            Guid AddressId = new Guid("5671c043-84d5-4da6-9863-c1a5710fca54");
            UserRegisterForm adminForm = new UserRegisterForm()
            {
                LastName = "Admin",
                FirstName = "Admin",
                Email = "Admin@example.com",
                UserPassword = "123456",
                ConfirmUserPassword = "123456",
                PersonRole = Role.Administrator
            };
            _userService.Create(adminForm, AddressId);
            UserRegisterForm vetForm = new UserRegisterForm()
            {
                LastName = "vet",
                FirstName = "vet",
                Email = "vet@example.com",
                UserPassword = "123456",
                ConfirmUserPassword = "123456",
                PersonRole = Role.Veterinary
            };
            _userService.Create(vetForm, AddressId);
            OwnerRegisterForm ownerRegisterForm = new OwnerRegisterForm()
            {
                LastName = "owner",
                FirstName = "owner",
                Email = "owner@example.com",
                PersonRole = Role.Owner
            };
            _userService.Create(ownerRegisterForm, AddressId);

            return Ok();

        }

        //**************************************************************************************//
        //                                       PATCH                                          //
        //**************************************************************************************//

        [HttpPatch("EditUser")]
        public IActionResult UpdateUser()
        {
            return Ok();
        }

        //**************************************************************************************//
        //                                      DELETE                                          //
        //**************************************************************************************//
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser()
        {
            return Ok();
        }
    }
}
