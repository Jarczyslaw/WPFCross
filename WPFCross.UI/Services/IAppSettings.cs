using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCross.UI.Services
{
    public interface IAppSettings
    {
        ConnectionStringSettings LiteDbConnectionString { get; }
    }
}
