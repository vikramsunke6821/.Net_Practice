using System.ComponentModel.DataAnnotations;

namespace EFDemo.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string? Number { get; set; }
    }
}
