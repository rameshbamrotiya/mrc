using ClosedXML.Excel;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using Unmehta.WebPortal.Web.Common;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.IO;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class SendSubscribe_Newsletter : System.Web.UI.Page
    {
        bool checkdPaging;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.UserDetails.UserName != null)
                {
                    if (!Page.IsPostBack)
                    {
                        Bind_DocGrid();
                        txtDescription.Text = "";
                        txtsubject.Text = "";
                    }
                }
                else
                {
                    Response.Redirect("~/LoginPortal");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/LoginPortal");
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        protected void Bind_DocGrid()
        {
            SendSubscribeNewsletterBAL objBALPOst = new SendSubscribeNewsletterBAL();
            DataSet ds1 = new DataSet();
            ds1 = objBALPOst.GetAllDocument("PROC_SendSubscribeNewsletterDOC_Dropdown");
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddlDocument.DataSource = ds1.Tables[0];
                    ddlDocument.DataTextField = "SSN_Name";
                    ddlDocument.DataValueField = "id";
                    ddlDocument.DataBind();
                    ddlDocument.Items.Insert(0, new ListItem("Select Document", "0"));
                }
            }
            SendSubscribeNewsletterBAL objBAL = new SendSubscribeNewsletterBAL();
            CareerMasterBO objBO = new CareerMasterBO();
            DataSet ds = new DataSet();
            ds = objBAL.GetSubscribe_Newsletter();
            gView.DataSourceID = string.Empty;
            gView.DataSource = ds;
            gView.DataBind();
        }
        protected void gView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CheckBox chckheader = (CheckBox)gView.HeaderRow.FindControl("chkSelectAll");
            checkdPaging = chckheader.Checked;
            gView.PageIndex = e.NewPageIndex;
            this.Bind_DocGrid();
            if (checkdPaging)
            {
                chkSelectAll_CheckedChanged(null, null);
            }
        }
        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chckheader = (CheckBox)gView.HeaderRow.FindControl("chkSelectAll");
            if (checkdPaging)
            {
                chckheader.Checked = checkdPaging;
            }
            foreach (GridViewRow row in gView.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");

                if (chckheader.Checked == true)
                {
                    chckrw.Checked = true;
                }
                else
                {
                    chckrw.Checked = false;
                }
            }
        }
        protected void btnSendCallLetter_Click(object sender, EventArgs e)
        {
            try
            {
                int aletrMessage = 0;
                SendSubscribeNewsletterBO objBo = new SendSubscribeNewsletterBO();
                if (ddlDocument.SelectedValue == "0")
                {
                    Functions.MessagePopup(this, "Please select Document file.", PopupMessageType.success);
                    return;
                }
                else
                {
                    objBo.DocId = Convert.ToInt32(ddlDocument.SelectedValue);
                }
                if (string.IsNullOrEmpty(txtDescription.Text))
                {
                    Functions.MessagePopup(this, "Please Mail Body Description.", PopupMessageType.success);
                    return;
                }
                else
                {
                    objBo.MailDescription = txtDescription.Text;
                }
                if (string.IsNullOrEmpty(txtsubject.Text))
                {
                    Functions.MessagePopup(this, "Please Mail Subject.", PopupMessageType.success);
                    return;
                }
                else
                {
                    objBo.MailSubject = txtsubject.Text;
                }
                foreach (GridViewRow row in gView.Rows)
                {
                    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        aletrMessage = aletrMessage + 1;
                        objBo.SSN_Id = Convert.ToInt32(gView.DataKeys[row.RowIndex].Values["Id"].ToString());
                        objBo.FullName = row.Cells[3].Text;
                        objBo.EmailId = row.Cells[4].Text;
                        objBo.MobileNo = row.Cells[5].Text;
                        objBo.Location = row.Cells[6].Text;
                        objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
                        objBo.ip_add = GetIPAddress;
                        objBo.flag = 1;
                        if (new SendSubscribeNewsletterBAL().InsertRecordNewsLeter(objBo))
                        {
                            string strMessage = "";
                            //SendEmailForCallLetter(objBo.EmailId, Convert.ToInt32(objBo.DocId), objBo.MailDescription,objBo.MailSubject);

                            SendSubscribeNewsletterDocBO objBos = new SendSubscribeNewsletterDocBO();
                            objBos.Id = objBo.DocId;
                            DataSet ds1 = new SendSubscribeNewsletterBAL().SelectRecordDoc(objBos);
                            string docpath = "";
                            if (ds1.Tables.Count.Equals(0) || ds1.Tables[0].Rows.Count.Equals(0))
                            {
                                return;
                            }
                            else
                            {
                                DataRow dr = ds1.Tables[0].Rows[0];
                                if (dr["SSN_DocPath"] != DBNull.Value)
                                    docpath = Convert.ToString(dr["SSN_DocPath"]);
                            }

                            List<Attachment> lstAttachment = new List<Attachment>();
                            lstAttachment.Add(new System.Net.Mail.Attachment(Server.MapPath(docpath)));

                            if (!Functions.SendEmail(objBo.EmailId, objBo.MailSubject, objBo.MailDescription, out strMessage, true, lstAttachment))
                            {
                                Functions.MessagePopup(this, strMessage, PopupMessageType.error);
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                if (aletrMessage != 0)
                {
                    Bind_DocGrid();
                    Functions.MessagePopup(this, "News Leter Send successfully.", PopupMessageType.success);
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                throw ex;
            }
        }

        protected void btnExportToExcel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                SendSubscribeNewsletterBAL objBAL = new SendSubscribeNewsletterBAL();
                CareerMasterBO objBO = new CareerMasterBO();
                DataSet ds = new DataSet();
                ds = objBAL.GetSubscribe_Newsletter();
                DataTable dt = ds.Tables[0];
                dt.Columns.Remove("Id");
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=SubscribeNewsletterDetails.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
                throw ex;
            }
        }

        //public void SendEmailForCallLetter(string emailId, int DocId, string MailBodyDescription,string MailSubject)
        //{
        //    try
        //    {
        //        CareerMasterBAL objBAL = new CareerMasterBAL();
        //        DataSet ds = new DataSet();
        //        ds = objBAL.MailCreditials();
        //        SendSubscribeNewsletterDocBO objBo = new SendSubscribeNewsletterDocBO();
        //        objBo.Id = DocId;
        //        DataSet ds1 = new SendSubscribeNewsletterBAL().SelectRecordDoc(objBo);
        //        string docpath = "";
        //        if (ds1.Tables.Count.Equals(0) || ds1.Tables[0].Rows.Count.Equals(0))
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            DataRow dr = ds1.Tables[0].Rows[0];
        //            if (dr["SSN_DocPath"] != DBNull.Value)
        //                docpath = Convert.ToString(dr["SSN_DocPath"]);
        //        }
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            string link = docpath.ToString();
        //            string smtpserver = ds.Tables[0].Rows[0]["SMTPServer"].ToString();
        //            int smtpport = Convert.ToInt32(ds.Tables[0].Rows[0]["SMTPPort"].ToString());
        //            bool isEnableSsl = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSSL"].ToString());
        //            string smtpPassword = ds.Tables[0].Rows[0]["SMTPPassword"].ToString();
        //            string fromemail = ds.Tables[0].Rows[0]["FromEmail"].ToString();
        //            string smtpaccount = ds.Tables[0].Rows[0]["SMTPAccount"].ToString();
        //            MailMessage msg = new MailMessage();
        //            SmtpClient client = new SmtpClient(smtpserver, smtpport);
        //            msg.To.Add(emailId);
        //            msg.IsBodyHtml = true;
        //            msg.Subject = MailSubject;
        //            msg.Body = MailBodyDescription;
        //            System.Net.Mail.Attachment attachment;
        //            attachment = new System.Net.Mail.Attachment(Server.MapPath(docpath));
        //            msg.Attachments.Add(attachment);
        //            msg.From = new System.Net.Mail.MailAddress(fromemail);
        //            client.EnableSsl = isEnableSsl;
        //            client.UseDefaultCredentials = false;
        //            client.Credentials = new NetworkCredential(smtpaccount, smtpPassword);
        //            client.Send(msg);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
        //        throw ex;
        //    }
        //}
    }
}