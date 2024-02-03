//namespace DAL.Tools
//{
//    public class AnimalRequester
//    {
//        private string _connectionString;
//        public AnimalRequester(string connectionString)
//        {
//            _connectionString = connectionString;
//        }
//        public IEnumerable<Animal> GetBy<TBody>(string query, string bodyName, TBody body)
//        {
//            using (SqlConnection connection = new SqlConnection(_connectionString))
//            using (SqlCommand command = connection.CreateCommand())
//            {
//                command.CommandText = query;

//                command.Parameters.AddWithValue("@" + bodyName, body);

//                List<Animal> animals = new();
//                connection.Open();
//                using (SqlDataReader reader = command.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        animals.Add(AnimalMapper.ToAnimal(
//                            (Guid)reader["AnimalId"],
//                            (string)reader["AnimalName"],
//                            (string)reader["Breed"],
//                            (DateTime)reader["BirthDate"],
//                            (Guid)reader["OwnerId"]));
//                    }
//                }
//                connection.Close();
//                return animals;
//            }
//        }
//    }
//}
