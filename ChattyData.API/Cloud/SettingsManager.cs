using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ChattyData.Cloud
{
    public class SettingsManager
    {
        public SettingsManager()
        {
            this.StorageConnectionString = ConfigurationManager.AppSettings["Microsoft.Storage.ConnectionString"];
            this.ServiceBusConnectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
        }

        public String StorageConnectionString { get; private set; }

        public String ServiceBusConnectionString { get; private set; }
    }
}