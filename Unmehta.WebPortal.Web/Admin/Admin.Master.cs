using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (SessionWrapper.UserDetails == null)
                    {
                        Response.Redirect("~/LoginPortal",false);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/LoginPortal", false);
                }
                List<string> list = new List<string>();
                list.Add("Admin/default");
                list.Add("Admin/Hospital/SQLExecuter");
                list.Add("Admin/Hospital/SchemeCharts");
                list.Add("Admin/Recruitment/PrintApplication");
                list.Add("Admin/Student/StudentDetails.aspx");
                list.Add("Admin/Student/BasicDetails.aspx");
                list.Add("Admin/Student/EducationDetails.aspx");
                String[] str = list.ToArray();
                if (SessionWrapper.UserDetails != null)
                {
                    if (!Functions.PageRightsCheck() && (str.All(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Contains)))
                    {
                        Response.Redirect("~/Admin/");
                    }

                    using (IConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(Functions.strSqlConnectionString))
                    {
                        string strError = "";
                        string browserInfo = "RemoteUser=" + Context.Request.ServerVariables["REMOTE_USER"] + ";\n"
                                                    + "RemoteHost=" + Context.Request.ServerVariables["REMOTE_HOST"] + ";\n"
                                                    + "Type=" + Context.Request.Browser.Type + ";\n"
                                                    + "Name=" + Context.Request.Browser.Browser + ";\n"
                                                    + "Version=" + Context.Request.Browser.Version + ";\n"
                                                    + "MajorVersion=" + Context.Request.Browser.MajorVersion + ";\n"
                                                    + "MinorVersion=" + Context.Request.Browser.MinorVersion + ";\n"
                                                    + "Platform=" + Context.Request.Browser.Platform + ";\n"
                                                    + "SupportsCookies=" + Context.Request.Browser.Cookies + ";\n"
                                                    + "SupportsJavaScript=" + Context.Request.Browser.EcmaScriptVersion.ToString() + ";\n"
                                                    + "SupportsActiveXControls=" + Context.Request.Browser.ActiveXControls + ";\n"
                                                    + "SupportsJavaScriptVersion=" + Context.Request.Browser["JavaScriptVersion"] + "\n";

                        configDetailsRepository.InsertOrUpdateDailyVisitEntry(browserInfo,true, out strError);
                    }
                    
                    bindmenu();
                }
                else
                {
                    Response.Redirect("~/LoginPortal");
                }
            }
        }


        public void bindmenu()
        {
            DataSet ds = new DataSet();
            MenuBO objbo = new MenuBO();
            objbo.user_type_id = Convert.ToInt16(SessionWrapper.UserDetails.RoleId);
            //objbo.user_type_id = Convert.ToInt16(1);
            UserRightsBAL objBAL = new UserRightsBAL();
            ds = objBAL.SelectResource(objbo);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                Session["dt_menu"] = dt;
                Application["dt_menu"] = dt;
                var dv = dt.DefaultView;
                dv.RowFilter = "col_parent_id = 0";
                rptMenu.DataSource = dt;
                rptMenu.DataBind();
            }

        }
        protected void rptChildMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rptSubMenu = e.Item.FindControl("rptSubChildMenu") as Repeater;
                DataTable dt = (DataTable)Session["dt_menu"];
                var dv = dt.DefaultView;
                dv.RowFilter = "col_parent_id =" + Convert.ToInt32(((DataRowView)(e.Item.DataItem)).Row["col_menu_id"]) + "";

                 var childUl = e.Item.FindControl("childUl") as HtmlGenericControl;
                 var subChildUl = e.Item.FindControl("subChildUl") as HtmlGenericControl;
                if (dv.Count > 0)
                {
                    if (subChildUl != null)
                    {
                        subChildUl.Visible = true;
                    }
                    
                    rptSubMenu.Visible = true;
                    var li = e.Item.FindControl("parentli") as HtmlGenericControl;
                    li.Attributes.Add("style", "display: block;");

                    //if (li != null) li.Attributes.Add("class", "treeview");
                    //var span = e.Item.FindControl("sp_caret1") as HtmlGenericControl;
                    //if (span != null) span.Attributes.Add("class", "caret");
                    if (rptSubMenu != null)
                    {
                        rptSubMenu.DataSource = dv.ToTable();
                        rptSubMenu.DataBind();
                    }
                }
                else
                {
                    if (childUl != null)
                    {
                        childUl.Visible = false;
                    }

                    if (subChildUl != null)
                    {
                        subChildUl.Visible = false;
                    }

                    rptSubMenu.Visible = false;
                    var li = e.Item.FindControl("parentli") as HtmlGenericControl;
                    if (li != null) { li.Attributes.Remove("class"); li.Attributes.Remove("style"); }
                    //var span = e.Item.FindControl("sp_caret1") as HtmlGenericControl;
                    //if (span != null) span.Attributes.Remove("class");
                }
            }

        }

        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rptSubMenu = e.Item.FindControl("rptChildMenu") as Repeater;
                DataTable dt = (DataTable)Session["dt_menu"];
                var dv = dt.DefaultView;
                dv.RowFilter = "col_parent_id =" + Convert.ToInt32(((DataRowView)(e.Item.DataItem)).Row["col_menu_id"]) +
                               "";

                var subChildUl = e.Item.FindControl("subChildUl") as HtmlGenericControl;
                if (dv.Count > 0)
                {
                    var li = e.Item.FindControl("parentli") as HtmlGenericControl;
                    li.Attributes.Add("style", "display: block;");
                    //if (li != null) li.Attributes.Add("class", "treeview");
                    //var span = e.Item.FindControl("sp_caret") as HtmlGenericControl;
                    //if (span != null) span.Attributes.Add("class", "caret");
                    if (rptSubMenu != null)
                    {
                        rptSubMenu.DataSource = dv.ToTable();
                        rptSubMenu.DataBind();
                    }
                }
                else
                {
                    if (subChildUl != null)
                    {
                        subChildUl.Visible = false;
                        var li = e.Item.FindControl("parentli") as HtmlGenericControl;
                        if (li != null) { li.Attributes.Remove("class"); li.Attributes.Remove("style"); }
                    }//var span = e.Item.FindControl("sp_caret") as HtmlGenericControl;
                    //if (span != null) span.Attributes.Remove("class");
                }
            }
        }
    }
}