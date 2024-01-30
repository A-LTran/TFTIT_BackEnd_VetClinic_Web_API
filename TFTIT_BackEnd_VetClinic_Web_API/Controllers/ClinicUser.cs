using BLL.Entities;
using Microsoft.AspNetCore.Mvc;

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
        //                                       GETS                                           //
        //**************************************************************************************//
        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            return Ok(_userService.Get());
        }
        [HttpGet("GetUsersByRole/{role}")]
        public IActionResult GetByRole([FromRoute] int role)
        {
            return Ok(_userService.GetUsersByRole(role));
        }

        //**************************************************************************************//
        //                                       POST                                           //
        //**************************************************************************************//
        [HttpPost("AddAdmininstrator/{form}")]
        public IActionResult CreateAdmin([FromBody] UserRegisterForm form)
        {
            form.UserRole = Role.Administrator;
            return Ok(_userService.Create(form));
        }

        [HttpPost("AddVeterinary")]
        public IActionResult CreateVeterinary([FromBody] UserRegisterForm form)
        {
            form.UserRole = Role.Veterinary;
            return Ok(_userService.Create(form));
        }

        [HttpPost("AddOwner")]
        public IActionResult CreateOwner([FromBody] OwnerRegisterForm form)
        {
            form.UserRole = Role.Owner;
            return Ok(_userService.Create(form));
        }

        [HttpPost("AddAddress")]
        public IActionResult CreateAddress([FromBody] AddressForm form)
        {
            return Ok(_userService.Create(form));
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
