using BAL;
using BO;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class EventFormEntry : System.Web.UI.Page
    {
        public static long EventId;
        public static string strEventName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                long id = 0;
                if (!string.IsNullOrWhiteSpace(queryString) && long.TryParse(queryString, out id))
                {
                    EventId = id;

                    EventMasterBO objBo = new EventMasterBO();
                    objBo.EventId =(int) EventId;
                    objBo.LanguageId = 1;
                    objBo.IsOnlineRegistration = null;
                    DataSet ds = new EventMasterBAL().SelectRecord(objBo);
                    DataRow dr = ds.Tables[0].Rows[0];
                    if (dr["EventName"] != DBNull.Value)
                        strEventName = dr["EventName"].ToString();
                                                
                    BindGridView();
                }
            }
        }

        private void BindGridView(string strSearch = "")
        {
            using (IEventFormFieldRepository objIEventFormFieldRepository = new EventFormFieldRepository(Functions.strSqlConnectionString))
            {
                var eventCheckList = objIEventFormFieldRepository.GetAllOnlineEventRegistrtion(EventId);
                if(!string.IsNullOrEmpty(strSearch))
                {
                    gView.DataSource = eventCheckList.Where(x => x.FirstName.Contains(strSearch) || x.LastName.Contains(strSearch) || x.EmailId.Contains(strSearch) || x.PhoneNumber.Contains(strSearch)).OrderByDescending(x => x.Id).ToList();
                }
                else
                {
                    gView.DataSource = eventCheckList.OrderByDescending(x => x.Id).ToList();
                }
                gView.DataBind();
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView(txtSearch.Text);
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            BindGridView();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            using (IEventFormFieldRepository objIEventFormFieldRepository = new EventFormFieldRepository(Functions.strSqlConnectionString))
            {
                List<GetAllOnlineEventRegistrtionResult> lstNewData = new List<GetAllOnlineEventRegistrtionResult>();
                var eventCheckList = objIEventFormFieldRepository.GetAllOnlineEventRegistrtion(EventId);

                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    lstNewData.AddRange(eventCheckList.Where(x => x.FirstName.Contains(txtSearch.Text) || x.LastName.Contains(txtSearch.Text) || x.EmailId.Contains(txtSearch.Text) || x.PhoneNumber.Contains(txtSearch.Text)).OrderByDescending(x => x.Id).ToList());
                }
                else
                {
                    lstNewData.AddRange(eventCheckList.OrderByDescending(x => x.Id).ToList());
                }


                DataTable ds = Functions.ToDataTable(lstNewData);
                
                ds.Columns.Remove("Id");
                ds.Columns.Remove("EventId");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ds, "OnlineEventRegistration");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=OnlineEventRegistration.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }

        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gView.PageIndex = e.NewPageIndex;
            BindGridView(txtSearch.Text);
        }

        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Id"]);
                    if (e.CommandName == "eDelete")
                    {
                        using (IEventFormFieldRepository objIEventFormFieldRepository = new EventFormFieldRepository(Functions.strSqlConnectionString))
                        {
                            if (!objIEventFormFieldRepository.RemoveRecordOnlineEventRegistrtion(bytID))
                            {
                                Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                                BindGridView();
                                //ShowMessage("Record deleted successfully.", MessageType.Success);
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }
    }
}