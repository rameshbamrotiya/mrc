using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.Admin
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

                configDetailsRepository.InsertOrUpdateDailyVisitEntry(browserInfo, false, out strError);
            }

            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Expires = 0;
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
            Response.Redirect("~/LoginPortal");
        }
    }
}