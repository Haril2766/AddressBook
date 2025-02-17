using System.ComponentModel.DataAnnotations;

namespace Address_Book.Models
{
    public class StudentModel
    {
        [Required]  
        public int Id { get; set; } // No [Required] attribute here

        [Required]
        [Display(Name = "First Name")]
        public string Name { get; set; }
    }
}
