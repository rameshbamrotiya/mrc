using BAL.Admission;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using static Unmehta.WebPortal.Web.Common.Functions;

namespace Unmehta.WebPortal.Web.Admin.Student
{
    public partial class CourseMaster : System.Web.UI.Page
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
            try
            {
                if (!IsPostBack)
                {
                    ClearControlValues(pnlEntry);
                    BindGridView();
                    BindDropDown();
                    ShowHideControl(VisibityType.GridView);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void gView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                using (StudentCourseBAL objStudentCourseBAL = new StudentCourseBAL())
                {
                    if ((e.CommandName.Equals("eView")) || (e.CommandName.Equals("eEdit")) || (e.CommandName.Equals("eDelete")))
                    {
                        int intIndex = ((GridViewRow)(((Control)e.CommandSource)).NamingContainer).RowIndex;
                        Int32 bytID;
                        bytID = Convert.ToInt32(gView.DataKeys[intIndex].Values["Id"]);
                        if (e.CommandName == "eDelete")
                        {
                            objStudentCourseBAL.RemoveData(bytID, SessionWrapper.UserDetails.UserName);
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
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            BindGridView();
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Add_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowHideControl(VisibityType.Insert);
                gvDoctor.DataSource = "";
                gvDoctor.DataBind();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Save_ServerClick(object sender, EventArgs e)
        {
            try
            {
                StudentCourseBO objBo = new StudentCourseBO();
                if (LoadControls(objBo))
                {
                    using (StudentCourseBAL objStudentCourseBAL = new StudentCourseBAL())
                    {
                        if (objStudentCourseBAL.InsertOrUpdateData(objBo))
                        {
                            if (objBo.Id == 0)
                            {
                                Functions.MessagePopup(this, "Record Insert successfully.", PopupMessageType.success);
                            }
                            else
                            {
                                Functions.MessagePopup(this, "Record updated successfully.", PopupMessageType.success);
                            }
                            if (objBo.Id==0)
                            {
                                var dataTa = objStudentCourseBAL.GetAll();
                                List<StudentCourseBO> data = Functions.ToListof<StudentCourseBO>(dataTa).OrderByDescending(x=> x.Id).ToList();
                                objBo.Id = data.FirstOrDefault().Id;
                            }
                            using (StudentEducationQualificationBAL objStudentEducationQualificationBAL = new StudentEducationQualificationBAL())
                            {
                                if (Functions.ToListof<StudentEducationTypeDetailBO>(objStudentCourseBAL.GetAlltMinimumEducationTypeDetailsById(Convert.ToInt32(objBo.Id))).Count > 0)
                                {
                                    objStudentCourseBAL.RemovetMinimumEducationTypeDetailsById(Convert.ToInt32(objBo.Id));
                                }                               

                                if(GetListValueSubCourse.Count>0)
                                { long rowData;
                                    foreach(var row in GetListValueSubCourse)
                                    {
                                        if(row.IsDelete==true && long.TryParse(row.TempId, out rowData))
                                        {
                                            if (objStudentCourseBAL.RemoveSubData(Convert.ToInt32(row.Id),SessionWrapper.UserDetails.UserName))
                                            {

                                            }
                                        }
                                        else
                                        {
                                            if(row.TempId.Contains("Temp_"))
                                            {
                                                row.Id = 0;
                                            }
                                            row.StudentCourseId = objBo.Id;
                                            if (objStudentCourseBAL.InsertOrUpdateSubData(row))
                                            {

                                            }
                                        }
                                    }
                                }
                            }                            
                            BindGridView();
                            ShowHideControl(VisibityType.GridView);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }

        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {
            try
            {
                StudentCourseBO objBo = new StudentCourseBO();
                if (LoadControls(objBo))
                {
                    using (StudentCourseBAL objStudentCourseBAL = new StudentCourseBAL())
                    {
                        if (objStudentCourseBAL.InsertOrUpdateData(objBo))
                        {
                            using (StudentEducationQualificationBAL objStudentEducationQualificationBAL = new StudentEducationQualificationBAL())
                            {
                                if (Functions.ToListof<StudentEducationTypeDetailBO>(objStudentCourseBAL.GetAlltMinimumEducationTypeDetailsById(Convert.ToInt32(objBo.Id))).Count > 0)
                                {
                                    objStudentCourseBAL.RemovetMinimumEducationTypeDetailsById(Convert.ToInt32(objBo.Id));
                                }                                

                                if (GetListValueSubCourse.Count > 0)
                                {
                                    long rowData;
                                    foreach (var row in GetListValueSubCourse)
                                    {
                                        if (row.IsDelete == true && long.TryParse(row.TempId, out rowData))
                                        {
                                            if (!row.TempId.Contains("Temp_"))
                                            {
                                                if (objStudentCourseBAL.RemoveSubData(Convert.ToInt32(row.Id), SessionWrapper.UserDetails.UserName))
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (row.TempId.Contains("Temp_"))
                                            {
                                                row.Id = 0;
                                            }
                                            row.StudentCourseId = objBo.Id;
                                            row.UpdateBy = SessionWrapper.UserDetails.UserName;
                                            if (objStudentCourseBAL.InsertOrUpdateSubData(row))
                                            {

                                            }
                                        }
                                    }
                                }
                            }

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
            }
            catch (Exception ex)
            {
                Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
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
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        #endregion

        #region Page Functions
        private bool FillControls(long iPkId)
        {
            using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
            {
                var dataOfUserName = objStudentAdvertisementBAL.GetAll();
                var GetSubAll = objStudentAdvertisementBAL.GetSubAll(iPkId);
                if (dataOfUserName != null)
                {
                    List<StudentCourseBO> data = Functions.ToListof<StudentCourseBO>(dataOfUserName);
                    if (GetSubAll != null)
                    {
                        GetListValueSubCourse = Functions.ToListof<StudentSubCourseBO>(GetSubAll);
                    }
                    BindgvDoctor();
                    if (data != null)
                    {
                        var objBo = data.Where(x => x.Id == iPkId).FirstOrDefault();
                        if (objBo != null)
                        {
                            hfTemplateId.Value = objBo.Id.ToString();
                            ddlActiveInactive.SelectedValue = Convert.ToString(objBo.IsVisible);
                            txtName.Text = objBo.Name;
                            txtCode.Text = objBo.Code;
                            ddlType.SelectedValue = objBo.Type;
                        }
                    }
                    ClearSubFormData();
                }
            }
            return true;
        }

        private void BindDropDown()
        {
            ddlType.Items.Clear();
            ddlType.Items.Insert(0, "Select");
            ddlType.Items.Insert(1, "Medical");
            ddlType.Items.Insert(2, "Paramedical");
            ddlType.SelectedIndex = 0;
        }

        private void BindGridView()
        {
            using (StudentCourseBAL objStudentAdvertisementBAL = new StudentCourseBAL())
            {
                var dataOfUserName = objStudentAdvertisementBAL.GetAll();
                if (dataOfUserName != null)
                {
                    List<StudentCourseBO> data = Functions.ToListof<StudentCourseBO>(dataOfUserName);
                    if (data != null)
                    {
                        if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                        {
                            gView.DataSource = data.Where(x => x.Name.Contains(txtSearch.Text) || x.Code.Contains(txtSearch.Text)).ToList();
                        }
                        else
                        {
                            gView.DataSource = data;
                        }

                        gView.DataBind();
                    }
                }
            }
        }

        private bool LoadControls(StudentCourseBO objBo)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                Functions.MessagePopup(this, "Please Enter Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Name = txtName.Text;
            }
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                Functions.MessagePopup(this, "Please Enter Code", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Code = txtCode.Text;
            }
            
            if (ddlType.SelectedIndex <= 0)
            {
                Functions.MessagePopup(this, "Please Select Type", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.Type = (ddlType.SelectedItem.Text);
            }
            string strError;            
            {
                objBo.IsVisible = Convert.ToBoolean(ddlActiveInactive.SelectedValue);
            }
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfTemplateId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                objBo.Id = Convert.ToInt32(hfTemplateId.Value);
            }
            
            objBo.UpdateBy = SessionWrapper.UserDetails.UserName;
            return true;
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
                    ClearControlValues(pnlEntry);
                    pnlView.Visible = false;
                    hfTemplateId.Value = "0";
                    pnlEntry.Visible = true;
                    btn_Save.Visible = true;
                    btn_Update.Visible = false;
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

        #region SubDetails Functions
        private List<StudentSubCourseBO> GetListValueSubCourse
        {
            get
            {
                List<StudentSubCourseBO> result = new List<StudentSubCourseBO>();

                if (ViewState["someID"] != null)
                {
                    result = (List<StudentSubCourseBO>)ViewState["someID"];
                }
                else
                {
                    result = new List<StudentSubCourseBO>();
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
            List<StudentSubCourseBO> lstStudentSubCourseBO = new List<StudentSubCourseBO>();
            lstStudentSubCourseBO = GetListValueSubCourse.Where(x => x.IsDelete == false).Select(x => { if (x.TempId == null) { x.TempId = ""; } if(!x.TempId.Contains("Temp_")) { x.TempId = x.Id.ToString(); }  return x; }).ToList();
            gvDoctor.DataSource = lstStudentSubCourseBO;
            gvDoctor.DataBind();
        }

        private bool LoadSubControls(StudentSubCourseBO objBo)
        {
            if (string.IsNullOrWhiteSpace(txtSubCourseName.Text))
            {
                Functions.MessagePopup(this, "Please Enter Sub Course Name", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.CourseName = txtSubCourseName.Text;
            }
            if (string.IsNullOrWhiteSpace(txtSubCourseCode.Text))
            {
                Functions.MessagePopup(this, "Please Enter Sub Course Code", PopupMessageType.error);
                return false;
            }
            else
            {
                objBo.CourseCode = txtSubCourseCode.Text;
            }
            if (string.IsNullOrWhiteSpace(txtTotalSeat.Text))
            {
                Functions.MessagePopup(this, "Please Enter Sub Course Total Seat", PopupMessageType.error);
                return false;
            }
            else
            {
                long lgTotalSeat;
                if (long.TryParse(txtTotalSeat.Text, out lgTotalSeat))
                {
                    objBo.TotalSeat = lgTotalSeat;
                }
            }

            if (string.IsNullOrWhiteSpace(txtCourseDuration.Text))
            {
                Functions.MessagePopup(this, "Please Enter Sub Course Duration ", PopupMessageType.error);
                return false;
            }
            else
            {               
                objBo.CourseDuration = txtCourseDuration.Text;
            }

            if (!string.IsNullOrWhiteSpace(txtInformation.Text))
            {
                objBo.Information = HttpUtility.HtmlEncode(txtInformation.Text);
            }

            if (!string.IsNullOrWhiteSpace(txtFeesDescription.Text))
            {
                objBo.FeesDescription = HttpUtility.HtmlEncode(txtFeesDescription.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtNote.Text))
            {
                objBo.CourseNote = HttpUtility.HtmlEncode(txtNote.Text);
            }

            string documentfile = string.Empty;
            if (fuDocUpload.HasFile)
            {
                var DocumentUpload = ConfigDetailsValue.GovApprovel;
                //var fname = Path.GetExtension(fuDocUpload.FileName);

                string fileName = Path.GetFileName(fuDocUpload.FileName);
                FileInfo fi = new FileInfo(fileName);
                string ext = fi.Extension.ToLower();
                if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".pdf")
                {
                    //if (fuDocUpload.PostedFile.ContentLength > 10485760)
                    //{
                    //    Functions.MessagePopup(this, "File size allow maximum 10 mb.", PopupMessageType.error);
                    //    return false;
                    //}
                    //else
                    {

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


                            documentfile = (DocumentUpload) + filename1;

                            objBo.ImagePath = documentfile;
                        }
                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Please upload only '.png,.jpg,.jpeg,.pdf'.", PopupMessageType.warning);
                    return false;
                }
            }
            else
            {
                objBo.ImagePath = hfLeftImage.Value;
            }
            //objBo.ChkMode = 0;
            if (string.IsNullOrWhiteSpace(hfSubId.Value))
            {
                objBo.Id = 0;
            }
            else
            {
                if (hfSubId.Value.Contains("Temp_"))
                {
                    objBo.TempId = (hfSubId.Value);
                }
                else
                {
                    objBo.Id = Convert.ToInt32(hfSubId.Value);
                    objBo.TempId = (hfSubId.Value);
                }
            }

            objBo.UpdateBy = SessionWrapper.UserDetails.UserName;
            return true;
        }
        
        private void ClearSubFormData()
        {
            hfSubId.Value = "";
            txtSubCourseCode.Text = "";
            txtSubCourseName.Text = "";
            txtCourseDuration.Text = "";

            hfLeftImage.Value = "";
            lblLeftImage.Text = "";
            aRemoveLeft.Visible = false;

            txtFeesDescription.Text = "";
            txtInformation.Text = "";
            txtNote.Text = "";

            txtTotalSeat.Text = "";
            BindgvDoctor();
        }
        #endregion

        #region SubDetails Method
        protected void ibtn_CourseDelete_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            hfSubId.Value = gvDoctor.DataKeys[rowindex]["Id"].ToString();
            string id = (hfSubId.Value);
            if (GetListValueSubCourse.Count > 0)
            {
                List<StudentSubCourseBO> lstSub = GetListValueSubCourse;
                var data = lstSub.FirstOrDefault(x => x.TempId == id);
                if (data != null)
                {
                    data.IsDelete = true; 
                }
                Functions.MessagePopup(this, "Sub Course Remove Successfully", PopupMessageType.success);
                GetListValueSubCourse = lstSub;
                ClearSubFormData();
            }
        }

        protected void btnAddSubCourse_Click(object sender, EventArgs e)
        {
            StudentSubCourseBO objBo=new StudentSubCourseBO();
            List<StudentSubCourseBO> lstSub = GetListValueSubCourse;
            if (LoadSubControls(objBo))
            {
                var data = GetListValueSubCourse.Where(x => x.TempId == objBo.TempId).FirstOrDefault();
                if (data != null)
                {
                    var found = lstSub.FirstOrDefault(x => x.TempId == objBo.TempId);
                    found.CourseName = objBo.CourseName;
                    found.CourseCode = objBo.CourseCode;
                    found.TotalSeat = objBo.TotalSeat;
                    found.CourseDuration = objBo.CourseDuration;

                    found.Information = objBo.Information;
                    found.ImagePath = objBo.ImagePath;
                    found.FeesDescription = objBo.FeesDescription;
                    found.CourseNote = objBo.CourseNote;

                    //lstSub.Where(x => x.TempId == objBo.TempId).Select(S => { S.CourseName = objBo.CourseName; S.CourseCode = objBo.CourseCode; return S; });
                    Functions.MessagePopup(this, "Sub Course Updated Successfully", PopupMessageType.success);
                    GetListValueSubCourse = lstSub;
                    ClearSubFormData();
                }
                else
                {
                    objBo.TempId = "Temp_"+(GetListValueSubCourse.Count+1);
                    lstSub.Add(objBo);
                    Functions.MessagePopup(this, "Sub Course Insert Successfully", PopupMessageType.success);
                    GetListValueSubCourse = lstSub;
                    ClearSubFormData();
                }
            }
        }

        protected void ibtn_CourseEdit_Click(object sender, EventArgs e)
        {
            ClearSubFormData();
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;
            var Row = GetListValueSubCourse.ToArray()[rowindex];
            hfSubId.Value = Row.TempId;
            string id = (hfSubId.Value);
            if(GetListValueSubCourse.Count>0)
            {
                var data = GetListValueSubCourse.Where(x => x.TempId == id).FirstOrDefault();
                if (data!=null)
                {
                    txtSubCourseName.Text = data.CourseName;
                    txtSubCourseCode.Text = data.CourseCode;

                    txtInformation.Text = HttpUtility.HtmlDecode(data.Information);
                    txtFeesDescription.Text = HttpUtility.HtmlDecode(data.FeesDescription);
                    txtNote.Text = HttpUtility.HtmlDecode(data.CourseNote);

                    if(!string.IsNullOrWhiteSpace(data.ImagePath))
                    {

                        hfLeftImage.Value = data.ImagePath;
                        lblLeftImage.Text = data.ImagePath;
                        aRemoveLeft.Visible = true;

                    }

                    txtTotalSeat.Text = data.TotalSeat.ToString();
                    txtCourseDuration.Text = data.CourseDuration;
                }
            }
        }
        #endregion
    }
}