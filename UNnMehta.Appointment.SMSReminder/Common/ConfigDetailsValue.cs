using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNnMehta.Appointment.SMSReminder.Repository;

namespace UNnMehta.Appointment.SMSReminder.Common
{
    public class InsertSMSLogModel
    {
        public long PatientId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Mobile { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string APIUrl { get; set; }
        public string transectionId { get; set; }
        public DateTime EntryDateTime { get; set; }

    }

    public class ConfigDetailsModel
    {
        public long Id { get; set; }

        public string ParameterName { get; set; }

        public string ParameterValue { get; set; }

        public string Description { get; set; }
    }
    public class ConfigDetailsValue
    {
        public static string strSqlConnectionString = ConfigurationManager.ConnectionStrings["UNnMehta.Appointment.SMSReminder.Properties.Settings.UNMWebConnectionString"].ToString();
        private static T GetFromTable<T>(string key)
        {
            using (ConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(strSqlConnectionString))
            {
                string strError = "";

                ConfigDetailsModel objConfigDetailsModel = new ConfigDetailsModel();
                if (!configDetailsRepository.GetConfigDetails(key, out objConfigDetailsModel, out strError))
                {
                    if (objConfigDetailsModel != null)
                    {
                        object obj = (objConfigDetailsModel.ParameterValue == null ? "" : objConfigDetailsModel.ParameterValue).ToString();
                        return (T)obj;
                    }
                    else
                    {
                        object obj = ("DataNotFound" + "|" + false).ToString();
                        return (T)obj;
                    }
                }
                else
                {
                    object obj = (strError + "|" + true).ToString();
                    return (T)obj;
                }
            }
        }


        public static string SMSUsername2
        {
            get
            {
                return GetFromTable<string>("SMSUsername2");
            }
        }

        public static string SMSPassword2
        {
            get
            {
                return GetFromTable<string>("SMSPassword2");
            }
        }
        public static string senderid2
        {
            get
            {
                return GetFromTable<string>("senderid2");
            }
        }
        public static string SMSAPI2
        {
            get
            {
                return GetFromTable<string>("SMSAPI2");
            }
        }
        public static string Templateid2
        {
            get
            {
                return GetFromTable<string>("Templateid2");
            }
        }
                
        public static string SMSTemplateIdReminder
        {
            get
            {
                return GetFromTable<string>("SMSTemplateIdReminder");
            }
        }

    }
}
