using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.IO;
using System.Configuration;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class ParaMedicalCourse : System.Web.UI.Page
    {
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
            if (!IsPostBack)
            {
                ShowHideControl(VisibityType.GridView);
                FillLanguage();
                BindGridView();

            }
        }
        private void BindGridView()
        {
            //ParamedicalBO objbo = new ParamedicalBO();
            //ParaMedicalBAL objBAL = new ParaMedicalBAL();
            //objbo.languageId = 1;
            //DataSet ds = objBAL.GetAllCourse(objbo);
            //DataTable dt = ds.Tables[0];
            //grdDetails.DataSource = dt;
            grdDetails.DataBind();
        }
        private void FillLanguage()
        {
            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(drpLanguage, dt, "Name", "Id", true);

        }
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
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    filename.Visible = false;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = "1";
                    drpLanguage.Enabled = false;
                    break;
                case VisibityType.Edit:
                    filename.Visible = true;
                    pnlView.Visible = false;
                    pnlEntry.Visible = true;
                    btn_Save.Visible = false;
                    btn_Update.Visible = true;
                    drpLanguage.Enabled = true;
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
        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt16(hdnColMenuID.Value), Convert.ToInt16(drpLanguage.SelectedValue.ToString()));
        }
        private void LoadControlsAdd(ParamedicalBO objBo)
        {
            if (fuDocUpload.HasFile)
            {
                string documentfile = string.Empty;
                documentfile = SaveFile();

                if (!string.IsNullOrEmpty(documentfile))
                    objBo.ImagePath = documentfile;
            }
            else
            {
                objBo.ImagePath = filename.InnerText.ToString();
            }
            if (!string.IsNullOrEmpty(txtCourseName.Text))
                objBo.CourseName = txtCourseName.Text;

            if (!string.IsNullOrEmpty(txtCourseCode.Text))
                objBo.coursecode = txtCourseCode.Text;

            objBo.Totalseats = HttpUtility.HtmlEncode(txtSeats.Text.ToString());
            objBo.enabled = (drpStatus.SelectedValue == "1" ? true : false);
            objBo.AddedBy = "Admin";
            objBo.ModifiedBy = "Admin";
            objBo.ipadd = GetIPAddress;
            objBo.CourseDuration = HttpUtility.HtmlEncode(txtCourseDuration.Text.ToString());
            objBo.Fees = HttpUtility.HtmlEncode(txtFees.Text);
            objBo.description = HttpUtility.HtmlEncode(txtDesc.Text.ToString());
            objBo.languageId = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            //objBo.templateId = drpTemplate.SelectedValue.ToString();
            //objBo.ContentDet = HttpUtility.HtmlEncode(CKEditorControl1.Text.ToString());
            //objBo.MaskingURL = txtMaskingUrl.Text;
            //objBo.col_menu_type = drpMenutype.SelectedValue.ToString();


        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hdnRecid.Value))
                {
                    hdnRecid.Value = "0";
                }
                ParamedicalBO objbo = new ParamedicalBO();
                LoadControlsAdd(objbo);
                //System.Drawing.Image img = System.Drawing.Image.FromStream(fuDocUpload.PostedFile.InputStream);
                //int height = img.Height;
                //int width = img.Width;
                //decimal size = Math.Round(((decimal)fuDocUpload.PostedFile.ContentLength / (decimal)1024), 2);
                //if (height != 259 || width != 172)
                //{
                //    Functions.MessagePopup(this, "Please upload 259px*172px.", PopupMessageType.error);
                //    return;
                //}
                //else
                {
                    if (new ParaMedicalBAL().InsertRecord(objbo))
                    {
                        ShowMessage("Record inserted successfully.", MessageType.Success);
                    }
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
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
                //System.Drawing.Image img = System.Drawing.Image.FromStream(fuDocUpload.PostedFile.InputStream);
                //int height = img.Height;
                //int width = img.Width;
                //decimal size = Math.Round(((decimal)fuDocUpload.PostedFile.ContentLength / (decimal)1024), 2);
                //if (height != 259 || width != 172)
                //{
                //    Functions.MessagePopup(this, "Please upload 259px*172px.", PopupMessageType.error);
                //    return;
                //}
                //else
                {
                    if (fuDocUpload.HasFile)
                    {
                        string fileName = Path.GetFileName(fuDocUpload.FileName);
                        FileInfo fi = new FileInfo(fileName);
                        string ext = fi.Extension;
                        if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".pdf")
                        {
                            //if (fuDocUpload.PostedFile.ContentLength > 10485760)
                            //{
                            //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                            //    return;
                            //}
                            //else
                            {
                                ParamedicalBO objBo = new ParamedicalBO();
                                LoadControlsAdd(objBo);
                                objBo.courseid = Convert.ToInt32(ViewState["PK"]);
                                if (new ParaMedicalBAL().UpdateRecord(objBo))
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
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg,.pdf'.", PopupMessageType.warning);
                            return;
                        }
                    }
                    else
                    {
                        ParamedicalBO objBo = new ParamedicalBO();
                        LoadControlsAdd(objBo);
                        objBo.courseid = Convert.ToInt32(ViewState["PK"]);
                        if (new ParaMedicalBAL().UpdateRecord(objBo))
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
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
                ViewState["T017PDetails"] = null;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }
        private void FillControls(int courseid, int languageid)
        {
            try
            {
                ParamedicalBO objbo = new ParamedicalBO();
                ParaMedicalBAL objBal = new ParaMedicalBAL();
                objbo.courseid = courseid;
                objbo.languageId = languageid;
                DataSet ds = new DataSet();
                ds = objBal.getCourseByIDAndLanguage(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtCourseName.Text = dr["coursename"].ToString();
                    txtCourseDuration.Text = HttpUtility.HtmlDecode(dr["CourseDuration"].ToString());
                    txtCourseCode.Text = dr["courseCode"].ToString();
                    txtSeats.Text = HttpUtility.HtmlDecode(dr["TotalSeats"].ToString());
                    txtFees.Text = HttpUtility.HtmlDecode(dr["Fees"].ToString());
                    hdnColMenuID.Value = courseid.ToString();

                    txtDesc.Text = HttpUtility.HtmlDecode(dr["Description"].ToString());
                    drpLanguage.SelectedValue = dr["Languageid"].ToString();
                    drpStatus.SelectedValue = Convert.ToBoolean(dr["enabled"]) ? "1" : "0";
                    filename.InnerText = dr["ImagePath"].ToString();

                    //hdnColMeknuID.Value = col_menu_id;

                    //DataSet ds1 = objBAL.SelectMenutype(objbo);
                    //DataTable dt = ds1.Tables[0];
                    //PopulateDropDownList(drpParentMenu, dt, "col_menu_name", "col_menu_id", true);

                }
                else
                {
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = languageid.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }


        }
        protected void lnkMenu_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                ViewState["PK"] = Convert.ToInt32(grdDetails.DataKeys[rowindex].Values["CourseID"]);
                //hdnRecid.Value = grdDetails.DataKeys[rowindex]["courseid"].ToString();
                FillControls(Convert.ToInt16(grdDetails.DataKeys[rowindex]["CourseID"].ToString()), Convert.ToInt16(grdDetails.DataKeys[rowindex]["Languageid"].ToString()));
                ShowHideControl(VisibityType.Edit);

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }

        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
        }

        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(), this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {

        }

        private string SaveFile()
        {
            try
            {

                if (fuDocUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.GovApprovel;
                    var fname = Path.GetExtension(fuDocUpload.FileName);
                    var count = fuDocUpload.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuDocUpload.FileName.Split('.').Length; i++)
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
                        Functions.MessagePopup(this, "Please Select Valid File Formate!!!", PopupMessageType.error);
                    }
                    else
                    {
                        //Get file name of selected file
                        var filename1 = fuDocUpload.FileName.Replace(" ", "_");

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
                        fuDocUpload.SaveAs(Server.MapPath(DocumentUpload) + filename1);
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
    }
}