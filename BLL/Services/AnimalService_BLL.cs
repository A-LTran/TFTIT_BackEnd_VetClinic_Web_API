﻿namespace BLL.Services
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
            if (!ToolSet.ObjectExistsCheck(!form.Equals(new AnimalRegisterForm()), "Animal"))
                return false;

            if (!ToolSet.SucceessCheck(_animalService.Create(form.ToAnimal()), "Animal", "created"))
                return false;
            return true;
        }

        public IEnumerable<Animal?> Get()
        {
            return _animalService.Get();
        }

        public IEnumerable<Animal?> GetByOwner(Guid ownerId)
        {
            return _animalService.GetByOwner(ownerId);
        }

        public Animal? GetAnimal(Guid animalId)
        {
            Animal? a = _animalService.GetAnimal(animalId);
            if (!ToolSet.ObjectExistsCheck(a is not null, "Animal"))
                return null;

            return a;
        }

        public bool Update(AnimalEditForm form, Guid animalId)
        {
            Animal? a = _animalService.GetAnimal(animalId);
            if (!ToolSet.ObjectExistsCheck(_animalService.GetAnimal(animalId) is not null, "Animal"))
                return false;

            if (!ToolSet.SucceessCheck(_animalService.Update(form.ToAnimal(animalId)), "Animal", "updated"))
                return false;

            return true;
        }

        public bool Delete(Guid animalId)
        {
            if (!ToolSet.ObjectExistsCheck(_animalService.GetAnimal(animalId) is not null, "Animal"))
                return false;

            if (!ToolSet.SucceessCheck(_animalService.Delete(animalId), "Animal", "deleted"))
                return false;

            return true;
        }
    }
}
