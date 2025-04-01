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
    public class ChairManMessageDAL:DBConnection
    {
        public class GetChairmanMessMasterByIdAndLangIdResult
        {
            public int DMId { get; set; }
            public string DOCPath { get; set; }
            public string DirectorMesshtmlContent { get; set; }
            public string MetaTitle { get; set; }
            public string MetaDescription { get; set; }
            public bool enabled { get; set; }
        }
        protected bool InsertOrUpdateChairmanMessMaster(ChairManMessageBO objbo)
        {
            try
            {

                Dictionary<string, object> parasInsertUpdate = new Dictionary<string, object>();
                parasInsertUpdate.Add("DOCPath", objbo.DOCPath);
                parasInsertUpdate.Add("MetaTitle", objbo.MetaTitle);
                parasInsertUpdate.Add("MetaDescription", objbo.MetaDescription);
                parasInsertUpdate.Add("enabled", objbo.enabled);
                parasInsertUpdate.Add("ip_add", objbo.ip_add);
                parasInsertUpdate.Add("CMId", objbo.CMId);
                parasInsertUpdate.Add("ChairmanMesshtmlContent", string.IsNullOrWhiteSpace(objbo.ChairmanMesshtmlContent) ? "" : objbo.ChairmanMesshtmlContent);
                parasInsertUpdate.Add("LanguageId", objbo.LanguageId);
                parasInsertUpdate.Add("UserName", string.IsNullOrWhiteSpace(objbo.UserName) ? "" : objbo.UserName.ToString());

                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras.Add("DOCPath", objbo.DOCPath);
                {
                    {
                        {
                            using (SqlDataReader resultss = executeProcedure("InsertOrUpdateChairManMaster", parasInsertUpdate))
                            {
                                while (resultss.Read())
                                {
                                    string tempId = resultss["CMId"].ToString();
                                    if (string.IsNullOrWhiteSpace(tempId))
                                    {
                                        return false;
                                    }
                                    objbo.CMId = Convert.ToInt32(tempId);
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

        protected bool DeleteChairmanMessMaster(int CMId)
        {
            try
            {
                SqlCommand command = new SqlCommand(SqlConnectionString);

                command.CommandText = "RemoveChairManMaster";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CMId", CMId);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected List<GetChairmanMessMasterByIdAndLangIdResult> GetChairmanMesMasterByIdAndLangId(int CMId, int LanguageId)
        {
            List<GetChairmanMessMasterByIdAndLangIdResult> studentList = new List<GetChairmanMessMasterByIdAndLangIdResult>();
            try
            {
                SqlCommand command = new SqlCommand(SqlConnectionString);

                command.CommandText = "GetChairmanMasterByIdAndLangId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CMId", CMId);
                command.Parameters.AddWithValue("@LangId", LanguageId);
                DataTable dt = new DataTable();
                DataSet ds = ExecuteQuery(command);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                }
                studentList = (from DataRow dr in dt.Rows
                               select new GetChairmanMessMasterByIdAndLangIdResult()
                               {
                                   DMId = Convert.ToInt32(dr["CMId"].ToString()),
                                   DOCPath = dr["DOCPath"].ToString(),
                                   DirectorMesshtmlContent = dr["ChairmanMesshtmlContent"].ToString(),
                                   MetaTitle = dr["MetaTitle"].ToString(),
                                   MetaDescription = dr["MetaDescription"].ToString(),
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
