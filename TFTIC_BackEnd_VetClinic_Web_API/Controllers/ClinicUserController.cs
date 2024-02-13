namespace TFTIC_BackEnd_VetClinic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicUserController : ControllerBase
    {
        private IUserRepository_BLL _userService;
        private Func<string> _getMessage;
        public ClinicUserController(IUserRepository_BLL userService)
        {
            _userService = userService;
            _getMessage += _userService.GetMessage;
        }

        //*******************************************************************//
        //                               GET                                 //
        //*******************************************************************//

        /// <summary>
        /// Get all active users
        /// </summary>
        /// <returns>IEnumerable&lt;UserDto?&gt;</returns>
        [Authorize("adminPolicy")]
        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            return Ok(_userService.Get());
        }

        /// <summary>
        /// Get active persons by role
        /// </summary>
        /// <param name="role">Enum UserRole Parsed to int</param>
        /// <returns>IEnumerable&lt;UserDto?&gt;</returns>
        [Authorize("adminPolicy")]
        [HttpGet("GetUsersByRole/{role}")]
        public IActionResult GetByRole([FromRoute] int role)
        {
            return Ok(_userService.GetPersonsByRole(role));
        }

        /// <summary>
        /// Get all active veterinarians
        /// </summary>
        /// <returns>IEnumerable&lt;UserDto?&gt;</returns>
        [Authorize("veterinaryPolicy")]
        [HttpGet("GetVeterinarians")]
        public IActionResult GetVeterinarians()
        {
            return Ok(_userService.GetPersonsByRole((int)Role.Veterinary));
        }

        /// <summary>
        /// Get a veterinary by PersonId
        /// </summary>
        /// <param name="userId">Guid - PersonId</param>
        /// <returns>UserDto</returns>
        [Authorize("veterinaryPolicy")]
        [HttpGet("GetUserById/{userId}")]
        public IActionResult GetUserById([FromRoute] Guid userId)
        {
            UserDto? u = _userService.GetUserById(userId);
            return (u is not null) ? Ok(u) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Get veterinary by Email addresss
        /// </summary>
        /// <param name="mail">string - Email</param>
        /// <returns>UserDto</returns>
        [Authorize("veterinaryPolicy")]
        [HttpGet("GetUserByMail/{mail}")]
        public IActionResult GetUserByMail([FromRoute] string mail)
        {
            UserDto? u = _userService.GetUserByMail(mail);
            return (u is not null) ? Ok(u) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Get all active owners
        /// </summary>
        /// <returns>IEnumerable&lt;UserDto?&gt;</returns>
        [Authorize("veterinaryPolicy")]
        [HttpGet("GetOwners")]
        public IActionResult GetOwners()
        {
            return Ok(_userService.GetPersonsByRole((int)Role.Owner));
        }

        /// <summary>
        /// Get owner by PersonId
        /// </summary>
        /// <param name="ownerId">Guid - PersonId</param>
        /// <returns>UserDto</returns>
        [Authorize("adminAndVetPolicy")]
        [HttpGet("GetOwnerById/{ownerId}")]
        public IActionResult GetOwnerById([FromRoute] Guid ownerId)
        {
            UserDto? o = _userService.GetOwnerById(ownerId);
            return (o is not null) ? Ok(o) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Get owner by Email address
        /// </summary>
        /// <param name="mail">string - Email</param>
        /// <returns>UserDto</returns>
        [Authorize("adminAndVetPolicy")]
        [HttpGet("GetOwnerByMail/{mail}")]
        public IActionResult GetOwnerByMail([FromRoute] string mail)
        {
            UserDto? o = _userService.GetOwnerByMail(mail);
            return (o is not null) ? Ok(o) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Get all active addresses
        /// </summary>
        /// <returns>IEnumerable&lt;Address&gt;</returns>
        [Authorize("adminAndVetPolicy")]
        [HttpGet("GetAddresses")]
        public IActionResult GetAddresses()
        {
            return Ok(_userService.GetAddresses());
        }

        /// <summary>
        /// Get address by PersonId
        /// </summary>
        /// <param name="personId">Guid - PersonId</param>
        /// <returns>Address</returns>
        [Authorize("adminAndVetPolicy")]
        [HttpGet("GetAddressByPersonId/{personId}")]
        public IActionResult GetAddressByPersonId([FromRoute] Guid personId)
        {
            return (_userService.GetAddressByPersonId(personId) is not null) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        //*******************************************************************//
        //                               POST                                //
        //*******************************************************************//

        /// <summary>
        /// Create an administrator
        /// </summary>
        /// <param name="form">UserRegisterForm</param>
        /// <param name="addressId">Guid - AddressId</param>
        /// <returns>void</returns>
        [Authorize("adminPolicy")]
        [HttpPost("AddAdministrator/{addressId}")]
        public IActionResult CreateAdmin([FromBody] UserRegisterForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            if (!_userService.AddressExistsCheckById(addressId))
                return BadRequest("Invalid Address");

            form.PersonRole = Role.Administrator;
            return _userService.Create(form, addressId) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Create a veterinary
        /// </summary>
        /// <param name="form">UserRegisterForm</param>
        /// <param name="addressId">Guid - AddressId</param>
        /// <returns>void</returns>
        [Authorize("adminPolicy")]
        [HttpPost("AddVeterinary/{addressId}")]
        public IActionResult CreateVeterinary([FromBody] UserRegisterForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            if (!_userService.AddressExistsCheckById(addressId))
                return BadRequest("Invalid Address");

            form.PersonRole = Role.Veterinary;
            return _userService.Create(form, addressId) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Create an owner
        /// </summary>
        /// <param name="form">OwnerRegisterForm</param>
        /// <param name="addressId">Guid - AddressId</param>
        /// <returns>void</returns>
        [Authorize("veterinaryPolicy")]
        [HttpPost("AddOwner/{addressId}")]
        public IActionResult CreateOwner([FromBody] OwnerRegisterForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            if (!_userService.AddressExistsCheckById(addressId))
                return BadRequest("Invalid Address");

            form.PersonRole = Role.Owner;
            return _userService.Create(form, addressId) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Create an address
        /// </summary>
        /// <param name="form">AddressRegisterForm</param>
        /// <returns>void</returns>
        [Authorize("adminAndVetPolicy")]
        [HttpPost("AddAddress")]
        public IActionResult CreateAddress([FromBody] AddressRegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            if (_userService.AddressExistsCheckByAddressInfo(form))
                return BadRequest("Address already exists");

            return _userService.Create(form) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Log into the webAPI
        /// </summary>
        /// <param name="form">UserLoginForm</param>
        /// <returns>string - token</returns>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Form");
            }

            if (!_userService.PersonExistsCheckByMail(form.Email))
            {
                return BadRequest("Identifiants incorrects.");
            }

            if (!_userService.VerifyPassword(form))
            {
                return BadRequest("Identifiants incorrects.");
            }

            UserTokenDto connectedUser = _userService.GetUserDtoByMail(form.Email)!;
            TokenManager tokenManager = new();

            string? token = tokenManager.GenerateToken(connectedUser);

            if (token is null)
                return BadRequest("Response failure : Something went wrong...");

            return Ok(token);
        }

        //**************************************************************************************//
        //                                       PATCH                                          //
        //**************************************************************************************//

        /// <summary>
        /// Update an administrator
        /// </summary>
        /// <param name="form">UserEditForm</param>
        /// <param name="userId">Guid - PersonId</param>
        /// <returns>void</returns>
        [Authorize("adminPolicy")]
        [HttpPatch("EditAdmin/{userId}")]
        public IActionResult UpdateAdmin([FromBody] UserEditForm form, [FromRoute] Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            if (!_userService.PersonExistsCheckById(userId))
                return BadRequest("Identifiants incorrects.");

            return (_userService.UpdateUser(form, userId, Role.Administrator)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Update a veterinary
        /// </summary>
        /// <param name="form">UserEditForm</param>
        /// <param name="userId">Guid - PersonId</param>
        /// <returns>void</returns>
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

            if (!_userService.PersonExistsCheckById(userId))
                return BadRequest("Identifiants incorrects.");

            return (_userService.UpdateUser(form, userId, Role.Veterinary)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Update an owner
        /// </summary>
        /// <param name="form">OwnerEditForm</param>
        /// <param name="ownerId">Guid - PersonId</param>
        /// <returns>void</returns>
        [Authorize("veterinaryPolicy")]
        [HttpPatch("EditOwner/{ownerId}")]
        public IActionResult EditOwner([FromBody] OwnerEditForm form, [FromRoute] Guid ownerId)
        {

            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            if (!_userService.PersonExistsCheckById(ownerId))
                return BadRequest("Identifiants incorrects.");

            return (_userService.UpdateOwner(form, ownerId)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Update an address
        /// </summary>
        /// <param name="form">AddressEditForm</param>
        /// <param name="addressId">Guid - AddressId</param>
        /// <returns>void</returns>
        [Authorize("adminAndVetPolicy")]
        [HttpPatch("EditAddress/{addressId}")]
        public IActionResult UpdateAddress([FromBody] AddressEditForm form, [FromRoute] Guid addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Form");

            if (!_userService.AddressExistsCheckById(addressId))
                return BadRequest("Invalid Address");

            return (_userService.UpdateAddress(form, addressId)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        //**************************************************************************************//
        //                                      DELETE                                          //
        //**************************************************************************************//

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="personId">Guid - PersonId</param>
        /// <returns>void</returns>
        [Authorize("adminPolicy")]
        [HttpDelete("DeletePerson/{personId}")]
        public IActionResult DeleteUser([FromRoute] Guid personId)
        {
            if (!_userService.PersonExistsCheckById(personId))
                return BadRequest("Identifiants incorrects.");

            return (_userService.DeleteUser(personId)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Delete an owner
        /// </summary>
        /// <param name="ownerId">Guid - PersonId</param>
        /// <returns>void</returns>
        [Authorize("veterinaryPolicy")]
        [HttpDelete("DeleteOwner/{ownerId}")]
        public IActionResult DeleteOwner([FromRoute] Guid ownerId)
        {
            if (!_userService.PersonExistsCheckById(ownerId))
                return BadRequest("Identifiants incorrects.");

            return (_userService.DeleteOwner(ownerId)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        /// <summary>
        /// Delete an address
        /// </summary>
        /// <param name="addressId">Guid - AddressId</param>
        /// <returns>void</returns>
        [Authorize("veterinaryPolicy")]
        [HttpDelete("DeleteAddress/{addressId}")]
        public IActionResult DeleteAddress([FromRoute] Guid addressId)
        {
            if (!_userService.AddressExistsCheckById(addressId))
                return BadRequest("Invalid Address");

            return (_userService.DeleteAddress(addressId)) ? Ok(_getMessage?.Invoke()) : BadRequest(_getMessage?.Invoke());
        }

        #region TESTING
        //*******************************************************************//
        //                              TESTING                              // 
        //*******************************************************************//

        /// <summary>
        /// Populate some persons
        /// </summary>
        /// <returns>void</returns>
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
        #endregion
    }
}
