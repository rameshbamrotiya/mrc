using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using static Unmehta.WebPortal.Web.Common.Functions;
using Unmehta.WebPortal.Common;

namespace Unmehta.WebPortal.Web.Admin.Student
{
    public partial class Generalinstruction : System.Web.UI.Page
    {
        #region Page Method
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.UserDetails.UserName == null)
                {
                    Response.Redirect("~/LoginPortal");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/LoginPortal");
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            try
            {
                if (!Page.IsPostBack)
                {
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }

        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                gView.DataSource = "";
                gView.DataBind();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Save_ServerClick(object sender, EventArgs e)
        {
            try
            {
                GeneralinstructionBO objBo = new GeneralinstructionBO();
                GeneralinstructionBAL objGeneralInBAL = new GeneralinstructionBAL();
                if (LoadControls(objBo))
                {

                    if (objGeneralInBAL.InsertRecord(objBo))
                    {
                        if (objBo.Id == 0)
                        {
                            Functions.MessagePopup(this, "Record Insert successfully.", PopupMessageType.success);
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                        }
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                    }

                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;

                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                GeneralinstructionBO objBo = new GeneralinstructionBO();
                LoadControls(objBo);
                objBo.Id = Convert.ToInt32(hfTemplateId.Value);
                GeneralinstructionBAL obBal = new GeneralinstructionBAL();
                if (obBal.InsertRecord(objBo))
                {
                    Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                    return;
                }
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GeneralinstructionBAL objGeneralInBAL = new GeneralinstructionBAL();

                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Id"]);
                    if (e.CommandName == "eDelete")
                    {
                        objGeneralInBAL.RemoveRecord(bytID, SessionWrapper.UserDetails.UserName);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        //ShowMessage("Record deleted successfully.", MessageType.Success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControl(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            hfTemplateId.Value = bytID.ToString();
                            ShowHideControl(VisibityType.Edit);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        private bool FillControls(long iPkId)
        {
            GeneralinstructionBAL objStudentAdvertisementBAL = new GeneralinstructionBAL();
            var dataOfUserName = objStudentAdvertisementBAL.GetAllInstruction();
            if (dataOfUserName != null)
            {
                List<GeneralinstructionBO> data = Functions.ToListof<GeneralinstructionBO>(dataOfUserName);

                if (data != null)
                {
                    var objBo = data.Where(x => x.Id == iPkId).FirstOrDefault();
                    if (objBo != null)
                    {
                        hfTemplateId.Value = objBo.Id.ToString();
                        txtDescription.Text = HttpUtility.HtmlDecode(objBo.Desciption.ToString());
                        if (!string.IsNullOrWhiteSpace(objBo.DocPath))
                        {
                            Fupathmarksheet.InnerText = objBo.DocPath.ToString();
                        }
                        ddlActiveInactive.SelectedValue = Convert.ToString(objBo.IsVisible);
                    }
                }
            }

            return true;
        }
        private bool LoadControls(GeneralinstructionBO objBo)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text.Trim()))
            {
                Functions.MessageFrontPopup(this, "Please Enter General Instrunction", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Desciption = HttpUtility.HtmlEncode(txtDescription.Text);
            }
            if (string.IsNullOrEmpty(Fupathmarksheet.InnerText))
            {
                if (!fumarksheet.HasFile)
                {
                    Functions.MessagePopup(this, "Upload document in pdf.", PopupMessageType.warning);
                    return false;
                }
                else
                {
                    string ext = Path.GetExtension(fumarksheet.FileName);
                    if (ext.ToLower() != ".pdf")
                    {
                        Functions.MessagePopup(this, "Select only .pdf files.", PopupMessageType.warning);
                        return false;
                    }
                }

            }
            objBo.IsVisible = Convert.ToBoolean(ddlActiveInactive.SelectedValue);
            string documentfile = string.Empty;
            documentfile = SaveFile();

            if (!string.IsNullOrEmpty(documentfile))
                objBo.DocPath = documentfile;
            objBo.CreateBy = SessionWrapper.UserDetails.UserName;
            return true;
        }

        private void BindGridView()
        {
            GeneralinstructionBAL objStudentAdvertisementBAL = new GeneralinstructionBAL();

            var dataOfUserName = objStudentAdvertisementBAL.GetAllInstruction();
            if (dataOfUserName != null)
            {
                List<GeneralinstructionBO> data = Functions.ToListof<GeneralinstructionBO>(dataOfUserName);
                if (data != null)
                {
                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        gView.DataSource = data.Where(x => x.Desciption.Contains(txtSearch.Text)).ToList();
                    }
                    else
                    {
                        gView.DataSource = data;
                    }

                    gView.DataBind();
                }
            }
        }

        private string SaveFile()
        {
            try
            {
                if (fumarksheet.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.GeneralDocument;
                    var fname = Path.GetExtension(fumarksheet.FileName);
                    var count = fumarksheet.FileName.Split('.');
                    string type = "";
                    for (int i = 0; i < fumarksheet.FileName.Split('.').Length; i++)
                    {
                        var ass = count[i];
                        switch (ass.ToString())
                        {
                            case "exe":
                                type = "application/x-msdownload";
                                break;
                            case "docx":
                                type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                break;
                            case "html":
                                type = "text/html";
                                break;
                            case "txt":
                                type = "text/plain";
                                break;
                            case "php":
                                type = "php";
                                break;
                            case "php5":
                                type = "php5";
                                break;
                            case "pht":
                                type = ".pht";
                                break;
                            case "phtm":
                                type = "phtm";
                                break;
                            case "swf":
                                type = "swf";
                                break;
                        }
                    }
                    if (type != "")
                    {
                        //ShowMessage("Please Select Valid File Formate!!!", MessageType.Error);
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = fumarksheet.FileName.Replace(" ", "_");
                        filename1 = filename1.ToLower();
                        if (!Directory.Exists(Server.MapPath(DocumentUpload)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                        }
                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = DocumentUpload + filename1;
                        // Create a temporary file name to use for checking duplicates.
                        var tempfileName1 = "";
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            var counter = 2;
                            while (File.Exists(Server.MapPath(pathToCheck1)))
                            {
                                // if a file with this name already exists,
                                // prefix the filename with a number.
                                tempfileName1 = counter + filename1;
                                pathToCheck1 = DocumentUpload + tempfileName1;
                                counter++;
                            }
                            filename1 = tempfileName1;
                        }
                        //Save selected file into specified location
                        fumarksheet.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
            return "";
        }
        #endregion

        #region ShowHideControl || Notification
        private void ShowHideControl(VisibityType e)
        {
            switch (e)
            {
                case VisibityType.GridView:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
                case VisibityType.View:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = false;
                    break;
                case VisibityType.Insert:
                    pnlView.Visible = false;
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    break;
                case VisibityType.Edit:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    break;
                case VisibityType.SaveAndAdd:
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        #endregion

        protected void gView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strStatus = HttpUtility.HtmlDecode(e.Row.Cells[1].Text); 
                
            }
        }
    }
}