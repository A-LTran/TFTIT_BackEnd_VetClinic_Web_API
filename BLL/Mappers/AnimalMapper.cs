namespace BLL.Mappers
{
    public static class AnimalMapper
    {
        public static Animal ToAnimal(this AnimalRegisterForm form)
        {
            return new Animal(form.AnimalName, form.Breed, form.BirthDate, form.OwnerId);
        }
    }
}
