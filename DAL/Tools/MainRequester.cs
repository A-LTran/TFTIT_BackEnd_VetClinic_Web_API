using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace DAL.Tools
{
    public class MainRequester
    {
        private readonly string _connectionString;
        public MainRequester(string connectionString)
        {
            _connectionString = connectionString;
        }

        //******************************************************************//
        //                              CREATE                              //
        //******************************************************************//

        public bool Create<TBody>(string query, TBody body)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                Type bodyType = typeof(TBody);
                foreach (var prop in bodyType.GetProperties())
                {
                    //Console.WriteLine($"@{prop.Name} : {prop.GetValue(body)}");
                    if (prop.Name == "PersonRole")
                        command.Parameters.AddWithValue("@" + prop.Name, (int)prop.GetValue(body));
                    else
                        command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(body));

                }

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        //******************************************************************//
        //                               GET                                //
        //******************************************************************//

        public TResult? GetTResult<TResult, TBody>(string query, string bodyName, TBody body) where TResult : class
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.AddWithValue("@" + bodyName, body);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Read<TResult>(reader);
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }
                }
            }
        }

        public IEnumerable<TResult?> GetEnumTResult<TResult, TBody>(string query, string bodyName, TBody body) where TResult : class
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.AddWithValue("@" + bodyName, body);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return Read<TResult>(reader);
                    }
                }
                connection.Close();
            }
        }

        private static readonly Dictionary<Type, PropertyInfo[]> propertyCache = new();

        private static TResult Read<TResult>(SqlDataReader reader)
        {
            Type type = typeof(TResult);
            TResult result = (TResult)Activator.CreateInstance(type);

            // Get the properties of the type once and cache them
            if (!propertyCache.ContainsKey(type))
            {
                propertyCache[type] = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }

            // Loop through the properties and set their values
            foreach (var prop in propertyCache[type])
            {
                // Check if the reader has a column with the same name as the property
                if (reader.GetOrdinal(prop.Name) >= 0)
                {
                    // Get the value from the reader and convert it to the property type
                    object value = reader[prop.Name];
                    if (value != DBNull.Value)
                    {
                        value = (prop.Name == "PersonRole") ? Enum.Parse(prop.PropertyType, value.ToString()) : Convert.ChangeType(value, prop.PropertyType);
                    }
                    else
                    {
                        value = default;
                    }

                    // Get the setter method of the property
                    var setter = prop.GetSetMethod(true);
                    // Check if the setter is null or non-public
                    if (!(setter == null || !setter.IsPublic))
                    {
                        // Set the value to the property
                        prop.SetValue(result, value);
                    }
                }
            }
            return result;
        }

        //******************************************************************//
        //                              UPDATE                              //
        //******************************************************************//

        public bool Update<TBody>(string query, TBody body)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                Type type = typeof(TBody);
                foreach (var prop in type.GetProperties())
                {
                    if (!(prop.GetValue(body) == null || prop.GetValue(body).ToString().IsNullOrEmpty()))
                    {
                        //Console.WriteLine($"@{prop.Name} : {prop.GetValue(body)}");
                        if (prop.Name == "PersonRole")
                            command.Parameters.AddWithValue("@" + prop.Name, (int)prop.GetValue(body));
                        else
                            command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(body));
                    }
                }

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        //******************************************************************//
        //                              DELETE                              //
        //******************************************************************//

        public bool Delete<TBody>(string query, string bodyName, TBody body)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
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
