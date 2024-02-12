namespace DAL.Services
{
    public class AnimalService_DAL : IAnimalRepository_DAL
    {
        private readonly MainRequester _mainRequester;
        public AnimalService_DAL(string connectionString)
        {
            _mainRequester = new MainRequester(connectionString);
        }

        #region POST
        //*****************************************************************//
        //                              POST                               //
        //*****************************************************************//

        public bool Create(Animal animal)
        {
            return _mainRequester.Create("INSERT INTO ClinicAnimal (AnimalId" +
                                                                    ", AnimalName" +
                                                                    ", BirthDate" +
                                                                    ", Breed" +
                                                                    ", OwnerId) " +
                                            "VALUES (@animalId" +
                                                        ", @animalName" +
                                                        ", @birthDate" +
                                                        ", @breed" +
                                                        ", @ownerId);", animal);
        }
        #endregion

        #region GET
        //*****************************************************************//
        //                              GET                                //
        //*****************************************************************//

        public IEnumerable<Animal?> Get()
        {
            return _mainRequester.GetEnumTResult<Animal, string>("SELECT * FROM ClinicAnimal " +
                                                                "WHERE IsActive = 1 ", "", "");
        }

        public IEnumerable<Animal?> GetByOwner(Guid ownerId)
        {
            return _mainRequester.GetEnumTResult<Animal, Guid>("SELECT * FROM ClinicAnimal " +
                                                                "WHERE IsActive = 1 " +
                                                                "AND OwnerId = @ownerId;", "ownerId", ownerId);
        }

        public Animal? GetAnimal(Guid animalId)
        {
            return _mainRequester.GetTResult<Animal, Guid>("SELECT * FROM ClinicAnimal " +
                                                            "WHERE IsActive = 1 " +
                                                            "AND AnimalId = @animalId", "animalId", animalId);
        }
        #endregion

        #region PATCH
        //*****************************************************************//
        //                              PATCH                              //
        //*****************************************************************//

        public bool Update(Animal animal)
        {
            return _mainRequester.Update<Animal>("UPDATE ClinicAnimal " +
                                        "SET AnimalName = @animalName" +
                                            ", Breed = @breed" +
                                            ", BirthDate = @birthDate" +
                                            ", OwnerId = @ownerId " +
                                        "WHERE AnimalId = @AnimalId", animal);
        }
        #endregion

        #region DELETE
        //*****************************************************************//
        //                             DELETE                              //
        //*****************************************************************//

        public bool Delete(Guid animalId)
        {
            return _mainRequester.Delete("DELETE FROM ClinicAnimal " +
                                            "WHERE AnimalId = @animalId", "animalId", animalId);
        }
        #endregion
    }
}
