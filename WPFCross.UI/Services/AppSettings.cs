using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFCross.UI.Services
{
    public class AppSettings : IAppSettings
    {
        public ConnectionStringSettings LiteDbConnectionString
        {
            get { return GetConnectionString(); }
        }

        private ConnectionStringSettings GetConnectionString([CallerMemberName] string key = null)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[key];
            if (connectionString == null)
                throw new Exception("No connection string for: " + key);
            return connectionString;
        }

        private string GetSetting([CallerMemberName] string key = null)
        {
            var setting = ConfigurationManager.AppSettings[key];
            if (setting == null)
                throw new Exception("No setting for: " + key);
            return setting;
        }
    }
}
