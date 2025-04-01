<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/LTEStudent.Master" AutoEventWireup="true" CodeBehind="ApplicationList.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Admission.ApplicationList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
      <div class="card">
        <div class="card-body">

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." OnRowDataBound="gView_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Application Form">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="lnkPriview" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip" OnClick="lnkPriview_Click"><i class="fa fa-print"></i></asp:LinkButton>
                                            <%--<asp:LinkButton ID="ibtn_Edit" CommandName="eEdit" runat="server" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                            <%--<asp:LinkButton ID="lnkDownload" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip" OnClick="lnkDownload_Click"><i class="fa fa-download"></i></asp:LinkButton>--%>
                                            <%--<asp:LinkButton ID="lnkUnlockProfile" CommandName="eView" runat="server" data-original-title="View" OnClientClick='<%# Eval("RegisrationId", "return confirm(\"Are you sure want to Unlock : {0} ? \")") %>' CssClass="btn btn-sm show-tooltip" OnClick="lnkUnlockProfile_Click"><i class="fa fa-key"></i></asp:LinkButton>--%>
                                            <asp:LinkButton ID="ibtn_Payment" CommandName="eDelete" OnClientClick='return confirm(\"Are you sure want to Payment  ? \")' OnClick="ibtn_Payment_Click" runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip" Text="Payment"></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                              <%--  <asp:TemplateField HeaderText="RegistrationId">
                                    <ItemTemplate>--%>
                                        <%--<asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server"  data-original-title="View" CssClass="btn btn-sm show-tooltip" OnClick="ibtn_View_Click" OnClientClick="SetTarget();"><i class="fa fa-search-plus fa-lg"></i><%# Eval("RegistrationId") %></asp:LinkButton>--%>
                                        <%--<asp:LinkButton ID="ibtn_View" style="cursor: auto;" CommandName="eView" runat="server"  data-original-title="View" CssClass="btn btn-sm show-tooltip"><%# Eval("RegistrationId") %></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                             <asp:BoundField DataField="RegistrationId" HeaderText="Registration Id" />
                                <asp:BoundField DataField="FirstName" HeaderText="Full Name" />
                                <%--<asp:BoundField DataField="MiddleName" HeaderText="Middle Name" />
                                <asp:BoundField DataField="LastName" HeaderText="Last Name" />--%>
                                <asp:BoundField DataField="CourseName" HeaderText="CourseName" />
                                <asp:BoundField DataField="ApplicationStatus" HeaderText="ApplicationStatus" />
                                <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" />
                                <%--<asp:BoundField DataField="PhotographName" HeaderText="Photograph Name" SortExpression="Doc_Name" />--%>
                                <asp:TemplateField HeaderText="Photograph Name">
                                    <ItemTemplate>
                                        <a id="afilePhotographPath" href='<%# Eval("PhotographPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"><%# Eval("PhotographName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="SignatureName" HeaderText="Signature Name" SortExpression="Doc_Name" />--%>
                                <asp:TemplateField HeaderText="Signature Name">
                                    <ItemTemplate>
                                        <a id="afileSignaturePath" href='<%# Eval("SignaturePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"><%# Eval("SignatureName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
</asp:Content>
