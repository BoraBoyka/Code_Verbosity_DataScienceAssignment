using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace ACDMAutomation.ACDMAutomation.Driver
{
    public class Startup
    {
        public string BrowserType { get; set; }
        public string OS { get; set; }
        public string ApplicationENV_URL { get; set; }
        public string ChromeVersion { get; set; }
        public string EdgeVersion { get; set; }
        public string ConnectionString { get; set; }
        public string ENV { get; set; }
    }
}