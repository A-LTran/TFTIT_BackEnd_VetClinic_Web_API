using Microsoft.Data.SqlClient;

namespace DAL.Services
{
    public class UserService_DAL : IUserRepository_DAL
    {
        private readonly string _connectionString = @"Data Source=GOS-VDI1708\TFTIC;Initial Catalog=TFTIC_VeterinaryClinic;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public bool Create(User user)
        {
            using SqlConnection connection = new(_connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO ClinicPerson (UserId, FirstName, LastName, Email, Phone, Mobile, BirthDate, UserRole, AddressId) VALUES (@userId, @firstName, @lastName, @email, @phone, @mobile,@birthdate, @password, @userRole, @addressId); " +
                    "INSERT INTO ClinicUser (UserPassword, UserId) VALUES (@password, @userId);";

                command.Parameters.AddWithValue("@userId", user.UserId);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@phone", user.Phone);
                command.Parameters.AddWithValue("@mobile", user.Mobile);
                command.Parameters.AddWithValue("@birthdate", user.BirthDate);
                command.Parameters.AddWithValue("@password", user.UserPassword);
                command.Parameters.AddWithValue("@userRole", (int)user.UserRole);
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
                command.CommandText = "INSERT INTO ClinicPerson (UserId, FirstName, LastName, Email, Phone, Mobile, BirthDate, UserRole, AddressId) VALUES (@userId, @firstName, @lastName, @email, @phone, @mobile,@birthdate, @userRole, @addressId); " +
                    "INSERT INTO ClinicUser (UserPassword, UserId) VALUES (@password, @userId);";

                command.Parameters.AddWithValue("@userId", owner.UserId);
                command.Parameters.AddWithValue("@firstName", owner.FirstName);
                command.Parameters.AddWithValue("@lastName", owner.LastName);
                command.Parameters.AddWithValue("@email", owner.Email);
                command.Parameters.AddWithValue("@phone", owner.Phone);
                command.Parameters.AddWithValue("@mobile", owner.Mobile);
                command.Parameters.AddWithValue("@birthdate", owner.BirthDate);
                command.Parameters.AddWithValue("@userRole", (int)owner.UserRole);
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
                    command.CommandText = "DELETE FROM [ClinicPerson] OUTPUT deleted.UserId WHERE UserId = @userId";
                    command.Parameters.AddWithValue("@userId", user.UserId);

                    connection.Open();

                    if (Guid.TryParse(command.ExecuteScalar().ToString(), out Guid userId) && userId == user.UserId)
                    {
                        connection.Close();
                        return true;
                    }
                    connection.Close();
                    return false;
                }
            }
        }

        public IEnumerable<User> Get()
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                List<User?> users = new List<User>();

                // Gets All persons + psswd
                command.CommandText = "EXECUTE GetActivePersons";
                //command.CommandText = "SELECT * " +
                //    "FROM [ClinicUser] CU JOIN [ClinicPerson] CP " +
                //    "ON CU.UserId = CP.UserId";

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string? phone = ToolSet.ReturnNonDBNull<string>(reader["Phone"]);
                        string? mobile = ToolSet.ReturnNonDBNull<string>(reader["Mobile"]);
                        DateTime birthDate = ToolSet.ReturnNonDBNull<DateTime>(reader["BirthDate"]);
                        Guid addressId = ToolSet.ReturnNonDBNull<Guid>(reader["AddressId"]);

                        users.Add(UserMapper.ToUser((Guid)reader["UserId"], (string)reader["LastName"], (string)reader["FirstName"], (string)reader["Email"], phone, mobile, birthDate, (string)reader["UserPassword"], addressId));
                    }
                }
                connection.Close();
                return users;
            }
        }

        public IEnumerable<Person> GetUsersByRole(Role personRole)
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
                            owners.Add(UserMapper.ToOwner((Guid)reader["UserId"], (string)reader["LastName"], (string)reader["FirstName"], (string)reader["Email"], phone, mobile, birthDate, addressId));
                        else
                            users.Add(UserMapper.ToUser((Guid)reader["UserId"], (string)reader["LastName"], (string)reader["FirstName"], (string)reader["Email"], phone, mobile, birthDate, (string)reader["UserPassword"], addressId));
                    }

                connection.Close();
                return personRole == Role.Owner ? owners : users;
            }
        }

        public Person? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Person? GetByMail(string mail)
        {
            throw new NotImplementedException();
        }

        public bool Update(Person user)
        {
            throw new NotImplementedException();
        }
    }
}
