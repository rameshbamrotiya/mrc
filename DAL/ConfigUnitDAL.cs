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
    public class ConfigUnitDAL : DBConnection
    {
        protected bool UpdateAppoinment(int patientId, string strUserName)
        {
            try
            {
                //  SqlCommand command = new SqlCommand();
                //  CreateParameters(objbo, ref command, "InsertOrUpdateConfigUnit");
                // // command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                //  ExecuteNonQuery(command);
                ////  objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());

                //  //if (objbo.IsExist > 0) return false; else 
                //      return true;

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdatePatientVisitInMaster";
                command.Parameters.AddWithValue("@PatientId", patientId);
                command.Parameters.AddWithValue("@UpdateBy", strUserName);
                //command.Parameters.AddWithValue("@Id", objbo.Id).Direction = ParameterDirection.Output;
                ExecuteQuery(command);
                
                        return true;
                //objbo.Id = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                
            }
            catch (Exception)
            {
                throw; return false;
            }
        }

        protected bool Insert(ref ConfigUnitMasterBO objbo)
        {
            try
            {
                //  SqlCommand command = new SqlCommand();
                //  CreateParameters(objbo, ref command, "InsertOrUpdateConfigUnit");
                // // command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                //  ExecuteNonQuery(command);
                ////  objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());

                //  //if (objbo.IsExist > 0) return false; else 
                //      return true;

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateConfigUnit";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@SpecilizationId", objbo.SpecilizationId);
                command.Parameters.AddWithValue("@UnitName", objbo.UnitName);
                command.Parameters.AddWithValue("@Username", objbo.Username);
                command.Parameters.AddWithValue("@IsActive", objbo.IsActive);
                //command.Parameters.AddWithValue("@Id", objbo.Id).Direction = ParameterDirection.Output;
                var daataset=ExecuteQuery(command);
                if(daataset != null)
                {
                    if (daataset.Tables[0].Rows.Count > 0) {
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
        protected bool Update(ConfigUnitMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "InsertOrUpdateConfigUnit");
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
        protected bool Delete(ConfigUnitMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ConfigUnit_Delete";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(ConfigUnitMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ConfigUnit_Select";
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
            cmd = new SqlCommand("PROC_GetAllConfigUnitDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        protected DataSet SelectSloteRecord(ConfigSloteDetailsByidforEdit objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllConfigUnitSloteMasterById";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@LangId", 1);

                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }


        protected DataSet SelectAppoinment(AppoinmentDetaiBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAppoinmentDetailList";
                command.Parameters.AddWithValue("@UnitId", objbo.UnitId ?? 0);
                command.Parameters.AddWithValue("@DoctorId", objbo.DoctorId ?? 0);
                command.Parameters.AddWithValue("@Deptid", objbo.deptid ?? 0);
                command.Parameters.AddWithValue("@fromdate", objbo.FromDate ?? "");
                command.Parameters.AddWithValue("@toDate", objbo.ToDate ?? "");

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
