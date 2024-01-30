namespace DAL.Entities
{
    public class Animal
    {
        public Animal()
        {

        }
        // AnimalMapper from form
        public Animal(string animalName, string breed, int age, Guid ownerId)
        {
            AnimalName = animalName;
            Breed = breed;
            Age = age;
            OwnerId = ownerId;
        }
        // DAL Get Animal
        public Animal(Guid animalId, string animalName, string breed, int age, Guid ownerId) : this(animalName, breed, age, ownerId)
        {
            AnimalId = animalId;
        }

        public Guid AnimalId { get; set; } = Guid.NewGuid();
        public string AnimalName { get; set; } = default!;
        public string Breed { get; set; } = default!;
        public int Age { get; set; }
        public Guid OwnerId { get; set; }
    }
}
