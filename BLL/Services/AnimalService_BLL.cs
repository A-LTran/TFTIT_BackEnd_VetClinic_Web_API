namespace BLL.Services
{
    public class AnimalService_BLL : IAnimalRepository_BLL
    {
        private readonly IAnimalRepository_DAL _animalService;

        public AnimalService_BLL(IAnimalRepository_DAL animalRepository)
        {
            _animalService = animalRepository;
        }

        public bool Create(AnimalRegisterForm form)
        {
            return _animalService.Create(form.ToAnimal());
        }

        public IEnumerable<Animal> Get()
        {
            return _animalService.Get();
        }

        public IEnumerable<Animal> GetByOwner(Guid ownerId)
        {
            return _animalService.GetByOwner(ownerId);
        }

        public Animal GetAnimal(Guid animalId)
        {
            return _animalService.GetAnimal(animalId);
        }

        public bool Update(AnimalEditForm form, Guid animalId)
        {
            return _animalService.Update(form.ToAnimal(animalId));
        }

        public bool Delete(Guid animalId)
        {
            return _animalService.Delete(animalId);
        }
    }
}
