using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class AccredationMasterDAL:DBConnection
    {
        protected bool Insert(AccredationMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateAccredationMasterDetails";
                command.Parameters.AddWithValue("@Acc_id", objbo.Acc_id);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@Accredation_Title", objbo.Accredation_Title);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@Img_Path", objbo.Img_Path);
                command.Parameters.AddWithValue("@ImgLogo", objbo.ImgLogo);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@AccredationURL", objbo.AccredationURL);
                command.Parameters.AddWithValue("@IsDisplayInHeader", objbo.IsDisplayInHeader);
                command.Parameters.AddWithValue("@IsDisplayInFooter", objbo.IsDisplayInFooter);
                command.Parameters.AddWithValue("@AccredationDesc", objbo.AccredationDesc);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@Is_active", objbo.IsVisible);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                DataSet ds = ExecuteQuery(command);
                objbo.Acc_id = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertSub(AccredationMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateAccredationMaster";
                command.Parameters.AddWithValue("@Acc_id", objbo.AM_id);
                command.Parameters.AddWithValue("@AM_id", objbo.Acc_id);
                command.Parameters.AddWithValue("@Accredation_Name", objbo.Accredation_Name);
                command.Parameters.AddWithValue("@Accredation_Description", objbo.Accredation_Description);
                command.Parameters.AddWithValue("@Ac_Date", objbo.date);
                command.Parameters.AddWithValue("@Accredation_MonthYear", objbo.Accredation_MonthYear);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetAllAccredationDetails(AccredationMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AccredationMasterdetails_Search";
                command.Parameters.AddWithValue("@Acc_id", objbo.Acc_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Update(AccredationMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AwardDetailsMaster_Update";
                command.Parameters.AddWithValue("@Acc_id", objbo.Acc_id);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Accredation_Name", objbo.Accredation_Name);
                command.Parameters.AddWithValue("@AccredationDesc", objbo.AccredationDesc);
                command.Parameters.AddWithValue("@Accredation_MonthYear", objbo.Accredation_MonthYear);
                command.Parameters.AddWithValue("@Is_active_album", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected bool Delete(AccredationMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AccredationMaster_Delete";
                command.Parameters.AddWithValue("@AM_id", objbo.AM_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteAccredationdesc(AccredationMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AccredationDescription_Delete";
                command.Parameters.AddWithValue("@Acc_id", objbo.Acc_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(long languageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AccredationMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", languageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Selectsub(long aM_id, long languageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AccredationMastersub_Select";
                command.Parameters.AddWithValue("@AM_id", aM_id);
                command.Parameters.AddWithValue("@LanguageId", languageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Selectsubselect(long aM_id, long languageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AccredationMastersubSelect";
                command.Parameters.AddWithValue("@AM_id", aM_id);
                command.Parameters.AddWithValue("@LanguageId", languageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAccredition(long languageId, int Acc_id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AccredationDetails_SelectID";
                command.Parameters.AddWithValue("@LanguageId", languageId);
                command.Parameters.AddWithValue("@Acc_id", Acc_id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
