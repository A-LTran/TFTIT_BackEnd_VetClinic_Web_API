namespace DAL.Services
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

        public bool Create(User user)
        {
            return _requester.Create<bool, User>("INSERT INTO ClinicPerson (PersonId, FirstName, LastName, Email, Phone, Mobile, BirthDate, PersonRole, AddressId) VALUES (@personId, @firstName, @lastName, @email, @phone, @mobile, @birthdate, @personRole, @addressId); " +
                    "INSERT INTO ClinicUser (UserPassword, PersonId) VALUES (@userPassword, @personId);", user);
        }

        public bool Create(Owner owner)
        {
            return _requester.Create<bool, Owner>("INSERT INTO ClinicPerson (PersonId, FirstName, LastName, Email, Phone, Mobile, BirthDate, PersonRole, AddressId) VALUES (@personId, @firstName, @lastName, @email, @phone, @mobile, @birthdate, @personRole, @addressId); ", owner);
        }

        public bool Create(Address address)
        {
            return _requester.Create<bool, Address>("INSERT INTO PersonAddress (AddressId, Address1, Address2, City, Country, PostalCode) " +
                    "VALUES (@addressId, @address1, @address2, @city, @country, @postalCode);", address);
        }

        public bool Delete(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [ClinicPerson] OUTPUT deleted.personId WHERE PersonId = @personId";
                    command.Parameters.AddWithValue("@personId", user.PersonId);
                    connection.Open();

                    if (Guid.TryParse(command.ExecuteScalar().ToString(), out Guid personId) && personId == user.PersonId)
                    {
                        connection.Close();
                        return true;
                    }
                    connection.Close();
                    return false;
                }
            }
        }

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

        public User? GetById(Guid id)
        {
            return _requester.GetUserBy<Guid>("SELECT * FROM ClinicPerson CP JOIN ClinicUser CU " +
                                                        "ON CP.PersonId = CU.PersonId " +
                                                        "WHERE PersonId = @id", "@id", id);
        }

        public User? GetByMail(string mail)
        {
            return _requester.GetUserBy<string>("SELECT * FROM ClinicPerson CP JOIN ClinicUser CU " +
                                                        "ON CP.PersonId = CU.PersonId " +
                                                        "WHERE Email = @mail", "@mail", mail);
        }


        public bool Update(Person user)
        {
            throw new NotImplementedException();
        }
    }
}
