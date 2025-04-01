using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UNnMehta.Appointment.SMSReminder.Comman;
using UNnMehta.Appointment.SMSReminder.Common;
using UNnMehta.Appointment.SMSReminder.Data;
using UNnMehta.Appointment.SMSReminder.Repository;

namespace UNnMehta.Appointment.SMSReminder
{
    public class Program
    {
        public static string connectionString = UNnMehta.Appointment.SMSReminder.Properties.Settings.Default.UNMWebConnectionString;


        public static bool sendSingleSMS(InsertSMSLogModel smsLogModel)
        {
            bool send = false;
            string requst = "", strrespons = "", strRtn = "0";
            try
            {
                string SMSAPI = ConfigDetailsValue.SMSAPI2;
                string username = ConfigDetailsValue.SMSUsername2;
                string password = ConfigDetailsValue.SMSPassword2;
                string senderid = ConfigDetailsValue.senderid2;
                string mobileNo = smsLogModel.Mobile;
                string message = smsLogModel.Message;


                requst = (SMSAPI + "?user=" + username + "&pass=" + password + "&sender=" + senderid + "&phone=" + mobileNo + "&text=" + message + "&priority=ndnd&stype=normal").ToString();

                smsLogModel.APIUrl=requst;

                var client = new RestClient(requst);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                IRestResponse response = client.Execute(request);

                //  ErrorLogger.WriteToErrorLog(" response :" + message, response.ToString(), "Error",p);
                strrespons = "response.Content: " + Convert.ToString(response.Content) + " StatusCode: " + Convert.ToString(response.StatusCode) + "\r\n DESC: " + Convert.ToString(response.StatusDescription);
                smsLogModel.Status= strrespons;
                ErrorLogger.ERROR(" \r\n" + HttpUtility.UrlDecode(message) + " \r\n" + requst, strrespons);
                strRtn = response.Content.ToString();
                strRtn = "1";
                send = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.ToString());
                strRtn = ex.Message.ToString();
                strRtn = "0";
                send = false;
            }
            finally
            {
                try
                {
                    using (ConfigDetailsRepository configDetailsRepository =new ConfigDetailsRepository(connectionString))
                    {

                        // Insert SMS log entry
                        string error;
                        bool isError = configDetailsRepository.InsertSMSLog(smsLogModel, out error);

                        // Handle insertion error, if any
                        if (isError)
                        {
                            ErrorLogger.ERROR($"Error inserting SMS log entry for PatientId: {smsLogModel.PatientId}. Error: {error}", error);
                            Console.WriteLine($"Error inserting SMS log entry for PatientId: {smsLogModel.PatientId}. Error: {error}");
                        }
                        else
                        {
                            ErrorLogger.ERROR($"SMS log entry inserted successfully for PatientId: {smsLogModel.PatientId}", "");
                            Console.WriteLine($"SMS log entry inserted successfully for PatientId: {smsLogModel.PatientId}");
                        }

                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.ERROR(ex.Message.ToString(), ex.ToString());
                }
            }
            return send;

        }
        public static void ProcessDailyVisitEntries()
        {
            // Get all daily visit entries that require SMS reminders
            List<GetAppointmentReminderSMSListResult> reminderSMSList = GetAllDailyVisitEntry();

            // Iterate through each reminder SMS entry
            foreach (var reminderSMS in reminderSMSList)
            {
                // Prepare model for inserting SMS log entry
                InsertSMSLogModel smsLogModel = new InsertSMSLogModel
                {
                    PatientId = reminderSMS.PatientId,
                    AppointmentDate = reminderSMS.AppointmentDate.Value,
                    Mobile = reminderSMS.MobileNo,
                    transectionId = "", // Generate unique transaction ID
                    Message = reminderSMS.Message
                };
                // Send SMS message
                
                sendSingleSMS(smsLogModel);
            }
        }
        public static List<GetAppointmentReminderSMSListResult> GetAllDailyVisitEntry()
        {
            using (ConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(connectionString))
            {
                return configDetailsRepository.GetAllDailyVisitEntry().ToList();
            }
        }
        public static void Main(string[] args)
        {

            ErrorLogger.ERROR("SMS Send", "Start");
            ProcessDailyVisitEntries();
            ErrorLogger.ERROR("SMS Send", "End");
        }
    }
}
