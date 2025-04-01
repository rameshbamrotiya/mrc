using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace DAL
{
   public class CategoryDAL:DBConnection
    {
        public IEnumerable<CategoryBO> GetAllCategory(long lgLangId=1)
        {
            try
            {
                IEnumerable<CategoryBO> studentList = new List<CategoryBO>();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(SqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "GetAllCategory";
                        cmd.Parameters.AddWithValue("@Languageid", lgLangId);

                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        ds = ExecuteQuery(cmd);
                        if (ds != null)
                        {
                            dt = ds.Tables[0];
                        }
                        studentList = (from DataRow dr in dt.Rows
                                       select new CategoryBO()
                                       {
                                           Recid = Convert.ToInt32(dr["Recid"]),
                                           CategoryName = dr["CategoryName"].ToString()
                                       }).ToList();
                    }
                }
                return studentList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool InsertCategory(CategoryBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertCategoryDetails";
                command.Parameters.AddWithValue("@CategoryName", objbo.CategoryName);
                command.Parameters.AddWithValue("@languageId", objbo.LanguageId);
                //command.Parameters.AddWithValue("@Schemeid", objbo.SchemeID);                
                command.Parameters.AddWithValue("@enabled", objbo.Enabled);
                command.Parameters.AddWithValue("@added_by", objbo.added_by);
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
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
        protected bool UpdateCategory(CategoryBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdatecatgeoryDetails";
                command.Parameters.AddWithValue("@CategoryName", objbo.CategoryName);
                command.Parameters.AddWithValue("@languageId", objbo.LanguageId);
                //command.Parameters.AddWithValue("@Schemeid", objbo.SchemeID);
                command.Parameters.AddWithValue("@enabled", objbo.Enabled);
                
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@CategoryID", objbo.CategoryID);


                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet GetCategoryByID(CategoryBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPatinetCareCategoryBYID";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@CategoryId", objbo.CategoryID);
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
