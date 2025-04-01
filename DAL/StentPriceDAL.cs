using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class StentPriceDAL : DBConnection
    {
        protected bool Insert(StentPriceBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StentPriceTypeMaster_Insert";
                command.Parameters.AddWithValue("@StentPrice_Type", objbo.StentPrice_Type);
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
        protected bool Update(StentPriceBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StentPriceTypeMaster_Update";
                command.Parameters.AddWithValue("@StentPrice_id", objbo.StentPrice_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@StentPrice_Type", objbo.StentPrice_Type);
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
        protected bool Delete(StentPriceBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StentPriceTypeMaster_Delete";
                command.Parameters.AddWithValue("@StentPrice_id", objbo.StentPrice_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(StentPriceBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StentPriceTypeMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@StentPrice_id", objbo.StentPrice_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertStentPriceSub(StentPriceTypeSubMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StentPriceTypeSubMaster_Insert";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@NAMEOFMANUFACTURINGCOMPANY", objbo.NAMEOFMANUFACTURINGCOMPANY);
                command.Parameters.AddWithValue("@StentPriceType_id", objbo.StentPriceType_id);
                command.Parameters.AddWithValue("@BRANDNAME", objbo.BRANDNAME);
                command.Parameters.AddWithValue("@COSTOFCORONARYSTENT", objbo.COSTOFCORONARYSTENT);
                command.Parameters.AddWithValue("@NAMEOFDISTRIBUTOR", objbo.NAMEOFDISTRIBUTOR);
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
        protected bool UpdateStentPriceSub(StentPriceTypeSubMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StentPriceTypeSubMaster_Update";
                command.Parameters.AddWithValue("@AccordionSub_id", objbo.StentPriceSub_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@NAMEOFMANUFACTURINGCOMPANY", objbo.NAMEOFMANUFACTURINGCOMPANY);
                command.Parameters.AddWithValue("@StentPriceType_id", objbo.StentPriceType_id);
                command.Parameters.AddWithValue("@BRANDNAME", objbo.BRANDNAME);
                command.Parameters.AddWithValue("@COSTOFCORONARYSTENT", objbo.COSTOFCORONARYSTENT);
                command.Parameters.AddWithValue("@NAMEOFDISTRIBUTOR", objbo.NAMEOFDISTRIBUTOR);
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
        protected bool DeleteStentPriceSub(StentPriceTypeSubMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_StentPriceTypeSubMaster_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectStentPriceSub(StentPriceTypeSubMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StentPriceTypeSubMaster_Select";
                command.Parameters.AddWithValue("@StentPriceSub_id", objbo.StentPriceSub_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectStentPriceType(StentPriceTypeSubMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStentPriceTypeMaster";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectStentPriceSubFront(int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StentPriceTypeSubMaster_Front";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
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
