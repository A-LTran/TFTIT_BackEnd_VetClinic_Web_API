namespace BLL.Interfaces
{
    public interface IAnimalRepository_BLL
    {
        public bool Create(AnimalRegisterForm form);
        public IEnumerable<Animal> Get();
        public Animal GetAnimal(Guid animalId);
    }
}
