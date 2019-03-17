namespace DataAccess.SQLiteAccess.Entities
{
    internal class Contact
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public int Favourite { get; set; }
        public int GroupId { get; set; }

        public bool MappedFavourite => Favourite != 0;
    }
}
