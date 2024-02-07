namespace BLL.Mappers
{
    public static class AnimalMapper
    {
        public static Animal ToAnimal(this AnimalRegisterForm form)
        {
            return new Animal(form.AnimalName, form.Breed, form.BirthDate, form.OwnerId);
        }

        public static Animal ToAnimal(this AnimalEditForm form, Guid animalId)
        {
            return new Animal(animalId, form.AnimalName, form.Breed, form.BirthDate, form.OwnerId == null ? default : (Guid)form.OwnerId);
        }
    }
}
