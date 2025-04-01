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
    public class ConfigUnitDoctorDetailDAL : DBConnection
    {
        protected bool Insert(ConfigUnitDoctorDetailMasterBO objbo)
        {
            try
            {
                //SqlCommand command = new SqlCommand();
                //CreateParameters(objbo, ref command, "InsertOrUpdateUnitDoctorDetail");
                //// command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                //ExecuteNonQuery(command);
                ////  objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());

                ////if (objbo.IsExist > 0) return false; else 
                //return true;
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateUnitDoctorDetail";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@DoctorId", objbo.DoctorId);
                command.Parameters.AddWithValue("@UnitId", objbo.UnitId);
                command.Parameters.AddWithValue("@Username", objbo.Username);
                command.Parameters.AddWithValue("@IsActive", objbo.IsActive);
                //command.Parameters.AddWithValue("@Id", objbo.Id).Direction = ParameterDirection.Output;
                var daataset = ExecuteQuery(command);
                if (daataset != null)
                {
                    if (daataset.Tables[0].Rows.Count > 0)
                    {
                        objbo.Id = Convert.ToInt32(daataset.Tables[0].Rows[0][0]);
                        return true;
                    }
                }
                //objbo.Id = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Update(ConfigUnitDoctorDetailMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "InsertOrUpdateUnitDoctorDetail");
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
                command.CommandText = "PROC_ConfigUnitDoctorDetail_Delete";
                command.Parameters.AddWithValue("@Id", Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(ConfigUnitDoctorDetailMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllConfigUnitDoctorDetail_Select";
                command.Parameters.AddWithValue("@Id", objbo.Id);

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
            cmd = new SqlCommand("PROC_GetAllConfigUnitDoctorDetail");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

    }
}
