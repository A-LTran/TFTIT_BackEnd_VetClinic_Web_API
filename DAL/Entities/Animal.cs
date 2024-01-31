namespace DAL.Entities
{
    public class Animal
    {
        public Animal()
        {

        }
        // AnimalMapper from form
        public Animal(string animalName, string breed, DateTime birthDate, Guid ownerId)
        {
            AnimalName = animalName;
            Breed = breed;
            BirthDate = birthDate;
            OwnerId = ownerId;
        }
        // DAL Get Animal
        public Animal(Guid animalId, string animalName, string breed, DateTime birthDate, Guid ownerId) : this(animalName, breed, birthDate, ownerId)
        {
            AnimalId = animalId;
        }

        public Guid AnimalId { get; set; } = Guid.NewGuid();
        public string AnimalName { get; set; } = default!;
        public string Breed { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public Guid OwnerId { get; set; }
    }
}
