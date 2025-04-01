using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin
{
    public partial class HeaderFooterMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfLeftImage.Value = "";
                lblLeftImage.Text = "";
                aRemoveLeft.Visible = false;


                hfRightImage.Value ="";
                lblRightImage.Text ="";
                aRemoveRight.Visible = false;

                BindDesignation();
            }
        }

        private void BindDesignation()
        {
            using (IHomePageRepository objData = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var data = objData.GetHeaderFooter();

                if (data != null)
                {
                    hfId.Value = data.Id.ToString();
                    txtHeaderDetails.Text = HttpUtility.HtmlDecode(data.HeaderDetails);
                    txtFooterDetails.Text = HttpUtility.HtmlDecode(data.FooterDetails);

                    if (!string.IsNullOrWhiteSpace(data.HeaderLogo))
                    {
                        hfLeftImage.Value = data.HeaderLogo;
                        lblLeftImage.Text = data.HeaderLogo;
                        aRemoveLeft.Visible = true;
                    }

                    if (!string.IsNullOrWhiteSpace(data.FooterLogo))
                    {
                        hfRightImage.Value = data.FooterLogo;
                        lblRightImage.Text = data.FooterLogo;
                        aRemoveRight.Visible = true;
                    }
                }
                else
                {
                    hfId.Value = 0.ToString();
                    txtHeaderDetails.Text = "";
                    txtFooterDetails.Text = "";
                    hfLeftImage.Value = "";
                    lblLeftImage.Text = "";
                    aRemoveLeft.Visible = false;


                    hfRightImage.Value = "";
                    lblRightImage.Text = "";
                    aRemoveRight.Visible = false;


                }
            }
        }

        private string SaveFile()
        {
            try
            {
                if (fuDocUpload.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.AccredationImg;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (IHomePageRepository objData = new HomePageRepository(Functions.strSqlConnectionString))
            {
                GetHeaderFooterResult getHeaderFooterResult = new GetHeaderFooterResult();
                getHeaderFooterResult.Id = Convert.ToInt32(hfId.Value);
                getHeaderFooterResult.HeaderDetails = HttpUtility.HtmlEncode(txtHeaderDetails.Text);
                getHeaderFooterResult.FooterDetails = HttpUtility.HtmlEncode(txtFooterDetails.Text);
                if (fuDocUpload.HasFile)
                {
                    string documentfile = string.Empty;
                    documentfile = SaveFile();

                    if (!string.IsNullOrEmpty(documentfile))
                        getHeaderFooterResult.FooterLogo = documentfile;
                }
                else
                {
                    getHeaderFooterResult.FooterLogo = hfRightImage.Value;
                }
                if (fuHeaderLogo.HasFile)
                {
                    string documentfile = string.Empty;
                    documentfile = SaveFileHeader();

                    if (!string.IsNullOrEmpty(documentfile))
                        getHeaderFooterResult.HeaderLogo = documentfile;
                }
                else
                {
                    getHeaderFooterResult.HeaderLogo = hfLeftImage.Value;
                }
                string errorMessage = "Saved Successfully.";
                if (!objData.InsertOrUpdateHeaderFooter(getHeaderFooterResult, out errorMessage))
                {
                    //Response.Redirect("HeaderFooterMaster.aspx");
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                    BindDesignation();
                }
                else
                {
                    Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                }
            }
        }

        private string SaveFileHeader()
        {
            try
            {
                if (fuHeaderLogo.HasFile)
                {
                    var DocumentUpload = ConfigDetailsValue.AccredationImg;
                    var fname = Path.GetExtension(fuHeaderLogo.FileName);
                    var count = fuHeaderLogo.FileName.Split('.');
                    string type = "";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocumentUpload));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocumentUpload));
                    for (int i = 0; i < fuHeaderLogo.FileName.Split('.').Length; i++)
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
                        var filename1 = fuHeaderLogo.FileName.Replace(" ", "_");
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
                        fuHeaderLogo.SaveAs(Server.MapPath(DocumentUpload) + filename1);
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