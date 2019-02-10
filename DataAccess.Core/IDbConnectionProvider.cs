namespace DataAccess.Core
{
    public interface IDbConnectionProvider
    {
        string DbFilePath { get; }
        string ConnectionString { get; }
    }
}
