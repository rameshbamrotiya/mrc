using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class CovidCareMasterDAL : DBConnection
    {
        protected bool Insert(CovidCareMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CovidCareMaster_Insert";
                command.Parameters.AddWithValue("@Language_Id", objbo.Language_Id);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@ImageUploadPath", objbo.ImageUploadPath);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@Left_Link_Video_Upload", objbo.Left_Link_Video_Upload);
                command.Parameters.AddWithValue("@LeftVideoPath", objbo.LeftVideoPath);
                command.Parameters.AddWithValue("@LeftVideoThumbnailPath", objbo.LeftVideoThumbnailPath);
                command.Parameters.AddWithValue("@Right_Link_Video_Upload", objbo.Right_Link_Video_Upload);
                command.Parameters.AddWithValue("@RightVideoPath", objbo.RightVideoPath);
                command.Parameters.AddWithValue("@RightVideoThumbnailPath", objbo.RightVideoThumbnailPath);
                command.Parameters.AddWithValue("@FAQsTitle", objbo.FAQsTitle);
                command.Parameters.AddWithValue("@FAQsImageUploadPath", objbo.FAQsImageUploadPath);
                command.Parameters.AddWithValue("@FAQsAccreditationTitle", objbo.FAQsAccreditationTitle);
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
        protected bool Update(CovidCareMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CovidCareMaster_Update";
                command.Parameters.AddWithValue("@CovidCare_Id", objbo.CovidCare_Id);
                command.Parameters.AddWithValue("@Language_Id", objbo.Language_Id);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@ImageUploadPath", objbo.ImageUploadPath);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@Left_Link_Video_Upload", objbo.Left_Link_Video_Upload);
                command.Parameters.AddWithValue("@LeftVideoPath", objbo.LeftVideoPath);
                command.Parameters.AddWithValue("@LeftVideoThumbnailPath", objbo.LeftVideoThumbnailPath);
                command.Parameters.AddWithValue("@Right_Link_Video_Upload", objbo.Right_Link_Video_Upload);
                command.Parameters.AddWithValue("@RightVideoPath", objbo.RightVideoPath);
                command.Parameters.AddWithValue("@RightVideoThumbnailPath", objbo.RightVideoThumbnailPath);
                command.Parameters.AddWithValue("@FAQsTitle", objbo.FAQsTitle);
                command.Parameters.AddWithValue("@FAQsImageUploadPath", objbo.FAQsImageUploadPath);
                command.Parameters.AddWithValue("@FAQsAccreditationTitle", objbo.FAQsAccreditationTitle);
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
        protected bool Delete(CovidCareMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CovidCareMaster_Delete";
                command.Parameters.AddWithValue("@CovidCare_Id", objbo.CovidCare_Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(CovidCareMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CovidCareMaster_Select";
                command.Parameters.AddWithValue("@CovidCare_Id", objbo.CovidCare_Id);
                command.Parameters.AddWithValue("@Language_Id", objbo.Language_Id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetCovidCareDetailsByLanguageId(CovidCareMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetCovidCareMasterDetailsByLanguageId";
                command.Parameters.AddWithValue("@Language_Id", objbo.Language_Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertAccredation(CovidCareAccredationDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CovidCareAccredationDetails_Insert";
                command.Parameters.AddWithValue("@CovidCareDetails_Id", objbo.CovidCareDetails_Id);
                command.Parameters.AddWithValue("@Language_id", objbo.Language_id);
                command.Parameters.AddWithValue("@AccredationSubTitle", objbo.AccredationSubTitle);
                command.Parameters.AddWithValue("@AccredationDescription", objbo.AccredationDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@Added_by", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@Accredation_level_id", objbo.Accredation_level_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateAccredation(CovidCareAccredationDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CovidCareAccredationDetails_Update";
                command.Parameters.AddWithValue("@Accredation_Id", objbo.Accredation_Id);
                command.Parameters.AddWithValue("@Language_id", objbo.Language_id);
                command.Parameters.AddWithValue("@AccredationSubTitle", objbo.AccredationSubTitle);
                command.Parameters.AddWithValue("@AccredationDescription", objbo.AccredationDescription);
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
        protected bool DeleteAccredation(CovidCareAccredationDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CovidCareAccredationDetails_Delete";
                command.Parameters.AddWithValue("@Accredation_Id", objbo.Accredation_Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAccredation(CovidCareAccredationDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CovidCareAccredationDetails_Select";
                command.Parameters.AddWithValue("@CovidCareDetails_Id", objbo.CovidCareDetails_Id);
                command.Parameters.AddWithValue("@Language_id", objbo.Language_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetAccredationDetailsByAccredationId(CovidCareAccredationDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllCovidCareAccredationDetails";
                command.Parameters.AddWithValue("@Accredation_Id", objbo.Accredation_Id);
                command.Parameters.AddWithValue("@Language_id", objbo.Language_id);
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
                command.CommandText = "PROC_CovidCareAccredationDetailsOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@Accredation_Id", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
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
                command.CommandText = "Get_SequenceNo_CovidCareAccredationDetails";
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
