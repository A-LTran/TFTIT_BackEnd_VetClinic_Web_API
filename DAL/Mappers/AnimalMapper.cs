namespace DAL.Mappers
{
    public static class AnimalMapper
    {
        public static Animal ToAnimal(this Guid id, string animalName, string breed, DateTime birthDate, Guid ownerId)
        {
            return new Animal(id, animalName, breed, birthDate, ownerId);

        }
    }
}
