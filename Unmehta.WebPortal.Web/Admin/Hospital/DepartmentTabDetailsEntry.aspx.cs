using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    [Serializable]
    public class SubDetailsModel: GetAllDeparmentTabDetailsTabIdResult
    {

    }

    [Serializable]
    public class TabDetails
    {
        public long SequanceNo { get; set; }

        public SubDetailsModel Data { get; set; }
    }

    [Serializable]
    public class TabSubDetails
    {
        public long SequanceNo { get; set; }

        public List<SubDetailsModel> Data { get; set; }
    }

    [Serializable]
    public class SequanceNoType
    {
        public long SequanceNo { get; set; }

        public long Type { get; set; }
    }

    public partial class DepartmentTabDetailsEntry : System.Web.UI.Page
    {
        #region page variables ViewState        

        public long lgTaabId
        {
            get
            {
                long dt = 0;
                if (ViewState["lgTaabId"] != null)
                {

                    dt = (long)ViewState["lgTaabId"];
                    return dt;

                }
                else
                {
                    ViewState["lgTaabId"] = 0;
                }

                return dt;
            }
            set { ViewState["lgTaabId"] = value; }
        }

        public long lgOurExId
        {
            get
            {
                long dt = 0;
                if (ViewState["lgOurExId"] != null)
                {
                    dt = (long)ViewState["lgOurExId"];

                    return dt;

                }
                else
                {
                    dt = 0;
                }

                return dt;
            }
            set { ViewState["lgOurExId"] = value; }
        }

        public long lgDepartmentId
        {
            get
            {
                long dt = 0;
                if (ViewState["lgDepartmentId"] != null)
                {
                    dt = (long)ViewState["lgDepartmentId"];
                    return dt;

                }
                else
                {
                    dt = 0;
                }

                return dt;
            }
            set { ViewState["lgDepartmentId"] = value; }
        }

        private TabDetails Type1
        {
            get
            {
                TabDetails dt = (TabDetails)ViewState["Type1"];
                if (dt != null)
                {

                    return dt;

                }
                else
                {
                    dt = new TabDetails();
                }

                return dt;
            }
            set { ViewState["Type1"] = value; }
        }

        private TabSubDetails Type2
        {
            get
            {
                TabSubDetails dt = (TabSubDetails)ViewState["Type2"];
                if (dt != null)
                {

                    if (dt.Data == null)
                    {
                        dt.Data = new List<SubDetailsModel>();
                    }
                    return dt;

                }
                else
                {
                    dt = new TabSubDetails();
                    dt.Data = new List<SubDetailsModel>();
                }

                return dt;
            }
            set { ViewState["Type2"] = value; }
        }

        public static long lgType2PageIndex;

        private TabSubDetails Type3
        {
            get
            {
                TabSubDetails dt = (TabSubDetails)ViewState["Type3"];
                if (dt != null)
                {

                    if (dt.Data == null)
                    {
                        dt.Data = new List<SubDetailsModel>();
                    }
                    return dt;

                }
                else
                {
                    dt = new TabSubDetails();
                    dt.Data = new List<SubDetailsModel>();
                }

                return dt;
            }
            set { ViewState["Type3"] = value; }
        }

        public static long lgType3PageIndex;

        //private TabDetails Type3
        //{
        //    get
        //    {
        //        TabDetails dt = (TabDetails)ViewState["Type3"];
        //        if (dt != null)
        //        {

        //            return dt;

        //        }
        //        else
        //        {
        //            dt = new TabDetails();
        //        }

        //        return dt;
        //    }
        //    set { ViewState["Type3"] = value; }
        //}

        private TabSubDetails Type5
        {
            get
            {
                TabSubDetails dt = (TabSubDetails)ViewState["Type5"];
                if (dt != null)
                {
                    if(dt.Data==null)
                    {
                        dt.Data = new List<SubDetailsModel>();
                    }
                    return dt;

                }
                else
                {
                    dt = new TabSubDetails();
                    dt.Data = new List<SubDetailsModel>();
                }

                return dt;
            }
            set { ViewState["Type5"] = value; }
        }

        public static long lgType5PageIndex;

        private TabSubDetails Type6
        {
            get
            {
                TabSubDetails dt = (TabSubDetails)ViewState["Type6"];
                if (dt != null)
                {

                    if (dt.Data == null)
                    {
                        dt.Data = new List<SubDetailsModel>();
                    }
                    return dt;

                }
                else
                {
                    dt = new TabSubDetails();
                    dt.Data = new List<SubDetailsModel>();
                }
                return dt;
            }
            set { ViewState["Type6"] = value; }
        }

        public static long lgType6PageIndex;

        private TabSubDetails Type7
        {
            get
            {
                TabSubDetails dt = (TabSubDetails)ViewState["Type7"];
                if (dt != null)
                {

                    if (dt.Data == null)
                    {
                        dt.Data = new List<SubDetailsModel>();
                    }
                    return dt;

                }
                else
                {
                    dt = new TabSubDetails();
                    dt.Data = new List<SubDetailsModel>();
                }
                return dt;
            }
            set { ViewState["Type7"] = value; }
        }

        public static long lgType7PageIndex;

        private TabSubDetails Type8
        {
            get
            {
                TabSubDetails dt = (TabSubDetails)ViewState["Type8"];
                if (dt != null)
                {

                    if (dt.Data == null)
                    {
                        dt.Data = new List<SubDetailsModel>();
                    }
                    return dt;

                }
                else
                {
                    dt = new TabSubDetails();
                    dt.Data = new List<SubDetailsModel>();
                }

                return dt;
            }
            set { ViewState["Type8"] = value; }
        }

        public static long lgType8PageIndex;

        #endregion

        #region Validation Type

        public bool ValidateType1()
        {
            bool isError = false;
            var data = Type1;
            if (data == null)
            {
                data = new TabDetails();
            }
            if (data.Data == null)
            {
                data.Data = new SubDetailsModel();
            }

            if (!string.IsNullOrWhiteSpace(txtType1Information.Text))
            {
                data.Data.IntroductionDesc = HttpUtility.HtmlEncode(txtType1Information.Text);
            }

            long lgSequanceNo = 0;
            if (long.TryParse(txtType1SquanceNo.Text, out lgSequanceNo))
            {
                if (lgSequanceNo > 0)
                {
                    data.Data.SequanceNo = lgSequanceNo;
                }
                else
                {
                    Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                    txtType1SquanceNo.Focus();
                    isError = true;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                txtType1SquanceNo.Focus();
                isError = true;
            }

            Type1 = data;

            return isError;
        }
        
        public bool ValidateSubType2(ref SubDetailsModel objBO)
        {
            bool isError = false;


            long lgSequanceNo = 0;
            if (long.TryParse(txtType2SquanceNo.Text, out lgSequanceNo))
            {
                if (lgSequanceNo > 0)
                {

                }
                else
                {
                    Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                    txtType2SquanceNo.Focus();
                    isError = true;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                txtType2SquanceNo.Focus();
                isError = true;
            }


            if (string.IsNullOrWhiteSpace(hfType2Id.Value))
            {
                objBO.Id = Type2.Data.Count + 1;
            }
            else
            {
                objBO.Id = Convert.ToInt32(hfType2Id.Value);
            }

            if (long.TryParse(txtType2SequenceRowNo.Text, out lgSequanceNo))
            {
                if (lgSequanceNo > 0)
                {
                    objBO.SequanceNo = lgSequanceNo;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type2 Sequence No.", PopupMessageType.success);
                txtType2SequenceRowNo.Focus();
                return true;
            }

            if (!string.IsNullOrWhiteSpace(txtType2ShortDescription.Text))
            {
                objBO.PopupBasicShortDesc = HttpUtility.HtmlEncode(txtType2ShortDescription.Text);
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type2 Short Description.", PopupMessageType.success);
                txtType2ShortDescription.Focus();
                return true;
            }

            if (!string.IsNullOrWhiteSpace(txtType2PopupDesc.Text))
            {
                objBO.PopupDesc = HttpUtility.HtmlEncode(txtType2PopupDesc.Text);
            }
            else
            {
                objBO.PopupDesc = "";
                //Functions.MessagePopup(this, "Please Enter Type2 Popup Description.", PopupMessageType.success);
                //txtType2PopupDesc.Focus();
                //return true;
            }


            if (fuType2PopupImage.HasFile)
            {
                string filePath = ConfigDetailsValue.AddBlogCategoryFileUploadPath;

                if (!filePath.Contains("|"))
                {
                    if (fuType2PopupImage.PostedFile.ContentLength > 210000000)
                    {
                        Functions.MessagePopup(this, "File size allow maximum Type2 Image 10 mb.", PopupMessageType.error);
                        txtType2ShortDescription.Focus();
                        return true;
                    }
                    string strImageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuType2PopupImage.FileName);
                    objBO.PopupImageName = filePath + "/" + strImageName;
                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + strImageName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(strImageName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension.ToUpper();
                    if (ext == ".PNG" || ext == ".JPG" || ext == ".JPEG")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuType2PopupImage.SaveAs(Server.MapPath(filePath) + "/" + strImageName);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please upload only Type2 Image '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return true;
                    }
                }
                else
                {
                    objBO.PopupImageName = "";
                    //Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    //return true;
                }
            }
            else
            {
                objBO.PopupImageName = (hfType2PopUpImage.Value);
            }
            return isError;
        }

        public bool ValidateSubType3(ref SubDetailsModel objBO)
        {
            bool isError = false;


            long lgSequanceNo = 0;
            if (long.TryParse(txtType3SequanceNo.Text, out lgSequanceNo))
            {
                if (lgSequanceNo > 0)
                {

                }
                else
                {
                    Functions.MessagePopup(this, "Please Enter Type3 Sequence No.", PopupMessageType.success);
                    txtType3SequanceNo.Focus();
                    isError = true;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type3 Sequence No.", PopupMessageType.success);
                txtType3SequanceNo.Focus();
                isError = true;
            }


            if (string.IsNullOrWhiteSpace(hfType3Id.Value))
            {
                objBO.Id = Type3.Data.Count + 1;
            }
            else
            {
                objBO.Id = Convert.ToInt32(hfType3Id.Value);
            }

            if (long.TryParse(txtType3SubSquanceNo.Text, out lgSequanceNo))
            {
                if (lgSequanceNo > 0)
                {
                    objBO.SequanceNo = lgSequanceNo;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type3 Sequence No.", PopupMessageType.success);
                txtType3SubSquanceNo.Focus();
                return true;
            }

            if (ddlType3Statistics.SelectedIndex > 0)
            {
                objBO.StatasticId = Convert.ToInt32(ddlType3Statistics.SelectedValue);
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type3 Statistics .", PopupMessageType.success);
                ddlType3Statistics.Focus();
                return true;
            }


            return isError;
        }

        public bool ValidateSubType5(ref SubDetailsModel objBO)
        {
            bool isError = false;

            long lgSequanceNo = 0;
            if (long.TryParse(txtType5SquanceNo.Text, out lgSequanceNo))
            {
                if (lgSequanceNo > 0)
                {

                }
                else
                {
                    Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                    txtType5SquanceNo.Focus();
                    isError = true;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                txtType5SquanceNo.Focus();
                isError = true;
            }


            if (fuType5PopupImage.HasFile)
            {
                string filePath = ConfigDetailsValue.AddBlogCategoryFileUploadPath;

                if (!filePath.Contains("|"))
                {
                    if (fuType5PopupImage.PostedFile.ContentLength > 210000000)
                    {
                        Functions.MessagePopup(this, "File size allow maximum Type5 Image 10 mb.", PopupMessageType.error);
                        fuType5PopupImage.Focus();
                        return true;
                    }
                    string strImageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuType5PopupImage.FileName);
                    objBO.PopupImageName = filePath + "/" + strImageName;
                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + strImageName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(strImageName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension.ToUpper();
                    if (ext == ".PNG" || ext == ".JPG" || ext == ".JPEG")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuType5PopupImage.SaveAs(Server.MapPath(filePath) + "/" + strImageName);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please upload only Type5 Image '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return true;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return true;
                }
            }
            else
            {
                objBO.PopupImageName = (hfType5PopUpImage.Value);
            }
            return isError;
        }

        public bool ValidateSubType6(ref SubDetailsModel objBO)
        {
            bool isError = false;


            long lgSequanceNo = 0;

            if (string.IsNullOrWhiteSpace(hfType6Id.Value))
            {
                objBO.Id = Type6.Data.Count + 1;
            }
            else
            {
                objBO.Id = Convert.ToInt32(hfType6Id.Value);
            }

            if (long.TryParse(txtType6SquanceNo.Text, out lgSequanceNo))
            {
                if (lgSequanceNo > 0)
                {

                }
                else
                {
                    Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                    txtType6SquanceNo.Focus();
                    isError = true;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                txtType6SquanceNo.Focus();
                isError = true;
            }

            if (long.TryParse(txtType6SequenceRowNo.Text, out lgSequanceNo))
            {
                if (lgSequanceNo > 0)
                {
                    objBO.SequanceNo = lgSequanceNo;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type6 Sequence No.", PopupMessageType.success);
                txtType6SequenceRowNo.Focus();
                return true;
            }

            if (!string.IsNullOrWhiteSpace(txtType6AccordionTitle.Text))
            {
                objBO.PopupBasicShortDesc = txtType6AccordionTitle.Text;
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type6 Accordion Title.", PopupMessageType.success);
                txtType6AccordionTitle.Focus();
                return true;
            }

            if (!string.IsNullOrWhiteSpace(txtType6Description.Text))
            {
                objBO.PopupDesc = HttpUtility.HtmlEncode(txtType6Description.Text);
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type6 Description.", PopupMessageType.success);
                txtType6Description.Focus();
                return true;
            }


            if (fuType6PopupImage.HasFile)
            {
                string filePath = ConfigDetailsValue.AddBlogCategoryFileUploadPath;

                if (!filePath.Contains("|"))
                {
                    if (fuType6PopupImage.PostedFile.ContentLength > 210000000)
                    {
                        Functions.MessagePopup(this, "File size allow maximum Type6 Image 10 mb.", PopupMessageType.error);
                        fuType6PopupImage.Focus();
                        return true;
                    }
                    string strImageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuType6PopupImage.FileName);
                    objBO.PopupImageName = filePath + "/" + strImageName;
                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + strImageName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(strImageName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension.ToUpper();
                    if (ext == ".PNG" || ext == ".JPG" || ext == ".JPEG")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuType6PopupImage.SaveAs(Server.MapPath(filePath) + "/" + strImageName);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please upload only Type6 Image '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return true;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return true;
                }
            }
            else
            {
                objBO.PopupImageName = (hfType6PopUpImage.Value);
            }
            return isError;
        }

        public bool ValidateSubType7(ref SubDetailsModel objBO)
        {
            bool isError = false;


            long lgSequanceNo = 0;

            if (string.IsNullOrWhiteSpace(hfType7Id.Value))
            {
                objBO.Id = Type7.Data.Count + 1;
            }
            else
            {
                objBO.Id = Convert.ToInt32(hfType7Id.Value);
            }

            if (long.TryParse(txtType7SquanceNo.Text, out lgSequanceNo))
            {
                if (lgSequanceNo > 0)
                {

                }
                else
                {
                    Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                    txtType7SquanceNo.Focus();
                    isError = true;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                txtType7SquanceNo.Focus();
                isError = true;
            }



            if (long.TryParse(txtType7SequenceRowNo.Text, out lgSequanceNo))
            {
                if (lgSequanceNo > 0)
                {
                    objBO.SequanceNo = lgSequanceNo;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type7 Sequence No.", PopupMessageType.success);
                txtType7SequenceRowNo.Focus();
                return true;
            }

            if (!string.IsNullOrWhiteSpace(txtType7Title.Text))
            {
                objBO.IntroductionDesc = txtType7Title.Text;
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type7 Title.", PopupMessageType.success);
                txtType7Title.Focus();
                return true;
            }

            if (!string.IsNullOrWhiteSpace(txtType7ShortDescription.Text))
            {
                objBO.PopupBasicShortDesc = txtType7ShortDescription.Text;
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type7 Short Description.", PopupMessageType.success);
                txtType7ShortDescription.Focus();
                return true;
            }

            if (!string.IsNullOrWhiteSpace(txtType7PopupDesc.Text))
            {
                objBO.PopupDesc = HttpUtility.HtmlEncode(txtType7PopupDesc.Text);
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type7 Popup Description.", PopupMessageType.success);
                txtType7PopupDesc.Focus();
                return true;
            }


            if (fuType7PopupImage.HasFile)
            {
                string filePath = ConfigDetailsValue.AddBlogCategoryFileUploadPath;

                if (!filePath.Contains("|"))
                {
                    if (fuType7PopupImage.PostedFile.ContentLength > 210000000)
                    {
                        Functions.MessagePopup(this, "File size allow maximum Type7 Image 10 mb.", PopupMessageType.error);
                        txtType7ShortDescription.Focus();
                        return true;
                    }
                    string strImageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuType7PopupImage.FileName);
                    objBO.PopupImageName = filePath + "/" + strImageName;
                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + strImageName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(strImageName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension.ToUpper();
                    if (ext == ".PNG" || ext == ".JPG" || ext == ".JPEG")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuType7PopupImage.SaveAs(Server.MapPath(filePath) + "/" + strImageName);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please upload only Type7 Image '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return true;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return true;
                }
            }
            else
            {
                objBO.PopupImageName = (hfType7PopUpImage.Value);
            }
            return isError;
        }

        public bool ValidateSubType8(ref SubDetailsModel objBO)
        {
            bool isError = false;


            long lgSequanceNo = 0;

            if (string.IsNullOrWhiteSpace(hfType8Id.Value))
            {
                objBO.Id = Type8.Data.Count + 1;
            }
            else
            {
                objBO.Id = Convert.ToInt32(hfType8Id.Value);
            }

            if (long.TryParse(txtType8SquanceNo.Text, out lgSequanceNo))
            {
                if (lgSequanceNo > 0)
                {

                }
                else
                {
                    Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                    txtType8SquanceNo.Focus();
                    isError = true;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                txtType8SquanceNo.Focus();
                isError = true;
            }

            if (long.TryParse(txtType8SequenceRowNo.Text, out lgSequanceNo))
            {
                if (lgSequanceNo > 0)
                {
                    objBO.SequanceNo = lgSequanceNo;
                }
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type8 Sequence No.", PopupMessageType.success);
                txtType8SequenceRowNo.Focus();
                return true;
            }


            if (!string.IsNullOrWhiteSpace(txtType8PopupDesc.Text))
            {
                objBO.PopupDesc = HttpUtility.HtmlEncode(txtType8PopupDesc.Text);
            }
            else
            {
                Functions.MessagePopup(this, "Please Enter Type8 Popup Description.", PopupMessageType.success);
                txtType8PopupDesc.Focus();
                return true;
            }


            if (fuType8PopupImage.HasFile)
            {
                string filePath = ConfigDetailsValue.AddBlogCategoryFileUploadPath;

                if (!filePath.Contains("|"))
                {
                    if (fuType8PopupImage.PostedFile.ContentLength > 210000000)
                    {
                        Functions.MessagePopup(this, "File size allow maximum Type8 Image 10 mb.", PopupMessageType.error);
                        fuType8PopupImage.Focus();
                        return true;
                    }
                    string strImageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(fuType8PopupImage.FileName);
                    objBO.PopupImageName = filePath + "/" + strImageName;
                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + strImageName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(strImageName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension.ToUpper();
                    if (ext == ".PNG" || ext == ".JPG" || ext == ".JPEG")
                    {
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (File.Exists(Server.MapPath(pathToCheck1)))
                        {
                            File.Delete(pathToCheck1);
                        }

                        //Save selected file into specified location
                        fuType8PopupImage.SaveAs(Server.MapPath(filePath) + "/" + strImageName);
                    }
                    else
                    {
                        Functions.MessagePopup(this, "Please upload only Type8 Image '.png,.jpg,.jpeg'.", PopupMessageType.warning);
                        return true;
                    }
                }
                else
                {
                    Functions.MessagePopup(this, filePath.Split('|')[0], PopupMessageType.error);
                    return true;
                }
            }
            else
            {
                objBO.PopupImageName = (hfType8PopUpImage.Value);
            }
            return isError;
        }

        #endregion

        #region Main Page Functions

        private void BindFormFieldData()
        {
            using (IStatisticsChartRepository objPatientsEducationBrochureRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            {
                List<GetAllStatisticsChartMasterResult> Data = objPatientsEducationBrochureRepository.GetAllStatisticsChart();
                Functions.PopulateDropDownList(ddlType3Statistics, Functions.ToDataTable(Data), "ChartName", "Id", true);
            }

            string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
            long lgtaId=0, lgOuExId = 0, lgDepartmId = 0;
            string[] splitString = queryString.Split('|');
            if (splitString.Length > 2)
            {
                if (!string.IsNullOrWhiteSpace(splitString[0]) && long.TryParse(splitString[0], out lgtaId) && !string.IsNullOrWhiteSpace(splitString[1]) && long.TryParse(splitString[1], out lgOuExId) && !string.IsNullOrWhiteSpace(splitString[2]) && long.TryParse(splitString[2], out lgDepartmId))
                {

                    lgTaabId = lgtaId;
                    lgOurExId = lgOuExId;
                    lgDepartmentId = lgDepartmId;

                    DataSet ds = new DataSet();
                    LanguageMasterBAL objBAL = new LanguageMasterBAL();
                    ds = objBAL.FillLanguage();
                    DataTable dt = ds.Tables[0];
                    Functions.PopulateDropDownList(ddlLanguage, dt, "Name", "Id", true);
                    ddlLanguage.SelectedIndex = 1;
                }
            }
        }

        private void BindFormData()
        {
            ClearAllDetails();
            long lgLanguageId = 1;
            if (ddlLanguage.SelectedIndex > 0)
            {
                lgLanguageId = Convert.ToInt64(ddlLanguage.SelectedValue);
            }
            using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
            {
                #region Data Bind
                var dataTypeList = objDepartmentTabRepository.GetAllDepartmentTabType();
                var dataTabDetails = objDepartmentTabRepository.GetAllDeparmentTabDetailListByTabId(lgTaabId, lgLanguageId);
                if (dataTabDetails.Count() > 0)
                    foreach (var dataType in dataTypeList)
                    {
                        switch (dataType.TabTypeName)
                        {
                            case "CkEditor":
                                {
                                    var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "CkEditor").Select(x=> new SubDetailsModel {
                                        Id=x.Id,
                                        FacultyId=x.FacultyId,
                                        IntroductionDesc=x.IntroductionDesc,
                                        IsDelete=x.IsDelete,
                                        IsVisable=x.IsVisable,
                                        LanguageId=x.LanguageId,
                                        ParentTabId=x.ParentTabId,
                                        PopupBasicShortDesc=x.PopupBasicShortDesc,
                                        PopupDesc=x.PopupDesc,
                                        PopupImageName=x.PopupImageName,
                                        SequanceNo=x.SequanceNo,
                                        StatasticId=x.StatasticId,
                                        TabId=x.TabId,
                                        TabTypeId=x.TabTypeId,
                                        TabTypeName=x.TabTypeName
                                    } ).FirstOrDefault();
                                    if (SubDetails != null)
                                    {
                                        Type1 = new TabDetails { Data = SubDetails, SequanceNo = (long)SubDetails.ParentTabId };
                                    }
                                    break;
                                }
                            case "UlDescriptionImageWithPopup":
                                {
                                    var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "UlDescriptionImageWithPopup").OrderBy(x => x.SequanceNo).ToList();
                                    if (SubDetails.Count()>0)
                                    {
                                        Type2 = new TabSubDetails { Data = SubDetails.Select(x=> new SubDetailsModel {
                                                Id = x.Id,
                                                FacultyId = x.FacultyId,
                                                IntroductionDesc = x.IntroductionDesc,
                                                IsDelete = x.IsDelete,
                                                IsVisable = x.IsVisable,
                                                LanguageId = x.LanguageId,
                                                ParentTabId = x.ParentTabId,
                                                PopupBasicShortDesc = x.PopupBasicShortDesc,
                                                PopupDesc = x.PopupDesc,
                                                PopupImageName = x.PopupImageName,
                                                SequanceNo = x.SequanceNo,
                                                StatasticId = x.StatasticId,
                                                TabId = x.TabId,
                                                TabTypeId = x.TabTypeId,
                                                TabTypeName = x.TabTypeName
                                            } ).ToList(), SequanceNo = (long)SubDetails.FirstOrDefault().ParentTabId };
                                    }
                                    break;
                                }

                            case "Statistics":
                                {
                                    var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "Statistics").OrderBy(x => x.SequanceNo).ToList();
                                    if (SubDetails.Count() > 0)
                                    {
                                        Type3 = new TabSubDetails
                                        {
                                            Data = SubDetails.Select(x => new SubDetailsModel
                                            {
                                                Id = x.Id,
                                                FacultyId = x.FacultyId,
                                                IntroductionDesc = x.IntroductionDesc,
                                                IsDelete = x.IsDelete,
                                                IsVisable = x.IsVisable,
                                                LanguageId = x.LanguageId,
                                                ParentTabId = x.ParentTabId,
                                                PopupBasicShortDesc = x.PopupBasicShortDesc,
                                                PopupDesc = x.PopupDesc,
                                                PopupImageName = x.PopupImageName,
                                                SequanceNo = x.SequanceNo,
                                                StatasticId = x.StatasticId,
                                                TabId = x.TabId,
                                                TabTypeId = x.TabTypeId,
                                                TabTypeName = x.TabTypeName
                                            }).ToList(),
                                            SequanceNo = (long)SubDetails.FirstOrDefault().ParentTabId
                                        };
                                    }
                                    break;
                                }

                            case "Slider":
                                {
                                    var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "Slider").ToList();
                                    if (SubDetails.Count() > 0)
                                    {
                                        Type5 = new TabSubDetails { Data = SubDetails.Select(x => new SubDetailsModel
                                        {
                                            Id = x.Id,
                                            FacultyId = x.FacultyId,
                                            IntroductionDesc = x.IntroductionDesc,
                                            IsDelete = x.IsDelete,
                                            IsVisable = x.IsVisable,
                                            LanguageId = x.LanguageId,
                                            ParentTabId = x.ParentTabId,
                                            PopupBasicShortDesc = x.PopupBasicShortDesc,
                                            PopupDesc = x.PopupDesc,
                                            PopupImageName = x.PopupImageName,
                                            SequanceNo = x.SequanceNo,
                                            StatasticId = x.StatasticId,
                                            TabId = x.TabId,
                                            TabTypeId = x.TabTypeId,
                                            TabTypeName = x.TabTypeName
                                        }).ToList(), SequanceNo = (long)SubDetails.FirstOrDefault().ParentTabId };
                                    }
                                    break;
                                }

                            case "Accordion":
                                {
                                    var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "Accordion").ToList();
                                    if (SubDetails.Count() > 0)
                                    {
                                        Type6 = new TabSubDetails { Data = SubDetails.Select(x => new SubDetailsModel
                                        {
                                            Id = x.Id,
                                            FacultyId = x.FacultyId,
                                            IntroductionDesc = x.IntroductionDesc,
                                            IsDelete = x.IsDelete,
                                            IsVisable = x.IsVisable,
                                            LanguageId = x.LanguageId,
                                            ParentTabId = x.ParentTabId,
                                            PopupBasicShortDesc = x.PopupBasicShortDesc,
                                            PopupDesc = x.PopupDesc,
                                            PopupImageName = x.PopupImageName,
                                            SequanceNo = x.SequanceNo,
                                            StatasticId = x.StatasticId,
                                            TabId = x.TabId,
                                            TabTypeId = x.TabTypeId,
                                            TabTypeName = x.TabTypeName
                                        }).ToList(), SequanceNo = (long)SubDetails.FirstOrDefault().ParentTabId };
                                    }
                                    break;
                                }

                            case "ImageWithPopup":
                                {
                                    var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "ImageWithPopup").ToList();
                                    if (SubDetails.Count() > 0)
                                    {
                                        Type7 = new TabSubDetails { Data = SubDetails.Select(x => new SubDetailsModel
                                        {
                                            Id = x.Id,
                                            FacultyId = x.FacultyId,
                                            IntroductionDesc = x.IntroductionDesc,
                                            IsDelete = x.IsDelete,
                                            IsVisable = x.IsVisable,
                                            LanguageId = x.LanguageId,
                                            ParentTabId = x.ParentTabId,
                                            PopupBasicShortDesc = x.PopupBasicShortDesc,
                                            PopupDesc = x.PopupDesc,
                                            PopupImageName = x.PopupImageName,
                                            SequanceNo = x.SequanceNo,
                                            StatasticId = x.StatasticId,
                                            TabId = x.TabId,
                                            TabTypeId = x.TabTypeId,
                                            TabTypeName = x.TabTypeName
                                        }).ToList(), SequanceNo = (long)SubDetails.FirstOrDefault().ParentTabId };
                                    }
                                    break;
                                }

                            case "ImageWithDescriptionLeftRight":
                                {
                                    var SubDetails = dataTabDetails.Where(x => x.TabTypeName == "ImageWithDescriptionLeftRight").ToList();
                                    if (SubDetails.Count() > 0)
                                    {
                                        Type8 = new TabSubDetails { Data = SubDetails.Select(x => new SubDetailsModel
                                        {
                                            Id = x.Id,
                                            FacultyId = x.FacultyId,
                                            IntroductionDesc = x.IntroductionDesc,
                                            IsDelete = x.IsDelete,
                                            IsVisable = x.IsVisable,
                                            LanguageId = x.LanguageId,
                                            ParentTabId = x.ParentTabId,
                                            PopupBasicShortDesc = x.PopupBasicShortDesc,
                                            PopupDesc = x.PopupDesc,
                                            PopupImageName = x.PopupImageName,
                                            SequanceNo = x.SequanceNo,
                                            StatasticId = x.StatasticId,
                                            TabId = x.TabId,
                                            TabTypeId = x.TabTypeId,
                                            TabTypeName = x.TabTypeName
                                        }).ToList(), SequanceNo = (long)SubDetails.FirstOrDefault().ParentTabId };
                                    }
                                    break;
                                }
                        }
                    }
                #endregion

                #region Form Bind

                if (Type1.Data != null)
                {
                    if (!string.IsNullOrWhiteSpace(Type1.Data.IntroductionDesc))
                    {
                        txtType1Information.Text = HttpUtility.HtmlDecode(Type1.Data.IntroductionDesc);
                    }
                    txtType1SquanceNo.Text = Type1.Data.ParentTabId.ToString();

                }

                if (Type2.Data != null)
                {
                    if (Type2.Data.Count()>0)
                    {
                        txtType2SquanceNo.Text = Type2.Data.FirstOrDefault().ParentTabId.ToString();
                    }
                }
                BindType2Form();

                //if (Type3.Data != null)
                //{
                //    if (Type3.Data.StatasticId != null)
                //    {
                //        ddlType3Statistics.SelectedValue = Type3.Data.StatasticId.ToString();
                //    }
                //    txtType3SequanceNo.Text = Type3.Data.ParentTabId.ToString();
                //}
                
                if (Type3.Data != null)
                {
                    if (Type3.Data.Count() > 0)
                    {
                        txtType3SequanceNo.Text = Type3.Data.FirstOrDefault().ParentTabId.ToString();
                    }
                }
                BindType3Form();

                if (Type5.Data != null)
                {
                    if (Type5.Data.Count() > 0)
                    {
                        txtType5SquanceNo.Text = Type5.Data.FirstOrDefault().ParentTabId.ToString();
                    }
                }
                BindType5Form();


                if (Type6.Data != null)
                {
                    if (Type6.Data.Count() > 0)
                    {
                        txtType6SquanceNo.Text = Type6.Data.FirstOrDefault().ParentTabId.ToString();
                    }
                }
                BindType6Form();


                if (Type7.Data != null)
                {
                    if (Type7.Data.Count() > 0)
                    {
                        txtType7SquanceNo.Text = Type7.Data.FirstOrDefault().ParentTabId.ToString();
                    }
                }
                BindType7Form();


                if (Type8.Data != null)
                {
                    if (Type8.Data.Count() > 0)
                    {
                        txtType8SquanceNo.Text = Type8.Data.FirstOrDefault().ParentTabId.ToString();
                    }
                }
                BindType8Form();
                #endregion
            }
        }

        private void ClearAllDetails()
        {
            Type1 =null;
            txtType1Information.Text = "";
            txtType1SquanceNo.Text = "";
            Type2 =null;
            ClearType2Form();
            BindType2Form();
            Type3 =null;
            ClearType3Form();
            BindType3Form();
            Type5 =null;
            ClearType5Form();
            BindType5Form();
            Type6 =null;
            ClearType6Form();
            BindType6Form();
            Type7 =null;
            ClearType7Form();
            BindType7Form();
            Type8 =null;
            ClearType8Form();
            BindType8Form();

        }

        #endregion

        #region Main Page Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lgType2PageIndex = 1;
                lgType5PageIndex = 1;
                lgType6PageIndex = 1;
                lgType7PageIndex = 1;
                lgType8PageIndex = 1;

                BindFormFieldData();
                BindFormData();
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindFormData();
        }

        protected void btnType5Save_Click1(object sender, EventArgs e)
        {
            using (IDepartmentTabRepository objDepartmentTabRepository = new DepartmentTabRepository(Functions.strSqlConnectionString))
            {
                var dataTypeList = objDepartmentTabRepository.GetAllDepartmentTabType();
                List<SubDetailsModel> lstData = new List<SubDetailsModel>();
                List<GetAllDeparmentTabDetailsTabIdResult> TotalDataList = new List<GetAllDeparmentTabDetailsTabIdResult>();
                long lgSequanceNo = 1;

                #region Data List Bind

                if (!string.IsNullOrWhiteSpace(txtType1SquanceNo.Text))
                {
                    if (!ValidateType1())
                    {

                        long lgSequancesNo = 0;
                        if (long.TryParse(txtType1SquanceNo.Text, out lgSequancesNo))
                        {
                            if (lgSequancesNo > 0)
                            {

                            }
                            else
                            {
                                Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                                txtType1SquanceNo.Focus();
                                return;
                            }
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please Enter Type1 Sequence No.", PopupMessageType.success);
                            txtType1SquanceNo.Focus();
                            return;
                        }

                        Type1.Data.TabTypeId = dataTypeList.Where(x => x.TabTypeName == "CkEditor").FirstOrDefault().Id;
                        Type1.Data.ParentTabId = lgSequancesNo;
                        lstData.Add(Type1.Data);
                    }
                }

                if (!string.IsNullOrWhiteSpace(txtType2SquanceNo.Text))
                {
                    if (Type2.Data.Count() > 0)
                    {

                        long lgSequancesNo = 0;
                        if (long.TryParse(txtType2SquanceNo.Text, out lgSequancesNo))
                        {
                            if (lgSequancesNo > 0)
                            {

                            }
                            else
                            {
                                Functions.MessagePopup(this, "Please Enter Type2 Sequence No.", PopupMessageType.success);
                                txtType2SquanceNo.Focus();
                                return;
                            }
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please Enter Type2 Sequence No.", PopupMessageType.success);
                            txtType2SquanceNo.Focus();
                            return;
                        }

                        Type2.Data.ForEach(x => { x.TabTypeId = dataTypeList.Where(y => y.TabTypeName == "UlDescriptionImageWithPopup").FirstOrDefault().Id; x.ParentTabId = lgSequancesNo; });

                        lstData.AddRange(Type2.Data);
                    }
                }

                if (!string.IsNullOrWhiteSpace(txtType3SequanceNo.Text))
                {
                    if (Type3.Data.Count() > 0)
                    {

                        long lgSequancesNo = 0;
                        if (long.TryParse(txtType3SequanceNo.Text, out lgSequancesNo))
                        {
                            if (lgSequancesNo > 0)
                            {

                            }
                            else
                            {
                                Functions.MessagePopup(this, "Please Enter Type3 Sequence No.", PopupMessageType.success);
                                txtType3SequanceNo.Focus();
                                return;
                            }
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please Enter Type3 Sequence No.", PopupMessageType.success);
                            txtType3SequanceNo.Focus();
                            return;
                        }

                        Type3.Data.ForEach(x => { x.TabTypeId = dataTypeList.Where(y => y.TabTypeName == "Statistics").FirstOrDefault().Id; x.ParentTabId = lgSequancesNo; });

                        lstData.AddRange(Type3.Data);
                    }
                }
                

                if (!string.IsNullOrWhiteSpace(txtType5SquanceNo.Text))
                {
                    if (Type5.Data.Count() > 0)
                    {
                        long lgSequancesNo = 0;
                        if (long.TryParse(txtType5SquanceNo.Text, out lgSequancesNo))
                        {
                            if (lgSequancesNo > 0)
                            {

                            }
                            else
                            {
                                Functions.MessagePopup(this, "Please Enter Type5 Sequence No.", PopupMessageType.success);
                                txtType5SquanceNo.Focus();
                                return;
                            }
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please Enter Type5 Sequence No.", PopupMessageType.success);
                            txtType5SquanceNo.Focus();
                            return;
                        }

                        Type5.Data.ForEach(x => { x.TabTypeId = dataTypeList.Where(y => y.TabTypeName == "Slider").FirstOrDefault().Id; x.ParentTabId = lgSequancesNo; });
                        lstData.AddRange(Type5.Data);
                    }
                }

                if (!string.IsNullOrWhiteSpace(txtType6SquanceNo.Text))
                {
                    if (Type6.Data.Count() > 0)
                    {
                        long lgSequancesNo = 0;
                        if (long.TryParse(txtType6SquanceNo.Text, out lgSequancesNo))
                        {
                            if (lgSequancesNo > 0)
                            {

                            }
                            else
                            {
                                Functions.MessagePopup(this, "Please Enter Type6 Sequence No.", PopupMessageType.success);
                                txtType6SquanceNo.Focus();
                                return;
                            }
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please Enter Type6 Sequence No.", PopupMessageType.success);
                            txtType6SquanceNo.Focus();
                            return;
                        }
                        Type6.Data.ForEach(x => { x.TabTypeId = dataTypeList.Where(y => y.TabTypeName == "Accordion").FirstOrDefault().Id; x.ParentTabId = lgSequancesNo;});
                    lstData.AddRange(Type6.Data);
                    }
                }
                if (!string.IsNullOrWhiteSpace(txtType7SquanceNo.Text))
                {
                    if (Type7.Data.Count() > 0)
                    {
                        long lgSequancesNo = 0;
                        if (long.TryParse(txtType7SquanceNo.Text, out lgSequancesNo))
                        {
                            if (lgSequancesNo > 0)
                            {

                            }
                            else
                            {
                                Functions.MessagePopup(this, "Please Enter Type7 Sequence No.", PopupMessageType.success);
                                txtType7SquanceNo.Focus();
                                return;
                            }
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please Enter Type7 Sequence No.", PopupMessageType.success);
                            txtType7SquanceNo.Focus();
                            return;
                        }
                        Type7.Data.ForEach(x => {x.TabTypeId = dataTypeList.Where(y => y.TabTypeName == "ImageWithPopup").FirstOrDefault().Id; x.ParentTabId = lgSequancesNo;});
                        lstData.AddRange(Type7.Data);
                    }
                }
                if (!string.IsNullOrWhiteSpace(txtType8SquanceNo.Text))
                {
                    if (Type8.Data.Count() > 0)
                    {
                        long lgSequancesNo = 0;
                        if (long.TryParse(txtType8SquanceNo.Text, out lgSequancesNo))
                        {
                            if (lgSequancesNo > 0)
                            {

                            }
                            else
                            {
                                Functions.MessagePopup(this, "Please Enter Type8 Sequence No.", PopupMessageType.success);
                                txtType8SquanceNo.Focus();
                                return;
                            }
                        }
                        else
                        {
                            Functions.MessagePopup(this, "Please Enter Type8 Sequence No.", PopupMessageType.success);
                            txtType8SquanceNo.Focus();
                            return;
                        }
                        Type8.Data.ForEach(x => { x.TabTypeId = dataTypeList.Where(y => y.TabTypeName == "ImageWithDescriptionLeftRight").FirstOrDefault().Id; x.ParentTabId = lgSequancesNo;});
                        lstData.AddRange(Type8.Data);
                    }
                }

                #endregion

                #region Data Set Sequance Logic

                List<SequanceNoType> sequanceNoTypes=new List<SequanceNoType>();
                var rowData = lstData.Where(x =>  x.TabTypeId != null);
                int type5Seq = 1;
                foreach (var row in rowData)
                {
                    if(sequanceNoTypes.Where(x=> x.Type==row.TabTypeId).Count()>0)
                    {
                        continue;
                    }
                    else
                    {
                        SequanceNoType sequanceNoType = new SequanceNoType();   
                        sequanceNoType.Type = (long)row.TabTypeId;
                        //if (row.TabTypeId != 5)
                        //{
                        //    sequanceNoType.SequanceNo = (long)rowData.Where(x => x.SequanceNo != null && x.TabTypeId == row.TabTypeId).FirstOrDefault().SequanceNo;
                        //}
                        //else
                        //{
                        //    sequanceNoType.SequanceNo = type5Seq;
                        //    type5Seq++;
                        //}
                        sequanceNoType.SequanceNo=(long)row.ParentTabId;
                        sequanceNoTypes.Add(sequanceNoType);
                    }
                }

                var sequanceList = sequanceNoTypes.OrderBy(x => x.SequanceNo).ToList();


                foreach (var row in sequanceList)
                {
                    var lstDetails = lstData.Where((x, i) => x.TabTypeId == row.Type).ToList();
                    if (lstDetails.Count() > 0)
                    {
                        int rowIndex = 1;
                        foreach (var item in lstDetails)
                        {
                            item.SequanceNo = rowIndex;
                            TotalDataList.Add(item);
                            rowIndex++;
                        }
                    }
                }

                #endregion

                #region Add into Database


                string errorMessage = "";
                try
                {
                    string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                    long lgtaId = 0, lgOuExId = 0, lgDepartmId = 0;
                    string[] splitString = queryString.Split('|');
                    if (splitString.Length > 2)
                    {
                        if (!string.IsNullOrWhiteSpace(splitString[0]) && long.TryParse(splitString[0], out lgtaId) && !string.IsNullOrWhiteSpace(splitString[1]) && long.TryParse(splitString[1], out lgOuExId) && !string.IsNullOrWhiteSpace(splitString[2]) && long.TryParse(splitString[2], out lgDepartmId))
                        {

                            lgTaabId = lgtaId;
                            lgOurExId = lgOuExId;
                            lgDepartmentId = lgDepartmId;

                            //using (var transaction = new TransactionScope())
                            {
                                if (!objDepartmentTabRepository.RemoveDeparmentTabDetailById(lgTaabId, out errorMessage))
                                {
                                    int lgSaveCount = 0;
                                    foreach (var row in TotalDataList)
                                    {
                                        row.Id = 0;
                                        row.TabId = lgTaabId;
                                        row.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
                                        if (!objDepartmentTabRepository.InsertOrUpdateDeparmentTabDetail(row, out errorMessage))
                                        {
                                            lgSaveCount++;
                                        }
                                        else
                                        {
                                            Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                            goto ERROR1;
                                        }
                                    }
                                    ERROR1: if (TotalDataList.Count() == lgSaveCount)
                                    {
                                        //transaction.Complete();
                                        Functions.MessagePopup(this, "Saved Successfully", PopupMessageType.success);
                                    }
                                }
                                else
                                {
                                    Functions.MessagePopup(this, errorMessage, PopupMessageType.error);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Functions.MessagePopup(this, ex.Message.ToString(), PopupMessageType.error);
                }

                #endregion

            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/Admin/Hospital/DepartmentTabMaster?" + Unmehta.WebPortal.Web.Common.Functions.Base64Encode(lgOurExId + "|" + lgDepartmentId)), false);
        }

        #endregion

        #region Page Sub Value Functions

        #region Type2

        #region Page Methods

        protected void btnType2Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (Type2 == null)
            {
                Type2 = new TabSubDetails();
                Type2.Data = new List<SubDetailsModel>();
            }
            if (Type2.Data == null)
            {
                Type2.Data = new List<SubDetailsModel>();
            }

            SubDetailsModel objBo = new SubDetailsModel();


            List<SubDetailsModel> lstData = Type2.Data;

            if (hfType2Id.Value == "0" && hfType2Command.Value == "0")
            {
                objBo = new SubDetailsModel();
            }
            else
            {
                objBo = lstData.Where((i,x)=> x== Convert.ToInt32(hfType2Id.Value)).FirstOrDefault();
            }

            if (!ValidateSubType2(ref objBo))
            {

                if (hfType2Id.Value == "0" && hfType2Command.Value == "0")
                {
                    lstData.Add(objBo);
                }

                Functions.MessagePopup(this, "Saved Successfully.", PopupMessageType.success);

                TabSubDetails tabSubDetails = new TabSubDetails();
                tabSubDetails.Data = lstData;
                tabSubDetails.SequanceNo = Convert.ToInt32(txtType2SquanceNo.Text);

                Type2 = tabSubDetails;

                ClearType2Form();
                BindType2Form();
            }
        }

        protected void btnType2Clear_Click(object sender, EventArgs e)
        {
            ClearType2Form();
        }

        protected void ibtn_Type2Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<SubDetailsModel> lstData = Type2.Data;
            var checkForUPdate = lstData.ElementAt((int)rowindex);
            if (checkForUPdate != null)
            {
                hfType2Id.Value = rowindex.ToString();
                hfType2Command.Value = "1";

                hfType2PopUpImage.Value = checkForUPdate.PopupImageName;

                lblType2PopUpImage.Text= checkForUPdate.PopupImageName;
                if(!string.IsNullOrWhiteSpace(checkForUPdate.PopupImageName))
                {
                    aRemoveType2PopUpImage.Visible = true;

                }
                else
                {
                aRemoveType2PopUpImage.Visible = false;
                }

                txtType2PopupDesc.Text = HttpUtility.HtmlDecode( checkForUPdate.PopupDesc);
                txtType2SequenceRowNo.Text = checkForUPdate.SequanceNo.ToString();
                txtType2ShortDescription.Text = HttpUtility.HtmlDecode(checkForUPdate.PopupBasicShortDesc);
            }
            else
            {
                ClearType2Form();
            }
        }

        protected void ibtn_Type2Delete_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<SubDetailsModel> lstData = Type2.Data;
            var checkForUPdate = lstData.ElementAt(((int)rowindex));
            if (checkForUPdate != null)
            {
                lstData.Remove(checkForUPdate);

                TabSubDetails tabSubDetails = new TabSubDetails();
                tabSubDetails.Data = lstData;
                tabSubDetails.SequanceNo = Convert.ToInt32(txtType2SquanceNo.Text);

                Type2 = tabSubDetails;

                ClearType2Form();
                BindType2Form();

                Functions.MessagePopup(this, "Remove Record Done", PopupMessageType.success);
                txtType2SquanceNo.Focus();
            }

        }

        protected void gvType2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindType2Form();
            gvType2.PageIndex = e.NewPageIndex;
            lgType2PageIndex = e.NewPageIndex;
            gvType2.DataBind();
        }

        #endregion

        #region Page Functions

        public void BindType2Form()
        {
            if (Type2 == null)
            {
                Type2 = new TabSubDetails();
                Type2.Data = new List<SubDetailsModel>();
            }
            if (Type2.Data == null)
            {
                Type2.Data = new List<SubDetailsModel>();
            }

            gvType2.DataSource = Type2.Data;
            gvType2.DataBind();
        }

        public void ClearType2Form()
        {
            hfType2Id.Value = "0";
            hfType2Command.Value = "0";
            hfType2PopUpImage.Value = "";
            txtType2PopupDesc.Text = "";
            txtType2SequenceRowNo.Text = "";
            aRemoveType2PopUpImage.Visible = false;
            lblType2PopUpImage.Text = "";
            txtType2ShortDescription.Text = "";
        }

        #endregion

        #endregion

        #region Type3

        #region Page Methods

        protected void btnType3Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (Type3 == null)
            {
                Type3 = new TabSubDetails();
                Type3.Data = new List<SubDetailsModel>();
            }
            if (Type3.Data == null)
            {
                Type3.Data = new List<SubDetailsModel>();
            }

            SubDetailsModel objBo = new SubDetailsModel();


            List<SubDetailsModel> lstData = Type3.Data;

            if (hfType3Id.Value == "0" && hfType3Command.Value == "0")
            {
                objBo = new SubDetailsModel();
            }
            else
            {
                objBo = lstData.Where((i, x) => x == Convert.ToInt32(hfType3Id.Value)).FirstOrDefault();
            }

            if (!ValidateSubType3(ref objBo))
            {

                if (hfType3Id.Value == "0" && hfType3Command.Value == "0")
                {
                    lstData.Add(objBo);
                }

                Functions.MessagePopup(this, "Saved Successfully.", PopupMessageType.success);

                TabSubDetails tabSubDetails = new TabSubDetails();
                tabSubDetails.Data = lstData;
                tabSubDetails.SequanceNo = Convert.ToInt32(txtType3SubSquanceNo.Text);

                Type3 = tabSubDetails;

                ClearType3Form();
                BindType3Form();
            }
        }

        protected void btnType3Clear_Click(object sender, EventArgs e)
        {
            ClearType3Form();
        }

        protected void ibtn_Type3Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<SubDetailsModel> lstData = Type3.Data;
            var checkForUPdate = lstData.ElementAt((int)rowindex);
            if (checkForUPdate != null)
            {
                hfType3Id.Value = rowindex.ToString();
                hfType3Command.Value = "1";
                txtType3SubSquanceNo.Text = checkForUPdate.SequanceNo.ToString();
            }
            else
            {
                ClearType3Form();
            }
        }

        protected void ibtn_Type3Delete_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<SubDetailsModel> lstData = Type3.Data;
            var checkForUPdate = lstData.ElementAt(((int)rowindex));
            if (checkForUPdate != null)
            {
                lstData.Remove(checkForUPdate);

                TabSubDetails tabSubDetails = new TabSubDetails();
                tabSubDetails.Data = lstData;
                tabSubDetails.SequanceNo = Convert.ToInt32(txtType3SequanceNo.Text);

                Type3 = tabSubDetails;

                ClearType3Form();
                BindType3Form();

                Functions.MessagePopup(this, "Remove Record Done", PopupMessageType.success);
                txtType3SequanceNo.Focus();
            }

        }

        protected void gvType3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindType3Form();
            gvType3.PageIndex = e.NewPageIndex;
            lgType3PageIndex = e.NewPageIndex;
            gvType3.DataBind();
        }

        #endregion

        #region Page Functions

        public void BindType3Form()
        {
            if (Type3 == null)
            {
                Type3 = new TabSubDetails();
                Type3.Data = new List<SubDetailsModel>();
            }
            if (Type3.Data == null)
            {
                Type3.Data = new List<SubDetailsModel>();
            }
            if(Type3.Data.Count()>0)
            {
                using (IStatisticsChartRepository objIStatisticsChartRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
                {
                    var ChartData = objIStatisticsChartRepository.GetAllStatisticsChart();
                    if(ChartData.Count()>0)
                    {
                        foreach(var row in Type3.Data)
                        {
                            var foundData = ChartData.Where(x => x.Id == row.StatasticId).FirstOrDefault();
                            if(foundData!=null)
                            {
                                if(foundData.Id>0)
                                {
                                    row.PopupBasicShortDesc = foundData.ChartName;
                                }
                            }
                        }
                    }
                }
            }
            gvType3.DataSource = Type3.Data;
            gvType3.DataBind();
        }

        public void ClearType3Form()
        {
            hfType3Id.Value = "0";
            hfType3Command.Value = "0";
            txtType3SubSquanceNo.Text = "";
            ddlType3Statistics.SelectedIndex = 0;
        }

        #endregion

        #endregion

        #region Type5

        #region Page Methods

        protected void btnType5Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (Type5 == null)
            {
                Type5 = new TabSubDetails();
                Type5.Data = new List<SubDetailsModel>();
            }
            if (Type5.Data == null)
            {
                Type5.Data = new List<SubDetailsModel>();
            }

            SubDetailsModel objBo = new SubDetailsModel();
            if (!ValidateSubType5(ref objBo))
            {
                List<SubDetailsModel> lstData = Type5.Data;
                if (lstData == null)
                {
                    lstData = new List<SubDetailsModel>();
                    lstData.Add(objBo);
                }
                else
                {
                    //int rowCount= lstData.Count();
                    //if(rowCount > objBo.Id)
                    //{
                    //    var checkForUPdate = lstData.ElementAt((int)objBo.Id);
                    //    if (checkForUPdate != null)
                    //    {
                    //        lstData.Remove(checkForUPdate);
                    //        lstData.Insert((int)objBo.Id, objBo);
                    //    }
                    //}
                    //else
                    {
                        lstData.Add(objBo);
                    }
                }

                Functions.MessagePopup(this, "Saved Successfully.", PopupMessageType.success);

                TabSubDetails tabSubDetails = new TabSubDetails();
                tabSubDetails.Data = lstData;
                tabSubDetails.SequanceNo = Convert.ToInt32(txtType5SquanceNo.Text);

                Type5 = tabSubDetails;

                ClearType5Form();
                BindType5Form();
            }
        }

        protected void btnType5Clear_Click(object sender, EventArgs e)
        {
            ClearType5Form();
        }
        
        protected void ibtn_Type5Delete_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<SubDetailsModel> lstData = Type5.Data;
            var checkForUPdate = lstData.ElementAt(rowindex);
            if (checkForUPdate != null)
            {
                lstData.Remove(checkForUPdate);


                TabSubDetails tabSubDetails = new TabSubDetails();
                tabSubDetails.Data = lstData;
                tabSubDetails.SequanceNo = Convert.ToInt32(txtType5SquanceNo.Text);

                Type5 = tabSubDetails;

                ClearType5Form();
                BindType5Form();

                Functions.MessagePopup(this, "Remove Record Done", PopupMessageType.success);
                txtType5SquanceNo.Focus();
            }

        }

        protected void gvType5_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindType5Form();
            gvType5.PageIndex = e.NewPageIndex;
            lgType5PageIndex = e.NewPageIndex;
            gvType5.DataBind();
        }

        #endregion

        #region Page Functions

        public void BindType5Form()
        {
            if (Type5 == null)
            {
                Type5 = new TabSubDetails();
                Type5.Data = new List<SubDetailsModel>();
            }
            if (Type5.Data == null)
            {
                Type5.Data = new List<SubDetailsModel>();
            }

            gvType5.DataSource = Type5.Data;
            gvType5.DataBind();
        }

        public void ClearType5Form()
        {
            hfType5PopUpImage.Value = "";
            aRemoveType5PopUpImage.Visible = false;
            lblType5PopUpImage.Text = "";
        }

        #endregion

        #endregion

        #region Type6

        #region Page Methods

        protected void btnType6Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (Type6 == null)
            {
                Type6 = new TabSubDetails();
                Type6.Data = new List<SubDetailsModel>();
            }
            if (Type6.Data == null)
            {
                Type6.Data = new List<SubDetailsModel>();
            }

            SubDetailsModel objBo = new SubDetailsModel();
            List<SubDetailsModel> lstData = Type6.Data;

            if (hfType6Id.Value == "0" && hfType6Command.Value == "0")
            {
                objBo = new SubDetailsModel();
            }
            else
            {
                objBo = lstData.Where((i, x) => x == Convert.ToInt32(hfType6Id.Value)).FirstOrDefault();
            }

            if (!ValidateSubType6(ref objBo))
            {

                if (hfType6Id.Value == "0" && hfType6Command.Value == "0")
                {
                    lstData.Add(objBo);
                }

                Functions.MessagePopup(this, "Saved Successfully.", PopupMessageType.success);


                TabSubDetails tabSubDetails = new TabSubDetails();
                tabSubDetails.Data = lstData;
                tabSubDetails.SequanceNo = Convert.ToInt32(txtType6SquanceNo.Text);

                Type6 = tabSubDetails;


                ClearType6Form();
                BindType6Form();
            }
        }

        protected void btnType6Clear_Click(object sender, EventArgs e)
        {
            ClearType6Form();
        }

        protected void gvType6_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindType6Form();
            gvType6.PageIndex = e.NewPageIndex;
            lgType6PageIndex = e.NewPageIndex;
            gvType6.DataBind();
        }

        protected void ibtn_Type6Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<SubDetailsModel> lstData = Type6.Data;
            var checkForUPdate = lstData.ElementAt((int)rowindex );
            if (checkForUPdate != null)
            {
                hfType6Id.Value = rowindex.ToString();
                hfType6Command.Value = "1";
                hfType6PopUpImage.Value = checkForUPdate.PopupImageName;
                
                lblType6PopUpImage.Text = checkForUPdate.PopupImageName;
                if (!string.IsNullOrWhiteSpace(checkForUPdate.PopupImageName))
                {
                aRemoveType6PopUpImage.Visible = true;

                }
                else
                {
                    aRemoveType6PopUpImage.Visible = false;
                }
                txtType6AccordionTitle.Text = checkForUPdate.PopupBasicShortDesc;
                txtType6Description.Text = HttpUtility.HtmlDecode(checkForUPdate.PopupDesc);
                txtType6SequenceRowNo.Text = checkForUPdate.SequanceNo.ToString();
            }
            else
            {
                ClearType6Form();
            }
        }

        protected void ibtn_Type6Delete_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<SubDetailsModel> lstData = Type6.Data;
            var checkForUPdate = lstData.ElementAt((int)rowindex);
            if (checkForUPdate != null)
            {
                lstData.Remove(checkForUPdate);


                TabSubDetails tabSubDetails = new TabSubDetails();
                tabSubDetails.Data = lstData;
                tabSubDetails.SequanceNo = Convert.ToInt32(txtType6SquanceNo.Text);

                Type6 = tabSubDetails;

                ClearType6Form();
                BindType6Form();

                Functions.MessagePopup(this, "Remove Record Done", PopupMessageType.success);
                txtType6SquanceNo.Focus();
            }

        }

        #endregion

        #region Page Functions

        public void BindType6Form()
        {
            if (Type6 == null)
            {
                Type6 = new TabSubDetails();
                Type6.Data = new List<SubDetailsModel>();
            }
            if (Type6.Data == null)
            {
                Type6.Data = new List<SubDetailsModel>();
            }

            gvType6.DataSource = Type6.Data;
            gvType6.DataBind();
        }

        public void ClearType6Form()
        {
            hfType6Id.Value = "0";
                hfType6Command.Value = "0";
            hfType6PopUpImage.Value = "";
            aRemoveType6PopUpImage.Visible = false;
            lblType6PopUpImage.Text = "";
            txtType6Description.Text = "";
            txtType6SequenceRowNo.Text = "";
            txtType6AccordionTitle.Text = "";
        }

        #endregion

        #endregion

        #region Type7

        #region Page Methods

        protected void gvType7_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindType7Form();
            gvType7.PageIndex = e.NewPageIndex;
            lgType7PageIndex = e.NewPageIndex;
            gvType7.DataBind();
        }

        protected void btnType7Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (Type7 == null)
            {
                Type7 = new TabSubDetails();
                Type7.Data = new List<SubDetailsModel>();
            }
            if (Type7.Data == null)
            {
                Type7.Data = new List<SubDetailsModel>();
            }

            SubDetailsModel objBo = new SubDetailsModel();
            List<SubDetailsModel> lstData = Type7.Data;

            if (hfType7Id.Value == "0" && hfType7Command.Value == "0")
            {
                objBo = new SubDetailsModel();
            }
            else
            {
                objBo = lstData.Where((i, x) => x == Convert.ToInt32(hfType7Id.Value)).FirstOrDefault();
            }

            if (!ValidateSubType7(ref objBo))
            {


                if (hfType7Id.Value == "0" && hfType7Command.Value == "0")
                {
                    lstData.Add(objBo);
                }


                Functions.MessagePopup(this, "Saved Successfully.", PopupMessageType.success);

                TabSubDetails tabSubDetails = new TabSubDetails();
                tabSubDetails.Data = lstData;
                tabSubDetails.SequanceNo = Convert.ToInt32(txtType7SquanceNo.Text);

                Type7 = tabSubDetails;

                ClearType7Form();
                BindType7Form();
            }
        }

        protected void btnType7Clear_Click(object sender, EventArgs e)
        {
            ClearType7Form();
        }

        protected void ibtn_Type7Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<SubDetailsModel> lstData = Type7.Data;
            var checkForUPdate = lstData.ElementAt((int)rowindex );
            if (checkForUPdate != null)
            {
                hfType7Id.Value = rowindex.ToString();
                hfType7Command.Value = "1";
                hfType7PopUpImage.Value = checkForUPdate.PopupImageName;

                lblType7PopUpImage.Text = checkForUPdate.PopupImageName;
                if (!string.IsNullOrWhiteSpace(checkForUPdate.PopupImageName))
                {
                    aRemoveType7PopUpImage.Visible = true;

                }
                else
                {
                    aRemoveType7PopUpImage.Visible = false;
                }

                txtType7PopupDesc.Text = HttpUtility.HtmlDecode( checkForUPdate.PopupDesc);
                txtType7SequenceRowNo.Text = checkForUPdate.SequanceNo.ToString();
                txtType7Title.Text = checkForUPdate.IntroductionDesc;
                txtType7ShortDescription.Text = checkForUPdate.PopupBasicShortDesc;
            }
            else
            {
                ClearType7Form();
            }
        }

        protected void ibtn_Type7Delete_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<SubDetailsModel> lstData = Type7.Data;
            var checkForUPdate = lstData.ElementAt((int)rowindex);
            if (checkForUPdate != null)
            {
                lstData.Remove(checkForUPdate);
                TabSubDetails tabSubDetails = new TabSubDetails();
                tabSubDetails.Data = lstData;
                tabSubDetails.SequanceNo = Convert.ToInt32(txtType7SquanceNo.Text);

                Type7 = tabSubDetails;

                ClearType7Form();
                BindType7Form();
                Functions.MessagePopup(this, "Remove Record Done", PopupMessageType.success);
                txtType7SquanceNo.Focus();
            }

        }

        #endregion

        #region Page Functions

        public void BindType7Form()
        {
            if (Type7 == null)
            {
                Type7 = new TabSubDetails();
                Type7.Data = new List<SubDetailsModel>();
            }
            if (Type7.Data == null)
            {
                Type7.Data = new List<SubDetailsModel>();
            }

            gvType7.DataSource = Type7.Data;
            gvType7.DataBind();
        }

        public void ClearType7Form()
        {
            hfType7Id.Value = "0";
            hfType7PopUpImage.Value = "";
            hfType7Command.Value = "0";
            aRemoveType7PopUpImage.Visible = false;
            lblType7PopUpImage.Text = "";
            txtType7PopupDesc.Text = "";
            txtType7SequenceRowNo.Text = "";
            txtType7ShortDescription.Text = "";
        }

        #endregion

        #endregion

        #region Type8

        #region Page Methods

        protected void gvType8_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindType8Form();
            gvType8.PageIndex = e.NewPageIndex;
            lgType8PageIndex = e.NewPageIndex;
            gvType8.DataBind();
        }

        protected void btnType8Save_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (Type8 == null)
            {
                Type8 = new TabSubDetails();
                Type8.Data = new List<SubDetailsModel>();
            }
            if (Type8.Data == null)
            {
                Type8.Data = new List<SubDetailsModel>();
            }

            SubDetailsModel objBo = new SubDetailsModel();
                List<SubDetailsModel> lstData = Type8.Data;
            if (hfType8Id.Value == "0" && hfType8Command.Value == "0")
            {
                objBo = new SubDetailsModel();
            }
            else
            {
                objBo = lstData.Where((i, x) => x == Convert.ToInt32(hfType8Id.Value)).FirstOrDefault();
            }
            if (!ValidateSubType8(ref objBo))
            {

                if (hfType8Id.Value == "0" && hfType8Command.Value == "0")
                {
                    lstData.Add(objBo);
                }

                Functions.MessagePopup(this, errorMessage, PopupMessageType.success);

                TabSubDetails tabSubDetails = new TabSubDetails();
                tabSubDetails.Data = lstData;
                tabSubDetails.SequanceNo = Convert.ToInt32(txtType8SquanceNo.Text);

                Type8 = tabSubDetails;

                ClearType8Form();
                BindType8Form();
            }
        }

        protected void btnType8Clear_Click(object sender, EventArgs e)
        {
            ClearType8Form();
        }

        protected void ibtn_Type8Edit_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<SubDetailsModel> lstData = Type8.Data;
            var checkForUPdate = lstData.ElementAt((int)rowindex);
            if (checkForUPdate != null)
            {
                hfType8Id.Value = rowindex.ToString();
                hfType8PopUpImage.Value = checkForUPdate.PopupImageName;

                lblType8PopUpImage.Text = checkForUPdate.PopupImageName;

                if (!string.IsNullOrWhiteSpace(checkForUPdate.PopupImageName))
                {
                    aRemoveType8PopUpImage.Visible = true;
                }
                else
                {
                    aRemoveType8PopUpImage.Visible = false;
                }

                hfType8Command.Value ="1";
                txtType8PopupDesc.Text = HttpUtility.HtmlDecode( checkForUPdate.PopupDesc);
                txtType8SequenceRowNo.Text = checkForUPdate.SequanceNo.ToString();
            }
            else
            {
                ClearType8Form();
            }
        }

        protected void ibtn_Type8Delete_Click(object sender, EventArgs e)
        {

            int rowindex = ((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex;

            List<SubDetailsModel> lstData = Type8.Data;
            var checkForUPdate = lstData.ElementAt((int)rowindex);
            if (checkForUPdate != null)
            {
                lstData.Remove(checkForUPdate);

                TabSubDetails tabSubDetails = new TabSubDetails();
                tabSubDetails.Data = lstData;
                tabSubDetails.SequanceNo = Convert.ToInt32(txtType8SquanceNo.Text);

                Type8 = tabSubDetails;

                ClearType8Form();
                BindType8Form();

                Functions.MessagePopup(this, "Remove Record Done", PopupMessageType.success);
                txtType8SquanceNo.Focus();
            }

        }

        #endregion

        #region Page Functions

        public void BindType8Form()
        {
            if (Type8 == null)
            {
                Type8 = new TabSubDetails();
                Type8.Data = new List<SubDetailsModel>();
            }
            if (Type8.Data == null)
            {
                Type8.Data = new List<SubDetailsModel>();
            }

            gvType8.DataSource = Type8.Data;
            gvType8.DataBind();
        }

        public void ClearType8Form()
        {

            hfType8Id.Value = "0"; hfType8Command.Value = "0";
            hfType8PopUpImage.Value = "";
            txtType8PopupDesc.Text = "";
            aRemoveType8PopUpImage.Visible = false;
            lblType8PopUpImage.Text = "";
            txtType8SequenceRowNo.Text = "";
        }

        #endregion

        #endregion        

        #endregion
    }
}