namespace DAL.Services
{
    public class UserService_DAL : IUserRepository_DAL
    {
        private readonly MainRequester _mainRequester;

        public UserService_DAL(string connectionString)
        {
            _mainRequester = new MainRequester(connectionString);
        }

        //*****************************************************************************//
        //                                    POST                                     //
        //*****************************************************************************//

        public bool Create(User user)
        {
            return _mainRequester.Create<User>("INSERT INTO ClinicPerson (" +
                                                                            "PersonId" +
                                                                            ", FirstName" +
                                                                            ", LastName" +
                                                                            ", Email" +
                                                                            ", Phone" +
                                                                            ", Mobile" +
                                                                            ", BirthDate" +
                                                                            ", PersonRole" +
                                                                            ", AddressId) " +
                                                "VALUES (@personId" +
                                                        ", @firstName" +
                                                        ", @lastName" +
                                                        ",@email" +
                                                        ", @phone" +
                                                        ", @mobile" +
                                                        ", @birthdate" +
                                                        ", @personRole" +
                                                        ", @addressId); " +
                                                "INSERT INTO ClinicUser (UserPassword, PersonId) " +
                                                "VALUES (@userPassword, @personId);", user);
        }

        public bool Create(Owner owner)
        {
            return _mainRequester.Create<Owner>("INSERT INTO ClinicPerson (PersonId" +
                                                                            ", FirstName" +
                                                                            ", LastName" +
                                                                            ", Email" +
                                                                            ", Phone" +
                                                                            ", Mobile" +
                                                                            ", BirthDate" +
                                                                            ", PersonRole" +
                                                                            ", AddressId) " +
                                                        "VALUES (@personId" +
                                                                ", @firstName" +
                                                                ", @lastName" +
                                                                ", @email" +
                                                                ", @phone" +
                                                                ", @mobile" +
                                                                ", @birthdate" +
                                                                ", @personRole" +
                                                                ", @addressId); ", owner);
        }

        public bool Create(Address address)
        {
            return _mainRequester.Create<Address>("INSERT INTO PersonAddress (AddressId, " +
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
            return _mainRequester.GetEnumTResult<User, Guid>("EXECUTE GetActivePersons", "@personId", Guid.Empty);
        }

        public IEnumerable<Person?> GetPersonsByRole(Role personRole)
        {
            switch (personRole)
            {
                case Role.Owner:
                    return _mainRequester.GetEnumTResult<Owner, string>("EXECUTE GetActiveOwners", "", "");
                case Role.Veterinary:
                    return _mainRequester.GetEnumTResult<User, string>("EXECUTE GetActiveVeterinarians", "", "");
                case Role.Administrator:
                    return _mainRequester.GetEnumTResult<User, string>("EXECUTE GetActiveAdmins", "", "");
                default:
                    return null;
            }
        }

        public IEnumerable<Address?> GetAddresses()
        {
            return _mainRequester.GetEnumTResult<Address, string>("SELECT * FROM PersonAddress", "", "");
        }

        public Address? GetAddressByPersonId(Guid personId)
        {
            return _mainRequester.GetTResult<Address, Guid>("SELECT * FROM PersonAddress " +
                                                                "WHERE AddressId = ( SELECT AddressId " +
                                                                                    "FROM ClinicPerson " +
                                                                                    "WHERE PersonId = @personId) ", "personId", personId);
        }

        public User? GetUserById(Guid id)
        {
            return _mainRequester.GetTResult<User, Guid>("SELECT * FROM ClinicPerson CP JOIN ClinicUser CU " +
                                                        "ON CP.PersonId = CU.PersonId " +
                                                        "WHERE PersonId = @id", "id", id);
        }

        public User? GetUserByMail(string mail)
        {
            return _mainRequester.GetTResult<User, string>("SELECT * FROM ClinicPerson CP JOIN ClinicUser CU " +
                                                            "ON CP.PersonId = CU.PersonId " +
                                                            "WHERE Email = @mail", "mail", mail);
        }

        public Owner? GetOwnerById(Guid ownerId)
        {
            return _mainRequester.GetTResult<Owner, Guid>("SELECT * FROM ClinicPerson " +
                                                            "WHERE IsActive = 1 " +
                                                            "AND PersonId = @personId", "personId", ownerId);
        }

        public Owner? GetOwnerByMail(string mail)
        {
            return _mainRequester.GetTResult<Owner, string>("SELECT * FROM ClinicPerson " +
                                                            "WHERE IsActive = 1 " +
                                                            "AND Email = @mail", "mail", mail);
        }

        //*****************************************************************************//
        //                                    PATCH                                    //
        //*****************************************************************************//

        public bool Update(Owner owner)
        {
            string query = "UPDATE ClinicPerson " +
                            "SET FirstName = @firstName " +
                            ", LastName = @lastName " +
                            ", Email = @email " +
                            ", Phone = @phone " +
                            ", Mobile = @mobile " +
                            ", BirthDate = @birthdate " +
                            ", PersonRole = @personRole " +
                            ", AddressId = @addressId " +
                            "WHERE PersonId = @personId";

            //return _requester.Update<bool, Owner>(query, owner);
            return _mainRequester.Update(query, owner);
        }

        public bool Update(User user)
        {
            string query = "UPDATE ClinicPerson " +
                            "SET FirstName = @firstName" +
                            ", LastName = @lastName" +
                            ", Email = @email" +
                            ", Phone = @phone" +
                            ", Mobile = @mobile" +
                            ", BirthDate = @birthdate" +
                            ", UserPassword = @userPassword" +
                            ", PersonRole = @personRole" +
                            ", AddressId = @addressId " +
                            "WHERE PersonId = @personId";

            return _mainRequester.Update(query, user);

        }

        //*****************************************************************************//
        //                                    DELETE                                   //
        //*****************************************************************************//

        public bool DeleteUser(Guid personId)
        {
            return _mainRequester.Delete<Guid>("DELETE FROM [ClinicUser] " +
                                                "WHERE PersonId = @personId; " +
                                                "DELETE FROM [ClinicPerson] " +
                                                "WHERE PersonId = @personId; ", "personId", personId);
        }

        public bool DeleteOwner(Guid ownerId)
        {
            return _mainRequester.Delete<Guid>("DELETE FROM [ClinicPerson] " +
                                                "WHERE PersonId = @personId; ", "personId", ownerId);
        }
    }
}
