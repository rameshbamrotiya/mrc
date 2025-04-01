using System;
using System.Data.SqlClient;
using System.Data;
using BO;

namespace DAL
{
    public abstract class HomePageContentDAL : DBConnection
    {

        #region Constructor

        public HomePageContentDAL()
        {
        }

        #endregion Constructors

        #region Insert method  
        protected bool Insert(HomePageContent_DetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_HomePageContent_Insert";
                command.Parameters.AddWithValue("@LeftVideoTitle", objbo.LeftVideoTitle);
                command.Parameters.AddWithValue("@LeftVideoURL", objbo.LeftVideoURL);
                command.Parameters.AddWithValue("@LeftVideoReadMore", objbo.LeftVideoReadMore);
                command.Parameters.AddWithValue("@RightVideoTitle", objbo.RightVideoTitle);
                command.Parameters.AddWithValue("@RightVideoURL", objbo.RightVideoURL);
                command.Parameters.AddWithValue("@RightVideoReadMore", objbo.RightVideoReadMore);
                command.Parameters.AddWithValue("@OPDImageIcon", objbo.OPDImageIcon);
                command.Parameters.AddWithValue("@IPDImageIcon", objbo.IPDImageIcon);
                command.Parameters.AddWithValue("@SurgeryImageIcon", objbo.SurgeryImageIcon);
                command.Parameters.AddWithValue("@ProceduresImageIcon", objbo.ProceduresImageIcon);
                command.Parameters.AddWithValue("@InvestigationsImageIcon", objbo.InvestigationsImageIcon);
                command.Parameters.AddWithValue("@LeftImage", objbo.LeftImage);
                command.Parameters.AddWithValue("@RightImage", objbo.RightImage);
                command.Parameters.AddWithValue("@OpdDay", objbo.OpdDay);
                command.Parameters.AddWithValue("@IpdDay", objbo.IpdDay);
                command.Parameters.AddWithValue("@SurgeryDay", objbo.SurgeryDay);
                command.Parameters.AddWithValue("@ProceduresDay", objbo.ProceduresDay);
                command.Parameters.AddWithValue("@InvestigationsDay", objbo.InvestigationsDay);
                command.Parameters.AddWithValue("@LanguageID", objbo.LanguageID);
                command.Parameters.AddWithValue("@IsDelete", objbo.IsDelete);
                command.Parameters.AddWithValue("@IsActive", objbo.IsActive);
                command.Parameters.AddWithValue("@AddedBy", objbo.AddedBy);
                command.Parameters.AddWithValue("@IPAddress", objbo.IPAddress);
                command.Parameters.AddWithValue("@link_Video_PathLeft", objbo.link_Video_PathLeft);
                command.Parameters.AddWithValue("@link_Video_PathRight", objbo.link_Video_PathRight);
                command.Parameters.AddWithValue("@Home_ID", objbo.Home_ID);
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
        protected bool Update(HomePageContentBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_HomePageContent_Update";
                command.Parameters.AddWithValue("@Home_ID", objbo.Home_ID);
                command.Parameters.AddWithValue("@IsActive", objbo.IsActive);
                command.Parameters.AddWithValue("@AddedBy", objbo.AddedBy);
                command.Parameters.AddWithValue("@AddedDate", objbo.AddedDate);
                command.Parameters.AddWithValue("@ModifyBy", objbo.ModifyBy);
                command.Parameters.AddWithValue("@ModifyDate", objbo.ModifyDate);
                command.Parameters.AddWithValue("@IsDelete", objbo.IsDelete);
                command.Parameters.AddWithValue("@IPAddress", objbo.IPAddress);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
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
        protected bool Delete(HomePageContentBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_HomePageContent_Delete";
                command.Parameters.AddWithValue("@Home_ID", objbo.Home_ID);
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
        protected DataTable SelectById(HomePageContent_DetailsBO objbo)
        {
            try
            {
                DataSet dt = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_HomePageContent_SelectById";
                command.Parameters.AddWithValue("@ID", objbo.LanguageID);
                dt = ExecuteQuery(command);
                return dt.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataTable SelectReadmore()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_HomePageContent_Readmore";
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Select All method  
        protected DataTable SelectAll(int languageId)
        {
            try
            {
                DataSet dt = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_HomePageContent_SelectAll";
                command.Parameters.AddWithValue("@LanguageID", languageId);
                dt = ExecuteQuery(command);
                return dt.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }
}
