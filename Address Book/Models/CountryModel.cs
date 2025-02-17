using System.ComponentModel.DataAnnotations;

namespace Address_Book.Models
{
    public class CountryModel   
    {
        [Required]
        public int CountryId { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string CountryCode { get; set; }

        [Required]
        public DateTime creationdate { get; set; }

        [Required]
        public int UserId { get; set; }

    }
}
