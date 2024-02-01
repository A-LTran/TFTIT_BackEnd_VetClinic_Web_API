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
                    else
                    {
                        return null;
                    }
                }
                connection.Close();
                return u;
            }
        }

        public IEnumerable<TResult?> GetTResultBy<TResult, TBody>(string query, string param, TBody body)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                command.Parameters.AddWithValue(param, body);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    string? phone = default;
                    string? mobile = default;
                    DateTime birthDate = default;
                    Guid addressId = default;
                    Role personRole = default;

                    while (reader.Read())
                    {
                        if (typeof(TResult) == typeof(User) || typeof(TResult) == typeof(Owner))
                        {
                            phone = ToolSet.ReturnNonDBNull<string>(reader["Phone"]);
                            mobile = ToolSet.ReturnNonDBNull<string>(reader["Mobile"]);
                            birthDate = ToolSet.ReturnNonDBNull<DateTime>(reader["BirthDate"]);
                            addressId = ToolSet.ReturnNonDBNull<Guid>(reader["AddressId"]);
                            personRole = ToolSet.ReturnNonDBNull<Role>(reader["PersonRole"]);
                        }

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
                        if (typeof(TResult) == typeof(Address))
                        {
                            string? address2 = ToolSet.ReturnNonDBNull<string>(reader["Address2"]);
                            Address? address = AddressMapper.ToAddress((Guid)reader["AddressId"], (string)reader["Address1"], address2, (string)reader["City"], (string)reader["Country"], (string)reader["PostalCode"]);
                            yield return (TResult?)(object)address;
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
        public TResult Update<TResult, TBody>(string query, TBody body)
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

        public bool Delete<TBody>(string query, string bodyName, TBody body)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    command.Parameters.AddWithValue("@" + bodyName, body);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected > 0;
                }
            }
        }
    }

}
