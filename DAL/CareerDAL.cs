using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class CareerDAL : DBConnection
    {
        protected DataSet Select()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CareerCount_Select";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected DataTable GetAllCareerRecords()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllCareerRecords";
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected DataTable GetAllCareerRecordsNotification()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllCareerRecordsNotification";
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected DataSet SelectJoblist(int rid = 0)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CareerCountJob_Select";
                command.Parameters.AddWithValue("@id", rid);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected DataSet SelectJoblistsearch(string designation = "", string PostType = "", string RecruitmentType = "")
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CareerCountJobsearch_Select";
                command.Parameters.AddWithValue("@designation", designation);
                command.Parameters.AddWithValue("@PostType", PostType);
                command.Parameters.AddWithValue("@RecruitmentType", RecruitmentType);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected DataSet SelectJoblistwalkin(int rid = 0)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CareerCountJobwalkin_Select";
                command.Parameters.AddWithValue("@id", rid);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected DataSet SelectDetails(int Rid = 0)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_RecruitmentAdvertisementMasterDetails";
                command.Parameters.AddWithValue("@id", Rid);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Insert(UploadCVCareer objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CareerCVMaster_Insert";
                command.Parameters.AddWithValue("@FullName", objbo.FullName);
                command.Parameters.AddWithValue("@EmailId", objbo.EmailId);
                command.Parameters.AddWithValue("@Location", objbo.Location);
                command.Parameters.AddWithValue("@MobileNo", objbo.MobileNo);
                command.Parameters.AddWithValue("@Designation", objbo.Designation);
                command.Parameters.AddWithValue("@FilePath", objbo.FilePath);
                string result = ExecuteScalar(command).ToString();
                if (result == "1")
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataTable GetAllCareerCV(string startdate, string enddate)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllCareerCVFiles";
                command.Parameters.AddWithValue("@startdate", startdate);
                command.Parameters.AddWithValue("@enddate", enddate);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected DataTable GetAllCareerCVAll()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllCareerCVMaster";
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
