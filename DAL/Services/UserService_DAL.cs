namespace DAL.Services
{
    public class UserService_DAL : IUserRepository_DAL
    {
        private readonly string _connectionString;
        private readonly Requester _requester;

        public UserService_DAL(string connectionString)
        {
            _connectionString = connectionString;
            _requester = new Requester(connectionString);
        }

        public bool Create(User user)
        {
            using SqlConnection connection = new(_connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO ClinicPerson (PersonId, FirstName, LastName, Email, Phone, Mobile, BirthDate, PersonRole, AddressId) VALUES (@personId, @firstName, @lastName, @email, @phone, @mobile, @birthdate, @personRole, @addressId); " +
                    "INSERT INTO ClinicUser (UserPassword, PersonId) VALUES (@password, @personId);";

                command.Parameters.AddWithValue("@personId", user.PersonId);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@phone", user.Phone);
                command.Parameters.AddWithValue("@mobile", user.Mobile);
                command.Parameters.AddWithValue("@birthdate", user.BirthDate);
                command.Parameters.AddWithValue("@password", user.UserPassword);
                command.Parameters.AddWithValue("@personRole", (int)user.PersonRole);
                command.Parameters.AddWithValue("@addressId", user.AddressId);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0;
            }
        }

        public bool Create(Owner owner)
        {
            using SqlConnection connection = new(_connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO ClinicPerson (personId, FirstName, LastName, Email, Phone, Mobile, BirthDate, PersonRole, AddressId) VALUES (@personId, @firstName, @lastName, @email, @phone, @mobile, @birthdate, @personRole, @addressId); ";

                command.Parameters.AddWithValue("@personId", owner.PersonId);
                command.Parameters.AddWithValue("@firstName", owner.FirstName);
                command.Parameters.AddWithValue("@lastName", owner.LastName);
                command.Parameters.AddWithValue("@email", owner.Email);
                command.Parameters.AddWithValue("@phone", owner.Phone);
                command.Parameters.AddWithValue("@mobile", owner.Mobile);
                command.Parameters.AddWithValue("@birthdate", owner.BirthDate);
                command.Parameters.AddWithValue("@personRole", (int)owner.PersonRole);
                command.Parameters.AddWithValue("@addressId", owner.AddressId);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0;
            }
        }

        public bool Create(Address address)
        {
            using SqlConnection connection = new(_connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO PersonAddress (AddressId, Address1, Address2, City, Country, PostalCode) " +
                    "VALUES (@addressId, @address1, @address2, @city, @country, @postalCode);";

                command.Parameters.AddWithValue("@addressId", address.AddressId);
                command.Parameters.AddWithValue("@address1", address.Address1);
                command.Parameters.AddWithValue("@address2", address.Address2);
                command.Parameters.AddWithValue("@city", address.City);
                command.Parameters.AddWithValue("@country", address.Country);
                command.Parameters.AddWithValue("@postalCode", address.PostalCode);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0;
            }
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
            using SqlConnection connection = new SqlConnection(_connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                List<User?> users = new List<User?>();

                // Gets All active persons + psswd
                command.CommandText = "EXECUTE GetActivePersons";

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string? phone = ToolSet.ReturnNonDBNull<string>(reader["Phone"]);
                        string? mobile = ToolSet.ReturnNonDBNull<string>(reader["Mobile"]);
                        DateTime birthDate = ToolSet.ReturnNonDBNull<DateTime>(reader["BirthDate"]);
                        Guid addressId = ToolSet.ReturnNonDBNull<Guid>(reader["AddressId"]);
                        Role personRole = ToolSet.ReturnNonDBNull<Role>(reader["PersonRole"]);

                        users.Add(UserMapper.ToUser((Guid)reader["PersonId"], (string)reader["LastName"], (string)reader["FirstName"], (string)reader["Email"], phone, mobile, birthDate, (string)reader["UserPassword"], personRole, addressId));
                    }
                }
                connection.Close();
                return users;
            }
        }

        public IEnumerable<Person?> GetPersonsByRole(Role personRole)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                List<Owner> owners = new List<Owner>();
                List<User> users = new List<User>();
                switch (personRole)
                {
                    case Role.Owner:
                        command.CommandText = "EXECUTE GetActiveOwners";
                        break;
                    case Role.Veterinary:
                        command.CommandText = "EXECUTE GetActiveVeterinarians";
                        break;
                    case Role.Administrator:
                        command.CommandText = "EXECUTE GetActiveAdmins";
                        break;
                }

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        string? phone = ToolSet.ReturnNonDBNull<string>(reader["Phone"]);
                        string? mobile = ToolSet.ReturnNonDBNull<string>(reader["Mobile"]);
                        DateTime birthDate = ToolSet.ReturnNonDBNull<DateTime>(reader["BirthDate"]);
                        Guid addressId = ToolSet.ReturnNonDBNull<Guid>(reader["AddressId"]);
                        if (personRole == Role.Owner)
                        {
                            owners.Add(UserMapper.ToOwner(
                                (Guid)reader["PersonId"],
                                (string)reader["LastName"],
                                (string)reader["FirstName"],
                                (string)reader["Email"],
                                phone,
                                mobile,
                                birthDate,
                                personRole,
                                addressId)!);
                        }
                        else
                            users.Add(UserMapper.ToUser((Guid)reader["PersonId"], (string)reader["LastName"], (string)reader["FirstName"], (string)reader["Email"], phone, mobile, birthDate, (string)reader["UserPassword"], personRole, addressId)!);
                    }

                connection.Close();
                return personRole == Role.Owner ? owners : users;
            }
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
