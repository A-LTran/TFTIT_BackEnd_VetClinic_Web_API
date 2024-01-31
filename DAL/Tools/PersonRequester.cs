namespace DAL.Tools
{
    public class PersonRequester
    {
        private string _connectionString;
        public PersonRequester(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User? GetUserBy<TBody>(string sqlCommandText, string bodyName, TBody body)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = sqlCommandText;
                command.Parameters.AddWithValue(bodyName, body);

                User? u = new();

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string? phone = ToolSet.ReturnNonDBNull<string>(reader["Phone"]);
                        string? mobile = ToolSet.ReturnNonDBNull<string>(reader["Mobile"]);
                        DateTime birthDate = ToolSet.ReturnNonDBNull<DateTime>(reader["BirthDate"]);
                        Guid addressId = ToolSet.ReturnNonDBNull<Guid>(reader["AddressId"]);
                        Role personRole = ToolSet.ReturnNonDBNull<Role>(reader["PersonRole"]);

                        u = (UserMapper.ToUser((Guid)reader["PersonId"], (string)reader["LastName"], (string)reader["FirstName"], (string)reader["Email"], phone, mobile, birthDate, (string)reader["UserPassword"], personRole, addressId)!);
                    }
                }
                connection.Close();
                return u;
            }
        }

        public IEnumerable<TResult?> GetUsersBy<TResult, TBody>(string query, string param, TBody body)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                command.Parameters.AddWithValue(param, body);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<TResult?> users = new List<TResult?>();
                    while (reader.Read())
                    {
                        string? phone = ToolSet.ReturnNonDBNull<string>(reader["Phone"]);
                        string? mobile = ToolSet.ReturnNonDBNull<string>(reader["Mobile"]);
                        DateTime birthDate = ToolSet.ReturnNonDBNull<DateTime>(reader["BirthDate"]);
                        Guid addressId = ToolSet.ReturnNonDBNull<Guid>(reader["AddressId"]);
                        Role personRole = ToolSet.ReturnNonDBNull<Role>(reader["PersonRole"]);

                        if (typeof(TResult) == typeof(User))
                        {
                            User? user = UserMapper.ToUser((Guid)reader["PersonId"], (string)reader["LastName"], (string)reader["FirstName"], (string)reader["Email"], phone, mobile, birthDate, (string)reader["UserPassword"], personRole, addressId);
                            yield return (TResult?)(object)user;
                        }
                        if (typeof(TResult) == typeof(Owner))
                        {
                            Owner? owner = UserMapper.ToOwner((Guid)reader["PersonId"], (string)reader["LastName"], (string)reader["FirstName"], (string)reader["Email"], phone, mobile, birthDate, personRole, addressId);
                            yield return (TResult?)(object)owner;
                        }

                    }
                }
            }
        }
        public TResult Create<TResult, TBody>(string query, TBody body)
        {
            using SqlConnection connection = new(_connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                Type bodyType = typeof(TBody);
                foreach (var prop in bodyType.GetProperties())
                {
                    // Get the name and value of the property
                    string propName = prop.Name;
                    object propValue = prop.GetValue(body);
                    command.Parameters.AddWithValue("@" + propName, propValue);
                }

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return (TResult)(object)(rowsAffected > 0);

            }
        }
    }
}
