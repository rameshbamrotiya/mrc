using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using ClassLib.BO;
using ClassLib.DAL;

namespace ClassLib.BL
{
    public class PatientFeedbackContentDetailsBAL : PatientFeedbackContentDetailsDAL
    {

        #region Constructor

        public PatientFeedbackContentDetailsBAL()
        {
        }

        #endregion Constructors

        #region Insert method  
        public bool InsertRecord(PatientFeedbackContentDetailsBO objPatientFeedbackContentDetails)
        {
            try
            {
                return Insert(objPatientFeedbackContentDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
      
        #region Select method  
        public DataTable SelectRecord(PatientFeedbackContentDetailsBO objPatientFeedbackContentDetails)
        {
            try
            {
                return SelectById(objPatientFeedbackContentDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable selectFeedbackDetails(PatientFeedbackContentDetailsBO objPatientFeedbackContentDetails)
        {
            try
            {
                return selectFeedback(objPatientFeedbackContentDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllPatientFeedbackExportList()
        {
            try
            {
                return GetAllPatientFeedbackList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
