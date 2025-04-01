using System;
using System.Data;
using System.Data.SqlClient;
using BO;

namespace DAL
{
    public class StudentReportDAL: DBConnection
    {
        protected DataSet SelectStudentMasterData()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStudentFinalData";               
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetApplicationStatus()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_Workflow_Select";
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetApplicationStatusWise(string ApplicationStatus, string PaymentStatus, string Course, string startdate, string enddate, string trastartdate, string traenddate)
        {
            try
            {               
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentListstatuswise";
                command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                command.Parameters.AddWithValue("@CourseId", Course);
                command.Parameters.AddWithValue("@startdate", startdate);
                command.Parameters.AddWithValue("@enddate", enddate);
                command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
                command.Parameters.AddWithValue("@Trastartdate", trastartdate);
                command.Parameters.AddWithValue("@Traenddate", traenddate);
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
