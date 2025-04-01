using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using UNnMehta.Appointment.SMSReminder.Common;
using UNnMehta.Appointment.SMSReminder.Data;

namespace UNnMehta.Appointment.SMSReminder.Repository
{
    public class ConfigDetailsRepository : IDisposable
    {
        private bool disposedValue;


        private string SqlConnectionSTring;
        public ConfigDetailsRepository()
        {
            string strConnection = ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString();
            SqlConnectionSTring = strConnection.ToString();
        }
        public ConfigDetailsRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public bool GetConfigDetails(string strParaName, out ConfigDetailsModel mdlConfigDetailsModel, out string strError)
        {
            bool isError = false;
            mdlConfigDetailsModel = new ConfigDetailsModel();
            strError = "";
            using (ConfigDetailsDataContext db = new ConfigDetailsDataContext(SqlConnectionSTring))
            {
                try
                {
                    mdlConfigDetailsModel = db.GetConfigdetailsByName(strParaName).Select(x => new ConfigDetailsModel()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        ParameterName = x.ParameterName,
                        ParameterValue = x.ParameterValue
                    }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    isError = true;
                    strError = ex.ToString();
                }
            }
            return isError;
        }


        public List<GetAppointmentReminderSMSListResult> GetAllDailyVisitEntry()
        {
            using (ConfigDetailsDataContext db = new ConfigDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAppointmentReminderSMSList().ToList();
            }
        }


        public bool InsertSMSLog(InsertSMSLogModel insertSMSLogModel, out string strError)
        {
            bool isError = false;
            strError = "";
            using (ConfigDetailsDataContext db = new ConfigDetailsDataContext(SqlConnectionSTring))
            {
                try
                {
                    var data = db.InsertSMSLog(insertSMSLogModel.PatientId, insertSMSLogModel.AppointmentDate, insertSMSLogModel.Mobile, insertSMSLogModel.transectionId, insertSMSLogModel.Message, insertSMSLogModel.Status, insertSMSLogModel.APIUrl, DateTime.Now);
                }
                catch (Exception ex)
                {
                    isError = true;
                    strError = ex.ToString();
                }
            }
            return isError;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ConfigDetailsRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
