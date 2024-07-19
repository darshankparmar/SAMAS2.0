namespace Service1.Application.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required string MobileNo { get; set; }
    }
}