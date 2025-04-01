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
    public class ConfigUnitSlotDetailDAL : DBConnection
    {
        protected bool Insert(ConfigUnitSlotDetailMasterBO objbo)
        {
            try
            {
                //SqlCommand command = new SqlCommand();
                //CreateParameters(objbo, ref command, "InsertOrUpdateConfigUnitSlotDetail");
                //// command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                //ExecuteNonQuery(command);
                ////  objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());

                ////if (objbo.IsExist > 0) return false; else 
                //return true;
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateConfigUnitSlotDetail";
                command.Parameters.AddWithValue("@Id", 0);
                command.Parameters.AddWithValue("@WeekNo", objbo.WeekNo);
                command.Parameters.AddWithValue("@UnitId", objbo.Id);
                command.Parameters.AddWithValue("@SloteName", objbo.SloteName);
                command.Parameters.AddWithValue("@StartTimeHour", objbo.StartTimeHour);
                command.Parameters.AddWithValue("@StartTimeMin", objbo.StartTimeMin);
                command.Parameters.AddWithValue("@StartTimeTT", objbo.StartTimeTT);
                command.Parameters.AddWithValue("@EndTimeHour", objbo.EndTimeHour);
                command.Parameters.AddWithValue("@EndTimeMin", objbo.EndTimeMin);
                command.Parameters.AddWithValue("@EndTimeTT", objbo.EndTimeTT);
                command.Parameters.AddWithValue("@MaxSlot", objbo.maxSlot);
                command.Parameters.AddWithValue("@Username", objbo.Username);
                command.Parameters.AddWithValue("@IsActive", objbo.IsActive);
                //command.Parameters.AddWithValue("@Id", objbo.Id).Direction = ParameterDirection.Output;
                var daataset = ExecuteQuery(command);
                //if (daataset != null)
                //{
                //    if (daataset.Tables[0].Rows.Count > 0)
                //    {
                //        objbo.Id = Convert.ToInt32(daataset.Tables[0].Rows[0][0]);
                //        return true;
                //    }
                //}
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Update(ConfigUnitSlotDetailMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "InsertOrUpdateConfigUnitSlotDetail");
                //  command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                //  objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                // if (objbo.IsExist > 0) return false; else 
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(long? Id)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ConfigUnitSlotDetail_Delete";
                command.Parameters.AddWithValue("@Id", Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(ConfigUnitSlotDetailMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllConfigUnitSlotDetail_Select";
                command.Parameters.AddWithValue("@banner_id", objbo.Id);

                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataTable GetALL_ConfigUnitDetails()
        {
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("PROC_GetAllConfigUnitSlotDetail");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

    }
}
