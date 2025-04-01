using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class PatientCareMasterDAL : DBConnection
    {
        protected bool Insert(PatientCareMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareMaster_Insert";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@TabName", objbo.TabName);
                command.Parameters.AddWithValue("@TabType", objbo.TabType);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@PatientCare_level_id", objbo.PatientCare_level_id);
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
        protected bool Update(PatientCareMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareMaster_Update";
                command.Parameters.AddWithValue("@PatientCare_id", objbo.PatientCare_id);
                command.Parameters.AddWithValue("LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@TabName", objbo.TabName);
                command.Parameters.AddWithValue("@TabType", objbo.TabType);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
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
        protected bool Delete(PatientCareMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareMaster_Delete";
                command.Parameters.AddWithValue("@PatientCare_id", objbo.PatientCare_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(PatientCareMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareMaster_Select";
                command.Parameters.AddWithValue("@PatientCare_id", objbo.PatientCare_id);
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
        protected DataSet GetTabTypeByLanguageId(PatientCareMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetTabTypeByLanguageId";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Insert(PatientCareGeneralDetailsBO objbo, DataTable dt)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareGeneralDetails_Insert";
                command.Parameters.AddWithValue("@TabTypeId", objbo.TabTypeId);
                command.Parameters.AddWithValue("@SubTabId", objbo.SubTabId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@TabDescription", objbo.TabDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@dtGeneral", dt);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool Update(PatientCareGeneralDetailsBO objbo, DataTable dt)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareGeneralDetails_Update";
                command.Parameters.AddWithValue("@GeneralDetailsId", objbo.GeneralDetailsId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Description", objbo.TabDescription);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@dtGeneral", dt);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet GetPatintGeneralDetailsById(PatientCareGeneralDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetPatintGeneralDetailsById";
                command.Parameters.AddWithValue("@TabTypeId", objbo.TabTypeId);
                command.Parameters.AddWithValue("@SubTabId", objbo.SubTabId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetPatintGeneralImageDetailsById(PatientCareGeneralDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetPatintGeneralImageDetailsById";
                command.Parameters.AddWithValue("@GeneralDetailsId", objbo.GeneralDetailsId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetFormTypeById(int Id,int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetFormTypeById";
                command.Parameters.AddWithValue("@CategoryID", Id);
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteGeneralDetailsById(PatientCareGeneralDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareGeneralDetails_Delete";
                command.Parameters.AddWithValue("@GeneralDetailsId", objbo.GeneralDetailsId);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetGeneralDetailsById(PatientCareGeneralDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetPatintGeneralDetailsById";
                command.Parameters.AddWithValue("@TabTypeId", objbo.TabTypeId);
                command.Parameters.AddWithValue("@SubTabId", objbo.SubTabId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertBrochureDetails(PatientCareBrochureDetailsBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareBrochureDetails_Insert";
                command.Parameters.AddWithValue("@TabTypeId", objbo.TabTypeId);
                command.Parameters.AddWithValue("@SubTabId", objbo.SubTabId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@ImagePath", objbo.ImagePath);
                command.Parameters.AddWithValue("@FileUploadPath", objbo.FileUploadPath);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
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
        protected bool UpdateBrochureDetails(PatientCareBrochureDetailsBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareBrochureDetails_Update";
                command.Parameters.AddWithValue("@BrochureId", objbo.BrochureId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@ImagePath", objbo.ImagePath);
                command.Parameters.AddWithValue("@FileUploadPath", objbo.FileUploadPath);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool DeleteBrochureDetails(PatientCareBrochureDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareBrochureDetails_Delete";
                command.Parameters.AddWithValue("@BrochureId", objbo.BrochureId);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectBrochureDetails(PatientCareBrochureDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareBrochureDetails_Select";
                command.Parameters.AddWithValue("@TabTypeId", objbo.TabTypeId);
                command.Parameters.AddWithValue("@SubTabId", objbo.SubTabId);
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
        protected DataSet GetPatintCareBrochureDetailsById(PatientCareBrochureDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetPatientCareBrochureDetailsById";
                command.Parameters.AddWithValue("@BrochureId", objbo.BrochureId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool InsertLeftRightContainDetails(PatientCareLeftRightContainDetailsBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareLeftRightContainDetails_Insert";
                command.Parameters.AddWithValue("@TabTypeId", objbo.TabTypeId);
                command.Parameters.AddWithValue("@SubTabId", objbo.SubTabId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@TabDescription", objbo.TabDescription);
                command.Parameters.AddWithValue("@ImagePath", objbo.ImagePath);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
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
        protected bool UpdateLeftRightContainDetails(PatientCareLeftRightContainDetailsBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareLeftRightContainDetails_Update";
                command.Parameters.AddWithValue("@LeftRightContainId", objbo.LeftRightContainId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Description", objbo.TabDescription);
                command.Parameters.AddWithValue("@ImagePath", objbo.ImagePath);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool DeleteLeftRightContainDetails(PatientCareLeftRightContainDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareLeftRightContainDetails_Delete";
                command.Parameters.AddWithValue("@LeftRightContainId", objbo.LeftRightContainId);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectLeftRightContainDetails(PatientCareLeftRightContainDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetPatientCareLeftRightContainDetails";
                command.Parameters.AddWithValue("@TabTypeId", objbo.TabTypeId);
                command.Parameters.AddWithValue("@SubTabId", objbo.SubTabId);
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
        protected DataSet GetPatintCareLeftRightContainDetailsById(PatientCareLeftRightContainDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetPatientCareLeftRightContainDetailsById";
                command.Parameters.AddWithValue("@LeftRightContainId", objbo.LeftRightContainId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }        
        protected DataTable GetTabListByTabTypeId(long TabId)
        {
            try
            {

                DataTable ds = new DataTable();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetTabListByTabTypeId";
                command.Parameters.AddWithValue("@TabTypeId", TabId);
                ds = ExecuteQuery(command).Tables[0];

                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetSubTabGeneralDetailsById(PatientCareGeneralDetailsBO objBO)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetSubTabGeneralDetailsById";
                command.Parameters.AddWithValue("@TabTypeId", objBO.TabTypeId);
                command.Parameters.AddWithValue("@SubTabId", objBO.SubTabId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetBrochureDetailsById(PatientCareGeneralDetailsBO objBO)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetBrochureDetailsById";
                command.Parameters.AddWithValue("@TabTypeId", objBO.TabTypeId);
                command.Parameters.AddWithValue("@SubTabId", objBO.SubTabId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetLeftRightContainDetailsById(PatientCareLeftRightContainDetailsBO objBO)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetLeftRightContainDetailsById";
                command.Parameters.AddWithValue("@TabTypeId", objBO.TabTypeId);
                command.Parameters.AddWithValue("@SubTabId", objBO.SubTabId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetFloorDetailsList()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetFloorDetailsList";
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
                command.CommandText = "PROC_PatientCareDetailsOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@PatientCare_id", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
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
                command.CommandText = "Get_SequenceNo_PatientCareDetails";
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
