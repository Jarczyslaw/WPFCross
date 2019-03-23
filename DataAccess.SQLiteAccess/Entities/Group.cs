namespace DataAccess.SQLiteAccess.Entities
{
    internal class Group
    {
        public int Id { get; set; }
        public int Default { get; set; }
        public string Name { get; set; }

        public bool MappedDefault => Default != 0;

        public Models.Group ToModel()
        {
            return new Models.Group
            {
                Default = Default == 1,
                Id = Id,
                Name = Name
            };
        }
    }
}
