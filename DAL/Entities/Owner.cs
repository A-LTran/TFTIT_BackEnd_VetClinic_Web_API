namespace DAL.Entities
{
    public class Owner : Person
    {
        public Owner() : base()
        {
        }

        public Owner(Guid userId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, Guid addressId) : base(userId, lastName, firstName, email, phone, mobile, birthDate, addressId)
        {

        }

        public List<Animal> AnimalList { get; set; } = new();

    }
}
