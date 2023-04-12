using log4net.Config;
using System.Configuration;

namespace accesa
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("app.config"));
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            Console.WriteLine("Configuration Settings for DB {0}", GetConnectionStringByName("quiz"));
            IDictionary<string, string?> props = new SortedList<string, string?>();
            props.Add("ConnectionString", GetConnectionStringByName("quiz"));
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

        }

        private static string? GetConnectionStringByName(string name)
        {
            string? result = null;
            var stringSettings = ConfigurationManager.ConnectionStrings[name];
            if (stringSettings != null)
            {
                result = stringSettings.ConnectionString;
            }

            return result;
        }
    }
}