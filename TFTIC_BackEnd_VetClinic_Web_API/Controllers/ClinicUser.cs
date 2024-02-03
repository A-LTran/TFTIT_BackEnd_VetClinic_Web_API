using BLL.Entities.PersonForms;
using Microsoft.AspNetCore.Authorization;

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

        //*******************************************************************//
        //                               GET                                 //
        //*******************************************************************//

        [Authorize("adminPolicy")]
        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            return Ok(_userService.Get());
        }

        [Authorize("adminPolicy")]
        [HttpGet("GetUsersByRole/{role}")]
        public IActionResult GetByRole([FromRoute] int role)
        {
            return Ok(_userService.GetPersonsByRole(role));
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetOwners")]
        public IActionResult GetOwners()
        {
            return Ok(_userService.GetPersonsByRole((int)Role.Owner));
        }

        [Authorize("adminAndVetPolicy")]
        [HttpGet("GetAddresses")]
        public IActionResult GetAddresses()
        {
            return Ok(_userService.GetAddresses());
        }

        //*******************************************************************//
        //                               POST                                //
        //*******************************************************************//

        [Authorize("adminPolicy")]
        [HttpPost("AddAdministrator/{addressId}")]
        public IActionResult CreateAdmin([FromBody] UserRegisterForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            form.PersonRole = Role.Administrator;
            return Ok(_userService.Create(form, addressId));
        }

        [Authorize("adminPolicy")]
        [HttpPost("AddVeterinary/{addressId}")]
        public IActionResult CreateVeterinary([FromBody] UserRegisterForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            form.PersonRole = Role.Veterinary;
            return Ok(_userService.Create(form, addressId));
        }

        [Authorize("veterinaryPolicy")]
        [HttpPost("AddOwner/{addressId}")]
        public IActionResult CreateOwner([FromBody] OwnerRegisterForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            form.PersonRole = Role.Owner;
            return Ok(_userService.Create(form, addressId));
        }

        [Authorize("adminAndVetPolicy")]
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

            User? connectedUser = _userService.Login(form);

            if (connectedUser is null)
            {
                return BadRequest("Identifiants incorrects.");
            }

            TokenManager tokenManager = new TokenManager();

            return Ok(tokenManager.GenerateToken(connectedUser));
        }

        //**************************************************************************************//
        //                                       PATCH                                          //
        //**************************************************************************************//

        [Authorize("adminPolicy")]
        [HttpPatch("EditAdmin/{userId}")]
        public IActionResult UpdateAdmin([FromBody] UserEditForm form, [FromRoute] Guid userId)
        {
            return Ok(_userService.UpdateUser(form, userId, Role.Administrator));
        }

        [Authorize("adminPolicy")]
        [HttpPatch("EditVeterinary/{userId}")]
        public IActionResult UpdateVeterinary([FromBody] UserEditForm form, [FromRoute] Guid userId)
        {
            return Ok(_userService.UpdateUser(form, userId, Role.Veterinary));
        }

        [Authorize("veterinaryPolicy")]
        [HttpPatch("EditOwner/{ownerId}")]
        public IActionResult EditOwner([FromBody] OwnerEditForm form, [FromRoute] Guid ownerId)
        {
            return Ok(_userService.UpdateOwner(form, ownerId));
        }

        //**************************************************************************************//
        //                                      DELETE                                          //
        //**************************************************************************************//

        [Authorize("adminPolicy")]
        [HttpDelete("DeletePerson/{personId}")]
        public IActionResult DeleteUser([FromRoute] Guid personId)
        {
            return Ok(_userService.DeleteUser(personId));
        }

        [Authorize("veterinaryPolicy")]
        [HttpDelete("DeleteOwner/{ownerId}")]
        public IActionResult DeleteOwner([FromRoute] Guid ownerId)
        {
            return Ok(_userService.DeleteOwner(ownerId));
        }

        //*******************************************************************//
        //                              TESTING                              // 
        //*******************************************************************//

        [Authorize("adminPolicy")]
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
    }
}
