using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class MedicalTourismAccordionMasterDAL:DBConnection
    {
        protected bool Insert(MedicalTourismAccordionMasterBO objbo, DataTable dt)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismAccordionMaster_Insert";
                command.Parameters.AddWithValue("@MTA_level_id", objbo.MTA_level_id);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@dtMTA", dt);
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
        protected bool Update(MedicalTourismAccordionMasterBO objbo, DataTable dt)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismAccordionMaster_Update";
                command.Parameters.AddWithValue("@MTA_id", objbo.MTA_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@MTA_level_id", objbo.MTA_level_id);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@MTADetailsId", objbo.MTADetails_Id);
                command.Parameters.AddWithValue("@dtMTA", dt);
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
        protected bool Delete(MedicalTourismAccordionMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_MedicalTourismAccordionMaster_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(MedicalTourismAccordionMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismAccordionMaster_Select";
                command.Parameters.AddWithValue("@MTA_id", objbo.MTA_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet SelectSubMTADetails(MedicalTourismAccordionSubMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismAccordion_SubMaster_SelectByPackageDetails_Id";
                command.Parameters.AddWithValue("@MTADetails_Id", objbo.MTADetails_Id);
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
                command.CommandText = "tbl_MedicalTourismAccordionMasterOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@MTAid", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
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
                command.CommandText = "Get_SequenceNo_MedicalTourismAccordion";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet FillMTATypeId(int LangId, int TypeId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAlltbl_Package_Type_MasterById";
                command.Parameters.Add("@LangId", SqlDbType.Int).Value = LangId;
                command.Parameters.Add("@TypeId", SqlDbType.Int).Value = TypeId;
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetMTAType(int LangId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PackageTypeMaster_Select";
                command.Parameters.Add("@LangId", SqlDbType.Int).Value = LangId;
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //protected bool InsertSubMTA(MedicalTourismAccordionSubMasterBO objbo)
        //{
        //    try
        //    {
        //        SqlCommand command = new SqlCommand();
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = "PROC_MTASubMaster_Insert";
        //        command.Parameters.AddWithValue("@Id", objbo.Id);
        //        command.Parameters.AddWithValue("@MTADetails_Id", objbo.MTADetails_Id);
        //        command.Parameters.AddWithValue("@SubTitle", objbo.SubTitle);
        //        command.Parameters.AddWithValue("@Price", objbo.Price);
        //        command.Parameters.AddWithValue("@MTATypeId", objbo.MTAId);
        //        command.Parameters.AddWithValue("@Description", objbo.Description);
        //        command.Parameters.AddWithValue("@Img_path", objbo.Img_path);
        //        command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
        //        command.Parameters.AddWithValue("@user_id", objbo.user_id);
        //        command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
        //        command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
        //        ExecuteNonQuery(command);
        //        objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
        //        if (objbo.IsExist > 0) return false; else return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        protected bool UpdateSubMTA(MedicalTourismAccordionSubMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismAccordionSubMaster_Update";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@SubTitle", objbo.SubTitle);
                command.Parameters.AddWithValue("@Price", objbo.Price);
                command.Parameters.AddWithValue("@MTAId", objbo.MTAId);
                command.Parameters.AddWithValue("@MTAType", objbo.MTAType);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@Img_path", objbo.Img_path);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return true; else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetSubMTAGridData(int MTADetailId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_Get_MedicalTourismAccordionSubMaster_ByID";
                command.Parameters.AddWithValue("@MTADetailId", MTADetailId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectSubMTA(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MedicalTourismAccordion_SubMaster_SelectByID";
                command.Parameters.AddWithValue("@Id", Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteSubMTA(MedicalTourismAccordionSubMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_MedicalTourismAccordionSubMaster_Delete");
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
