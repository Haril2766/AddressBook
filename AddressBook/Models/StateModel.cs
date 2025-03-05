namespace AddressBook._2.Models
{
    public class StateModel
    {
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }

    }
}