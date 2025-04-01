using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public class DublicateTemplateNameFound
    {
        public string TemplateName { get; set; }
    }


    public class GetAllPageListWithMaskingUrlResult
    {
        public int col_menu_id { get; set; }
        public string col_menu_name { get; set; }
        public int LanguageId { get; set; }
        public string MaskingURL { get; set; }
        public string col_menu_url { get; set; }
        //public int LanguageId { get; set; }
    }

    public class GetTemplateMasterByIdAndLangIdResult
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplatehtmlContent { get; set; }
        public bool enabled { get; set; }
    }



    public class TemplateMasterDAL : DBConnection
    {
        protected DataSet GetAllTemplateMaster()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllCMSTemplateMaster";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetTemplateMasterById(int TempId)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(SqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetTemplateMasterById", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@TemplateId", SqlDbType.Int).Value = TempId;

                        con.Open();
                        ds = ExecuteQuery(cmd);
                    }
                }
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetPageDataFromPageIdDetail(string pageName, int langId)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(SqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetPageDataFromPageName", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@PageName", SqlDbType.VarChar).Value = pageName;
                        cmd.Parameters.Add("@LangId", SqlDbType.Int).Value = langId;

                        con.Open();
                        ds = ExecuteQuery(cmd);
                    }
                }
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected List<GetAllPageListWithMaskingUrlResult> GetAllPageListWithMaskingUrl()
        {
            try
            {
                List<GetAllPageListWithMaskingUrlResult> studentList = new List<GetAllPageListWithMaskingUrlResult>();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(SqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetAllPageListWithMaskingUrl", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        ds = ExecuteQuery(cmd);
                        if (ds != null)
                        {
                            dt = ds.Tables[0];
                        }
                        studentList = (from DataRow dr in dt.Rows
                                       select new GetAllPageListWithMaskingUrlResult()
                                       {
                                           col_menu_id = Convert.ToInt32(dr["col_menu_id"]),
                                           col_menu_name = dr["col_menu_name"].ToString(),
                                           LanguageId = Convert.ToInt32(dr["LanguageId"].ToString()),
                                           col_menu_url = dr["col_menu_url"].ToString(),
                                           MaskingURL = dr["MaskingURL"].ToString()
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

        protected bool InsertOrUpdateTemplateMaster(TemplateMasterBO objbo)
        {
            try
            {

                Dictionary<string, object> parasInsertUpdate = new Dictionary<string, object>();
                parasInsertUpdate.Add("TemplateName", objbo.TemplateName);
                parasInsertUpdate.Add("enabled", objbo.enabled);
                parasInsertUpdate.Add("ip_add", objbo.ip_add);
                parasInsertUpdate.Add("TemplateId", objbo.TemplateId);
                parasInsertUpdate.Add("TemplatehtmlContent", string.IsNullOrWhiteSpace(objbo.TemplatehtmlContent) ? "" : objbo.TemplatehtmlContent);
                parasInsertUpdate.Add("LanguageId", objbo.LanguageId);
                parasInsertUpdate.Add("UserName", string.IsNullOrWhiteSpace(objbo.UserName) ? "" : objbo.UserName.ToString());

                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras.Add("TemplateName", objbo.TemplateName);
                //using (SqlDataReader results = executeProcedure("CheckDublicateTemplateName", paras))
                {
                    //while (results.Read())
                    {
                        //int rec = Convert.ToInt32(results["RecordCount"].ToString());
                        //if (rec > 0 && objbo.TemplateId == 0)
                        //{
                        //    return false;
                        //}
                        //else
                        {
                            using (SqlDataReader resultss = executeProcedure("InsertOrUpdateTemplateMaster", parasInsertUpdate))
                            {
                                while (resultss.Read())
                                {
                                    string tempId = resultss["TemplateId"].ToString();
                                    if (string.IsNullOrWhiteSpace(tempId))
                                    {
                                        return false;
                                    }
                                    objbo.TemplateId = Convert.ToInt32(tempId);
                                    return true;
                                    //do something with the rows returned
                                }
                            }
                        }
                        //do something with the rows returned
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool DeleteTemplateMaster(int TemplateId)
        {
            try
            {
                SqlCommand command = new SqlCommand(SqlConnectionString);

                command.CommandText = "RemoveTemplateMaster";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TemplateId", TemplateId);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected List<GetTemplateMasterByIdAndLangIdResult> GetTemplateMasterByIdAndLangId(int TemplateId,int LanguageId)
        {
            List<GetTemplateMasterByIdAndLangIdResult> studentList = new List<GetTemplateMasterByIdAndLangIdResult>();
            try
            {
                SqlCommand command = new SqlCommand(SqlConnectionString);

                command.CommandText = "GetTemplateMasterByIdAndLangId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TempId", TemplateId);
                command.Parameters.AddWithValue("@LangId", LanguageId);
                DataTable dt=new DataTable();
                DataSet ds = ExecuteQuery(command);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                }
                studentList = (from DataRow dr in dt.Rows
                               select new GetTemplateMasterByIdAndLangIdResult()
                               {
                                   TemplateId = Convert.ToInt32(dr["TemplateId"].ToString()),
                                   TemplateName = dr["TemplateName"].ToString(),
                                   TemplatehtmlContent = dr["TemplatehtmlContent"].ToString(),
                                   enabled = Convert.ToBoolean(dr["enabled"].ToString())
                               }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return studentList;
        }

        protected SqlDataReader executeProcedure(string commandName,
                                         Dictionary<string, object> paramss)
        {
            SqlConnection conn = new SqlConnection(SqlConnectionString);
            conn.Open();
            SqlCommand comm = conn.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = commandName;
            if (paramss != null)
            {
                foreach (KeyValuePair<string, object> kvp in paramss)
                    comm.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
            }
            return comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
    }
}
