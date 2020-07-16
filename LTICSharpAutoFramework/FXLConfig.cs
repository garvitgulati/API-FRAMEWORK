using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LTICSharpAutoFramework
{
   public  class FXLConfig: ConfigurationSection

    {
        public static FXLConfig GetConfiguration()
        {
            FXLConfig configuration = ConfigurationManager.GetSection("fxlConfig") as FXLConfig;

            if (configuration != null)
                return configuration;

            return new FXLConfig();
        }

        [ConfigurationProperty("app_url", IsRequired = false)]

        public string AppUrl
        {
            get
            {
                return this["app_url"] as string;
            }
        }

        [ConfigurationProperty("app_user", IsRequired = false)]

        public string AppUser
        {
            get
            {
                return this["app_user"] as string;
            }
        }
        [ConfigurationProperty("app_pass", IsRequired = false)]
        public string AppPass
        {
            get
            {
                return this["app_pass"] as string;
            }
        }

        [ConfigurationProperty("downloadFolder", IsRequired = false)]
        public string DownloadFolder
        {
            get
            {
                return this["downloadFolder"] as string;
            }
        }

        [ConfigurationProperty("db_url", IsRequired = false)]
        public string DbUrl
        {
            get
            {
                return this["db_url"] as string;
            }
        }

        [ConfigurationProperty("client_name", IsRequired = false)]
        public string ClientName
        {
            get
            {
                return this["client_name"] as string;
            }
        }

        [ConfigurationProperty("environment_name", IsRequired = false)]
        public string EnvironementName
        {
            get
            {
                return this["environment_name"] as string;
            }
        }

        [ConfigurationProperty("db_port", IsRequired = false)]
        public string DbPort
        {
            get
            {
                return this["db_port"] as string;
            }
        }

        [ConfigurationProperty("db_name", IsRequired = false)]
        public string DbName
        {
            get
            {
                return this["db_name"] as string;
            }
        }
        [ConfigurationProperty("db_username", IsRequired = false)]
        public string DbUsername
        {
            get
            {
                return this["db_username"] as string;
            }
        }
        [ConfigurationProperty("db_password", IsRequired = false)]
        public string DbPassword
        {
            get
            {
                return this["db_password"] as string;
            }
        }

        [ConfigurationProperty("browser_name", IsRequired = false)]
        public string BrowserName
        {
            get
            {
                return this["browser_name"] as string;
            }
        }
        [ConfigurationProperty("app_server", IsRequired = false)]
        public string AppServer
        {
            get
            {
                return this["app_server"] as string;
            }
        }

        [ConfigurationProperty("sqldb_server", IsRequired = false)]
        public string SQLDbServer
        {
            get
            {
                return this["sqldb_server"] as string;
            }
        }

        [ConfigurationProperty("sqldb_name", IsRequired = false)]
        public string SQLDbName
        {
            get
            {
                return this["sqldb_name"] as string;
            }
        }

        [ConfigurationProperty("sqldb_username", IsRequired = false)]
        public string SQLDbUserName
        {
            get
            {
                return this["sqldb_username"] as string;
            }
        }
        [ConfigurationProperty("sqldb_password", IsRequired = false)]
        public string SQLDbPassword
        {
            get
            {
                return this["sqldb_password"] as string;
            }
        }







    }
}
