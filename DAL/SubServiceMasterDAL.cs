using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SubServiceMasterDAL:DBConnection
    {
        protected bool Insert(SubServiceMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_SubSpecialityMaster_Insert";
                
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Img_path", objbo.Img_path);
                command.Parameters.AddWithValue("@OS_id", objbo.OS_id);
                command.Parameters.AddWithValue("@Description", objbo.Description);
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
        protected bool Update(SubServiceMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_SubSpecialityMaster_Update";
                command.Parameters.AddWithValue("@Speciality_id", objbo.Speciality_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);                
                command.Parameters.AddWithValue("@Img_path", objbo.Img_path);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@OS_id", objbo.OS_id);
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
        protected bool Delete(SubServiceMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_SubServiceMaster_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(SubServiceMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_SubSpecialityMaster_Select";
                command.Parameters.AddWithValue("@Speciality_id", objbo.Speciality_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectOtherSpeciality(SubServiceMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllOtherSpeciality";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
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
