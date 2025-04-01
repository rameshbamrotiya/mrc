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
    public abstract class FacilityInECMSDetailsDAL : DBConnection
    {

        #region Constructor

        public FacilityInECMSDetailsDAL()
        {
        }

        #endregion Constructors

        #region Insert method  
        protected bool Insert(FacilityInECMSDetailsBO objbo, DataTable dt)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacilityInECMSDetails_Insert";
                command.Parameters.AddWithValue("@FIEID", objbo.FIEID);
                command.Parameters.AddWithValue("@FIEMID", objbo.FIEMID);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@LanguageID", objbo.LanguageID);
                command.Parameters.AddWithValue("@Enabled", objbo.Enabled);
                command.Parameters.AddWithValue("@Added_By", objbo.Added_By);
                command.Parameters.AddWithValue("@IP_Add", objbo.IP_Add);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@dtEMCS", dt);
                int rowsAffected = ExecuteNonQuery(command);
                if (rowsAffected > 0) return true; else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Update method  
        protected bool Update(FacilityInECMSDetailsBO objbo, DataTable dt)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacilityInECMSDetails_Update";
                command.Parameters.AddWithValue("@FIEID", objbo.FIEID);
                command.Parameters.AddWithValue("@FIEMID", objbo.FIEMID);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@LanguageID", objbo.LanguageID);
                command.Parameters.AddWithValue("@Enabled", objbo.Enabled);
                command.Parameters.AddWithValue("@Added_By", objbo.Added_By);
                command.Parameters.AddWithValue("@IP_Add", objbo.IP_Add);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@dtEMCS", dt);
                int rowsAffected = ExecuteNonQuery(command);
                if (rowsAffected > 0) return true; else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Delete method  
        protected bool Delete(FacilityInECMSDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacilityInECMSDetails_Delete";
                command.Parameters.AddWithValue("@FIEMID", objbo.FIEMID);
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
        protected DataSet SelectById(FacilityInECMSDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacilityInECMSDetails_SelectById";
                command.Parameters.AddWithValue("@FIEMID", objbo.FIEMID);
                command.Parameters.AddWithValue("@LanguageID", objbo.LanguageID);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectFacilityDetailsById(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacilityInECMSDetailsByID";
                command.Parameters.AddWithValue("@FIEMDID", Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectFacilityDetailsByFIEMID(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacilityInECMSDetailsByFIEMID";
                command.Parameters.AddWithValue("@FIEMID", Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateFacilityDetails(FacilityInECMSDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacilityInECMSDetails_UpdateByFIEMDID";
                command.Parameters.AddWithValue("@FIEMDID", objbo.FIEMDID);
                command.Parameters.AddWithValue("@Subtitle", objbo.Subtitle);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@ImageUrl", objbo.ImageUrl);
                command.Parameters.AddWithValue("@user_id", objbo.Modify_By);
                command.Parameters.AddWithValue("@ip_add", objbo.IP_Add);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return true; else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Select All method  
        protected DataTable SelectAll()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacilityInECMSDetails_SelectAll";
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
