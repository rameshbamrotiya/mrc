using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Unmehta.WebPortal.Web.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;

namespace Unmehta.WebPortal.Web.Admin.CMS
{
    public class CHkBoxList
    {
        public string Id { get; set; }
        public bool Checked { get; set; }
    }
    public partial class EventMasterForm : System.Web.UI.Page
    {
            public static long EventId;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string queryString = Functions.Base64Decode(HttpUtility.UrlDecode(Request.QueryString.ToString()));
                long id = 0;
                {
                    foreach (Control control in this.Controls)
                    {
                        if (control is CheckBox)
                        {
                            ((CheckBox)control).Checked = false;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(queryString) && long.TryParse(queryString, out id))
                    {
                        EventId = id;
                        using (IEventFormFieldRepository objIEventFormFieldRepository = new EventFormFieldRepository(Functions.strSqlConnectionString))
                        {
                            var eventCheckList = objIEventFormFieldRepository.GetAlllongAboutUsMaster(EventId);
                            if (eventCheckList.Count() > 0)
                            {
                                var checkBoxList= eventCheckList.Select(x => x.ColumnName).ToList();
                                ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("bodyPart");
                                foreach(var chkBox in checkBoxList)
                                {
                                    CheckBox chkBoxs = (CheckBox)cph.FindControl("chk"+chkBox);
                                    if(chkBoxs!=null)
                                    {
                                        chkBoxs.Checked = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoopControls(List<CHkBoxList> checkBoxes, ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is CheckBox)
                    checkBoxes.Add( new CHkBoxList { Id= control.ID, Checked=((CheckBox)control).Checked });
                if (control.Controls.Count > 0)
                    LoopControls(checkBoxes, control.Controls);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/Admin/CMS/EventMaster"), false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (IEventFormFieldRepository objIEventFormFieldRepository = new EventFormFieldRepository(Functions.strSqlConnectionString))
            {
                List<CHkBoxList> checkBoxes = new List<CHkBoxList>();
                LoopControls(checkBoxes, this.Controls);
                if (checkBoxes.Where(x => x.Checked).Count() > 5)
                {
                    objIEventFormFieldRepository.RemoveEventFormFieldMasterByEventId((int)EventId);
                    foreach (var chk in checkBoxes.Where(x => x.Checked))
                    {
                            {
                                GetAllEventFormFieldMasterByEventIdResult objData = new GetAllEventFormFieldMasterByEventIdResult();
                                objData.ColumnName = chk.Id.Replace("chk", "");
                                objData.EventId = EventId;
                                objData.IsVisible = true;
                                objData.IsRequired = false;

                                objIEventFormFieldRepository.InsertEventFormFieldMaster(objData);
                            }
                    }
                }
                else
                {
                    Functions.MessagePopup(this, "Please select at least five check box on successfully.", PopupMessageType.success);
                    return;
                }
            }
        }
    }
}