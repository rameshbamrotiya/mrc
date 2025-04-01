using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class PostApplicationMasterDAL : DBConnection
    {
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        protected bool Insert(PostApplicationMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "InsertUpdate_tbl_PostApplication_Master");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected DataSet Select(int Id, string spName)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                command.Parameters.AddWithValue("@Id", Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool InsertQualification(QualificationMasterBo objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "InsertUpdate_tbl_EducationDetailMaster");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool InsertJobApplication(JobApplicationMasterBo objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "InsertUpdate_tblJobMaster");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool Delete(int Id, string spName)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                command.Parameters.AddWithValue("@Id", Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetAllQualificationDetails(int IsEducation)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllQualificationDetails";
                command.Parameters.AddWithValue("@IsPostGraduate", IsEducation);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetAllPostJobApplication(int JobId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllJobApplication";
                command.Parameters.AddWithValue("@Id", JobId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetAllPostMasterDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllPostApplication_Master";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetQualification(string spName)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
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
