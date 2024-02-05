using BLL.Entities.AddressForms;
using BLL.Entities.Models;
using BLL.Entities.PersonForms;
using BLL.Tools;
using DAL.Entities.Enumerations;
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
        [HttpGet("GetVeterinarians")]
        public IActionResult GetVeterinarians()
        {
            return Ok(_userService.GetPersonsByRole((int)Role.Veterinary));
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetUserById/{userId}")]
        public IActionResult GetUserById([FromRoute] Guid userId)
        {
            UserForDisplay? u = _userService.GetUserById(userId);
            return (u is not null) ? Ok(u) : BadRequest(ToolSet.Message);
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetUserByMail/{mail}")]
        public IActionResult GetUserByMail([FromRoute] string mail)
        {
            UserForDisplay? u = _userService.GetUserByMail(mail);
            return (u is not null) ? Ok(u) : BadRequest(ToolSet.Message);
        }

        [Authorize("veterinaryPolicy")]
        [HttpGet("GetOwners")]
        public IActionResult GetOwners()
        {
            return Ok(_userService.GetPersonsByRole((int)Role.Owner));
        }

        [Authorize("adminAndVetPolicy")]
        [HttpGet("GetOwnerById/{ownerId}")]
        public IActionResult GetOwnerById([FromRoute] Guid ownerId)
        {
            UserForDisplay? o = _userService.GetOwnerById(ownerId);
            return (o is not null) ? Ok(o) : BadRequest(ToolSet.Message);
        }

        [Authorize("adminAndVetPolicy")]
        [HttpGet("GetOwnerByMail/{mail}")]
        public IActionResult GetOwnerByMail([FromRoute] string mail)
        {
            UserForDisplay? o = _userService.GetOwnerByMail(mail);
            return (o is not null) ? Ok(o) : BadRequest(ToolSet.Message);
        }

        [Authorize("adminAndVetPolicy")]
        [HttpGet("GetAddresses")]
        public IActionResult GetAddresses()
        {
            return Ok(_userService.GetAddresses());
        }

        [Authorize("adminAndVetPolicy")]
        [HttpGet("GetAddresseByPersonId/{personId}")]
        public IActionResult GetAddresseByPersonId([FromRoute] Guid personId)
        {
            return (_userService.GetAddressByPersonId(personId) is not null) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        //*******************************************************************//
        //                               POST                                //
        //*******************************************************************//

        [Authorize("adminPolicy")]
        [HttpPost("AddAdministrator/{addressId}")]
        public IActionResult CreateAdmin([FromBody] UserRegisterForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            form.PersonRole = Role.Administrator;
            return _userService.Create(form, addressId) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        [Authorize("adminPolicy")]
        [HttpPost("AddVeterinary/{addressId}")]
        public IActionResult CreateVeterinary([FromBody] UserRegisterForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            form.PersonRole = Role.Veterinary;
            return _userService.Create(form, addressId) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        [Authorize("veterinaryPolicy")]
        [HttpPost("AddOwner/{addressId}")]
        public IActionResult CreateOwner([FromBody] OwnerRegisterForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            form.PersonRole = Role.Owner;
            return _userService.Create(form, addressId) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        [Authorize("adminAndVetPolicy")]
        [HttpPost("AddAddress")]
        public IActionResult CreateAddress([FromBody] AddressRegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            return _userService.Create(form) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Form");
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
            return (_userService.UpdateUser(form, userId, Role.Administrator)) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        [Authorize("adminAndVetPolicy")]
        [HttpPatch("EditVeterinary/{userId}")]
        public IActionResult UpdateVeterinary([FromBody] UserEditForm form, [FromRoute] Guid userId)
        {
            Guid connectedUserId = HttpContext.User.FindFirst("userId")?.Value is null ? Guid.Empty : Guid.Parse(HttpContext.User.FindFirst("userId")?.Value);
            Role connectedRole = HttpContext.User.FindFirst("role")?.Value is null ? Role.Administrator : (Role)Enum.Parse(typeof(Role), HttpContext.User.FindFirst("role")?.Value);

            if (!(connectedUserId == userId || connectedRole == Role.Administrator))
                return Unauthorized("You are not authorized for this action.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            return (_userService.UpdateUser(form, userId, Role.Veterinary)) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        [Authorize("veterinaryPolicy")]
        [HttpPatch("EditOwner/{ownerId}")]
        public IActionResult EditOwner([FromBody] OwnerEditForm form, [FromRoute] Guid ownerId)
        {
            return (_userService.UpdateOwner(form, ownerId)) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        [Authorize("adminAndVetPolicy")]
        [HttpPatch("EditAddress/{addressId}")]
        public IActionResult UpdateAddress([FromBody] AddressEditForm form, [FromRoute] Guid addressId)
        {
            return (_userService.UpdateAddress(form, addressId)) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        //**************************************************************************************//
        //                                      DELETE                                          //
        //**************************************************************************************//

        [Authorize("adminPolicy")]
        [HttpDelete("DeletePerson/{personId}")]
        public IActionResult DeleteUser([FromRoute] Guid personId)
        {
            return (_userService.DeleteUser(personId)) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        [Authorize("veterinaryPolicy")]
        [HttpDelete("DeleteOwner/{ownerId}")]
        public IActionResult DeleteOwner([FromRoute] Guid ownerId)
        {
            return (_userService.DeleteOwner(ownerId)) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
        }

        [Authorize("veterinaryPolicy")]
        [HttpDelete("DeleteAddress/{addressId}")]
        public IActionResult DeleteAddress([FromRoute] Guid addressId)
        {
            return (_userService.DeleteAddress(addressId)) ? Ok(ToolSet.Message) : BadRequest(ToolSet.Message);
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
