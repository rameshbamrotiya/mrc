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
    public class LanguageMasterDal : DBConnection
    {
        public IEnumerable<LanguageMasterBO> GetAllLanguage()
        {
            try
            {
                IEnumerable<LanguageMasterBO> studentList = new List<LanguageMasterBO>();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(SqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetAllLanguage", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        ds = ExecuteQuery(cmd);
                        if (ds != null)
                        {
                            dt = ds.Tables[0];
                        }
                        studentList = (from DataRow dr in dt.Rows
                                       select new LanguageMasterBO()
                                       {
                                           Id = Convert.ToInt32(dr["Id"]),
                                           Name = dr["Name"].ToString()
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
        public DataSet GetLanguages()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllLanguage";               

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
