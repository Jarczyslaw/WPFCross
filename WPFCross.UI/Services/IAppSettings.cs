using System.Configuration;

namespace WPFCross.UI.Services
{
    public interface IAppSettings
    {
        string DbConnection { get; }
    }
}
