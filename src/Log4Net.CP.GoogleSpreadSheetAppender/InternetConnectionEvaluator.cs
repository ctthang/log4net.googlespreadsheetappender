using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using log4net.Core;

namespace Log4Net.CP.GoogleSpreadSheetAppender
{
    public class InternetConnectionEvaluator : ITriggeringEventEvaluator
    {
        public bool IsTriggeringEvent(LoggingEvent loggingEvent)
        {
            return DnsTest();
        }

        public static bool DnsTest()
        {
            try
            {
                System.Net.IPHostEntry ipHe =
                    System.Net.Dns.GetHostEntry("www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
