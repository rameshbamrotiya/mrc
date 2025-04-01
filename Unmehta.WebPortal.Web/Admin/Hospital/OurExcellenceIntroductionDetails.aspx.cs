using System;
using System.IO;
using System.Web;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin
{
    public partial class OurExcellenceIntroductionDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (SessionWrapper.UserDetails.UserName == null)
                {
                    Response.Redirect("~/Login");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login");
            }
            if (!IsPostBack)
            {
                string strQuery = Request.QueryString.ToString();
                if (string.IsNullOrWhiteSpace(strQuery))
                {
                    Response.Redirect("~/Admin/Hospital/OurExcellence.aspx");
                }
                else
                {
                    hfId.Value = strQuery;
                    using (IOurExcellenceMasterRepository objOurExcellenceMasterRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
                    {
                        var dataDetails = objOurExcellenceMasterRepository.GetTblOurExcellenceMasterInformationById(Convert.ToInt32(hfId.Value), 1);
                        if (dataDetails != null)
                        {
                            txtHODName.Text = dataDetails.HODName;
                            txtHODDesignation.Text = dataDetails.HODDesignation;
                            imgProfile.ImageUrl = dataDetails.HODImage;
                            txtIntoductiondesc.Text = HttpUtility.HtmlDecode(dataDetails.IntroductionDesc);
                        }
                    }
                }
            }
        }

        #region Information
        protected void btnInformationSave_Click(object sender, EventArgs e)
        {

            string errorMessage = "";
            string fileNameS = "";
            if (fuHODImage.HasFile)
            {
                string filePath = ConfigDetailsValue.AddOurExcellenceFileUploadHODPath;

                if (!filePath.Contains("|"))
                {
                    if (fuHODImage.PostedFile.ContentLength > 1048576)
                    {
                        Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                        return;
                    }
                    fileNameS = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuHODImage.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + fileNameS;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(fuHODImage.FileName);
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
                        fuHODImage.SaveAs(Server.MapPath(filePath) + "/" + fileNameS);
                    }
                    else
                    {

                        Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return;

                    }
                }
                else
                {

                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return;
                }
            }
            else
            {
                fileNameS = System.IO.Path.GetFileName(imgProfile.ImageUrl);
            }
            using (IOurExcellenceMasterRepository objOurExcellenceMasterRepository = new OurExcellenceMasterRepository(Functions.strSqlConnectionString))
            {

                if (!objOurExcellenceMasterRepository.UpdateTblOurExcellenceInformationMaster(Convert.ToInt32(hfId.Value), txtHODName.Text, fileNameS,
                    txtHODDesignation.Text, txtIntoductiondesc.Text, out errorMessage))
                {
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                }
            }
        }
        #endregion
    }
}