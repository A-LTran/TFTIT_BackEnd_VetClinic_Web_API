namespace DAL.Entities
{
    public class Owner : Person
    {
        public Owner() : base()
        {
        }

        //BLL ToOwner
        public Owner(string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, Role personRole, Guid addressId) : base(lastName, firstName, email, phone, mobile, birthDate, personRole, addressId)
        {

        }
        // DAL ToOwner
        public Owner(Guid personId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, Role personRole, Guid addressId) : base(personId, lastName, firstName, email, phone, mobile, birthDate, personRole, addressId)
        {

        }

        public List<Animal> AnimalList { get; set; } = new();
    }
}
