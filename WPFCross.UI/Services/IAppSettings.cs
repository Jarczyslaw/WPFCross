using System.Configuration;

namespace WPFCross.UI.Services
{
    public interface IAppSettings
    {
        string LiteDbConnection { get; }
        string SQLiteConnection { get; }
    }
}
