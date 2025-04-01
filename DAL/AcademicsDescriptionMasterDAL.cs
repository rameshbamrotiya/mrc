using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class AcademicsDescriptionMasterDAL : DBConnection
    {
        protected bool InsertAcademicsDescription(AcademicsDescriptionMasterBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AcademicsDescriptionMaster_Insert";
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@ParamedicalMainDescription", objbo.ParamedicalMainDescription);
                command.Parameters.AddWithValue("@MedicalMainDescription", objbo.MedicalMainDescription);
                command.Parameters.AddWithValue("@ParamedicalInnerDescription", objbo.ParamedicalInnerDescription);
                command.Parameters.AddWithValue("@MedicalInnerDescription", objbo.MedicalInnerDescription);
                command.Parameters.AddWithValue("@AlumniVisible", objbo.AlumniVisible);
                command.Parameters.AddWithValue("@enabled", objbo.Is_active);
                command.Parameters.AddWithValue("@added_by", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdateAcademicsDescription(AcademicsDescriptionMasterBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AcademicsDescriptionMaster_Update";
                command.Parameters.AddWithValue("@AcademicsId", objbo.AcademicsId);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@ParamedicalMainDescription", objbo.ParamedicalMainDescription);
                command.Parameters.AddWithValue("@MedicalMainDescription", objbo.MedicalMainDescription);
                command.Parameters.AddWithValue("@ParamedicalInnerDescription", objbo.ParamedicalInnerDescription);
                command.Parameters.AddWithValue("@MedicalInnerDescription", objbo.MedicalInnerDescription);
                command.Parameters.AddWithValue("@enabled", objbo.Is_active);
                command.Parameters.AddWithValue("@AlumniVisible", objbo.AlumniVisible);
                command.Parameters.AddWithValue("@modified_by", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet GetAllAcademicsDescriptionMasterDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllAcademicsDescriptionMasterDetails";                
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetAllAcademicsDescriptionByLangaugeId(AcademicsDescriptionMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAcademicsDescriptionMasterLanguageWise";
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetAcademicsDescriptionDetailsByID(AcademicsDescriptionMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAcademicsDescriptionMasterById";
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                command.Parameters.AddWithValue("@AcademicsId", objbo.AcademicsId);                         
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(AcademicsDescriptionMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AcademicsDescriptionMaster_Delete";
                command.Parameters.AddWithValue("@AcademicsId", objbo.AcademicsId);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
