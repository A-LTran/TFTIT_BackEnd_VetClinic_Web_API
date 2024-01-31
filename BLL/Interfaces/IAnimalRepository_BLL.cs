namespace BLL.Interfaces
{
    public interface IAnimalRepository_BLL
    {
        public bool Create(AnimalRegisterForm form);
        public IEnumerable<Animal> Get();
        public IEnumerable<Animal> GetByOwner(Guid ownerId);
        public Animal GetAnimal(Guid animalId);
    }
}
