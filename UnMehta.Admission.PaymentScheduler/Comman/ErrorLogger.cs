using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Data.Comman.Static;

namespace UnMehta.WebPortal.EmailScheduler.Comman
{
    public class ErrorLogger
    {
        public static void ERROR(string message, string stackTrace)
        {
            using (NLogger log = new NLogger(LogErrorFIle.ErrorLog))
            {
                log.Log(" Exception Web Log : " + message + " ### Trace : " + stackTrace, LogType.Info);
            }
        }
    }
}
