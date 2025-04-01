using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class CountryDAL:DBConnection
    {
        protected bool Insert(CountryBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CountryMaster_Insert";
                command.Parameters.AddWithValue("@Country_Name", objbo.Country_Name);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
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
        protected bool Update(CountryBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CountryMaster_Update";
                command.Parameters.AddWithValue("@Country_id", objbo.Country_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Country_Name", objbo.Country_Name);
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
        protected bool Delete(CountryBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CountryMaster_Delete";
                command.Parameters.AddWithValue("@Country_id", objbo.Country_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(CountryBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CountryMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Country_id", objbo.Country_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectCountry_Code()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CountryCode_Select";
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectTaluka_Code(int District_id,int LangId)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllTalukaLangId";
                command.Parameters.AddWithValue("@District_id", District_id);
                command.Parameters.AddWithValue("@LangId", LangId);
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
