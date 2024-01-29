namespace DAL.Entities
{
    public class Animal
    {
        public Guid AnimalId { get; set; } = Guid.NewGuid();
        public string AnimalName { get; set; } = default!;
        public string Breed { get; set; } = default!;
        public int UserId { get; set; }

    }
}
