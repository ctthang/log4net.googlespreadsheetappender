using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Docs = Google.GData.Documents;

namespace ConsoleApplicationTest
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

            //Disconnect internet connection first

            //Log.Debug("Buffer First test");

            //Log.Error("Buffer First error");

            //Console.WriteLine("Logging done! Connect internet and press a key");

            //Console.ReadKey();

            //Log.Info("First info");

            //Log.Fatal("First fatal");

            //Console.WriteLine("OK! Check if buffer logs is submited!");
            Console.WriteLine("Starting logging...");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Log.Debug("Log message from console app");
            stopWatch.Stop();
            Console.WriteLine("Done in {0} ms", stopWatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
