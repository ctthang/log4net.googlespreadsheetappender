using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProjectTest
{
    public partial class Form1 : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Program));

        public Form1()
        {
            InitializeComponent();

            //Load Config.xml to setup log4net
            string path = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly()
               .GetModules()[0].FullyQualifiedName)
               + "\\log4net.xml";
            if (System.IO.File.Exists(path))
            {
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //write some log
            Log.Debug("Logging from Windows mobile app");

            Log.Error("Error logging from windows mobile app");

            Log.Info("Info logging from windows mobile app");

            Log.Fatal("Fatal logging from windows mobile app");
        }
    }
}