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
using Unmehta.WebPortal.Web.Common;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public partial class BannerMaster : System.Web.UI.Page
    {
        #region Page Load
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
            try
            {
                //if (HttpContext.Current.Session["UserName"] != null)
                //{
                if (!Page.IsPostBack)
                {
                    ShowHideControl(VisibityType.GridView);
                }
                //}
                //else
                //    Response.Redirect("/CMS/LoginPage.aspx");
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }
        #endregion

        #region Search || Advanced Search
        protected void btn_SearchCancel_Click(object sender, EventArgs e)
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
        #endregion

        #region GridView Operation
        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["banner_id"]);
                    if (e.CommandName == "eDelete")
                    {
                        BannerMasterBO objBo = new BannerMasterBO();
                        objBo.banner_id = bytID;
                        //objBo.UpdatedBy = SessionWrapper.UserID;
                        new BannerMasterBAL().DeleteRecord(objBo);
                        BindGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    ClearControlValues(pnlEntry);
                    if (FillControls(bytID))
                    {
                        if (e.CommandName == "eView")
                            ShowHideControl(VisibityType.View);
                        if (e.CommandName == "eEdit")
                        {
                            rfvBannerImage.Enabled = false;
                            hfID.Value = bytID.ToString();
                            ShowHideControl(VisibityType.Edit);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private bool FillControls(Int32 iPkId)
        {
            // ddlCompany.DataBind();
            BannerMasterBO objBo = new BannerMasterBO();
            objBo.banner_id = iPkId;
            DataSet ds = new BannerMasterBAL().SelectRecord(objBo);
            if (ds == null) return false;
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;

            if (dr["banner_title"] != DBNull.Value)
                txtBannerTitle.Text = dr["banner_title"].ToString();

            //if (dr["banner_desc"] != DBNull.Value)
            //    txtbannerdesc.Text = dr["banner_desc"].ToString();


            if (dr["banner_rank"] != DBNull.Value)
                txtBannerRank.Text = dr["banner_rank"].ToString();

            //if (dr["banner_url"] != DBNull.Value)
            //    fuBannerImage.= dr["banner_desc"].ToString();
            hfFilePOst.Value=dr["banner_url"].ToString();
            imgddo_photo.ImageUrl = dr["banner_url"].ToString();


            using (IBannerSubDetailsRepository objAcc = new BannerSubDetailsRepository(Functions.strSqlConnectionString))
            {
                GetListValueDoctor = Functions.ToDataTable(objAcc.GetAllBannerSubDetails((long)objBo.banner_id).ToList());
                BindgvDoctor();
            }

            if (dr["is_active"] != DBNull.Value)
                ddlActiveInactive.SelectedValue = Convert.ToBoolean(dr["is_active"]) ? "1" : "0";

            return true;
        }
        private void BindGridView()
        {
            gView.DataBind();
        }
        #endregion

        #region Save || Update || Cancel
        private bool LoadControls(BannerMasterBO objBo)
        {
            if (!string.IsNullOrEmpty(txtBannerTitle.Text))
                objBo.banner_title = txtBannerTitle.Text;
            else
                objBo.banner_title = "";


            if (!string.IsNullOrEmpty(txtBannerRank.Text))
                objBo.banner_rank = Convert.ToInt32(txtBannerRank.Text);

            if (fuBannerImage.HasFile)
            {
                //System.Drawing.Image img = System.Drawing.Image.FromStream(fuBannerImage.PostedFile.InputStream);
                //int height = img.Height;
                //int width = img.Width;
                //decimal size = Math.Round(((decimal)fuBannerImage.PostedFile.ContentLength / (decimal)1024), 2);
                //if (!((height > 350 && height < 450) ||( width > 1400 && width< 1550)))
                //{
                //    Functions.MessagePopup(this, "Please upload 1500px*400px.", PopupMessageType.error);
                //    return false;
                //}
                //else
                {
                    string documentfile = string.Empty;
                    documentfile = SaveFile();
                    if (!string.IsNullOrEmpty(documentfile))
                        objBo.banner_url = documentfile;
                }
            }
            else
            {
                if(!string.IsNullOrWhiteSpace(hfFilePOst.Value))
                {
                        objBo.banner_url = hfFilePOst.Value;

                }else
                {
                        objBo.banner_url ="";
                    
                }
            }
            //if (fuBannerImage.HasFile)
            //    imgddo_photo.ImageUrl = ConfigurationManager.AppSettings["BannerImagePath"] + fuBannerImage.FileName;
            //else
            //    imgddo_photo.ImageUrl = "~/Media/BannerImages/nophoto.jpg";

            objBo.IsActive = ddlActiveInactive.SelectedValue.ToString();
            objBo.user_id = Convert.ToInt32(SessionWrapper.UserDetails.Id);
            objBo.ip_add = GetIPAddress;
            return true;
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                using (IHomePageRepository objHomePageRepository = new HomePageRepository(Functions.strSqlConnectionString))
                {

                    BannerMasterBO objBo = new BannerMasterBO();
                    if (LoadControls(objBo))
                    {
                        if (new BannerMasterBAL().InsertRecord(objBo))
                        {
                            var dataListAward = objHomePageRepository.GetAllBannerMasterHome().OrderByDescending(x => x.Id).FirstOrDefault();
                            using (IBannerSubDetailsRepository objAcc = new BannerSubDetailsRepository(Functions.strSqlConnectionString))
                            {
                                if (GetListValueDoctor.Rows.Count > 0)
                                {
                                    foreach (DataRow row in GetListValueDoctor.Rows)
                                    {
                                        if (row["Id"].ToString().Contains("Temp_"))
                                        {
                                            string errorMessage = "";
                                            if (objAcc.InsertOrUpdateBannerSubDetails(new Data.Hospital.GetAllBannerSubDetailsByBannerIdResult { Id = 0, BannerId = dataListAward.Id, BannerDescription = row["BannerDescription"].ToString(), TextXPosition = row["TextXPosition"].ToString(), TextYPosition = row["TextYPosition"].ToString() }, out errorMessage))
                                            {
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            Functions.MessagePopup(this, "Record inserted successfully.", PopupMessageType.success);
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.error);
                            return;
                        }
                    }
                }
                BindGridView();
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                BannerMasterBO objBo = new BannerMasterBO();
                if (LoadControls(objBo))
                {
                    objBo.banner_id = Convert.ToInt32(hfID.Value);
                    if (new BannerMasterBAL().UpdateRecord(objBo))
                    {
                        using (IBannerSubDetailsRepository objAcc = new BannerSubDetailsRepository(Functions.strSqlConnectionString))
                        {
                            if (GetListValueDoctor.Rows.Count > 0)
                            {
                                foreach (DataRow row in GetListValueDoctor.Rows)
                                {
                                    if (row["Id"].ToString().Contains("Temp_"))
                                    {
                                        string errorMessage = "";
                                        if (objAcc.InsertOrUpdateBannerSubDetails(new Data.Hospital.GetAllBannerSubDetailsByBannerIdResult { Id = 0, BannerId = objBo.banner_id, BannerDescription = row["BannerDescription"].ToString(), TextXPosition = row["TextXPosition"].ToString(), TextYPosition = row["TextYPosition"].ToString() }, out errorMessage))
                                        {
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        string errorMessage = "";
                                        if (objAcc.InsertOrUpdateBannerSubDetails(new Data.Hospital.GetAllBannerSubDetailsByBannerIdResult { Id = Convert.ToInt32(row["Id"].ToString()), BannerId = objBo.banner_id, BannerDescription = row["BannerDescription"].ToString(), TextXPosition = row["TextXPosition"].ToString(), TextYPosition = row["TextYPosition"].ToString() }, out errorMessage))
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Record already exists in database.", PopupMessageType.warning);
                        return;
                    }
                    BindGridView();
                    ShowHideControl(VisibityType.GridView);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Functions.MessagePopup(this, "Record discarded.", PopupMessageType.info);
                ShowHideControl(VisibityType.GridView);
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }


        private string SaveFile()
        {
            try
            {
                if (fuBannerImage.HasFile)
                {

                    var BannerImagePath = ConfigDetailsValue.BannerImageUploadPath;
                    //HttpContext.Current.Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["BannerImagePath"]));

                    var fname = Path.GetExtension(fuBannerImage.FileName);
                    var count = fuBannerImage.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(BannerImagePath));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(BannerImagePath));
                    for (int i = 0; i < fuBannerImage.FileName.Split('.').Length; i++)
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
                        var filename1 = fuBannerImage.FileName.Replace(" ", "_");

                        filename1 = filename1.ToLower();

                        if (!Directory.Exists(Server.MapPath(BannerImagePath)))
                        {
                            //If No any such directory then creates the new one
                            Directory.CreateDirectory(Server.MapPath(BannerImagePath));
                        }


                        // Create the path and file name to check for duplicates.
                        var pathToCheck1 = BannerImagePath + filename1;



                        // Create a temporary file name to use for checking duplicates.
                        var tempfileName1 = "";

                        ErrorLogger.ERROR(pathToCheck1, "Main Path=> " + ConfigDetailsValue.BannerImageUploadPath, this);



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
                                pathToCheck1 = BannerImagePath + tempfileName1;
                                counter++;
                            }


                            filename1 = tempfileName1;
                        }


                        //Save selected file into specified location
                        fuBannerImage.SaveAs(Server.MapPath(BannerImagePath) + filename1);
                        return (BannerImagePath) + filename1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
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

        #endregion

        #region Doctor List Logic
        #region Functions
        private DataTable GetListValueDoctor
        {
            get
            {
                DataTable result = new DataTable();

                if (ViewState["someID"] != null)
                {
                    result = (DataTable)ViewState["someID"];
                }
                else
                {
                    result = new DataTable("tblgvDoctor");
                    result.Columns.Add(new DataColumn("Id", typeof(string)));
                    result.Columns.Add(new DataColumn("BannerId", typeof(string)));
                    result.Columns.Add(new DataColumn("BannerDescription", typeof(string)));
                    result.Columns.Add(new DataColumn("TextXPosition", typeof(string)));
                    result.Columns.Add(new DataColumn("TextYPosition", typeof(string)));
                }

                return result;
            }
            set
            {
                ViewState["someID"] = value;
            }
        }

        private void BindgvDoctor()
        {

            gvDoctor.DataSource = GetListValueDoctor;
            gvDoctor.DataBind();
        }
        #endregion

        #region Page Load
        protected void ibtn_DoctorEdit_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string Id = gvDoctor.DataKeys[rowindex]["Id"].ToString();

            foreach (DataRow row in GetListValueDoctor.Rows)
            {
                if (row["Id"].ToString() == Id)
                {
                    hfSubId.Value = row["Id"].ToString();
                    txtbannerdesc.Text = HttpUtility.HtmlDecode( row["BannerDescription"].ToString());
                    ddlXAxis.SelectedValue = row["TextXPosition"].ToString();
                    //ddlYAxis.SelectedValue = row["TextYPosition"].ToString();
                    break;
                }
            }
        }

        protected void ibtn_DoctorDelete_Click(object sender, EventArgs e)
        {
            string strError = "";
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            string rowId = (gvDoctor.DataKeys[rowindex]["Id"].ToString());
            GridViewRow gvRow = gvDoctor.Rows[rowindex];

            DataTable dt = null;
            dt = GetListValueDoctor;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["Id"].ToString() == rowId)
                    dr.Delete();
            }
            dt.AcceptChanges();

            if (rowId != "0" && !string.IsNullOrWhiteSpace(rowId) && !rowId.Contains("Temp_"))
            {
                using (IBannerSubDetailsRepository objAcc = new BannerSubDetailsRepository(Functions.strSqlConnectionString))
                {
                    if (!objAcc.RemoveBannerSubDetails(Convert.ToInt32(rowId), out strError))
                    {

                        Functions.MessagePopup(this, strError, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, strError, PopupMessageType.error);

                    }
                }
            }
            else
            {
                Functions.MessagePopup(this, "Record Removed Done", PopupMessageType.success);
            }
            GetListValueDoctor = dt;
            gvDoctor.DataSource = GetListValueDoctor;
            gvDoctor.DataBind();
            ShowHideControl(VisibityType.Edit);
        }

        protected void btnAddDoctor_Click(object sender, EventArgs e)
        {
            bool isNoError = true;
            if (string.IsNullOrWhiteSpace(txtbannerdesc.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter Caption", PopupMessageType.error);
                return;
            }
            if (isNoError)
            {
                DataTable dt = null;
                dt = GetListValueDoctor;
                bool isExist = false;
                foreach (DataRow row in GetListValueDoctor.Rows)
                {

                    if (row["Id"].ToString() == hfSubId.Value)
                    {
                        row["BannerDescription"] = HttpUtility.HtmlEncode(txtbannerdesc.Text);
                        row["TextXPosition"] = ddlXAxis.SelectedValue;
                        row["TextYPosition"] = "top";
                        isExist = true;
                    }
                }
                if (!isExist)
                {
                    if (GetListValueDoctor.Rows.Count >= 2)
                    {
                        Functions.MessagePopup(this, "Maximum 2 Caption Allow.", PopupMessageType.error);
                        return;
                    }
                    DataRow dr = dt.NewRow();
                    int rowCount = 0;
                    if (dt.Rows.Count > 0)
                    {
                        rowCount = dt.Rows.Count;
                    }
                    dr["Id"] = "Temp_" + (rowCount + 1);
                    dr["BannerDescription"] = HttpUtility.HtmlEncode(txtbannerdesc.Text);
                    dr["TextXPosition"] = ddlXAxis.SelectedValue;
                    dr["TextYPosition"] = "top";
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
                GetListValueDoctor = dt;
            }
            gvDoctor.DataSource = GetListValueDoctor;
            gvDoctor.DataBind();

            txtbannerdesc.Text = "";
            ddlXAxis.SelectedIndex = 0;
            //ddlYAxis.SelectedIndex = 0;

            //ShowHideControl(VisibityType.Edit);
        }
        #endregion

        #endregion
    }
}