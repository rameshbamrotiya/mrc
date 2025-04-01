using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using ClassLib.BO;
using DAL;

namespace ClassLib.DAL
{
    public abstract class PatientFeedbackContentDetailsDAL : DBConnection
    {

        #region Constructor

        public PatientFeedbackContentDetailsDAL()
        {
        }

        #endregion Constructors

        #region Insert method  
        protected bool Insert(PatientFeedbackContentDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientFeedbackContentDetails_Insert";
                command.Parameters.AddWithValue("@PF_ID", objbo.PF_ID);
                command.Parameters.AddWithValue("@PatientFeedback", objbo.PatientFeedback);
                command.Parameters.AddWithValue("@PatientHospitalGuide", objbo.PatientHospitalGuide);
                command.Parameters.AddWithValue("@enabled", objbo.enabled);
                command.Parameters.AddWithValue("@added_by", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                int rowsAffected = ExecuteNonQuery(command);
                if (rowsAffected > 0) return true; else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion



        #region Select method  
        protected DataTable SelectById(PatientFeedbackContentDetailsBO objbo)
        {
            try
            {
                DataSet dt = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientFeedbackContentDetails_SelectById";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                dt = ExecuteQuery(command);
                return dt.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Select method  
        protected DataTable selectFeedback(PatientFeedbackContentDetailsBO objbo)
        {
            try
            {
                DataSet dt = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientFeedbackContentDetailsByLanguageId";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                dt = ExecuteQuery(command);
                return dt.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataTable GetAllPatientFeedbackList()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllPatientFeedback";
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


    }
}
