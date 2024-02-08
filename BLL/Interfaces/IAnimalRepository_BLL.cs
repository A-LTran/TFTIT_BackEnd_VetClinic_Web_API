namespace BLL.Interfaces
{
    public interface IAnimalRepository_BLL
    {
        public string GetMessage();
        public bool Create(AnimalRegisterForm form);
        public IEnumerable<Animal?> Get();
        public IEnumerable<Animal?> GetByOwner(Guid ownerId);
        public Animal? GetAnimal(Guid animalId);
        public bool Update(AnimalEditForm form, Guid animalId);
        public bool Delete(Guid animalId);
    }
}
