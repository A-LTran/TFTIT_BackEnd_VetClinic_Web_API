namespace DAL.Mappers
{
    public static class AnimalMapper
    {
        public static Animal ToAnimal(this Guid id, string animalName, string breed, int age, Guid ownerId)
        {
            return new Animal(id, animalName, breed, age, ownerId);

        }
    }
}
