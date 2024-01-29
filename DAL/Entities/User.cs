namespace DAL.Entities
{
    internal class User
    {
        internal Guid UserId { get; set; } = Guid.NewGuid();
        internal string LastName { get; set; } = default!;
        internal string FirstName { get; set; } = default!;
        internal string Email { get; set; } = default!;
        internal string Phone { get; set; } = default!;
        internal string Mobile { get; set; } = default!;
        internal DateTime BirthDate { get; set; }
        internal string UserPassword { get; set; } = default!;

    }
}
