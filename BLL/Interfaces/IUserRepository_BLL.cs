﻿using DAL.Entities.Enumerations;

namespace BLL.Interfaces
{
    public interface IUserRepository_BLL
    {
        public bool Create(UserRegisterForm form, Guid addressId);
        public bool Create(OwnerRegisterForm form, Guid addressId);
        public bool Create(AddressRegisterForm form);
        public bool UpdateUser(UserEditForm form, Guid userId, Role role);
        public bool UpdateOwner(OwnerEditForm form, Guid ownerId);
        public bool UpdateAddress(AddressEditForm form, Guid addressId);
        public bool DeleteUser(Guid userId);
        public bool DeleteOwner(Guid ownerId);
        public bool DeleteAddress(Guid addressId);
        public IEnumerable<UserDto?> Get();
        public IEnumerable<UserDto> GetPersonsByRole(int role);
        public UserDto? GetUserById(Guid userId);
        public UserDto? GetUserByMail(string mail);
        public UserDto? GetOwnerById(Guid ownerId);
        public UserDto? GetOwnerByMail(string mail);
        public UserTokenDto? GetUserDtoByMail(string mail);
        public bool PersonExistsCheckById(Guid id);
        public bool PersonExistsCheckByMail(string mail);
        public bool AddressExistsCheckById(Guid id);
        public bool AddressExistsCheckByAddressInfo(AddressRegisterForm form);
        public string? GetUserPasswordByMail(string mail);
        public IEnumerable<Address> GetAddresses();
        public Address? GetAddressByPersonId(Guid personId);
        public bool VerifyPassword(UserLoginForm form);
    }
}
