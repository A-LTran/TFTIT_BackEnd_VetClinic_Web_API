namespace BLL.Services
{
    public class AnimalService_BLL : IAnimalRepository_BLL
    {
        private readonly IAnimalRepository_DAL _animalService;
        private readonly ToolSet _toolSet;

        //public string Message { get; set; } = default!;
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        private void LogMessage(string message)
        {
            Message = message;
        }

        public string GetMessage()
        {
            return Message;
        }
        public AnimalService_BLL(IAnimalRepository_DAL animalRepository)
        {
            _animalService = animalRepository;
            _toolSet = new(LogMessage);
        }

        public bool Create(AnimalRegisterForm form)
        {
            if (!_toolSet.ObjectExistsCheck(!form.Equals(new AnimalRegisterForm()), "Animal"))
                return false;

            if (!_toolSet.SuccessCheck(_animalService.Create(form.ToAnimal()), "Animal", "created"))
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
            if (!_toolSet.ObjectExistsCheck(a is not null, "Animal"))
                return null;

            return a;
        }

        public bool Update(AnimalEditForm form, Guid animalId)
        {
            Animal? currentAnimal = _animalService.GetAnimal(animalId);
            if (!_toolSet.ObjectExistsCheck(currentAnimal is not null, "Animal"))
                return false;

            Animal? newAnimal = form.ToAnimal(animalId);

            Type type = typeof(Animal);
            foreach (var prop in type.GetProperties())
            {
                if (!(prop.GetValue(newAnimal) is null || prop.GetValue(newAnimal) == default))
                {
                    prop.SetValue(currentAnimal, prop.GetValue(newAnimal));
                }
            }

            if (!_toolSet.SuccessCheck(_animalService.Update(currentAnimal), "Animal", "updated"))
                return false;

            return true;
        }

        public bool Delete(Guid animalId)
        {
            if (!_toolSet.ObjectExistsCheck(_animalService.GetAnimal(animalId) is not null, "Animal"))
                return false;

            if (!_toolSet.SuccessCheck(_animalService.Delete(animalId), "Animal", "deleted"))
                return false;

            return true;
        }
    }
}
