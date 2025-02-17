using System.ComponentModel.DataAnnotations;

namespace Address_Book.Models
{
    public class HelperTagModel
    {
        [Required(ErrorMessage = "Please Enter Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Correct Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Correct PhoneNumber")]
        public string PhoneNumber { get; set; }

    }
}