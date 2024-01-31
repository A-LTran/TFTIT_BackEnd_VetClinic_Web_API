
namespace BLL.Services
{
    public class AnimalService_BLL : IAnimalRepository_BLL
    {
        private readonly IAnimalRepository_DAL _animalRepository;

        public AnimalService_BLL(IAnimalRepository_DAL animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public bool Create(AnimalRegisterForm form)
        {
            return _animalRepository.Create(form.ToAnimal());
        }

        public IEnumerable<Animal> Get()
        {
            return _animalRepository.Get();
        }

        public IEnumerable<Animal> GetByOwner(Guid ownerId)
        {
            return _animalRepository.GetByOwner(ownerId);
        }

        public Animal GetAnimal(Guid animalId)
        {
            return _animalRepository.GetAnimal(animalId);
        }
    }
}
