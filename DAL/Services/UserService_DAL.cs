using DAL.Interfaces;
using Microsoft.Data.SqlClient;

namespace DAL.Services
{
    public class UserService_DAL : IUserRepository_DAL
    {
        private readonly string _connectionString = @"Data Source=GOS-VDI1708\TFTIC;Initial Catalog=TFTIC_VeterinaryClinic;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        // If value is DBNull, return type default. Else, return type parsed value.
        private static TResult? ReturnNonDBNull<TResult>(Object value)
        {
            return Convert.IsDBNull(value) ? default : (TResult)value;
        }
        public bool Create(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
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
                    command.Parameters.AddWithValue("@userRole", user.UserRole);
                    command.Parameters.AddWithValue("@addressId", user.AddressId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
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
                        string? phone = ReturnNonDBNull<string>(reader["Phone"]);
                        string? mobile = ReturnNonDBNull<string>(reader["Mobile"]);
                        DateTime birthDate = ReturnNonDBNull<DateTime>(reader["BirthDate"]);
                        Guid addressId = ReturnNonDBNull<Guid>(reader["AddressId"]);

                        users.Add(UserMapper.ToUser((Guid)reader["UserId"], (string)reader["LastName"], (string)reader["FirstName"], (string)reader["Email"], phone, mobile, birthDate, (string)reader["UserPassword"], addressId));
                    }
                }
                connection.Close();
                return users;
            }
        }

        public IEnumerable<Person> GetUsersByRole(Role personRole)
        {
            List<Person> persons = new List<Person>();
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
                {
                    while (reader.Read())
                    {
                        string? phone = ReturnNonDBNull<string>(reader["Phone"]);
                        string? mobile = ReturnNonDBNull<string>(reader["Mobile"]);
                        DateTime birthDate = ReturnNonDBNull<DateTime>(reader["BirthDate"]);
                        Guid addressId = ReturnNonDBNull<Guid>(reader["AddressId"]);
                        if (personRole == Role.Owner)
                            owners.Add(UserMapper.ToOwner((Guid)reader["UserId"], (string)reader["LastName"], (string)reader["FirstName"], (string)reader["Email"], phone, mobile, birthDate, addressId));
                        else
                            users.Add(UserMapper.ToUser((Guid)reader["UserId"], (string)reader["LastName"], (string)reader["FirstName"], (string)reader["Email"], phone, mobile, birthDate, (string)reader["UserPassword"], addressId));
                    }
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
