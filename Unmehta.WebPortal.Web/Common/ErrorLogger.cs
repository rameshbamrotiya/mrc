using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Configuration;
using System.Web.UI;

/// <summary>
/// To write Errorlogs
/// </summary>
public class ErrorLogger
    {
        public static void Info(string Message, Page p)
        {
            WriteToErrorLog(Message, "", "INFO", p);
        }

        public static void SUCCESS(string Message, Page p)
        {
            WriteToErrorLog(Message, "", "SUCCESS", p);
        }

        public static void WARNING(string Message, Page p)
        {
            WriteToErrorLog(Message, "", "WARNING", p);
        }

        public static void ERROR(string Message, string stkTrace, Page p)
        {
            WriteToErrorLog(Message, stkTrace, "ERROR", p);
        }

        public static void WriteToErrorLog(string msg, string stkTrace, string Level,Page p)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                var logFilePath = p.Server.MapPath(ConfigurationManager.AppSettings["ErrorLogFilePath"].ToString()); ;

                logFilePath = logFilePath + "Log_" + DateTime.Today.ToString("yyyyMMdd") + "." + "log";

                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists

                var logFileInfo = new FileInfo(logFilePath);
                if (logFileInfo.DirectoryName != null)
                {
                    var logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                    if (!logDirInfo.Exists) logDirInfo.Create();
                }

                #endregion Create the Log file directory if it does not exists

                fileStream = !logFileInfo.Exists ? logFileInfo.Create() : new FileStream(logFilePath, FileMode.Append);
                streamWriter = new StreamWriter(fileStream);
                streamWriter.Write("\n\rLevel: " + Level);
                streamWriter.Write("Message: " + msg);
                streamWriter.Write("StackTrace: " + stkTrace);
                streamWriter.Write("Date/Time: " + DateTime.Now.ToString());
                streamWriter.Write("============================================");
            }
            catch { }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }

        }

    }


