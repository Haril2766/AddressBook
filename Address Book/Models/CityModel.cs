using System.ComponentModel.DataAnnotations;

namespace Address_Book.Models
{
    public class CityModel
    {

        [Required]
        [Display(Name = "City ID")]
        public int CityId { get; set; }

        [Required]
        [Display(Name = "State ID")]
        public int StateId { get; set; }
        
        [Required]
        [Display(Name = "Country ID")]
        public int CountryId { get; set; }


        [Required]
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        [Required]
        [Display(Name = "STD Code")]
        public int STDCode { get; set; }

        [Required]
        [Display(Name = "Pin Code")]
        public int PinCode { get; set; }

        [Required]
        [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public int UserId { get; set; }
    }
}
