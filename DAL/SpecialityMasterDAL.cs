using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class SpecialityMasterDAL : DBConnection
    {
        protected bool Insert(SpecialityMasterBO objbo, DataTable dt)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OtherSpecialityMaster_Insert";
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@ShortDesc", objbo.ShortDesc);
                command.Parameters.AddWithValue("@Imgpath", objbo.Imgpath);
                command.Parameters.AddWithValue("@Iconpath", objbo.Iconpath);
                command.Parameters.AddWithValue("@InnerDesc", objbo.InnerDesc);
                command.Parameters.AddWithValue("@IsImg", objbo.IsImg);
                command.Parameters.AddWithValue("@IsStatistics", objbo.IsStatistics);
                command.Parameters.AddWithValue("@StatisticsId", objbo.StatisticsId);
                command.Parameters.AddWithValue("@InnerImgpath", objbo.InnerImgpath);
                command.Parameters.AddWithValue("@InnerVideoLink", objbo.InnerVideoLink);
                command.Parameters.AddWithValue("@OSLevelId", objbo.OSLevelId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@dtimg", dt);
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
        protected bool Update(SpecialityMasterBO objbo, DataTable dt)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OtherSpecialityMaster_Update";
                command.Parameters.AddWithValue("@OS_id", objbo.OS_id);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@ShortDesc", objbo.ShortDesc);
                command.Parameters.AddWithValue("@Imgpath", objbo.Imgpath);
                command.Parameters.AddWithValue("@Iconpath", objbo.Iconpath);
                command.Parameters.AddWithValue("@InnerDesc", objbo.InnerDesc);
                command.Parameters.AddWithValue("@IsImg", objbo.IsImg);
                command.Parameters.AddWithValue("@IsStatistics", objbo.IsStatistics);
                command.Parameters.AddWithValue("@StatisticsId", objbo.StatisticsId);
                command.Parameters.AddWithValue("@InnerImgpath", objbo.InnerImgpath);
                command.Parameters.AddWithValue("@InnerVideoLink", objbo.InnerVideoLink);
                command.Parameters.AddWithValue("@OSLevelId", objbo.OSLevelId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@dtimg", dt);
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
        protected bool Delete(SpecialityMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OtherSpecialityMaster_Delete";
                command.Parameters.AddWithValue("@OS_id", objbo.OS_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(SpecialityMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OtherSpecialityMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@OS_id", objbo.OS_id);
                ExecuteNonQuery(command);
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
                command.CommandText = "tbl_OtherSpecialityMasterOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@OS_id", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet SelectIMG(SpecialityMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OtherSpecialityMasterIMG_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@OS_id", objbo.OS_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectSubrecord(SpecialityMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OtherSpecialityMasterSub_Select";
                command.Parameters.AddWithValue("@Img_id", objbo.Img_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateSubSpecility(SpecialityMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_SpecilitySubMaster_Update";
                command.Parameters.AddWithValue("@Img_id", objbo.Img_id);
                command.Parameters.AddWithValue("@ImgTitle", objbo.ImgTitle);
                command.Parameters.AddWithValue("@ImgShortDesc", objbo.ImgShortDesc);
                command.Parameters.AddWithValue("@ImgPOPUpDesc", objbo.ImgPOPUpDesc);
                command.Parameters.AddWithValue("@SubImgpath", objbo.SubImgpath);
                command.Parameters.AddWithValue("@Is_activeImg", objbo.Is_activeImg);
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
        protected DataSet SelectSequenceNo()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_SequenceNo_OtherSpecialityMaster";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteImg(SpecialityMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OtherSpecialityimg_Delete";
                command.Parameters.AddWithValue("@Img_id", objbo.Img_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectSidemenu(int osid,int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OtherSpeciality_Selectallbylanguage";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
                command.Parameters.AddWithValue("@osid", osid);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectFacility(int osid, int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OtherSpeciality_Facilitys";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
                command.Parameters.AddWithValue("@osid", osid);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectStafDetails(int osid, int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OtherSpeciality_SatfDetails";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
                command.Parameters.AddWithValue("@osid", osid);
                ExecuteNonQuery(command);
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
