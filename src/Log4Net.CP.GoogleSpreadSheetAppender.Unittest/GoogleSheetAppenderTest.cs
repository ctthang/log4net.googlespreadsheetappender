using Log4Net.CP.GoogleSpreadSheetAppender;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net.Core;
using System;

namespace Log4Net.CP.GoogleSpreadSheetAppender.Unittest
{
    
    
    /// <summary>
    ///This is a test class for GoogleSheetAppenderTest and is intended
    ///to contain all GoogleSheetAppenderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GoogleSheetAppenderTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AddRows
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Log4Net.CP.GoogleSpreadSheetAppender.dll")]
        public void AddRowsTest()
        {
            GoogleSheetAppender_Accessor target = new GoogleSheetAppender_Accessor()
            {
                Username = "l4nsheetappender@gmail.com",
                Password = "Test!234",
                SheetName = "LoggingTest"
            };
            LoggingEvent[] events = new LoggingEvent[1]
            {
                new LoggingEvent(new LoggingEventData{
                    Level = Level.Debug,
                    TimeStamp = DateTime.Now,
                    Message = "This is a test message from Unittest"
                })
            };
            target.AddRows(events);
        }
    }
}
