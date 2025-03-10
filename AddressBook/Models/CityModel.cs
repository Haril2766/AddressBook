namespace AddressBook._2.Models
{
    public class CityModel
    {
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string CityName { get; set; }
        public string STDCode { get; set; }
        public string PinCode { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
    }

}