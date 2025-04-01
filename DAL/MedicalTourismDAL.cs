using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class MedicalTourismDAL:DBConnection
    {
        protected bool Insert(MedicalTourismBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismMaster_Insert";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@MTDescription", objbo.MTDescription);
                command.Parameters.AddWithValue("@MTInnerImgpath", objbo.MTInnerImgpath);
                command.Parameters.AddWithValue("@MTInnerVideolink", objbo.MTInnerVideolink);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Update(MedicalTourismBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismMaster_Update";
                command.Parameters.AddWithValue("@MT_Id", objbo.MT_Id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@MTDescription", objbo.MTDescription);
                command.Parameters.AddWithValue("@MTInnerImgpath", objbo.MTInnerImgpath);
                command.Parameters.AddWithValue("@MTInnerVideolink", objbo.MTInnerVideolink);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(MedicalTourismBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismMaster_Delete";
                command.Parameters.AddWithValue("@MT_Id", objbo.MT_Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(MedicalTourismBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismMaster_Select";
                command.Parameters.AddWithValue("@MT_Id", objbo.MT_Id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertMTDoc(MedicalTourismDocumentBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismDocumentDetails_Insert";
                command.Parameters.AddWithValue("@MTDetails_Id", objbo.MTDetails_Id);
                command.Parameters.AddWithValue("@Language_id", objbo.Language_id);
                command.Parameters.AddWithValue("@DocumentTitle", objbo.DocumentTitle);
                command.Parameters.AddWithValue("@DocPath", objbo.DocPath);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@Added_by", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateMTDoc(MedicalTourismDocumentBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismDocumentDetails_Update";
                command.Parameters.AddWithValue("@MTDOC_Id", objbo.MTDOC_Id);
                command.Parameters.AddWithValue("@MTDetails_Id", objbo.MTDetails_Id);
                command.Parameters.AddWithValue("@Language_id", objbo.Language_id);
                command.Parameters.AddWithValue("@DocumentTitle", objbo.DocumentTitle);
                command.Parameters.AddWithValue("@DocPath", objbo.DocPath);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@Added_by", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteMTDoc(MedicalTourismDocumentBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismDocumentDetails_Delete";
                command.Parameters.AddWithValue("@MTDOC_Id", objbo.MTDOC_Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectMTDoc(MedicalTourismDocumentBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismDocumentDetails_Select";
                command.Parameters.AddWithValue("@MTDetails_Id", objbo.MTDetails_Id);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetMTDocDetailsByMTDocId(MedicalTourismDocumentBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllMedicalTourismDocumentDetails";
                command.Parameters.AddWithValue("@MTDOC_Id", objbo.MTDOC_Id);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool InsertFacilities(MedicalTourismFacilitiesBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismFacilitiesMaster_Insert";
                command.Parameters.AddWithValue("@Name", objbo.Name);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@Doc_path", objbo.Doc_path);
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
        protected bool UpdateFacilities(MedicalTourismFacilitiesBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismFacilitiesMaster_Update";
                command.Parameters.AddWithValue("@MTF_id", objbo.MTF_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Name", objbo.Name);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@Doc_path", objbo.Doc_path);
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
        protected bool DeleteFacilities(MedicalTourismFacilitiesBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismFacilitiesMaster_Delete";
                command.Parameters.AddWithValue("@MTF_id", objbo.MTF_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectFacilities(MedicalTourismFacilitiesBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismFacilitiesMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@MTF_id", objbo.MTF_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAccrodianSubFront(int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismAccordion_Front";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetIntroductionDetailsByLanguageId(MedicalTourismDocumentBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetMedicalTourismMasterMasterDetailsByLanguageId";
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectDocument(MedicalTourismDocumentBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetMedicalTourismDocumentDetails_Select";
                command.Parameters.AddWithValue("@MTDetails_Id", objbo.MTDetails_Id);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetFacilitiesDetailsByLanguageId(MedicalTourismDocumentBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetMedicalTourismFacilitiesMasterDetailsByLanguageId";
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
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
