using System.ComponentModel.DataAnnotations;

namespace Confidentiel.Data.Entities
{
    public class Secret
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Value { get; set; }
    }
}
