using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Unmehta.WebPortal.Common;

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

        public static bool SendEmail(string strToEMail, string strSubject, string strBody, out string strError, bool ishtml = false, List<Attachment> lstAttachment = null)
        {
            strError = "";
            try
            {
                string smtpserver = "";
                string smtpPassword = "";
                string fromemail = "";
                string smtpaccount = "";
                string smtpPort = "";
                string smtpIsSecure = "";

                if (ConfigDetailsValue.SMTPIsTest != "1")
                {
                    smtpserver = ConfigDetailsValue.SMTPServer;
                    smtpPassword = ConfigDetailsValue.SMTPPassword;
                    fromemail = ConfigDetailsValue.SMTPFromEmail;
                    smtpaccount = ConfigDetailsValue.SMTPAccount;
                    smtpPort = ConfigDetailsValue.SMTPPort;
                    smtpIsSecure = ConfigDetailsValue.SMTPIsSecure;
                }
                else
                {

                    smtpserver = ConfigDetailsValue.TestSMTPServer;
                    smtpPassword = ConfigDetailsValue.TestSMTPPassword;
                    fromemail = ConfigDetailsValue.TestSMTPFromEmail;
                    smtpaccount = ConfigDetailsValue.TestSMTPAccount;
                    smtpPort = ConfigDetailsValue.TestSMTPPort;
                    smtpIsSecure = ConfigDetailsValue.TestSMTPIsSecure;
                }
                MailMessage msg = new MailMessage();
                //SmtpClient client;

                //if (!string.IsNullOrEmpty(ConfigDetailsValue.SMTPPort))
                //{
                //    client = new SmtpClient(smtpserver, Convert.ToInt32(ConfigDetailsValue.SMTPPort));

                //}
                //else
                //{
                //    client = new SmtpClient(smtpserver);

                //}
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = smtpserver;
                if (!string.IsNullOrWhiteSpace(smtpPort))
                {
                    client.Port = Convert.ToInt32(smtpPort);
                }
                // setup Smtp authentication
                System.Net.NetworkCredential credentials =
                    new System.Net.NetworkCredential(smtpaccount, smtpPassword);
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;
                msg.To.Add(strToEMail);
                //msg.CC.Add("hardik.mistry@kcspl.co.in");      
                // msg.CC.Add("parul@kcspl.co.in");
                msg.IsBodyHtml = ishtml;
                if (lstAttachment != null)
                {
                    foreach (var row in lstAttachment)
                    {
                        msg.Attachments.Add(row);
                    }
                }
                msg.Subject = strSubject;
                msg.Body = strBody;

                msg.From = new System.Net.Mail.MailAddress(fromemail);
                if (smtpIsSecure != "0")
                {
                    client.EnableSsl = smtpIsSecure == "1" ? true : false;
                    //client.UseDefaultCredentials = true;
                }
                else
                {
                    client.EnableSsl = false;
                }
                //client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential(smtpaccount, smtpPassword);
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                strError = "Message=>" + ex.Message + " Inner Exception=>" + ex.InnerException!=null? ex.InnerException.Message:"";
                ErrorLogger.ERROR("Email Not Send to " + strToEMail, "");
                ErrorLogger.ERROR(" Request : ", ex.ToString());
                return false;
            }
        }

    }
}
