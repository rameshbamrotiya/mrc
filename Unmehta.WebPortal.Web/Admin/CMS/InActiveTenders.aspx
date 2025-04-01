<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="InActiveTenders.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.InActiveTenders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>In Active Tender</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>InActive Tender</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">InActive Tender</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <section class="content">
        <div class="card">
            <div class="card-body">
                <!-- Bootstrap alert -->
                <div class="row">
                    <div class="form-group col-md-12">
                        <div class="messagealert" id="alert_container">
                        </div>
                    </div>
                </div>
                <!-- END Bootstrap alert -->

                <asp:Panel ID="pnlView" runat="server">
                    <div class="row">
                        <div class="col-md-9" id="tblSearch">
                            <div class="form-group">
                                <div class="controls">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button runat="server" id="btn_Search" class="btn btn-primary" title="Search">
                                                <i class="fa fa-search">&nbsp;Search</i>
                                            </button>
                                        </span>
                                        <span class="input-group-btn">
                                            <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_SearchCancel_Click">
                                                <i class="fa fa-remove">&nbsp;Cancel</i>
                                            </button>
                                        </span>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="pull-right">
                                <div class="input-group">
                                    <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                        { %>
                                    <button runat="server" id="btn_Inactive" class="btn btn-primary" title="Move To Active" onserverclick="btn_Inactive_Click" causesvalidation="false">
                                        Move To Active
                                    </button>
                                    <%} %>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="TenderID" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                    DataSourceID="sqlds" AllowPaging="true" AllowSorting="false" PageSize="10">
                                    <Columns>
                                        <asp:BoundField DataField="RowNo" HeaderText="No." SortExpression="RowNo" />
                                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                        <asp:BoundField DataField="TenderNo" HeaderText="Tender No." SortExpression="TenderNo" />
                                        <asp:BoundField DataField="PBMeetingDate" HeaderText="Pre Bid Meeting" SortExpression="PBMeetingDate" />
                                        <asp:BoundField DataField="LastDate" HeaderText="Last Date" SortExpression="LastDate" />
                                        <asp:BoundField DataField="OpeningDate" HeaderText="Opening Date" SortExpression="OpeningDate" />
                                        <asp:TemplateField HeaderText="Active">
                                            <HeaderTemplate>
                                                <p>In Active</p>
                                                <asp:CheckBox ID="CheckBox2" runat="server" ToolTip="In Active All" onclick="javascript:Selectallcheckbox(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ibtn_check" runat="server" CssClass="checkboxselection" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                  <a href='<%# ResolveUrl( "~/Admin/CMS/TenderDetails?"+Unmehta.WebPortal.Web.Common.Functions.Base64Encode(Eval("TenderID").ToString())) %>' target="_blank" title="view" class="btn btn-sm btn-danger show-tooltip"><i class="fa fa-eye"></i></a>
                                                 </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:UNMehtaConnectionString %>"
                                    SelectCommand="PROC_InActiveGILTender_TenderMaster_Search" SelectCommandType="StoredProcedure" FilterExpression="Title like '%{0}%' ">
                                    <FilterParameters>
                                        <asp:ControlParameter ControlID="txtSearch" Name="Title" />
                                        <asp:ControlParameter ControlID="txtSearch" Name="TenderNo" />
                                        <asp:ControlParameter ControlID="txtSearch" Type="String" Name="PBMeetingDate" />
                                        <asp:ControlParameter ControlID="txtSearch" Type="String" Name="LastDate" />
                                        <asp:ControlParameter ControlID="txtSearch" Type="String" Name="OpeningDate" />
                                    </FilterParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script type="text/javascript">
        function Selectallcheckbox(val) {
            if (!$(this).is(':checked')) {
                $('input:checkbox').prop('checked', val.checked);
            } else {
                $("#chkroot").removeAttr('checked');
            }
        }
    </script>
</asp:Content>
