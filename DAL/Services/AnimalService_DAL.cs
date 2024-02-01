namespace DAL.Services
{
    public class AnimalService_DAL : IAnimalRepository_DAL
    {
        private readonly string _connectionString;
        private readonly AnimalRequester _requester;
        public AnimalService_DAL(string connectionString)
        {
            _connectionString = connectionString;
            _requester = new AnimalRequester(connectionString);
        }

        //*****************************************************************//
        //                              POST                               //
        //*****************************************************************//

        public bool Create(Animal animal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO ClinicAnimal (AnimalId, AnimalName, Breed, OwnerId) " +
                                        "VALUES (@animalId, @animalName, @breed, @ownerId);";
                command.Parameters.AddWithValue("@animalId", animal.AnimalId);
                command.Parameters.AddWithValue("@animalName", animal.AnimalName);
                command.Parameters.AddWithValue("@breed", animal.Breed);
                command.Parameters.AddWithValue("@ownerId", animal.OwnerId);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        //*****************************************************************//
        //                              GET                                //
        //*****************************************************************//

        public IEnumerable<Animal> Get()
        {
            return _requester.GetBy<string>("SELECT * FROM ClinicAnimal", "", "");
        }

        public IEnumerable<Animal> GetByOwner(Guid ownerId)
        {
            return _requester.GetBy<Guid>("SELECT * FROM ClinicAnimal WHERE OwnerId = @ownerId;", "ownerId", ownerId);
        }

        public Animal? GetAnimal(Guid animalId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM ClinicAnimal WHERE AnimalId = @animalId";
                command.Parameters.AddWithValue("@animalId", animalId);

                Animal animal = new Animal();

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        animal = AnimalMapper.ToAnimal(
                            (Guid)reader["AnimalId"],
                            (string)reader["AnimalName"],
                            (string)reader["Breed"],
                            (DateTime)reader["Age"],
                            (Guid)reader["OwnerId"]);
                        connection.Close();
                        return animal;
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }
                }
            }
        }

        //*****************************************************************//
        //                              PATCH                              //
        //*****************************************************************//

        public bool Update(Animal animal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE ClinicAnimal " +
                                        "SET AnimalName = @animalName " +
                                        "Breed = @breed, " +
                                        "BirthDate = @birthDate, " +
                                        "OwnerId = @ownerId " +
                                        "WHERE AnimalId = @id";
                command.Parameters.AddWithValue("@id", animal.AnimalId);
                command.Parameters.AddWithValue("@animalName", animal.AnimalName);
                command.Parameters.AddWithValue("@breed", animal.Breed);
                command.Parameters.AddWithValue("@birthDate", animal.BirthDate);
                command.Parameters.AddWithValue("@ownerId", animal.OwnerId);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        //*****************************************************************//
        //                             DELETE                              //
        //*****************************************************************//

        public bool Delete(Guid animalId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM ClinicAnimal WHERE AnimalId = @id";
                command.Parameters.AddWithValue("@id", animalId);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }
    }
}
