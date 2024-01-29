namespace DAL.Entities
{
    internal class Animal
    {
        internal Guid AnimalId { get; set; } = Guid.NewGuid();
        internal string AnimalName { get; set; } = default!;
        internal string Breed { get; set; } = default!;

    }
}
