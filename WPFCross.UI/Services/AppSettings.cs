﻿using DataAccess.Core;
using System;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace WPFCross.UI.Services
{
    public class AppSettings : IAppSettings, IDbConnectionProvider
    {
        public string DbConnection
        {
            get { return GetConnectionString().ConnectionString; }
        }

        private ConnectionStringSettings GetConnectionString([CallerMemberName] string key = null)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[key];
            if (connectionString == null)
            {
                throw new Exception("No connection string for: " + key);
            }

            return connectionString;
        }

        private string GetSetting([CallerMemberName] string key = null)
        {
            var setting = ConfigurationManager.AppSettings[key];
            if (setting == null)
            {
                throw new Exception("No setting for: " + key);
            }

            return setting;
        }
    }
}
