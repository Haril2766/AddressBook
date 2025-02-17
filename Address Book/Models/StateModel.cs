using System.ComponentModel.DataAnnotations;

namespace Address_Book.Models
{
    public class StateModel
    {
        [Required]
        [Display(Name = "Country ID")]
        public int CountryId { get; set; }

        [Required]
        [Display(Name = "State ID")]
        public int StateId { get; set; }

        [Required]
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        [Required]
        [Display(Name = "State Code")]
        public int StateCode { get; set; }

        [Required]
        [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public int UserId { get; set; }
    }
}
