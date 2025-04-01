using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class EMCSDAL : DBConnection
    {
        protected bool Insert(EMCSBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_EMCSMaster_Insert";
                command.Parameters.AddWithValue("@EMCSName", objbo.EMCSName);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@IsStatistics", objbo.IsStatistics);
                command.Parameters.AddWithValue("@StatisticsId", objbo.StatisticsId);
                command.Parameters.AddWithValue("@EMCSDescription", objbo.EMCSDescription);
                command.Parameters.AddWithValue("@EMCSLevelId", objbo.EMCSLevelId);
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
        protected bool Update(EMCSBO objbo, DataTable dt)
        {
            try
            {
                int? EId = objbo.EId;
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_EMCSMaster_Update";
                command.Parameters.AddWithValue("@EId", objbo.EId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@EMCSName", objbo.EMCSName);
                command.Parameters.AddWithValue("@IsStatistics", objbo.IsStatistics);
                command.Parameters.AddWithValue("@StatisticsId", objbo.StatisticsId);
                command.Parameters.AddWithValue("@EMCSDescription", objbo.EMCSDescription);
                command.Parameters.AddWithValue("@EMCSLevelId", objbo.EMCSLevelId);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                int rowaffect =    ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());

                int rowaffect2 = 0;
                foreach (DataColumn col in dt.Columns)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        SqlCommand newcmd = new SqlCommand();
                        newcmd.CommandType = CommandType.StoredProcedure;
                        newcmd.CommandText = "PROC_EMCSMasterImages_Insert";
                        newcmd.Parameters.AddWithValue("@EId", EId);
                        newcmd.Parameters.AddWithValue("@ImgURL", Convert.ToString(row["ImgURL"]));
                        rowaffect2 = ExecuteNonQuery(newcmd);
                    }
                }
                if (rowaffect > 0 && rowaffect2 >0) return true; else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(EMCSBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_EMCSMaster_Delete";
                command.Parameters.AddWithValue("@EId", objbo.EId);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(EMCSBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_EMCSMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@EId", objbo.EId);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_EMCSMasterOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@EId", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet SelectSequenceNo()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_SequenceNo_EMCS";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectSidemenu(int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_EMCS_Selectall";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet SelectFacilityInECMS(int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacilityInECMSDetails";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
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
