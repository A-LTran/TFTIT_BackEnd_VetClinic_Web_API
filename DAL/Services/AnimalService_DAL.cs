namespace DAL.Services
{
    public class AnimalService_DAL : IAnimalRepository_DAL
    {
        private readonly string _connectionString;

        public AnimalService_DAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Create(Animal animal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO ClinicAnimal (AnimalId, AnimalName, Breed, OwnerId) VALUES (@animalId, @animalName, @breed, @ownerId);";
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

        public IEnumerable<Animal> Get()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM ClinicAnimal";

                List<Animal> animals = new();

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        animals.Add(AnimalMapper.ToAnimal(
                            (Guid)reader["AnimalId"],
                            (string)reader["AnimalName"],
                            (string)reader["Breed"],
                            (DateTime)reader["BirthDate"],
                            (Guid)reader["OwnerId"]));
                    }
                }
                connection.Close();
                return animals;
            }
        }

        public Animal GetAnimal(Guid animalId)
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
                    }
                }
                connection.Close();
                return animal;
            }
        }
    }
}
