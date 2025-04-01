using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class ContactUsDAL : DBConnection
    {
        protected DataSet SelectCkeditor(int LanguageId,string MenuURL)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetBreadCumCKEditorByPageName";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
                command.Parameters.AddWithValue("@MenuURL", MenuURL);
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
