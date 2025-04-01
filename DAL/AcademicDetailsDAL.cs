using System;
using System.Data;
using System.Data.SqlClient;
using BO;

namespace DAL
{
    public class AcademicDetailsDAL:DBConnection
    {
        protected bool InsertAcademicDetails(AcademicDetailsBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertAcademicDetailsLanguage";
                command.Parameters.AddWithValue("@LanguageID", objbo.LanguageID);
                command.Parameters.AddWithValue("@CanRead", objbo.CanRead);
                command.Parameters.AddWithValue("@CanSpeak", objbo.CanSpeak);
                command.Parameters.AddWithValue("@Canwrite", objbo.Canwrite);
                command.Parameters.AddWithValue("@Advertisementid", objbo.Advertisementid);
                command.Parameters.AddWithValue("@CandidateId", objbo.CandidateId);
                command.Parameters.AddWithValue("@RegisrationId", objbo.RegisrationId);
                command.Parameters.AddWithValue("@Userid", objbo.Userid);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet SelectResource()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_RecruitmentLanguageMaster_SelectAll";
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAllAcademicDetails(AcademicDetailsBO objBO)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllAcademicDetails";
                command.Parameters.AddWithValue("@CandidateId", objBO.CandidateId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(AcademicDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AcademicDetailsMaster_Delete";
                command.Parameters.AddWithValue("@CandidateId", objbo.CandidateId);
                //CreateParameters(objbo, ref command, "PROC_AcademicDetailsMaster_Delete");
                //command.Parameters.AddWithValue("@CandidateId", objbo.CandidateId);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(AcademicDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AcademicDetailsMaster_Select";
                command.Parameters.AddWithValue("@id", objbo.id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectMenuRecord(AcademicDetailsBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_SelectLanguage_CandidateId";
                command.Parameters.AddWithValue("@CandidateId", objbo.CandidateId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectEducationType()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllEducationTypeMaster";
                //command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelecteducationName()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllRecruitmentEducationMaster";
                //command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectEducation(AcademicDetailsBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllRecruitmentAdvertisementEducationTypeDetailsByAddId";
                command.Parameters.AddWithValue("@AddId", objbo.Advertisementid);
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
