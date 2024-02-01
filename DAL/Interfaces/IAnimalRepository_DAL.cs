namespace DAL.Interfaces
{
    public interface IAnimalRepository_DAL
    {
        public bool Create(Animal animal);
        public IEnumerable<Animal> Get();
        public IEnumerable<Animal> GetByOwner(Guid ownerId);
        public Animal GetAnimal(Guid animalId);
        public bool Update(Animal animal);
        public bool Delete(Guid animalId);
    }
}
