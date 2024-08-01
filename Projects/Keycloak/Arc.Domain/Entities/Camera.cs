namespace Arc.Domain.Entities
{
    public class Camera
    {
        public required string Id { get; set; }
        public required string CameraName { get; set; }
        public required string CameraIP { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public bool IsEnabled { get; set; }
    }
}
