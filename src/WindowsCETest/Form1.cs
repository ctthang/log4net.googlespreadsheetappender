using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsCETest
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

        private void button1_Click(object sender, EventArgs e)
        {
            //write some log
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Log.Debug("Log message from mobile testing");
            stopWatch.Stop();
            MessageBox.Show(string.Format("Done in {0} ms", stopWatch.ElapsedMilliseconds));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Log.Error("Error logging message from mobile testing");
            stopWatch.Stop();
            MessageBox.Show(string.Format("Done in {0} ms", stopWatch.ElapsedMilliseconds));
        }
    }
}