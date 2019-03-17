namespace DataAccess.SQLiteAccess.Entities
{
    internal class Group
    {
        public int Id { get; set; }
        public int Default { get; set; }
        public string Name { get; set; }

        public bool MappedDefault => Default != 0;
    }
}
