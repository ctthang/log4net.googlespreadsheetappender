using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SmartDeviceProject.Console
{
    class Program
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            //Load Config.xml to setup log4net
            string path = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly()
               .GetModules()[0].FullyQualifiedName)
               + "\\log4net.xml";
            if (System.IO.File.Exists(path))
            {
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));
            }

            //write some log
            Log.Debug("Logging from Windows mobile app");
            System.Console.WriteLine("Done");
        }
    }
}
