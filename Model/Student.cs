using System.ComponentModel.DataAnnotations;

namespace CIt_Demo_Project.Model
{
    public class Student
    {
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string email { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string dept { get; set; } = string.Empty;

        public double marks { get; set; }
    }
}
