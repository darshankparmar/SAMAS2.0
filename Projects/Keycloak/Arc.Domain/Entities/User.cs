namespace Arc.Domain.Entities
{
    public class User
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string MobileNo { get; set; }
        public required string Password { get; set; }
        public bool IsEnabled { get; set; }
    }
}
