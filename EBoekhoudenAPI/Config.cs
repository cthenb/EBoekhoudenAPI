using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBoekhoudenAPI
{
    public class Config
    {
        public string AbsolutePath
        {
            get
            {
                return "";
            }
        }

        public string Username
        {
            get
            {
                return "";
            }
        }

        public string SecurityCode1
        {
            get
            {
                return "";
            }
        }

        public string SecurityCode2
        {
            get
            {
                return "";
            }
        }
        
        public static Config Get()
        {
            var config = new Config();

            if (config.Username == null || config.AbsolutePath == null || config.SecurityCode1 == null || config.SecurityCode2 == null)
                throw new Exception("Config not yet set");

            return config;
        }
    }
}
