using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{

    #region  Page Model Class

    public class GetAllICUOnWheelSubDetailsModel 
    {
        public long Index { get; set; }

        public GetAllICUOnWheelSubDetailsResult objData { get; set; }
    }

    public class GetAllICUOnWheelImageDetailsModel 
    {
        public long Index { get; set; }

        public GetAllICUOnWheelImageDetailsResult objData { get; set; }
    }

    #endregion

    public partial class ICUOnWheel : System.Web.UI.Page
    {
        #region Page Variable

        public List<GetAllICUOnWheelSubDetailsModel> ICUOnWheelSubDetailsList
        {
            get
            {
                var obj = this.Session["ICUOnWheelSubDetailsList"];
                if (obj == null) { obj = this.Session["ICUOnWheelSubDetailsList"] = new List<GetAllICUOnWheelSubDetailsModel>(); }
                return (List<GetAllICUOnWheelSubDetailsModel>)obj;
            }

            set
            {
                this.Session["ICUOnWheelSubDetailsList"] = value;
            }
        }


        public class GetAllICUOnWheelImageDetailsModel
        {
            public long Index { get; set; }

            public GetAllICUOnWheelImageDetailsResult objData { get; set; }
        }


        public List<GetAllICUOnWheelImageDetailsModel> ICUOnWheelImageDetailsList
        {
            get
            {
                var obj = this.Session["ICUOnWheelImageDetailsList"];
                if (obj == null) { obj = this.Session["ICUOnWheelImageDetailsList"] = new List<GetAllICUOnWheelImageDetailsModel>(); }
                return (List<GetAllICUOnWheelImageDetailsModel>)obj;
            }

            set
            {
                this.Session["ICUOnWheelImageDetailsList"] = value;
            }
        }

        #endregion

        #region Main Page Events

        #region Page Main Functions

        private void BindGridView(string strSearch = "")
        {
            using (ICUOnWheelRepository objICUOnWheelRepository = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {
                var subDetails = objICUOnWheelRepository.GetAllICUOnWheelMaster();
                if (!string.IsNullOrWhiteSpace(strSearch))
                {
                    var FilterDetails = subDetails.Where(x => x.ICUName.Contains(strSearch)).ToList();
                    subDetails = FilterDetails;
                }
                if (subDetails.Count() > 0)
                {
                    grdUser.DataSource = subDetails;
                    var mainData = objICUOnWheelRepository.GetAllICUOnWheelMainDesc();
                    if (mainData != null)
                    {
                        hfMainDetailsId.Value = mainData.Id.ToString();
                        txtDescription.Text = HttpUtility.HtmlDecode(mainData.ICUOnWheelDesc);
                    }
                    else
                    {
                        txtDescription.Text = HttpUtility.HtmlDecode(subDetails.FirstOrDefault().ICUDetails);
                    }
                    BindImage();
                }
                else
                {
                    grdUser.DataSource = null;
                }
                grdUser.DataBind();
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
                    btn_Update.Visible = false;
                    ICUOnWheelSubDetailsList = new List<GetAllICUOnWheelSubDetailsModel>();

                    BindImage();
                    BindSubDetails();
                    ClearControlValues();
                    ClearSubDetails();
                    ClearSubImageDetails();
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
                    break;
                default:
                    pnlView.Visible = true;
                    pnlEntry.Visible = false;
                    break;
            }
        }

        private void ClearControlValues()
        {
            hfID.Value = "0";
            txtICUOnWheelName.Text = "";
            ClearSubDetails();
            ClearSubImageDetails();
            BindImage();
            BindSubDetails();
        }

        private bool LoadMain(GetAllICUOnWheelMasterResult objBo)
        {
            bool isActive = true;

            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfID.Value);
            }


            if (!string.IsNullOrEmpty(txtICUOnWheelName.Text.Trim()))
            {
                objBo.ICUName = txtICUOnWheelName.Text;
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Name", PopupMessageType.error);
                isActive = false;
            }

            objBo.ICUDetails = HttpUtility.HtmlEncode(txtDescription.Text);


            return isActive;
        }

        #endregion

        #region Page Main Methods

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
            }
            if (!Page.IsPostBack)
            {
                BindGridView();
                ClearControlValues();
                ClearSubDetails();
                ClearSubImageDetails();
                ShowHideControl(VisibityType.GridView);
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

        protected void btn_Cancel_ServerClick(object sender, EventArgs e)
        {
            ClearControlValues();
            BindGridView();
            ShowHideControl(VisibityType.GridView);
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (ICUOnWheelRepository objAcc = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {
                GetAllICUOnWheelMasterResult objBO = new GetAllICUOnWheelMasterResult();
                if (LoadMain(objBO))
                {

                    GetAllICUOnWheelMainDescResult objBOs = new GetAllICUOnWheelMainDescResult();
                    objBOs.Id = Convert.ToInt32(hfMainDetailsId.Value);

                    objBOs.ICUOnWheelDesc = objBO.ICUDetails;

                    if (!objAcc.InsertOrUpdateICUOnWheelMainDesc(objBOs, out errorMessage))
                    {
                        if (!objAcc.InsertOrUpdateICUOnWheelMaster(objBO, out errorMessage))
                        {
                            if (ICUOnWheelSubDetailsList.Count > 0)
                            {
                                foreach (var row in ICUOnWheelSubDetailsList.Select(x => x.objData))
                                {
                                    row.MainId = objBO.Id;
                                    //if (row["Id"].ToString().Contains("Temp_"))
                                    if (row.IsDelete == false)
                                    {
                                        if (objAcc.InsertOrUpdateICUOnWheelSubDetails(row, out errorMessage))
                                        {
                                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (row.Id != 0)
                                        {
                                            if (objAcc.RemoveICUOnWheelSubDetails(row.Id, out errorMessage))
                                            {
                                                Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            ClearControlValues();
                            BindGridView();
                            ShowHideControl(VisibityType.GridView);
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                        else
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);

                        }
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);

                    }
                }
            }
        }

        protected void ibtn_Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfID.Value = grdUser.DataKeys[rowindex]["Id"].ToString();

            using (ICUOnWheelRepository objAcc = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {
                var dataInfo = objAcc.GetAllICUOnWheelMasterById((Convert.ToInt32(hfID.Value)));
                if (dataInfo != null)
                {
                    txtICUOnWheelName.Text = dataInfo.ICUName;
                    ICUOnWheelSubDetailsList = objAcc.GetAllICUOnWheelSubDetails().Where(x => x.MainId == dataInfo.Id).OrderBy(x => x.Id).Select((x, index) => new GetAllICUOnWheelSubDetailsModel { objData = x, Index = index }).ToList();
                    //ICUOnWheelImageDetailsList = objAcc.GetAllICUOnWheelImageDetails().Where(x => x.MainId == dataInfo.Id).OrderBy(x => x.Id).Select((x, index) => new GetAllICUOnWheelImageDetailsModel { objData = x, Index = index }).ToList();
                    BindImage();
                    BindSubDetails();
                    ShowHideControl(VisibityType.Edit);
                }
            }
        }

        protected void ibtn_Delete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                int rowId = Convert.ToInt32(grdUser.DataKeys[rowindex]["Id"].ToString());
                using (ICUOnWheelRepository objAcc = new ICUOnWheelRepository(Functions.strSqlConnectionString))
                {

                    if (!objAcc.RemoveICUOnWheelMaster(rowId, out errorMessage))
                    {
                        ClearControlValues();
                        BindGridView();
                        ShowHideControl(VisibityType.GridView);
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            hfLeftImage.Value = "";
            lblLeftImage.Text = "";
            aRemoveLeft.Visible = false;
            ShowHideControl(VisibityType.Insert);
        }

        protected void btnMainPageDescription_Click(object sender, EventArgs e)
        {
            string ICUDetails;
            if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                ICUDetails = HttpUtility.HtmlEncode(txtDescription.Text);
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Description", PopupMessageType.error);
                return;
            }
            string errorMessage = "";
            using (ICUOnWheelRepository objAcc = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {
                var getData = objAcc.GetAllICUOnWheelMaster().FirstOrDefault();
                if (getData == null)
                {
                    getData = new GetAllICUOnWheelMasterResult
                    {
                        ICUName = "",
                        ICUDetails = ICUDetails,
                        Id = 0
                    };
                }
                else
                {
                    getData.ICUDetails = ICUDetails;
                }
                if (!objAcc.InsertOrUpdateICUOnWheelMaster(getData, out errorMessage))
                {
                    ShowHideControl(VisibityType.GridView);
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
            }
        }

        private int _counter;
        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (_counter == 0)
                {
                    var ibtn_Delete = e.Row.FindControl("ibtn_Delete")
                        as LinkButton;
                    //ibtn_Delete.Visible = false;
                    _counter++;
                }
            }
        }
        #endregion

        #endregion

        #region Image Events

        #region Image Page Methods
        
        protected void ibtn_SubmitImageDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (ICUOnWheelRepository objAcc = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {
                int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
                hfSubImageId.Value = gvSubmitImage.DataKeys[rowindex]["Id"].ToString();
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    //var lblImageName = row.FindControl("lblImageName") as Label;
                    if (!objAcc.RemoveICUOnWheelImageDetails(Convert.ToInt32(hfSubImageId.Value), out errorMessage))
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    }
                    else
                    {
                        Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                    }
                }
                BindImage();
            }
        }

        protected void btnSubmitImage_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            using (ICUOnWheelRepository objAcc = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {
                long indexId = 0;
                GetAllICUOnWheelImageDetailsResult objBO = new GetAllICUOnWheelImageDetailsResult();

                if (LoadImage(objBO))
                {
                    if (long.TryParse(hfSubImageId.Value, out indexId))
                    {
                        //if (indexId != 0)
                        {
                            if (!objAcc.InsertOrUpdateICUOnWheelImageDetails(objBO, out errorMessage))
                            {
                                Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                                ClearSubImageDetails();
                            }
                            else
                            {
                                Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                            }
                        }
                    }
                }
            }
            BindImage();
        }

        #endregion

        #region Image Page Functions

        private bool LoadImage(GetAllICUOnWheelImageDetailsResult objBo)
        {
            bool isActive = true;
            
            if(objBo==null)
            {
                objBo = new GetAllICUOnWheelImageDetailsResult();
            }

            if (string.IsNullOrWhiteSpace(hfSubImageId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfSubImageId.Value);
            }
                if (!string.IsNullOrWhiteSpace(hfID.Value))
                {
                    objBo.MainId = Convert.ToInt32(hfID.Value);
                }
            
            else
            {
                objBo.MainId = 0;
            }
            if (fuPic.HasFile)
            {
                string filePath = ConfigDetailsValue.AcademicMedicalFiles;

                //System.Drawing.Image img = System.Drawing.Image.FromStream(fuPic.PostedFile.InputStream);
                //int height = img.Height;
                //int width = img.Width;
                decimal size = Math.Round(((decimal)fuPic.PostedFile.ContentLength / (decimal)1024), 2);
                //if (height != 800 || width != 1200)
                //{
                //    Functions.MessagePopup(this, "Please upload 1200px*800px.", PopupMessageType.error);
                //    return false;
                //}

                if (!filePath.Contains("|"))
                {
                    if (fuPic.PostedFile.ContentLength > 10485760)
                    {
                        Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                        return false;
                    }
                    objBo.ImageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuPic.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 =  filePath + objBo.ImageName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(objBo.ImageName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension;
                    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuPic.SaveAs(Server.MapPath(filePath) + objBo.ImageName);

                        objBo.ImageName = filePath + objBo.ImageName;
                    }
                    else
                    {

                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return false;

                    }
                }
                else
                {

                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return false;
                }
            }
            else
            {
                if (objBo.Id != 0)
                {
                    Functions.MessagePopup(this, "Please upload file '.png,.jpg,.jpeg'.", PopupMessageType.warning);

                }
                else
                {
                    objBo.ImageName = (imgProfile.ImageUrl);
                }
            }

            return isActive;
        }

        private void ClearSubImageDetails()
        {
            hfSubImageId.Value = "0";
            hfIndexId.Value = "0";
            BindImage();
        }

        private void BindImage()
        {
            long lgId;
            using (ICUOnWheelRepository objICUOnWheelRepository = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {
                var dataImage = objICUOnWheelRepository.GetAllICUOnWheelImageDetails().Where(x => x.IsDelete != true).OrderBy(x => x.Id).ToList();
                if (dataImage != null)
                {
                    gvSubmitImage.DataSource = dataImage;
                    gvSubmitImage.DataBind();
                }
            }
        }

        #endregion

        #endregion

        #region Sub Details Events

        #region Sub Details Page Methods

        protected void btnSubDetails_Click(object sender, EventArgs e)
        {

            string errorMessage = "";
            using (ICUOnWheelRepository objAcc = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {
                GetAllICUOnWheelSubDetailsModel objBO = new GetAllICUOnWheelSubDetailsModel();
                objBO.objData = new GetAllICUOnWheelSubDetailsResult();

                long indexId = 0;
                if (long.TryParse(hfSubIndexId.Value, out indexId))
                {
                    if (indexId == -1)
                    {
                        objBO.Index = ICUOnWheelSubDetailsList.Count ;
                    }
                    else
                    {
                        objBO.Index = Convert.ToInt32(hfSubIndexId.Value);
                    }
                }
                else
                {
                    hfSubIndexId.Value = "Insert";
                    //objBO.Index = 0;
                }

                if (LoadSubDetails(objBO.objData))
                {
                    bool isUpdate = false;
                    if (string.IsNullOrWhiteSpace(hfSubIndexId.Value) || hfSubIndexId.Value == "Insert")
                    {
                        objBO.Index = ICUOnWheelSubDetailsList.Count;
                        ICUOnWheelSubDetailsList.Add(objBO);
                    }

                    ICUOnWheelSubDetailsList.ToList().ForEach(x =>
                    {
                        if (x.Index == objBO.Index)
                        {
                            x.objData.Id = objBO.objData.Id;
                            x.objData.ImageName = objBO.objData.ImageName;
                            x.objData.SubTitle = objBO.objData.SubTitle;
                            x.objData.SubDescription = (objBO.objData.SubDescription);
                            isUpdate = true;
                        }
                    });
                }
            }
            ClearSubDetails();
            BindSubDetails();
        }
        
        protected void ibtn_SubmitSubDetailsEdit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfSubImageId.Value = gvSubDetails.DataKeys[rowindex]["Id"].ToString();

            hfLeftImage.Value = "";
            lblLeftImage.Text = "";
            using (ICUOnWheelRepository objAcc = new ICUOnWheelRepository(Functions.strSqlConnectionString))
            {
                ICUOnWheelSubDetailsList.ToList().ForEach(x =>
                {
                    if (x.Index == rowindex)
                    {
                        hfSubIndexId.Value = rowindex.ToString();
                        hfSubId.Value = x.objData.Id.ToString();
                        txtSubTitle.Text = x.objData.SubTitle;
                        txtSubDescription.Text = HttpUtility.HtmlDecode(x.objData.SubDescription);
                        if (!string.IsNullOrWhiteSpace(x.objData.ImageName))
                        {
                            hfLeftImage.Value = x.objData.ImageName;
                            lblLeftImage.Text = x.objData.ImageName;
                            aRemoveLeft.Visible = true;
                        }
                    }
                });
            }
        }

        protected void ibtn_SubmitSubDetailsDelete_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfSubId.Value = gvSubDetails.DataKeys[rowindex]["Id"].ToString();
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                //var lblImageName = row.FindControl("lblImageName") as Label;
                ICUOnWheelSubDetailsList.ToList().ForEach(x =>
                {
                    if (x.Index == rowindex)
                    {
                        x.objData.IsDelete = true;
                    }
                });
                //ICUOnWheelImageDetailsList.Remove(model);
            }
            BindSubDetails();
        }

        #endregion

        #region Sub Details Page Functions

        private bool LoadSubDetails(GetAllICUOnWheelSubDetailsResult objBo)
        {
            bool isActive = true;

            if (objBo == null)
            {
                objBo = new GetAllICUOnWheelSubDetailsResult();
            }
            if (string.IsNullOrWhiteSpace(hfSubId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfSubId.Value);
            }

            if (string.IsNullOrWhiteSpace(hfID.Value))
            {
                objBo.MainId = 0;
            }
            else
            {
                objBo.MainId = Convert.ToInt32(hfID.Value);
            }

            if (fuSubList.HasFile)
            {
                string filePath = ConfigDetailsValue.AcademicMedicalFiles;

                //System.Drawing.Image img = System.Drawing.Image.FromStream(fuSubList.PostedFile.InputStream);
                //int height = img.Height;
                //int width = img.Width;
                decimal size = Math.Round(((decimal)fuSubList.PostedFile.ContentLength / (decimal)1024), 2);
                //if (height != 800 || width != 1200)
                //{
                //    Functions.MessagePopup(this, "Please upload 1200px*800px.", PopupMessageType.error);
                //    return false;
                //}

                if (!filePath.Contains("|"))
                {
                    if (fuSubList.PostedFile.ContentLength > 10485760)
                    {
                        Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                        return false;
                    }
                    objBo.ImageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuSubList.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = filePath + objBo.ImageName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(objBo.ImageName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension;
                    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuSubList.SaveAs(Server.MapPath(filePath) + objBo.ImageName);

                        objBo.ImageName = filePath + objBo.ImageName;
                    }
                    else
                    {

                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return false;

                    }
                }
                else
                {
                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return false;
                }
            }
            else
            {
                objBo.ImageName = (hfLeftImage.Value);
            }

            if (!string.IsNullOrEmpty(txtSubTitle.Text.Trim()))
            {
                objBo.SubTitle = txtSubTitle.Text;
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Sub Title", PopupMessageType.error);
                isActive = false;
            }

            if (!string.IsNullOrEmpty(txtSubDescription.Text.Trim()))
            {
                objBo.SubDescription = HttpUtility.HtmlEncode(txtSubDescription.Text);
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Sub Description", PopupMessageType.error);
                isActive = false;
            }

            return isActive;
        }

        private void ClearSubDetails()
        {
            hfSubId.Value = "0";
            hfSubIndexId.Value = "Insert";
            txtSubTitle.Text = "";
            txtSubDescription.Text = "";

            hfLeftImage.Value = "";
            lblLeftImage.Text = "";
            aRemoveLeft.Visible = false;
        }

        private void BindSubDetails()
        {
            long lgId;
            if (ICUOnWheelSubDetailsList.Count() <= 0)
            {

                {
                    ICUOnWheelSubDetailsList = new List<GetAllICUOnWheelSubDetailsModel>();
                }
            }

            gvSubDetails.DataSource = ICUOnWheelSubDetailsList.Select(x => x.objData).Where(x => x.IsDelete != true).ToList();
            gvSubDetails.DataBind();
        }

        #endregion

        #endregion
    }
}