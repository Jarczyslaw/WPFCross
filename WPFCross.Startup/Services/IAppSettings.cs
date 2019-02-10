using System.Configuration;

namespace WPFCross.Startup.Services
{
    public interface IAppSettings
    {
        string LiteDbFileName { get; }
        string SQLiteFileName { get; }
        string LiteDbConnection { get; }
        string SQLiteConnection { get; }
    }
}
