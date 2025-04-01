using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class SQLExecuter : System.Web.UI.Page
    {
        public static string strScript;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    strScript = "";
                    if (SessionWrapper.UserDetails.RoleId != 1)
                    {
                        Response.Redirect("~/Admin/");
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                    Response.Redirect("~/Admin/");
                }
            }
        }

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            strHtmlData.InnerHtml = GetHtmlData();
        }
        protected SqlConnection GetConnection(SqlConnection connection)
        {
            try
            {
                connection = new SqlConnection(Functions.strSqlConnectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                Functions.MessagePopup(this, "GetConnection=> " + ex.Message.ToString(), Model.Common.EnumClass.PopupMessageType.error);
            }
            return connection;
        }

        protected void CloseConnection(SqlConnection connection)
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                Functions.MessagePopup(this, "CloseConnection=> "+ex.Message.ToString(), Model.Common.EnumClass.PopupMessageType.error);
            }
        }

        protected DataSet ExecuteQuery(SqlCommand command)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                DataSet ds = new DataSet();
                command.CommandTimeout = 0;
                command.Connection = GetConnection(connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                Functions.MessagePopup(this, "Query Executed Successfully", Model.Common.EnumClass.PopupMessageType.success);
                return ds;
            }
            catch (Exception ex)
            {
                CloseConnection(connection);
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                Functions.MessagePopup(this, ex.Message.ToString(), Model.Common.EnumClass.PopupMessageType.error);
                return new DataSet();
            }
            finally
            {
                CloseConnection(connection);
            }
        }
        private string GetHtmlData()
        {
            DataSet ds = new DataSet();
            string strQuery = txtSqlQuery.Text;
            SqlCommand cmdCommand = new SqlCommand(strQuery);
            ds = ExecuteQuery(cmdCommand);
            StringBuilder strHTMLBuilder = new StringBuilder();
            StringBuilder strTableJS = new StringBuilder();
            if (ds.Tables.Count>0)
            {
                int i = 1;
                foreach(DataTable dt in ds.Tables)
                {

                    strHTMLBuilder.Append("</br>");
                    strHTMLBuilder.Append("</br>");
                    strHTMLBuilder.Append("<div class='row' > <div class='col-md-12' style='max-width:50%;overflow:scroll;'> <table id='table" + i+"' class='table table-bordered table-hover table-striped'>");

                    strHTMLBuilder.Append("<thead><tr >");
                    foreach (DataColumn myColumn in dt.Columns)
                    {
                        strHTMLBuilder.Append("<th >");
                        strHTMLBuilder.Append(""+myColumn.ColumnName+ "" );
                        strHTMLBuilder.Append("</th>");

                    }
                    strHTMLBuilder.Append("</tr></thead>");


                        strHTMLBuilder.Append("<tbody>");
                    foreach (DataRow myRow in dt.Rows)
                    {

                        strHTMLBuilder.Append("<tr >");
                        foreach (DataColumn myColumn in dt.Columns)
                        {
                            strHTMLBuilder.Append("<td >");
                            strHTMLBuilder.Append(myRow[myColumn.ColumnName].ToString());
                            strHTMLBuilder.Append("</td>");

                        }
                        strHTMLBuilder.Append("</tr>");
                    }
                    strHTMLBuilder.Append("</tbody>");
                    //Close tags.  
                    strHTMLBuilder.Append("</table> </div> </div>");

                    strTableJS.Append("$('#table"+i+ "').DataTable({ 'scrollY': '400px','scrollCollapse': true, destroy: true});");
                    i++;
                }
                string strMain = "$(document).ready(function (){ " + strTableJS.ToString() + " }); ";
                strScript=strMain;
            }

            return strHTMLBuilder.ToString();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSqlQuery.Text = "";
            strHtmlData.InnerHtml = "";
        }
    }
}