namespace DAL.Interfaces
{
    public interface IAnimalRepository_DAL
    {
        public bool Create(Animal animal);
        public IEnumerable<Animal> Get();
        public Animal GetAnimal(Guid animalId);
    }
}
