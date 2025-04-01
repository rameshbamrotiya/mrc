using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UnMehta.WebPortal.EmailScheduler.Comman
{
    public class Functions
    {
        public static string strSqlConnectionString = ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString();
        
        public static string strWebHostedPath = ConfigurationManager.AppSettings["WebHostedPath"].ToString();

        public static long strIntervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"].ToString());

        public static string strMode = ConfigurationManager.AppSettings["Mode"].ToString();

        public static string strScheduledTime = ConfigurationManager.AppSettings["ScheduledTime"].ToString();


        public static bool ValidateEmailId(string emailId)
        {
            return Regex.IsMatch(emailId, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

    }
}
