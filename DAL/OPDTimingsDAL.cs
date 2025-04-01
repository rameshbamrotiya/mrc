using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class OPDTimingsDAL:DBConnection
    {
        protected bool Insert(OPDTimingsBO objbo, DataTable dt,DataTable dtunit)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OPDTimingsMaster_Insert";
                command.Parameters.AddWithValue("@OPD_Name", objbo.OPDName);
                command.Parameters.AddWithValue("@DepartmentId", objbo.DepartmentId);
                command.Parameters.AddWithValue("@UnitName", objbo.UnitName);
                command.Parameters.AddWithValue("@UnitId", objbo.UnitId);
                command.Parameters.AddWithValue("@Week", objbo.Week);
                command.Parameters.AddWithValue("@StartTimeHH", objbo.StartTimeHH);
                command.Parameters.AddWithValue("@StartTimeMM", objbo.StartTimeMM);
                command.Parameters.AddWithValue("@StartTimeTT", objbo.StartTimeTT);
                command.Parameters.AddWithValue("@EndTimeHH", objbo.EndTimeHH);
                command.Parameters.AddWithValue("@EndTimeMM", objbo.EndTimeMM);
                command.Parameters.AddWithValue("@EndTimeTT", objbo.EndTimeTT);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_Active", objbo.IsActive);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@dtimg", dt);
                command.Parameters.AddWithValue("@dtunit", dtunit);
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
        protected bool Update(OPDTimingsBO objbo, DataTable dt, DataTable dtunit)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OPDTimingsMaster_Update";
                command.Parameters.AddWithValue("@OPD_id", objbo.OPD_id);
                command.Parameters.AddWithValue("@OPD_Name", objbo.OPDName);
                command.Parameters.AddWithValue("@DepartmentId", objbo.DepartmentId);
                command.Parameters.AddWithValue("@UnitName", objbo.UnitName);
                command.Parameters.AddWithValue("@UnitId", objbo.UnitId);
                command.Parameters.AddWithValue("@Week", objbo.Week);
                command.Parameters.AddWithValue("@StartTimeHH", objbo.StartTimeHH);
                command.Parameters.AddWithValue("@StartTimeMM", objbo.StartTimeMM);
                command.Parameters.AddWithValue("@StartTimeTT", objbo.StartTimeTT);
                command.Parameters.AddWithValue("@EndTimeHH", objbo.EndTimeHH);
                command.Parameters.AddWithValue("@EndTimeMM", objbo.EndTimeMM);
                command.Parameters.AddWithValue("@EndTimeTT", objbo.EndTimeTT);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_Active", objbo.IsActive);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@dtimg", dt);
                command.Parameters.AddWithValue("@dtunit", dtunit);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected DataSet GetDoctorListLanguageWise(OPDTimingsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetDoctorLanguageWise";
                command.Parameters.AddWithValue("@Language_id", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetUnitListLanguageWise(OPDTimingsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetUnitLanguageWise";
                command.Parameters.AddWithValue("@Language_id", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(OPDTimingsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_OPDMaster_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(OPDTimingsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_OPDMaster_Select");
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectDoctor(OPDTimingsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_OPDMasterDoctor_Select");
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectUnit(OPDTimingsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_OPDMasterUnit_Select");
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteDoctor(OPDTimingsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_OPDDoctor_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteUnit(OPDTimingsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_OPDUnit_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet selectGetFacultylistforUnitConfig(facultyNameForUnitConfig objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllFacultyDetailsForUnitConfigur";
                command.Parameters.AddWithValue("@Language_id", objbo.LanguageId);
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
