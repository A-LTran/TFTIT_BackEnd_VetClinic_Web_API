﻿namespace DAL.Services
{
    public class UserService_DAL : IUserRepository_DAL
    {
        private readonly string _connectionString;
        private readonly PersonRequester _requester;

        public UserService_DAL(string connectionString)
        {
            _connectionString = connectionString;
            _requester = new PersonRequester(connectionString);
        }

        //*****************************************************************************//
        //                                    POST                                     //
        //*****************************************************************************//

        public bool Create(User user)
        {
            return _requester.Create<bool, User>("INSERT INTO ClinicPerson (" +
                                                                            "PersonId, " +
                                                                            "FirstName, " +
                                                                            "LastName, " +
                                                                            "Email, " +
                                                                            "Phone, " +
                                                                            "Mobile, " +
                                                                            "BirthDate, " +
                                                                            "PersonRole, " +
                                                                            "AddressId) " +
                                                "VALUES (@personId, " +
                                                        "@firstName, " +
                                                        "@lastName, " +
                                                        "@email, " +
                                                        "@phone, " +
                                                        "@mobile, " +
                                                        "@birthdate, " +
                                                        "@personRole, " +
                                                        "@addressId); " +
                                                "INSERT INTO ClinicUser (UserPassword, PersonId) " +
                                                "VALUES (@userPassword, @personId);", user);
        }

        public bool Create(Owner owner)
        {
            return _requester.Create<bool, Owner>("INSERT INTO ClinicPerson (PersonId, " +
                                                                            "FirstName, " +
                                                                            "LastName, " +
                                                                            "Email, " +
                                                                            "Phone, " +
                                                                            "Mobile, " +
                                                                            "BirthDate, " +
                                                                            "PersonRole, " +
                                                                            "AddressId) " +
                                                        "VALUES (@personId, " +
                                                                "@firstName, " +
                                                                "@lastName, " +
                                                                "@email, " +
                                                                "@phone, " +
                                                                "@mobile, " +
                                                                "@birthdate, " +
                                                                "@personRole, " +
                                                                "@addressId); ", owner);
        }

        public bool Create(Address address)
        {
            return _requester.Create<bool, Address>("INSERT INTO PersonAddress (AddressId, " +
                                                                                "Address1, " +
                                                                                "Address2, " +
                                                                                "City, " +
                                                                                "Country, " +
                                                                                "PostalCode) " +
                                                    "VALUES (@addressId, " +
                                                            "@address1, " +
                                                            "@address2, " +
                                                            "@city, " +
                                                            "@country, " +
                                                            "@postalCode);", address);
        }

        //*****************************************************************************//
        //                                     GET                                     //
        //*****************************************************************************//

        public IEnumerable<User?> Get()
        {
            return _requester.GetTResultBy<User, Guid>("EXECUTE GetActivePersons", "@personId", Guid.Empty);
        }

        public IEnumerable<Person?> GetPersonsByRole(Role personRole)
        {
            switch (personRole)
            {
                case Role.Owner:
                    return _requester.GetTResultBy<Owner, string>("EXECUTE GetActiveOwners", "", "");
                case Role.Veterinary:
                    return _requester.GetTResultBy<User, string>("EXECUTE GetActiveVeterinarians", "", "");
                case Role.Administrator:
                    return _requester.GetTResultBy<User, string>("EXECUTE GetActiveAdmins", "", "");
                default:
                    return null;
            }
        }

        public IEnumerable<Address?> GetAddresses()
        {
            return _requester.GetTResultBy<Address, string>("SELECT * FROM PersonAddress", "", "");
        }

        public User? GetUserById(Guid id)
        {
            return _requester.GetBy<User, Guid>("SELECT * FROM ClinicPerson CP JOIN ClinicUser CU " +
                                                        "ON CP.PersonId = CU.PersonId " +
                                                        "WHERE PersonId = @id", "@id", id);
        }

        public User? GetUserByMail(string mail)
        {
            return _requester.GetBy<User, string>("SELECT * FROM ClinicPerson CP JOIN ClinicUser CU " +
                                                        "ON CP.PersonId = CU.PersonId " +
                                                        "WHERE Email = @mail", "@mail", mail);
        }

        public Owner? GetOwnerById(Guid ownerId)
        {
            return _requester.GetBy<Owner, Guid>("SELECT * FROM ClinicPerson WHERE OwnerId = @ownerId", "@ownerId", ownerId);
        }

        public Owner? GetOwnerByMail(string mail)
        {
            return _requester.GetBy<Owner, string>("SELECT * FROM ClinicPerson WHERE Email = @mail", "@mail", mail);
        }

        //*****************************************************************************//
        //                                    PATCH                                    //
        //*****************************************************************************//

        public bool Update(Owner owner)
        {
            string query = "UPDATE ClinicPerson " +
                                    "SET FirstName = @firstName, " +
                                        "LastName = @lastName, " +
                                        "Email = @email, " +
                                        "Phone = @phone, " +
                                        "Mobile = @mobile, " +
                                        "BirthDate = @birthdate, " +
                                        "PersonRole = @personRole, " +
                                        "AddressId = @addressId " +
                                        "WHERE OwnerId = @ownerId";

            return _requester.Update<bool, Owner>(query, owner);
        }

        public bool Update(User user)
        {
            string query = "UPDATE ClinicPerson " +
                                    "SET FirstName = @firstName, " +
                                        "LastName = @lastName, " +
                                        "Email = @email, " +
                                        "Phone = @phone, " +
                                        "Mobile = @mobile, " +
                                        "BirthDate = @birthdate, " +
                                        "UserPassword = @userPassword, " +
                                        "PersonRole = @personRole, " +
                                        "AddressId = @addressId " +
                                        "WHERE PersonId = @personId";

            return _requester.Update<bool, User>(query, user);
        }

        //*****************************************************************************//
        //                                    DELETE                                   //
        //*****************************************************************************//

        public bool DeleteUser(Guid personId)
        {
            return _requester.Delete<Guid>("DELETE FROM [ClinicUser] WHERE PersonId = @personId; " +
                                "DELETE FROM [ClinicPerson] " +
                                "WHERE PersonId = @personId; ", "personId", personId);
        }

        public bool DeleteOwner(Guid ownerId)
        {
            return _requester.Delete<Guid>("DELETE FROM [ClinicPerson] WHERE OwnerId = @ownerId; ", "ownerId", ownerId);
        }
    }
}
