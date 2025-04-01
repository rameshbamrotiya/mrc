using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class VisitorsMasterDAL : DBConnection
    {
        protected bool Insert(VisitorsMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateVisitorsMaster";
                command.Parameters.AddWithValue("@VisitorId", objbo.VisitorId);
                command.Parameters.AddWithValue("@VisitingHoursDesc", objbo.VisitingHoursDesc);
                command.Parameters.AddWithValue("@DDDescription", objbo.DDDescription);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@UserName", objbo.user_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertFacilities(VisitorsMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateVisitorsMasterFacilitiesDetails";
                command.Parameters.AddWithValue("@VisitorId", objbo.VisitorId);
                command.Parameters.AddWithValue("@Img_id", objbo.Img_id);
                command.Parameters.AddWithValue("@ImgTitle", objbo.ImgTitle);
                command.Parameters.AddWithValue("@ImgPOPUpDesc", objbo.ImgPOPUpDesc);
                command.Parameters.AddWithValue("@Iconpath", objbo.Iconpath);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_activeImg);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(long lgLangId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllVisitorsMaster";
                command.Parameters.AddWithValue("@LangId", lgLangId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected DataSet SelectFacility(long VisitorId, long lgLangId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllByVisitorsMasterFacilitiesDetailsDetail";
                command.Parameters.AddWithValue("@LangId", lgLangId);
                command.Parameters.AddWithValue("@VisitorId", VisitorId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected DataSet SelectFacilityEdit(long Id, long lgLangId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllByVisitorsMasterFacilitiesDetailsDetailEdit";
                command.Parameters.AddWithValue("@LangId", lgLangId);
                command.Parameters.AddWithValue("@Id", Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected bool Delete(long Id,int UserId)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveVisitorsMasterFacilitiesDetails";
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@UserName", UserId);
                ExecuteQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectFacilityFront(long lgLangId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllByVisitorsMasterFacilitiesDetailsByLanguage";
                command.Parameters.AddWithValue("@LangId", lgLangId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
