using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.Hospital
{
    public partial class StudentAllocateSeats : System.Web.UI.Page
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
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtRoundName.Text.Trim()))
            {
                Functions.MessagePopup(this, "Please Enter Academics Name", PopupMessageType.error);
            }
            else
            {
                using (AllocateSeatBAL objAllocateSeatBAL = new AllocateSeatBAL())
                {
                    DataTable dt;
                    if (objAllocateSeatBAL.AllocateSeatAsPerStudentChoiceFilling(txtRoundName.Text, SessionWrapper.UserDetails.UserName, out dt))
                    {
                        long lgInsertRecord = 0, lgUpdateRecord = 0, lgSkipRecord = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            lgInsertRecord = Convert.ToInt32(row["InsertRecord"].ToString());
                            lgUpdateRecord = Convert.ToInt32(row["UpdateRecord"].ToString());
                            lgSkipRecord = Convert.ToInt32(row["SkipRecord"].ToString());
                        }
                        if (lgInsertRecord > 0 && lgInsertRecord > 0)
                        {
                            Functions.MessagePopup(this, "Seat Allocate Success. <br/> Insert Record:" + lgInsertRecord + " <br/> Update Record:" + lgUpdateRecord + " <br/> Skip Record:" + lgSkipRecord + "", PopupMessageType.success);
                            BindGridView();
                        }
                        else
                        {
                            Functions.MessagePopup(this, "<br/> Insert Record:" + lgInsertRecord + " <br/> Update Record:" + lgUpdateRecord + " <br/> Skip Record:" + lgSkipRecord + "", PopupMessageType.error);
                        }
                    }
                }
            }
        }

        private void BindGridView()
        {
            using (AllocateSeatBAL objAllocateSeatBAL = new AllocateSeatBAL())
            {
            }
        }
    }
}