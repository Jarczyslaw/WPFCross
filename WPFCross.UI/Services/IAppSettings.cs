using System.Configuration;

namespace WPFCross.UI.Services
{
    public interface IAppSettings
    {
        string LiteDbFileName { get; }
        string SQLiteFileName { get; }
        string LiteDbConnection { get; }
        string SQLiteConnection { get; }
    }
}
