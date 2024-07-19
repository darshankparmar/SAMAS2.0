using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service1.Domain.Entities
{
    public class Camera
    {
        public static readonly string TableName = "Cameras";
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string BrandName { get; set; }

        [Required]
        public required string ModelNumber { get; set; }
        
        [Required]
        public required string IPAddress { get; set; }

        public string? MACAddress { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}