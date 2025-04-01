using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.FrontEnd;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.FrontEnd;
using Unmehta.WebPortal.Web.Common;
using System.Web.Script.Services;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Data;
using BAL;

namespace Unmehta.WebPortal.Web
{
    public partial class Contribution : System.Web.UI.Page
    {

        public static string strBoard;

        public static string strHeaderImage;

        public static string strOutSide;

        public static string strOfflineDetails;

        public static string strQuickLink;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strHeaderImage = GetHeaderImage();
                strOutSide = GetDataDetails();
                strQuickLink = Functions.CreateQuickLink("Cares", "Contribution");

                aPrimaryProof.Visible = false;
                aSecondaryIdProof.Visible = false;
            
            }
        }

        private string GetDataDetails()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IHomePageRepository objBlogCategoryMasterRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetAllContributionMasterDetails(languageId).Where(x => x.Is_active == true).FirstOrDefault();

                if (dataMain != null)
                {
                LableData:
                    strBoardOfDirector = new StringBuilder();
                    strOfflineDetails = HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(dataMain.OfflineDonationDescription));
                    if (!string.IsNullOrWhiteSpace(dataMain.PageDescription))
                    {
                        strBoardOfDirector.Append(HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(dataMain.PageDescription)));
                    }
                    else
                    {
                        languageId = 1;
                        dataMain = objBlogCategoryMasterRepository.GetAllContributionMasterDetails(languageId).FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();

        }

        private string GetHeaderImage()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();
            using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                var dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Contribution").FirstOrDefault();

                if (dataMain != null)
                {
                LableData:
                    strBoardOfDirector = new StringBuilder();
                    if (!string.IsNullOrWhiteSpace(dataMain.HeaderImage))
                    {
                        strBoardOfDirector.Append(ResolveUrl(ConfigDetailsValue.HeaderImagePath + dataMain.HeaderImage));
                    }
                    else
                    {
                        languageId = 1;
                        dataMain = objBlogCategoryMasterRepository.GetBreadCumImageByPageName(languageId, "Contribution").FirstOrDefault();
                        if (languageId != 1)
                        {
                            goto LableData;
                        }
                    }
                }
            }
            return strBoardOfDirector.ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strError = "";
            string strTransactioNo = "";
            string strTxnId = "";
            string strFullName = "";

            using (IHomePageRepository objCandidateDetailsRepository = new HomePageRepository(Functions.strSqlConnectionString))
            {
                GetAllContributionTransactionMasterResult objBo = new GetAllContributionTransactionMasterResult();
                if (ValidateForm(ref objBo))
                {
                    var trno = Functions.GetRandomNumberStringForPayment();
                    DateTime d = DateTime.Now;
                    string strMerchTxnId = d.ToString("yyMMddHHmmss") + trno;
                    objBo.TxnId = strMerchTxnId;

                    if (!objCandidateDetailsRepository.InsertContributionTransaction(objBo, out strError))
                    {
                        var data = objCandidateDetailsRepository.GetAllContributionTransaction().Where(x => x.Id == objBo.Id).FirstOrDefault();

                        string strStudentId = objBo.Id.ToString();// strTransactioNo;
                        string strCourseId = strMerchTxnId;
                        string strPrice = objBo.Amount;
                        string strEmail = objBo.Email;
                        string strMobile = objBo.ContactNo;

                        string strQu = HttpUtility.UrlEncode(Functions.Base64Encode(strStudentId + "|" + strCourseId + "|" + strFullName + "|" + strPrice + "|" + strEmail + "|" + strMobile)).Replace("%", "PP");

                        string strReturnUrl = "";
                        strReturnUrl = "Contribution";
                        string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=" + strStudentId + "|CourseId=" + strCourseId + "|MercTxnId=" + strCourseId + "|Price=" + strPrice + "|Email=" + strEmail + "|Mobile=" + strMobile + "|ReturnUrl=" + strReturnUrl + "|Query=" + strQu));
                        Response.Redirect("~/Hospital/Payment/Request.aspx?" + strdQuery, false);

                        ////string strMerchTxnId = data.TxnId;//strTxnId;
                        ////string Amount = txtAmount.Text.Trim();

                        ////string strQu = HttpUtility.UrlEncode(Functions.Base64Encode(strCourseId + "|" + strPrice + "|" + Email + "|" + Mobile)).Replace("%", "PP");

                        ////string strReturnUrl = "";
                        ////strReturnUrl = "Contribution";
                        ////string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("MerchTxnId=" + strMerchTxnId + "|Amount=" + Amount + "|Email=" + Email + "|Mobile=" + Mobile + "|ReturnUrl=" + strReturnUrl + "|Query=" + strQu));
                        ////Response.Redirect("~/Hospital/Payment/Request.aspx?" + strdQuery, false);
                    }
                }
            }
        }

        private bool ValidateForm(ref GetAllContributionTransactionMasterResult objBo)
        {
            bool isError = true;

            #region Mandatory Validation
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblError.Text = "Please Enter Donor Name !";
                lblError.ForeColor = System.Drawing.Color.Red;
                txtName.Focus();
                return false;
            }
            else
            {
                objBo.Name = txtName.Text;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblError.Text = "Please Enter Email Id !";
                lblError.ForeColor = System.Drawing.Color.Red;
                txtEmail.Focus();
                return false;
            }
            else
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(txtEmail.Text);
                if (match.Success)
                {
                    objBo.Email = txtEmail.Text;
                }
                else
                {
                    lblError.Text = "Please Enter Valid Email Id !";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    txtEmail.Focus();
                    return false;
                }
            }

            if (txtContactNo.Text.Trim() == "")
            {
                lblError.Text = "Please Enter Contact no !";
                lblError.ForeColor = System.Drawing.Color.Red;
                txtContactNo.Focus();
                return false;
            }
            else
            {
                if (txtContactNo.Text.Trim().Length != 10)
                {
                    lblError.Text = "Please Enter Valid Contact no !";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    txtContactNo.Focus();
                    return false;
                }
                else
                {
                    objBo.ContactNo = txtContactNo.Text.Trim();
                }
            }

            if (ddlDonationType.SelectedIndex > 0)
            {
                objBo.DonationType = ddlDonationType.SelectedValue;
            }
            else
            {
                lblError.Text = "Please Select Donation Type !";
                lblError.ForeColor = System.Drawing.Color.Red;
                ddlDonationType.Focus();
                return false;
            }

            if (ddlPrimaryIdProof.SelectedIndex > 0)
            {
                objBo.PrimaryIdProof = ddlPrimaryIdProof.SelectedValue;
            }
            else
            {
                lblError.Text = "Please Select Primary Proof Type !";
                lblError.ForeColor = System.Drawing.Color.Red;
                ddlPrimaryIdProof.Focus();
                return false;
            }
        
            if (string.IsNullOrWhiteSpace(txtPrimaryProofNo.Text))
            {
                lblError.Text = "Please Enter Primary Proof number !";
                lblError.ForeColor = System.Drawing.Color.Red;
                txtPrimaryProofNo.Focus();
                return false;
            }
            else
            {
                if (ddlPrimaryIdProof.SelectedIndex == 1)
                {
                    Regex regexpan = new Regex("([A-Z]){5}([0-9]){4}([A-Z]){1}$");
                    Match match = regexpan.Match(txtPrimaryProofNo.Text.ToUpper());
                    if (match.Success)
                    {
                        objBo.PrimaryProofNo = txtPrimaryProofNo.Text;
                    }
                    else
                    {
                        lblError.Text = "Please Enter Valid PAN Card Number !";
                        lblError.ForeColor = System.Drawing.Color.Red;
                        txtEmail.Focus();
                        return false;
                    }
                }
                if (ddlPrimaryIdProof.SelectedIndex == 2)
                {
                    bool isValidnumber = aadharcard.validateVerhoeff(txtPrimaryProofNo.Text);
                    if (isValidnumber)
                    {
                        objBo.PrimaryProofNo = txtPrimaryProofNo.Text;
                    }
                    else
                    {
                        lblError.Text = "Please Enter Valid Aadhar Card Number !";
                        lblError.ForeColor = System.Drawing.Color.Red;
                        txtEmail.Focus();
                        return false;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(hfPrimaryProof.Value))
            {
                objBo.PrimaryProofFile = hfPrimaryProof.Value;
            }
            else
            {
                lblError.Text = "Please Upload Primary Proof !";
                lblError.ForeColor = System.Drawing.Color.Red;
                fuPrimaryProof.Focus();
                return false;
            }

            decimal dcAmount = 0;
            if (!decimal.TryParse(txtAmount.Text, out dcAmount))
            {
                lblError.Text = "Please Enter Amount !";
                lblError.ForeColor = System.Drawing.Color.Red;
                txtAmount.Focus();
                return false;
            }
            else
            {
                if (dcAmount > 0)
                {
                    objBo.Amount = txtAmount.Text;
                }
                else
                {
                    lblError.Text = "Please Enter Valid Amount !";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    txtAmount.Focus();
                    return false;
                }
                
            }
          
            #endregion


            if (!string.IsNullOrWhiteSpace(txtSecondaryIdProof.Text))
            {
                objBo.SecondaryProofNo = txtSecondaryIdProof.Text;
            }
            if (ddlSecondaryIdProof.SelectedIndex > 0)
            {
                objBo.SecondaryIdProof = ddlSecondaryIdProof.SelectedValue;
            }
            if (!string.IsNullOrWhiteSpace(hfSecondaryIdProof.Value))
            {
                objBo.SecondaryProofFile = hfSecondaryIdProof.Value;
            }
            if (!string.IsNullOrWhiteSpace(txtDonerAddress.Text))
            {
                objBo.Address = txtDonerAddress.Text;
            }
          

            return isError;
        }

        private static string GetPageData()
        {
            int languageId = Functions.LanguageId;
            StringBuilder strBoardOfDirector = new StringBuilder();

            //    DataSet ds = new AdmissionPageDetailsBAL().SelectAdmission(languageId);
            //LableData:
            //    if (ds != null)
            //    {
            //        strBoardOfDirector = new StringBuilder();
            //        if (!ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
            //        {
            //            int i = 1;
            //            strBoardOfDirector.Append("<div class='card  mb-0'>");
            //            strBoardOfDirector.Append("	<div class='card-body'>");
            //            strBoardOfDirector.Append("		<div class='table-responsive'>");
            //            strBoardOfDirector.Append("			<table id='gvPayData' class='table table-hover table-center mb-0 maintable'>");
            //            strBoardOfDirector.Append("				<thead>");
            //            strBoardOfDirector.Append("					<tr>");
            //            strBoardOfDirector.Append("						<th>Sr No.</th>");
            //            strBoardOfDirector.Append("						<th>Course Name</th>");
            //            strBoardOfDirector.Append("						<th>Year Of Admission</th>");
            //            strBoardOfDirector.Append("						<th>List Of Student</ th>");
            //            strBoardOfDirector.Append("					</tr>");
            //            strBoardOfDirector.Append("				</thead>");
            //            strBoardOfDirector.Append("				<tbody>");
            //            foreach (DataRow row in ds.Tables[0].Rows)
            //            {
            //                string strURL = ResolveUrl(row["AdmissionFilePath"].ToString());
            //                strBoardOfDirector.Append("");
            //                strBoardOfDirector.Append("<tr>");
            //                strBoardOfDirector.Append("	<td>" + i + "</td>");
            //                strBoardOfDirector.Append("	<td>" + row["CourseName"] + "</td>");
            //                strBoardOfDirector.Append("	<td>" + row["YearOfAdmission"] + "</td>");
            //                strBoardOfDirector.Append("	<td>");
            //                strBoardOfDirector.Append("		<h2 class='table-avatar'>");
            //                strBoardOfDirector.Append("			<a href='" + strURL + "' target='_blank' class='btn btn-sm bg-info-light'>");
            //                strBoardOfDirector.Append("				<i class='far fa-eye'></i> View");
            //                strBoardOfDirector.Append("			</a>");
            //                strBoardOfDirector.Append("		</h2>");
            //                strBoardOfDirector.Append("	</td>");
            //                strBoardOfDirector.Append("</tr>");
            //                i++;
            //            }
            //            strBoardOfDirector.Append("    </tbody>");
            //            strBoardOfDirector.Append("				</table>");
            //            strBoardOfDirector.Append("			</div>");
            //            strBoardOfDirector.Append("		</div>");
            //            strBoardOfDirector.Append("	</div>");
            //        }
            //        else
            //        {
            //            if (Functions.LanguageId != 1 && languageId != 1)
            //            {
            //                languageId = 1;
            //                ds = new AdmissionPageDetailsBAL().SelectAdmission(languageId);
            //                goto LableData;
            //            }
            //        }
            //    }
            return "dfgsdgherrerhgrtgw";
        }

        protected void btnPrimaryProof_Click(object sender, EventArgs e)
        {
            if (fuPrimaryProof.HasFile)
            {
                string filePath = ConfigDetailsValue.AddOurExcellenceFileUploadPath;

                if (!filePath.Contains("|"))
                {

                    var filename = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuPrimaryProof.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = filePath + filename;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";

                    // Check to see if a file already exists with the
                    // same name as the file to upload.
                    if (File.Exists(Server.MapPath(pathToCheck1)))
                    {
                        File.Delete(Server.MapPath(pathToCheck1));
                    }

                    aPrimaryProof.NavigateUrl = ResolveUrl(filePath + filename);
                    hfPrimaryProof.Value = filePath + filename;

                    //Save selected file into specified location
                    fuPrimaryProof.SaveAs(Server.MapPath(filePath) + filename);

                    aPrimaryProof.Visible = true;
                }
                else
                {
                    string errorMessageFile = filePath.Split('|')[0];
                    lblError.Text = errorMessageFile;
                    lblError.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
        }

        protected void btnSecondaryIdProof_Click(object sender, EventArgs e)
        {

            if (fuSecondaryIdProof.HasFile)
            {
                string filePath = ConfigDetailsValue.AddOurExcellenceFileUploadPath;

                if (!filePath.Contains("|"))
                {

                    var filename = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuSecondaryIdProof.FileName);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = filePath + filename;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";

                    // Check to see if a file already exists with the
                    // same name as the file to upload.
                    if (File.Exists(Server.MapPath(pathToCheck1)))
                    {
                        File.Delete(Server.MapPath(pathToCheck1));
                    }

                    aSecondaryIdProof.NavigateUrl = ResolveUrl(filePath + filename);
                    hfSecondaryIdProof.Value = filePath + filename;

                    //Save selected file into specified location
                    fuSecondaryIdProof.SaveAs(Server.MapPath(filePath) + filename);

                    aSecondaryIdProof.Visible = true;
                }
                else
                {
                    string errorMessageFile = filePath.Split('|')[0];
                    lblError.Text = errorMessageFile;
                    lblError.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
        }

        protected void btnpaystatus_Click(object sender, EventArgs e)
        {
            if (txtContactNo1.Text.Trim() == "")
            {
                lblError1.Text = "Please Enter Contact no!";
                lblError1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (txtContactNo1.Text.Trim().Length != 10)
            {
                lblError1.Text = "Invalid Contact no!";
                lblError1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (txtOpt.Text.Trim() == "")
            {
                lblError1.Text = "Please Enter OTP!";
                lblError1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (txtOpt.Text.Trim().Length != 6)
            {
                lblError1.Text = "Invalid OTP!";
                lblError1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                if (SessionWrapper.OTP ==Convert.ToInt32(txtOpt.Text.Trim()))
                {
                    string Mobile = txtContactNo1.Text.Trim();

                    string strQu = HttpUtility.UrlEncode(Functions.Base64Encode(Mobile)).Replace("%", "PP");

                    string strReturnUrl = "";
                    strReturnUrl = "Contribution";
                    string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("Mobile=" + Mobile + "|ReturnUrl=" + strReturnUrl + "|Query=" + strQu));

                    Response.Write("<script>window.open('Hospital/Payment/PaymentStatus.aspx?" + strdQuery + "','_blank');</script>");
                    //Response.Redirect("~/Hospital/Payment/PaymentList.aspx?" + strdQuery, false); 
                    txtContactNo1.Text = "";
                    txtOpt.Text = "";
                    lnkbtnOTP.Enabled = true;
                    lnkbtnOTP.Visible = true;
                    txtOpt.Enabled = false;
                    txtOpt.Visible = false;
                    lnkbtngo.Visible = false;
                    SessionWrapper.OTP = 0;

                }
            }
        }


        protected void btnOTP_Click(object sender, EventArgs e)
        {
            if (txtContactNo1.Text.Trim() == "")
            {
                lblError1.Text = "Please Enter Contact no!";
                lblError1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (txtContactNo1.Text.Trim().Length != 10)
            {
                lblError1.Text = "Invalid Contact no!";
                lblError1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                string Mobile = txtContactNo1.Text.Trim();
                DataSet ds = new DataSet();
                PaymentBAL objBAL = new PaymentBAL();
                ds = objBAL.GetPayment(Mobile);

                if (ds.Tables[0].Rows.Count > 0 && ds != null)
                {
                    lblError1.Text = "";
                    string SMSUsername2 = ConfigDetailsValue.SMSUsername2;
                    string SMSPassword2 = ConfigDetailsValue.SMSPassword2;
                    string senderid2 = ConfigDetailsValue.senderid2;
                    string SMSAPI2 = ConfigDetailsValue.SMSAPI2;
                    string Templateid2 = ConfigDetailsValue.Templateid2;
                    string massage = "";

                    string OTP = Functions.GenerateOTP();
                    massage = (HttpUtility.UrlEncode("U.N.MEHTA HOSPITAL, Your one time OTP for Registration is " + OTP + "", Encoding.UTF8).Replace("+", "%20"));
                    SessionWrapper.OTP = Convert.ToInt32(OTP);
                    string strRtn = Functions.sendSingleSMS(SMSUsername2, SMSPassword2, senderid2, Mobile, massage, SMSAPI2, Convert.ToInt32(OTP));
                    if (strRtn == "1")
                    {
                        lnkbtnOTP.Enabled = false;
                        txtOpt.Visible = true;
                        txtOpt.Enabled = true;
                        lnkbtngo.Visible = true;
                    }
                    else
                    {
                        lnkbtnOTP.Enabled = true;
                        txtOpt.Enabled = false;
                        txtOpt.Visible = false;
                        lnkbtngo.Visible = false;
                    }
                }
                else
                {
                    lblError1.Text = "Alert: No Donation History Found For This Mobile Number.!";
                    lblError1.ForeColor = System.Drawing.Color.Red;
                    return;
                }




                
               




            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            //string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=1|CourseId=1|CourseName=Blood Banking|Email=test@gmail.com|Mobile=9320145821"));
            string strdQuery = HttpUtility.UrlEncode(Functions.Base64Encode("StudentId=1|CourseId=1|CourseName=Blood Banking|Email=test@gmail.com|Mobile=9320145821"));

            Response.Write("<script>window.open('admin/admission/payment.aspx?" + strdQuery + "','_blank');</script>");
        }

        protected void paymentlink_Click(object sender, EventArgs e)
        {
            txtContactNo1.Visible = true;
            //lnkbtngo.Visible = true;
            lnkbtnOTP.Enabled = true;
            lnkbtnOTP.Visible = true;


        }
    }
}