using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class RecruitmentTypeMasterDAL:DBConnection
    {
        protected bool Insert(RecruitmentTypeMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_RecruitmentTypeMaster_Insert";
                command.Parameters.AddWithValue("@Recruitment_Name", objbo.Recruitment_Name);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@isWalkin", objbo.isWalkin);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Update(RecruitmentTypeMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_RecruitmentTypeMaster_Update";
                command.Parameters.AddWithValue("@id", objbo.id);
                command.Parameters.AddWithValue("@isWalkin", objbo.isWalkin);
                command.Parameters.AddWithValue("@Recruitment_Name", objbo.Recruitment_Name);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(RecruitmentTypeMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_RecruitmentTypeMaster_Delete";
                command.Parameters.AddWithValue("@id", objbo.id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(RecruitmentTypeMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_RecruitmentTypeMaster_Select";
                command.Parameters.AddWithValue("@id", objbo.id);
                ExecuteNonQuery(command);
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
