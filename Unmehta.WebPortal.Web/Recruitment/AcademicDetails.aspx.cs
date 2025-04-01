using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Candidate;
using Unmehta.WebPortal.Repository.Interface.Recrutment;
using Unmehta.WebPortal.Repository.Repository;
using Unmehta.WebPortal.Repository.Repository.Candidate;
using Unmehta.WebPortal.Repository.Repository.Recrutment;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Recruitment
{
    public partial class AcademicDetails : System.Web.UI.Page
    {
        public static string strJobId;
        public static string strRegId;
        public static string strCandId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(SessionWrapper.UserDetails.UserName))
                    {
                        Response.Redirect("~/Recruitment/Careers");
                    }
                    //string strEndQueryString = "Sm9iSWQ9MXxSZWdJZD0yMDIxMDYzMDA3MzU0OHxDYW5kSWQ9MQ%3d%3d";
                    string strEndQueryString = Request.QueryString.ToString();
                    if (string.IsNullOrWhiteSpace(strEndQueryString))
                    {
                        Response.Redirect("~/Recruitment/Careers");
                    }

                    string strQueryString = Functions.Base64Decode(HttpUtility.UrlDecode(strEndQueryString));

                    string[] strQuery = strQueryString.Split('|').ToArray();
                    if (strQuery.Count() == 3)
                    {
                        strJobId = strQuery[0].ToString().Replace("JobId=", "");
                        strRegId = strQuery[1].ToString().Replace("RegId=", "");
                        strCandId = strQuery[2].ToString().Replace("CandId=", "");
                    }
                    else
                    {
                        Response.Redirect("~/Recruitment/Careers");
                    }
                    FillAllCheckBox();
                    BindEducationType();
                    FillExtraAcademicDetails();
                    BindYear();
                    AcademicDetailsBO objBO = new AcademicDetailsBO();
                    AcademicDetailsBAL objBAL = new AcademicDetailsBAL();
                    objBO.CandidateId = Convert.ToInt16(strCandId);
                    DataSet ds = objBAL.selectMenu(objBO);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        FillLanguageCheckBOx(ds.Tables[0]);
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);

                    Response.Redirect("~/Recruitment/Careers");
                }
                BindAcademicDetailsGridView();
            }
        }
        private void FillLanguageCheckBOx(DataTable dataTable)
        {
            try
            {
                foreach (RepeaterItem repeated in rptUserRights.Items)
                {
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk1"));
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk2"));
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk3"));
                    BindCheckBox(repeated, dataTable);
                }
            }

            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }

        }
        private void BindCheckBox(RepeaterItem repeated, DataTable dataTable)
        {
            try
            {
                var filteredTable = new DataTable();
                var lblLanguageid =
                     (Label)FindControlRecursive(repeated, "lblLanguageID");
                if (lblLanguageid != null)
                {
                    var dvView = dataTable.DefaultView;
                    dvView.RowFilter = "LanguageID =" + Convert.ToInt32(lblLanguageid.Text.Trim());
                    filteredTable = dvView.ToTable();
                }
                if (filteredTable.Rows.Count > 0)
                {
                    var chkRead =
                           (CheckBox)FindControlRecursive(repeated, "chk1");
                    if (chkRead != null)
                    {
                        chkRead.Checked = (bool)filteredTable.Rows[0]["Read"];
                    }
                    var chkWrite =
                        (CheckBox)FindControlRecursive(repeated, "chk2");
                    if (chkWrite != null)
                    {
                        chkWrite.Checked = (bool)filteredTable.Rows[0]["Write"];
                    }
                    var ChkSpeak =
                        (CheckBox)FindControlRecursive(repeated, "chk3");
                    if (ChkSpeak != null)
                    {
                        ChkSpeak.Checked = (bool)filteredTable.Rows[0]["Speak"];
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                {
                    int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                    Int32 bytID;
                    bytID = Convert.ToInt32(gvAcademicDetails.DataKeys[intIndex].Values["Id"]);
                    if (e.CommandName == "eDelete")
                    {
                        AcademicDetailsBO objBo = new AcademicDetailsBO();
                        objBo.CandidateId = bytID;
                        new AcademicDetailsBAL().DeleteRecord(objBo);
                        BindAcademicDetailsGridView();
                        Functions.MessagePopup(this, "Record deleted successfully.", PopupMessageType.success);
                        return;
                    }
                    //ClearControlValues(pnlEntry);
                    if (FillControls(bytID))
                    {
                        if (e.CommandName == "eView")
                            //ShowHideControl(VisibityType.View);
                            if (e.CommandName == "eEdit")
                            {
                                ViewState["PK"] = bytID;
                                //ShowHideControl(VisibityType.Edit);
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        private bool FillControls(Int32 iPkId)
        {
            AcademicDetailsBO objBo = new AcademicDetailsBO();
            objBo.id = iPkId;
            //objBo.LanguageId = languageId;
            hfTemplateId.Value = iPkId.ToString();
            DataSet ds = new AcademicDetailsBAL().SelectRecord(objBo);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                ClearControlValues();
                return false;
            }
            if (ds.Tables.Count.Equals(0) || ds.Tables[0].Rows.Count.Equals(0)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.HasErrors) return false;
            ViewState["T017PDetails"] = ds.Tables[0];
            if (dr["NameOfSchoolCollege"] != DBNull.Value)
                txtschoolname.Text = dr["NameOfSchoolCollege"].ToString();
            if (dr["BoardUniversity"] != DBNull.Value)
                txtboard.Text = dr["BoardUniversity"].ToString();
            //if (dr["PassingYear"] != DBNull.Value)
            //    txtyear.Text = dr["PassingYear"].ToString();

            if (dr["PassingYear"] != DBNull.Value)
            {
                DateTime dt = DateTime.ParseExact(dr["PassingYear"].ToString(), "MMM yyyy", CultureInfo.InvariantCulture);
                ddlAcademicYear.SelectedValue = dt.Year.ToString();
                ddlMonth.SelectedValue = dt.ToString("MMM");
            }
            if (dr["MajorSubjects"] != DBNull.Value)
                txtmsubject.Text = dr["MajorSubjects"].ToString();
            if (dr["Division"] != DBNull.Value)
                txtdivision.Text = dr["Division"].ToString().Trim();                     
            if (dr["EducationTypeId"] != DBNull.Value)
                ddlEductionType.SelectedValue = Convert.ToString(dr["EducationTypeId"]);
            BindEducationName();
            if (dr["EducationId"] != DBNull.Value)
                ddlEducationName.SelectedValue = Convert.ToString(dr["EducationId"]);
            return true;
        }
        private void FillAllCheckBox()
        {
            try
            {
                {
                    AcademicDetailsBAL objBAL = new AcademicDetailsBAL();
                    DataSet ds = objBAL.SelectMenuResourceWise();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var dtView = ds.Tables[0].DefaultView;
                        rptUserRights.DataSource = dtView;
                        rptUserRights.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }
        private void SetCheckBox(string chk1, Repeater repeater, CheckBox chkBox)
        {
            foreach (RepeaterItem repeated in repeater.Items)
            {
                var chkView =
                    (CheckBox)FindControlRecursive(repeated, chk1);
                if (chkView != null)
                {
                    chkView.Checked = chkBox.Checked;
                }

                var rpt1 =
                    (Repeater)FindControlRecursive(repeated, "rptUserRightschild");
                if (rpt1 != null)
                {
                    foreach (RepeaterItem repeated1 in rpt1.Items)
                    {
                        var chkView1 =
                    (CheckBox)FindControlRecursive(repeated1, chk1);
                        if (chkView1 != null)
                        {
                            chkView1.Checked = chkBox.Checked;
                        }

                        var rpt2 =
                    (Repeater)FindControlRecursive(repeated1, "rptUserRightschild1");
                        if (rpt2 != null)
                        {
                            foreach (RepeaterItem repeated2 in rpt2.Items)
                            {
                                var chkView2 =
                            (CheckBox)FindControlRecursive(repeated2, chk1);
                                if (chkView2 != null)
                                {
                                    chkView2.Checked = chkBox.Checked;
                                }
                            }
                        }
                    }
                }
            }
        }
        public static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
                return root;

            return root.Controls.Cast<Control>()
               .Select(c => FindControlRecursive(c, id))
               .FirstOrDefault(c => c != null);
        }
        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                if (string.IsNullOrWhiteSpace(txtboard.Text.Trim()))
                {
                    Functions.MessagePopup(this, "Please Enter Board/University.", PopupMessageType.warning);
                }
                //if (string.IsNullOrWhiteSpace(txtdegree.Text.Trim()))
                //{
                //    Functions.MessagePopup(this, "Please Enter Degree.", PopupMessageType.warning);
                //}
                if (string.IsNullOrWhiteSpace(txtschoolname.Text.Trim()))
                {
                    Functions.MessagePopup(this, "Please Enter Name of School/Colege.", PopupMessageType.warning);
                }
                //if (string.IsNullOrWhiteSpace(txtyear.Text.Trim()))
                //{
                //    Functions.MessagePopup(this, "Please Enter Year of passing.", PopupMessageType.warning);
                //}
                if (string.IsNullOrWhiteSpace(txtmsubject.Text.Trim()))
                {
                    Functions.MessagePopup(this, "Please Enter Major Subject.", PopupMessageType.warning);
                }
                if (string.IsNullOrWhiteSpace(txtdivision.Text.Trim()))
                {
                    Functions.MessagePopup(this, "Please Enter Division & %.", PopupMessageType.warning);
                }
                using (ICandidateDetailsRepository objRecruitmentCastRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    CandidateEducationDetailsModel objBO = new CandidateEducationDetailsModel();
                    if (LoadControlsForAcademicDetails(objBO))
                    {
                        if (!objRecruitmentCastRepository.InsertOrUpdateTblCandidateEducationDetails(objBO, out errorMessage))
                        {
                            ClearControlValues();
                            //BindGridView();
                            BindAcademicDetailsGridView();
                            hfTemplateId.Value = "0";
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.success);
                        }
                        else
                        {
                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }
        }

        private void SetLanguageParameter(RepeaterItem repeated, AcademicDetailsBO objLanguage, int count)
        {
            objLanguage.RegisrationId = strRegId;
            objLanguage.Advertisementid = strJobId;
            objLanguage.CandidateId = Convert.ToInt16(strCandId);
            objLanguage.Userid = "admin";
            var lblLanguageID =
                      (Label)FindControlRecursive(repeated, "lblLanguageID");
            if (lblLanguageID != null)
            {
                objLanguage.LanguageID = Convert.ToInt32(lblLanguageID.Text);
            }
            var chkRead =
                       (CheckBox)FindControlRecursive(repeated, "chk1");
            if (chkRead != null)
            {
                objLanguage.CanRead = chkRead.Checked;
            }
            var chkWrite =
                (CheckBox)FindControlRecursive(repeated, "chk2");
            if (chkWrite != null)
            {
                objLanguage.Canwrite = chkWrite.Checked;
            }
            var chkSpeak =
                (CheckBox)FindControlRecursive(repeated, "chk3");
            if (chkSpeak != null)
            {
                objLanguage.CanSpeak = chkSpeak.Checked;
            }

            if (chkRead.Checked == true || chkSpeak.Checked == true || chkWrite.Checked == true)
            {
                count = count + 1;
            }

        }
        private bool LoadControlsUpdateAcademicDetails(CandidateDetailsForAcademicModel objBo)
        {
            objBo.CandidateId = Convert.ToInt64(strCandId);
            objBo.IsBasicComputerLiteracy = Convert.ToBoolean(ddlcomputerlit.SelectedValue.ToString() == "1" ? true : false);
            objBo.Other = txtremerks.InnerText.ToString();
            return true;
        }
        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = "";
                using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
                {
                    CandidateDetailsForAcademicModel objBO = new CandidateDetailsForAcademicModel();
                    if (LoadControlsUpdateAcademicDetails(objBO))
                    {
                        if (!objCandidateDetailsRepository.UpdateCandidateDetailsForAcademic(objBO, out errorMessage))
                        {
                            ddlcomputerlit.SelectedIndex = 0;
                            txtremerks.InnerText = "";
                        }
                    }
                }
                int count = 0;
                AcademicDetailsBO objLanguage = new AcademicDetailsBO();
                objLanguage.CandidateId = Convert.ToInt16(strCandId);
                AcademicDetailsBAL objBAL = new AcademicDetailsBAL();
                DataSet ds = objBAL.GetAllAcademicDetails(objLanguage);
                DataTable dt = ds.Tables[0];
                using (IRecruitmentEducationTypeMasterRepository objRecruitmentEducationTypeMasterRepository = new RecruitmentEducationTypeMasterRepository(Functions.strSqlConnectionString))
                {
                    using (IRecruitmentAdvertisementRepository objRecruitmentAdvertisementRepository = new RecruitmentAdvertisementRepository(Functions.strSqlConnectionString))
                    {
                        var minRec = objRecruitmentEducationTypeMasterRepository.GetAllRecruitmentAdvertisementByAddId(Convert.ToInt32(strJobId)).Select(x => x.EducationTypeId).ToList();
                        var remain = dt.AsEnumerable().Select(x => x.Field<long>("EducationTypeId")).ToList().Distinct();

                        var result = minRec.Where(myRow => !remain.Contains(myRow)).ToList().Distinct();

                        var rowName = objRecruitmentEducationTypeMasterRepository.GetAllEducationTypeMaster().Where(x => result.Contains(x.Id)).Select(x => x.TypeName).ToList();

                        string RemainEducation = string.Join(",", rowName.ToArray());

                        if (!string.IsNullOrWhiteSpace(RemainEducation))
                        {
                            Functions.MessagePopup(this, "Please Enter Remain education details :" + RemainEducation, PopupMessageType.error);
                            return;
                        }
                    }
                }
                int countI = 0;
                foreach (RepeaterItem repeated2 in rptUserRights.Items)
                {
                    CheckBox chk1=(repeated2.FindControl("chk1") as CheckBox);
                    CheckBox chk2 = (repeated2.FindControl("chk2") as CheckBox);
                    CheckBox chk3 = (repeated2.FindControl("chk3") as CheckBox);
                    if(chk1.Checked)
                    {
                        countI++;
                    }
                    if (chk2.Checked )
                    {
                        countI++;
                    }
                    if(chk3.Checked)
                    {
                        countI++;
                    }

                }
                if (countI > 0)
                {
                    foreach (RepeaterItem repeated2 in rptUserRights.Items)
                    {
                        SetLanguageParameter(repeated2, objLanguage, count);
                        if (objBAL.InsertRecord(objLanguage))
                        {
                        }
                    }
                    string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId=" + strJobId + "|RegId=" + strRegId + "|CandId=" + strCandId));
                    Response.Redirect("~/Recruitment/ProfessionalExperience?" + strEndQueryString);
                    Functions.MessagePopup(this, "Recorde Inserted successfully.", PopupMessageType.success);
                }
                else
                {
                    Functions.MessagePopup(this, "Please select one checkbox check in Language section.", PopupMessageType.error);

                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(), ex.StackTrace.ToString(),this);
            }

        }
        private void BindAcademicDetailsGridView()
        {
            AcademicDetailsBO objLanguage = new AcademicDetailsBO();
            objLanguage.CandidateId = Convert.ToInt16(strCandId);
            AcademicDetailsBAL objBAL = new AcademicDetailsBAL();
            DataSet ds = objBAL.GetAllAcademicDetails(objLanguage);
            DataTable dt = ds.Tables[0];
            gvAcademicDetails.DataSource = dt;
            gvAcademicDetails.DataBind();
        }
        private void BindYear()
        {
            ddlAcademicYear.Items.Clear();
            ddlAcademicYear.Items.Insert(0, new ListItem("Select Year", "-1"));
            for (int i = 1960; i <= DateTime.Now.Year; i++)
            {
                ddlAcademicYear.Items.Add(i.ToString());
            }
        }
        private bool LoadControlsForAcademicDetails(CandidateEducationDetailsModel objBo)
        {
            if (string.IsNullOrWhiteSpace(hfTemplateId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfTemplateId.Value);
            }
            //objBo.Id = Convert.ToInt64(hfTemplateId.Value);
            objBo.CandidateId = Convert.ToInt16(strCandId);
            objBo.RegisrationId = strRegId;
            objBo.NameOfSchoolCollege = txtschoolname.Text.ToString();
            objBo.BoardUniversity = txtboard.Text.ToString();
            if (ddlMonth.SelectedIndex > 0 && ddlAcademicYear.SelectedIndex > 0)
            {
                objBo.PassingYear = ddlMonth.SelectedValue + " " + ddlAcademicYear.SelectedValue;
            }
            //objBo.PassingYear = txtyear.Text.ToString();
            objBo.IsVisible = true;
            objBo.MajorSubjects = txtmsubject.Text.ToString();
            objBo.PercentageOrPercentile =Convert.ToDecimal(txtdivision.Text.ToString().Trim());
            objBo.Division = txtdivision.Text.ToString().Trim();
            objBo.EducationId = Convert.ToInt32(ddlEducationName.SelectedValue.ToString());
            objBo.EducationTypeId = Convert.ToInt32(ddlEductionType.SelectedValue.ToString());
            return true;
        }
        private void ClearControlValues()
        {
            //txtdegree.Text = "";
            txtschoolname.Text = "";
            ddlAcademicYear.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;
            ddlEducationName.SelectedIndex = 0;
            ddlEductionType.SelectedIndex = 0;
            txtboard.Text = "";
            //txtyear.Text = "";
            txtmsubject.Text = "";
            txtdivision.Text = "";
            BindAcademicDetailsGridView();
        }
        private void BindEducationType()
        {
            AcademicDetailsBAL objBal = new AcademicDetailsBAL();
            ddlEductionType.DataSource = objBal.SelectRecordEducationType();
            ddlEductionType.DataTextField = "TypeName";
            ddlEductionType.DataValueField = "Id";
            ddlEductionType.DataBind();
            ddlEductionType.Items.Insert(0, new ListItem("Select", "-1"));

        }
        private void BindEducationName()
        {
            AcademicDetailsBAL objBal = new AcademicDetailsBAL();
            DataTable dt = objBal.SelectRecordEducationName();
            if (dt != null)
            {
                Functions.GetDatatableWithWhereCondition(dt, "IsVisible=True", out dt);
                Functions.GetDatatableWithWhereCondition(dt, "EducationType=" + ddlEductionType.SelectedValue, out dt);
            }
            ddlEducationName.DataSource = dt;
            ddlEducationName.DataTextField = "EducationDetailName";
            ddlEducationName.DataValueField = "Id";
            ddlEducationName.DataBind();
            ddlEducationName.Items.Insert(0, new ListItem("Select", "-1"));

        }
        private void FillExtraAcademicDetails()
        {
            using (ICandidateDetailsRepository objCandidateDetailsRepository = new CandidateDetailsRepository(Functions.strSqlConnectionString))
            {
                CandidateDetailsForAcademicModel objBO = new CandidateDetailsForAcademicModel();
                objBO = objCandidateDetailsRepository.GetCandidateDetailsForAcademicByCanId(Convert.ToInt64(strCandId));
                if (objBO != null)
                {
                    ddlcomputerlit.SelectedValue = Convert.ToString(objBO.IsBasicComputerLiteracy == false ? 0 : 1);
                    txtremerks.InnerText = objBO.Other;
                }
            }
        }
        protected void ddlEductionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AcademicDetailsBAL objBal = new AcademicDetailsBAL();
            DataTable dt = objBal.SelectRecordEducationName();
            if (dt != null)
            {
                Functions.GetDatatableWithWhereCondition(dt, "IsVisible=True", out dt);
                Functions.GetDatatableWithWhereCondition(dt, "EducationType=" + ddlEductionType.SelectedValue, out dt);
            }
            ddlEducationName.DataSource = dt;
            ddlEducationName.DataTextField = "EducationDetailName";
            ddlEducationName.DataValueField = "Id";
            ddlEducationName.DataBind();
            ddlEducationName.Items.Insert(0, new ListItem("Select", "-1"));
        }

        protected void btnPrivious_Click(object sender, EventArgs e)
        {
            string strEndQueryString = HttpUtility.UrlEncode(Functions.Base64Encode("JobId=" + strJobId + "|RegId=" + strRegId + "|CandId=" + strCandId));
            Response.Redirect("~/Recruitment/Default?" + strEndQueryString);
        }
    }
}