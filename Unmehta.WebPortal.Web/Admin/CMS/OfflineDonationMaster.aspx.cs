using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using BAL;
using static Unmehta.WebPortal.Web.Common.Functions;
using BO;
using System.IO;
using Unmehta.WebPortal.Web.Common;
using System.Web;
using Unmehta.WebPortal.Common;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class OfflineDonationMaster : System.Web.UI.Page
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            if (!IsPostBack)
            {
                ShowHideControl(VisibityType.GridView);
                FillLanguage();
            }
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
                    ReciptImgfilename.Visible = false;
                    btn_Update.Visible = false;
                    ClearControlValues(pnlEntry);
                    drpLanguage.SelectedValue = "1";
                    drpLanguage.Enabled = false;
                    break;
                case VisibityType.Edit:
                    ReciptImgfilename.Visible = true;
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
        private void FillLanguage()
        {
            DataSet ds = new DataSet();
            LanguageMasterBAL objBAL = new LanguageMasterBAL();
            ds = objBAL.FillLanguage();
            DataTable dt = ds.Tables[0];
            PopulateDropDownList(drpLanguage, dt, "Name", "Id", true);

        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hdnRecid.Value))
                {
                    hdnRecid.Value = "0";
                }

                OfflineDonationBO objbo = new OfflineDonationBO();
                LoadControlsAdd(objbo);
                if (new OfflineDonationBAL().InsertRecord(objbo))
                {

                    Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                }
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }
        private string SaveFile(FileUpload fuUpload)
        {
            try
            {

                if (fuUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.DonationRecipt;
                    var fname = Path.GetExtension(fuUpload.FileName);
                    var count = fuUpload.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuUpload.FileName.Split('.').Length; i++)
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
                        var filename1 = fuUpload.FileName.Replace(" ", "_");

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
                        fuUpload.SaveAs(Server.MapPath(DocumentUpload) + filename1);
                        return (DocumentUpload) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            return "";
        }
        private void LoadControlsAdd(OfflineDonationBO objBo)
        {
            objBo.Language = Convert.ToInt16(drpLanguage.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtFirstName.Text))
                objBo.FirstName = txtFirstName.Text;
            if (!string.IsNullOrEmpty(txtLastName.Text))
                objBo.LastName = txtLastName.Text;
            if (!string.IsNullOrEmpty(txtemail.Text))
                objBo.Email = txtemail.Text;
            if (!string.IsNullOrEmpty(txtMoNo.Text))
                objBo.MoNo = txtMoNo.Text;
            if (!string.IsNullOrEmpty(txtPanno.Text))
                objBo.PanNo = txtPanno.Text;
            if (!string.IsNullOrEmpty(txtamount.Text))
                objBo.Amount = txtamount.Text;
            if (!string.IsNullOrEmpty(txtaddress.Text))
                objBo.Address = txtaddress.Text;
            if (reciptDoc.HasFile)
            {
                string Recipt = string.Empty;
                Recipt = SaveFile(reciptDoc);

                if (!string.IsNullOrEmpty(Recipt))
                    objBo.ReciptPath = Recipt;
            }
            else
            {
                objBo.ReciptPath = ReciptImgfilename.InnerText.ToString();
            }
            //if (!string.IsNullOrEmpty(txtsequence.Text))
            //    objBo.SS_level_id = Convert.ToInt32(txtsequence.Text);
            //if (!string.IsNullOrEmpty(txtMetaTitle.Text))
            //    objBo.MetaTitle = txtMetaTitle.Text;
            //if (!string.IsNullOrEmpty(txtMetaDesc.Text))
            //    objBo.MetaDescription = txtMetaDesc.Text;
            objBo.Status = (ddlPaidUnpaid.SelectedValue == "1" ? true : false);
            objBo.added_by = SessionWrapper.UserDetails.UserName;
            objBo.modified_by = SessionWrapper.UserDetails.UserName; ;
            objBo.ip_add = GetIPAddress;
        }
        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            try
            {

                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {

        }
        private void BindGridView()
        {
            grdOfflineDonation.DataBind();
        }
        protected void btn_SearchCancel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                BindGridView();
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            ShowHideControl(VisibityType.Insert);
            //DataSet ds = new OfflineDonationBAL().SequenceNo();
            //DataRow drs = ds.Tables[0].Rows[0];
            //if (drs["SS_level_id"] != DBNull.Value)
            //    txtsequence.Text = drs["SS_level_id"].ToString();
        }
        private void FillControls(int ODId, int languageid)
        {
            try
            {
                OfflineDonationBO objbo = new OfflineDonationBO();
                OfflineDonationBAL objBAL = new OfflineDonationBAL();
                objbo.ODId = ODId;
                objbo.Language = languageid;
                hdnODID.Value = Convert.ToInt16(ODId).ToString();
                DataSet ds = new DataSet();
                ds = objBAL.SelectOfflineDonationByID(objbo);
                DataTable dtDetails = new DataTable();
                dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {
                    DataRow dr = dtDetails.Rows[0];
                    txtFirstName.Text = dr["FirstName"].ToString();
                    txtLastName.Text = dr["LastName"].ToString();
                    txtemail.Text = dr["Email"].ToString();
                    txtMoNo.Text = dr["MoNo"].ToString();
                    txtPanno.Text = dr["PanNo"].ToString();
                    txtamount.Text = dr["Amount"].ToString();
                    txtaddress.Text = dr["Address"].ToString();
                    ReciptImgfilename.InnerText = dr["ReciptPath"].ToString();
                    hdnRecid.Value = dr["recid"].ToString();
                    //txtMetaDesc.Text = dr["MetaDescription"].ToString();
                    //txtMetaTitle.Text = dr["MetaTitle"].ToString();
                    drpLanguage.SelectedValue = dr["Languageid"].ToString();
                    ddlPaidUnpaid.SelectedValue = Convert.ToBoolean(dr["Status"]) ? "1" : "0";
                    //txtsequence.Text = dr["SS_level_id"].ToString();

                }
                else
                {
                    ClearControlValues(pnlEntry);
                    ReciptImgfilename.InnerText = "";
                    drpLanguage.SelectedValue = languageid.ToString();
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }


        }

        protected void lnkMenu_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                hdnRecid.Value = grdOfflineDonation.DataKeys[rowindex]["recid"].ToString();
                FillControls(Convert.ToInt16(grdOfflineDonation.DataKeys[rowindex]["ODId"].ToString()), Convert.ToInt16(grdOfflineDonation.DataKeys[rowindex]["Languageid"].ToString()));
                ShowHideControl(VisibityType.Edit);

            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            OfflineDonationBO objBo = new OfflineDonationBO();
            LoadControlsAdd(objBo);
            objBo.ODId = Convert.ToInt16(hdnODID.Value);
            if (new OfflineDonationBAL().UpdateRecord(objBo))
            {
                Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
            }
            else
            {
                Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.success);
                return;
            }
            //BindGridView();
            ShowHideControl(VisibityType.GridView);

        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControls(Convert.ToInt16(hdnODID.Value), Convert.ToInt16(drpLanguage.SelectedValue.ToString()));

        }

        protected void grdOfflineDonation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(grdOfflineDonation.DataKeys[intIndex].Values["ODId"]);
                    if (e.CommandName == "eDelete")
                    {
                        OfflineDonationBO objBo = new OfflineDonationBO();
                        objBo.ODId = bytID;
                        new OfflineDonationBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                }
                //else
                //{
                //    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

                //    if (commandArgs != null && commandArgs.Length > 0 && !string.IsNullOrEmpty(commandArgs[0]))
                //    {

                //        string col_parent_id = commandArgs[0];
                //        string col_menu_level = commandArgs[1];
                //        string cmd = commandArgs[2];

                //        switch (cmd)
                //        {
                //            case "up":
                //                SetPageOrder(cmd, col_menu_level, col_parent_id);
                //                break;
                //            case "down":
                //                SetPageOrder(cmd, col_menu_level, col_parent_id);
                //                break;

                //        }
                //        BindGridView();
                //    }
                //}
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        //private void SetPageOrder(string cmd, string col_menu_level, string col_parent_id)
        //{
        //    if (new OfflineDonationBAL().UpdatePageOrder(cmd, col_menu_level, col_parent_id))
        //    {

        //    }
        //}
    }
}