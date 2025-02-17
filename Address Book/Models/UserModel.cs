using System.ComponentModel.DataAnnotations;

namespace Address_Book.Models
{
    public class UserModel
    {
        [Required]
        public int UserId { get; set; }


        [Required]
        public string UserName { get; set; }



        [Required]
        public string mobileno { get; set; }

        [Required]
        public string Email { get; set; }


        [Required]
        public DateTime creationDate { get; set; }
    }
}
