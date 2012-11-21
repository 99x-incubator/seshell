using SeShell.Test.Enums;
using log4net;
using System.IO;

namespace SeShell.Test.Core
{
   public class EventLogger
    {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(EventLogger));

       private FileInfo GetTestConfig()
       {
           return new FileInfo(@"Core\Settings\TestLog.config");
       }

        public void LogEvent(string message)
        {
            log4net.Config.XmlConfigurator.Configure(GetTestConfig());
            Log.Info(message);
        }

        public void LogEvent(string message, EventTypes eType)
        {
            log4net.Config.XmlConfigurator.Configure(GetTestConfig());

            if (eType==EventTypes.Info)
            {
                Log.Info(message);
            }else if (eType==EventTypes.Warn)
            {
                Log.Warn(message);
            }else if (eType==EventTypes.Error)
            {
                Log.Error(message);
            }
            else if (eType == EventTypes.Fatal)
            {
                Log.Fatal(message);
            }
            else
            { Log.Info(message); }
        }
    }
}
