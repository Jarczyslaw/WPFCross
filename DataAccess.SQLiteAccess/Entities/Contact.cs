namespace DataAccess.SQLiteAccess.Entities
{
    internal class Contact
    {
        public Contact()
        {
        }

        public Contact(Models.Contact contact)
        {
            Id = contact.Id;
            Title = contact.Title;
            Name = contact.Name;
            Favourite = contact.Favourite ? 1 : 0;
            GroupId = contact.Group.Id;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public int Favourite { get; set; }
        public int GroupId { get; set; }

        public bool MappedFavourite => Favourite != 0;

        public Models.Contact ToModel()
        {
            return new Models.Contact
            {
                Id = Id,
                Favourite = MappedFavourite,
                Name = Name,
                Title = Title
            };
        }
    }
}
