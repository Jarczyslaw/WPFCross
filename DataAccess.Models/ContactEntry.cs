namespace DataAccess.Models
{
    public class ContactEntry
    {
        public int Id { get; set; }
        public ContactEntryType Type { get; set; }
        public string Value { get; set; }
    }
}
