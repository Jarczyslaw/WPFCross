using System.Configuration;

namespace WPFCross.UI.Services
{
    public interface IAppSettings
    {
        ConnectionStringSettings LiteDbConnectionString { get; }
    }
}
