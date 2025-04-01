using BAL;
using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Repository.Interface.Rights;
using Unmehta.WebPortal.Repository.Repository.Rights;
using Unmehta.WebPortal.Web.Common;

namespace Unmehta.WebPortal.Web.Admin.Rights
{
    public partial class UserRights : System.Web.UI.Page
    {
        public UserRightsBO ObjUserRights = new UserRightsBO();
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
            //if (SessionWrapper.UserName != null && Application["user_rights_table"] != null)
            //{
            //    Common.SetPagePermission((DataTable)Application["user_rights_table"], HttpContext.Current.Request.RawUrl, ref ObjUserRights);
            //}
            //else
            //{
            //    Response.Redirect("~/Pages/Loginpage.aspx");
            //}
            //if (!ObjUserRights.CanView)
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('You are not authorized to access this page!!!');", true);
            //    Response.Redirect("Default.aspx");
            //}
            //if (!ObjUserRights.CanAdd)
            //{
            //    btn_Save.Visible = false;
            //}
            if (!IsPostBack)
            {
                FillRole();
                FillMenutype();
            }
        }
        private void FillRole()
        {
            DataSet ds = new DataSet();
            UserMasterBO objbo = new UserMasterBO();
            //UserMasterBAL objBAL = new UserMasterBAL();
            //ds = objBAL.SelectRole(objbo);
            //DataTable dt = ds.Tables[0];
            //Functions.PopulateDropDownList(drpRole, dt, "rolename", "roleid", true);
            using (IRoleMasterRepositry objAdminMenuLinkMasterRepository = new RoleMasterRepositry(Functions.strSqlConnectionString))
            {
                drpRole.DataSource = objAdminMenuLinkMasterRepository.GetAllRoleMasterActiveList();
                drpRole.DataTextField = "Rolename";
                drpRole.DataValueField = "Id";
                drpRole.DataBind();
            }

        }
        private void FillMenutype()
        {
            DataSet ds = new DataSet();

            //ResourceBAL objBAL = new ResourceBAL();
            //ds = objBAL.SelectAllResource();
            //DataTable dt = ds.Tables[0];
            //Functions.PopulateDropDownList(drpParentMenu, dt, "col_menu_name", "col_menu_id", true);
            using (IAdminMenuLinkMasterRepository objAdminMenuLinkMasterRepository = new AdminMenuLinkMasterRepository(Functions.strSqlConnectionString))
            {
                drpParentMenu.DataSource = objAdminMenuLinkMasterRepository.GetAllAdminMenuMaster().Where(x=> x.ParentId==0);
                drpParentMenu.DataTextField = "MenuName";
                drpParentMenu.DataValueField = "Id";
                drpParentMenu.DataBind();

            }
        }
        protected void btnFetch_ServerClick(object sender, EventArgs e)
        {
            UserRightsBO objBO = new UserRightsBO();
            UserRightsBAL objBAL = new UserRightsBAL();
            objBO.roleid = Convert.ToInt16(drpRole.SelectedValue.ToString());
            FillAllModules();
            DataSet ds = objBAL.selectMenu(objBO);
            if (ds.Tables[0].Rows.Count > 0)
            {

                BindModulePermission(ds.Tables[0]);

            }

        }
        private void FillAllModules()
        {
            try
            {

                {

                    UserRightsBAL objBAL = new UserRightsBAL();
                    DataSet ds = objBAL.SelectMenuResourceWise();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Session["dt_menu"] = ds.Tables[0];
                        var dtView = ds.Tables[0].DefaultView;
                        dtView.RowFilter = "col_parent_id = 0";
                        rptUserRights.DataSource = dtView;
                        rptUserRights.DataBind();
                    }
                }


            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void BindModulePermission(DataTable dataTable)
        {
            try
            {
                foreach (RepeaterItem repeated in rptUserRights.Items)
                {
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk1"));
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk2"));
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk3"));
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk4"));

                    BindCheckBox(repeated, dataTable);
                    #region RepeaterFirstChild
                    var rpt1 =
                                    (Repeater)repeated.FindControl("rptUserRightschild");
                    if (rpt1 != null)
                    {
                        foreach (RepeaterItem repeated1 in rpt1.Items)
                        {
                            ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated1.FindControl("chk1"));
                            ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated1.FindControl("chk2"));
                            ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated1.FindControl("chk3"));
                            ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated1.FindControl("chk4"));
                            BindCheckBox(repeated1, dataTable);
                            #region RepeaterSecondChild
                            var rpt2 =
                                            (Repeater)repeated1.FindControl("rptUserRightschild1");
                            if (rpt2 != null)
                            {
                                foreach (RepeaterItem repeated2 in rpt2.Items)
                                {
                                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated2.FindControl("chk1"));
                                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated2.FindControl("chk2"));
                                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated2.FindControl("chk3"));
                                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated2.FindControl("chk4"));
                                    BindCheckBox(repeated2, dataTable);
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
            }

            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }

        }
        private void BindCheckBox(RepeaterItem repeated, DataTable dataTable)
        {
            try
            {
                var filteredTable = new DataTable();
                var lblmenu =
                     (Label)FindControlRecursive(repeated, "lblmenuid");
                if (lblmenu != null)
                {
                    var dvView = dataTable.DefaultView;
                    dvView.RowFilter = "col_menu_id =" + Convert.ToInt32(lblmenu.Text.Trim()) + " and col_user_type_id = " + Convert.ToInt32(drpRole.SelectedValue);
                    filteredTable = dvView.ToTable();
                }
                if (filteredTable.Rows.Count > 0)
                {
                    var chkView =
                           (CheckBox)FindControlRecursive(repeated, "chk1");
                    if (chkView != null)
                    {
                        chkView.Checked = (bool)filteredTable.Rows[0]["col_isView"];
                    }
                    var chkAdd =
                        (CheckBox)FindControlRecursive(repeated, "chk2");
                    if (chkAdd != null)
                    {
                        chkAdd.Checked = (bool)filteredTable.Rows[0]["col_isAdd"];
                    }
                    var chkUpdate =
                        (CheckBox)FindControlRecursive(repeated, "chk3");
                    if (chkUpdate != null)
                    {
                        chkUpdate.Checked = (bool)filteredTable.Rows[0]["col_isUpdate"];
                    }
                    var chkDelete =
                        (CheckBox)FindControlRecursive(repeated, "chk4");
                    if (chkDelete != null)
                    {
                        chkDelete.Checked = (bool)filteredTable.Rows[0]["col_isDelete"];
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
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
        protected void btn_Save_ServerClick(object sender, EventArgs e)
        {
            UserRightsBAL objBAL = new UserRightsBAL();

            UserRightsBO objBO = new UserRightsBO();

            var objRightList = new List<UserRightsBO>();
            SetRightsForUserType(ref objRightList);
            var errorcount = 0;
            objBO.roleid = Convert.ToInt16(drpRole.SelectedValue.ToString());
            if (new UserRightsBAL().DeleteRecord(objBO))
            {
                foreach (var userRights in objRightList)
                {
                    try
                    {
                        userRights.roleid = Convert.ToInt16(drpRole.SelectedValue.ToString());
                        userRights.added_by = SessionWrapper.UserDetails.UserName;
                        userRights.added_date = DateTime.Now.ToString();
                        userRights.ipaddress = "";// GetIPAddress;
                        if (objBAL.InsertMenu(userRights))
                        {
                            errorcount = 0;
                        }
                        else
                        {
                            errorcount = errorcount + 1;
                        }


                    }

                    catch (Exception ex)
                    {
                        ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
                    }
                }
                Response.Redirect( Request.Url.ToString());
                //ShowMessage("alert('Record inserted Sucessfully'); location.replace('" + Request.Url.ToString() + "'); ");
            }
        }
        private void SetRightsForUserType(ref List<UserRightsBO> objRightList)
        {
            try
            {
                foreach (RepeaterItem repeated in rptUserRights.Items)
                {
                    var objUserRights = new UserRightsBO();
                    SetPermission(repeated, objUserRights);
                    objRightList.Add(objUserRights);
                    #region RepeaterFirstChild
                    var rpt1 =
                                    (Repeater)repeated.FindControl("rptUserRightschild");
                    if (rpt1 != null)
                    {
                        foreach (RepeaterItem repeated1 in rpt1.Items)
                        {
                            objUserRights = new UserRightsBO();
                            SetPermission(repeated1, objUserRights);
                            objRightList.Add(objUserRights);
                            #region RepeaterSecondChild
                            var rpt2 =
                                            (Repeater)repeated1.FindControl("rptUserRightschild1");
                            if (rpt2 != null)
                            {
                                foreach (RepeaterItem repeated2 in rpt2.Items)
                                {
                                    objUserRights = new UserRightsBO();
                                    SetPermission(repeated2, objUserRights);
                                    objRightList.Add(objUserRights);
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void SetPermission(RepeaterItem repeated, UserRightsBO objUserRights)
        {
            var lblmenu =
                      (Label)FindControlRecursive(repeated, "lblmenuid");
            if (lblmenu != null)
            {
                objUserRights.MenuId = Convert.ToInt32(lblmenu.Text);
            }

            var chkView =
                       (CheckBox)FindControlRecursive(repeated, "chk1");
            if (chkView != null)
            {
                objUserRights.CanView = chkView.Checked;
            }
            var chkAdd =
                (CheckBox)FindControlRecursive(repeated, "chk2");
            if (chkAdd != null)
            {
                objUserRights.CanAdd = chkAdd.Checked;
            }
            var chkUpdate =
                (CheckBox)FindControlRecursive(repeated, "chk3");
            if (chkUpdate != null)
            {
                objUserRights.CanUpdate = chkUpdate.Checked;
            }
            var chkDelete =
                (CheckBox)FindControlRecursive(repeated, "chk4");
            if (chkDelete != null)
            {
                objUserRights.CanDelete = chkDelete.Checked;
            }
        }
        protected void ShowMessage(string Message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertBox", Message, true);
        }
        protected void rptUserRights_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rptSubMenu = e.Item.FindControl("rptUserRightschild") as Repeater;
                var _dt = (DataTable)Session["dt_menu"];
                var dv = _dt.DefaultView;
                dv.RowFilter = "col_parent_id =" + Convert.ToInt32(((DataRowView)(e.Item.DataItem)).Row["col_menu_id"]) +
                               "";

                if (dv.Count > 0)
                {

                    if (rptSubMenu != null)
                    {
                        var lbl = e.Item.FindControl("first") as Label;
                        if (lbl != null)
                        {
                            lbl.Font.Bold = true;
                            lbl.ForeColor = Color.Black;
                        }
                        rptSubMenu.DataSource = dv.ToTable();
                        rptSubMenu.DataBind();
                    }
                }

            }
        }
        protected void rptUserRightschild_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rptSubMenu = e.Item.FindControl("rptUserRightschild1") as Repeater;

                var _dt = (DataTable)Session["dt_menu"];
                var dv = _dt.DefaultView;
                dv.RowFilter = "col_parent_id =" + Convert.ToInt32(((DataRowView)(e.Item.DataItem)).Row["col_menu_id"]) +
                               "";

                if (dv.Count > 0)
                {

                    if (rptSubMenu != null)
                    {
                        var lbl = e.Item.FindControl("second") as Label;
                        if (lbl != null)
                        {
                            lbl.Font.Bold = true;
                            lbl.ForeColor = Color.Indigo;
                        }
                        rptSubMenu.DataSource = dv.ToTable();
                        rptSubMenu.DataBind();
                    }
                }

            }
        }
        protected void rptUserRightschild1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lbl1 = e.Item.FindControl("third") as Label;
                if (lbl1 != null)
                {
                    lbl1.Font.Bold = true;
                    lbl1.ForeColor = Color.DodgerBlue;
                }
            }
        }
        protected void chkbView_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckBoxState("chk1", rptUserRights, chkbview);
        }
        private void SetCheckBoxState(string chk1, Repeater repeater, CheckBox chkBox)
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
        protected void chkbAdd_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckBoxState("chk2", rptUserRights, chkbadd);
        }
        protected void chkbdelete_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckBoxState("chk4", rptUserRights, chkbdelete);
        }
        protected void chkbupdate_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckBoxState("chk3", rptUserRights, chkbupdate);
        }
        protected void rptUserRights_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        protected void chk2_OnCheckedChanged(object sender, EventArgs e)
        {
            //SetCheckBoxState(sender, "chk1");
            var chkbox1 = (CheckBox)((CheckBox)sender).Parent.FindControl("chk1");
            if (((CheckBox)sender).Checked)
            {
                chkbox1.Checked = true;
            }
            SetAsyncPostBackForControl();
        }
        private void SetAsyncPostBackForControl()
        {
            //ToolkitScriptManager1.RegisterAsyncPostBackControl(checkBox.Parent.FindControl("chk1"));
            //ToolkitScriptManager1.RegisterAsyncPostBackControl(checkBox.Parent.FindControl("chk2"));
            //ToolkitScriptManager1.RegisterAsyncPostBackControl(checkBox.Parent.FindControl("chk3"));
            //ToolkitScriptManager1.RegisterAsyncPostBackControl(checkBox.Parent.FindControl("chk4"));

            try
            {
                foreach (RepeaterItem repeated in rptUserRights.Items)
                {
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk1"));
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk2"));
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk3"));
                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated.FindControl("chk4"));

                    //BindCheckBox(repeated, dataTable);
                    #region RepeaterFirstChild
                    var rpt1 =
                                    (Repeater)repeated.FindControl("rptUserRightschild");
                    if (rpt1 != null)
                    {
                        foreach (RepeaterItem repeated1 in rpt1.Items)
                        {
                            ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated1.FindControl("chk1"));
                            ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated1.FindControl("chk2"));
                            ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated1.FindControl("chk3"));
                            ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated1.FindControl("chk4"));
                            //BindCheckBox(repeated1, dataTable);
                            #region RepeaterSecondChild
                            var rpt2 =
                                            (Repeater)repeated1.FindControl("rptUserRightschild1");
                            if (rpt2 != null)
                            {
                                foreach (RepeaterItem repeated2 in rpt2.Items)
                                {
                                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated2.FindControl("chk1"));
                                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated2.FindControl("chk2"));
                                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated2.FindControl("chk3"));
                                    ToolkitScriptManager1.RegisterAsyncPostBackControl(repeated2.FindControl("chk4"));
                                    //BindCheckBox(repeated2, dataTable);
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR(ex.Message.ToString(),ex.StackTrace.ToString(),this);
            }
        }
        private void SetCheckBoxState(object sender, string control)
        {
            var checkBox = (CheckBox)sender;
            var chkbox1 = (CheckBox)checkBox.Parent.FindControl(control);
            chkbox1.Checked = checkBox.Checked;
            //SetAsyncPostBackForControl(checkBox);
        }
        protected void chk3_OnCheckedChanged(object sender, EventArgs e)
        {
            //SetCheckBoxState(sender, "chk1");
            var chkbox1 = (CheckBox)((CheckBox)sender).Parent.FindControl("chk1");
            if (((CheckBox)sender).Checked)
            {
                chkbox1.Checked = true;
            }
            SetAsyncPostBackForControl();
        }
        protected void chk4_OnCheckedChanged(object sender, EventArgs e)
        {
            //SetCheckBoxState(sender, "chk1");
            var chkbox1 = (CheckBox)((CheckBox)sender).Parent.FindControl("chk1");
            if (((CheckBox)sender).Checked)
            {
                chkbox1.Checked = true;
            }
            SetAsyncPostBackForControl();
        }
        protected void chk21_OnCheckedChanged(object sender, EventArgs e)
        {
            //SetCheckBoxState(sender, "chk1");
            var chkbox1 = (CheckBox)((CheckBox)sender).Parent.FindControl("chk1");
            if (((CheckBox)sender).Checked)
            {
                chkbox1.Checked = true;
            }
            SetAsyncPostBackForControl();
        }
        protected void chk31_OnCheckedChanged(object sender, EventArgs e)
        {
            //SetCheckBoxState(sender, "chk1");
            var chkbox1 = (CheckBox)((CheckBox)sender).Parent.FindControl("chk1");
            if (((CheckBox)sender).Checked)
            {
                chkbox1.Checked = true;
            }
            SetAsyncPostBackForControl();
        }
        protected void chk41_OnCheckedChanged(object sender, EventArgs e)
        {
            //SetCheckBoxState(sender, "chk1");
            var chkbox1 = (CheckBox)((CheckBox)sender).Parent.FindControl("chk1");
            if (((CheckBox)sender).Checked)
            {
                chkbox1.Checked = true;
            }
            SetAsyncPostBackForControl();
        }
        protected void chk22_OnCheckedChanged(object sender, EventArgs e)
        {
            //SetCheckBoxState(sender, "chk1");
            var chkbox1 = (CheckBox)((CheckBox)sender).Parent.FindControl("chk1");
            if (((CheckBox)sender).Checked)
            {
                chkbox1.Checked = true;
            }

            var lblparentid = (Label)((CheckBox)sender).Parent.FindControl("lblparent");
            if (lblparentid != null)
            {
                if (lblparentid.Text.Equals("0"))
                {
                    foreach (RepeaterItem repeated in rptUserRights.Items)
                    {
                        var menuid = (Label)repeated.FindControl("lblmenuid");
                        if (menuid.Text.Equals(lblparentid.Text))
                        {
                            var chk1 = (CheckBox)repeated.FindControl("chk1");
                            if (((CheckBox)sender).Checked)
                            {
                                chk1.Checked = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (RepeaterItem repeated in rptUserRights.Items)
                    {
                        //BindCheckBox(repeated, dataTable);
                        #region RepeaterFirstChild
                        var rpt1 =
                                        (Repeater)repeated.FindControl("rptUserRightschild");
                        if (rpt1 != null)
                        {
                            foreach (RepeaterItem repeated1 in rpt1.Items)
                            {
                                var menuid = (Label)repeated1.FindControl("lblmenuid");
                                if (menuid.Text.Equals(lblparentid.Text))
                                {
                                    var chk1 = (CheckBox)repeated1.FindControl("chk1");
                                    if (((CheckBox)sender).Checked)
                                    {
                                        chk1.Checked = true;
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }


            }
            SetAsyncPostBackForControl();
        }
        protected void chk32_OnCheckedChanged(object sender, EventArgs e)
        {
            //SetCheckBoxState(sender, "chk1");
            var chkbox1 = (CheckBox)((CheckBox)sender).Parent.FindControl("chk1");
            if (((CheckBox)sender).Checked)
            {
                chkbox1.Checked = true;
            }
            var lblparentid = (Label)((CheckBox)sender).Parent.FindControl("lblparent");
            if (lblparentid != null)
            {
                if (lblparentid.Text.Equals("0"))
                {
                    foreach (RepeaterItem repeated in rptUserRights.Items)
                    {
                        var menuid = (Label)repeated.FindControl("lblmenuid");
                        if (menuid.Text.Equals(lblparentid.Text))
                        {
                            var chk1 = (CheckBox)repeated.FindControl("chk1");
                            if (((CheckBox)sender).Checked)
                            {
                                chk1.Checked = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (RepeaterItem repeated in rptUserRights.Items)
                    {
                        //BindCheckBox(repeated, dataTable);
                        #region RepeaterFirstChild
                        var rpt1 =
                                        (Repeater)repeated.FindControl("rptUserRightschild");
                        if (rpt1 != null)
                        {
                            foreach (RepeaterItem repeated1 in rpt1.Items)
                            {
                                var menuid = (Label)repeated1.FindControl("lblmenuid");
                                if (menuid.Text.Equals(lblparentid.Text))
                                {
                                    var chk1 = (CheckBox)repeated1.FindControl("chk1");
                                    if (((CheckBox)sender).Checked)
                                    {
                                        chk1.Checked = true;
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }


            }
            SetAsyncPostBackForControl();
        }
        protected void chk42_OnCheckedChanged(object sender, EventArgs e)
        {
            //SetCheckBoxState(sender, "chk1");
            var chkbox1 = (CheckBox)((CheckBox)sender).Parent.FindControl("chk1");
            if (((CheckBox)sender).Checked)
            {
                chkbox1.Checked = true;
            }
            var lblparentid = (Label)((CheckBox)sender).Parent.FindControl("lblparent");
            if (lblparentid != null)
            {
                if (lblparentid.Text.Equals("0"))
                {
                    foreach (RepeaterItem repeated in rptUserRights.Items)
                    {
                        var menuid = (Label)repeated.FindControl("lblmenuid");
                        if (menuid.Text.Equals(lblparentid.Text))
                        {
                            var chk1 = (CheckBox)repeated.FindControl("chk1");
                            if (((CheckBox)sender).Checked)
                            {
                                chk1.Checked = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (RepeaterItem repeated in rptUserRights.Items)
                    {
                        //BindCheckBox(repeated, dataTable);
                        #region RepeaterFirstChild
                        var rpt1 =
                                        (Repeater)repeated.FindControl("rptUserRightschild");
                        if (rpt1 != null)
                        {
                            foreach (RepeaterItem repeated1 in rpt1.Items)
                            {
                                var menuid = (Label)repeated1.FindControl("lblmenuid");
                                if (menuid.Text.Equals(lblparentid.Text))
                                {
                                    var chk1 = (CheckBox)repeated1.FindControl("chk1");
                                    if (((CheckBox)sender).Checked)
                                    {
                                        chk1.Checked = true;
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }


            }
            SetAsyncPostBackForControl();
        }
        protected void chk1_OnCheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var chkbox2 = (CheckBox)checkBox.Parent.FindControl("chk2");
            if (!checkBox.Checked)
                chkbox2.Checked = false;

            var chkbox3 = (CheckBox)checkBox.Parent.FindControl("chk3");
            if (!checkBox.Checked)
                chkbox3.Checked = false;

            var chkbox4 = (CheckBox)checkBox.Parent.FindControl("chk4");
            if (!checkBox.Checked)
                chkbox4.Checked = false;


            SetAsyncPostBackForControl();

        }
        protected void chk11_OnCheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var chkbox1 = (CheckBox)checkBox.Parent.FindControl("chk2");
            if (!checkBox.Checked)
                chkbox1.Checked = false;

            var chkbox2 = (CheckBox)checkBox.Parent.FindControl("chk3");
            if (!checkBox.Checked)
                chkbox2.Checked = false;

            var chkbox3 = (CheckBox)checkBox.Parent.FindControl("chk4");
            if (!checkBox.Checked)
                chkbox3.Checked = false;


            SetAsyncPostBackForControl();
        }
        protected void chk12_OnCheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var chkbox2 = (CheckBox)checkBox.Parent.FindControl("chk2");
            if (!checkBox.Checked)
                chkbox2.Checked = false;

            var chkbox3 = (CheckBox)checkBox.Parent.FindControl("chk3");
            if (!checkBox.Checked)
                chkbox3.Checked = false;

            var chkbox4 = (CheckBox)checkBox.Parent.FindControl("chk4");
            if (!checkBox.Checked)
                chkbox4.Checked = false;

            var lblparentid = (Label)checkBox.Parent.FindControl("lblparent");
            if (lblparentid != null)
            {
                if (lblparentid.Text.Equals("0"))
                {
                    foreach (RepeaterItem repeated in rptUserRights.Items)
                    {
                        var menuid = (Label)repeated.FindControl("lblmenuid");
                        if (menuid.Text.Equals(lblparentid.Text))
                        {
                            var chk1 = (CheckBox)repeated.FindControl("chk1");
                            if (checkBox.Checked)
                            {
                                chk1.Checked = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (RepeaterItem repeated in rptUserRights.Items)
                    {
                        //BindCheckBox(repeated, dataTable);
                        #region RepeaterFirstChild
                        var rpt1 =
                                        (Repeater)repeated.FindControl("rptUserRightschild");
                        if (rpt1 != null)
                        {
                            foreach (RepeaterItem repeated1 in rpt1.Items)
                            {
                                var menuid = (Label)repeated1.FindControl("lblmenuid");
                                if (menuid.Text.Equals(lblparentid.Text))
                                {
                                    var chk1 = (CheckBox)repeated1.FindControl("chk1");
                                    if (checkBox.Checked)
                                    {
                                        chk1.Checked = true;
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }


            }



            SetAsyncPostBackForControl();
        }
    }
}