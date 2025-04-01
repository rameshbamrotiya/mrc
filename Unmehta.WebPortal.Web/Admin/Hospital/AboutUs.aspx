<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>About Us</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>AboutUs</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">AboutUs</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">

    <div class="card" id="divForm" runat="server">
        <div class="card-body">
            <h4>About Us</h4>
            <div class="row">
                <asp:HiddenField ID="hfID" runat="server" />
                <asp:HiddenField ID="hfDesignationId" runat="server" />
                <asp:HiddenField ID="hfAboutUsId" runat="server" />
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddllanguage">Language</label>
                        <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="Profile" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile"
                            ErrorMessage="Enter select language" SetFocusOnError="true" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtHeadingTitle">Heading Title</label>
                        <asp:TextBox ID="txtHeadingTitle" runat="server" CssClass="form-control" placeholder="Enter Heading Title"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvHeadingTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtHeadingTitle" ValidationGroup="Profile"
                            ErrorMessage="Enter Heading Title" SetFocusOnError="true" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtRightHeadingTitle">Right Side Heading Title</label>
                        <asp:TextBox ID="txtRightHeadingTitle" runat="server" CssClass="form-control" placeholder="Enter Right Side Heading Title"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRightHeadingTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtRightHeadingTitle" ValidationGroup="Profile"
                            ErrorMessage="Enter Right Side Heading Title" SetFocusOnError="true" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="exampleInputFile">Description</label>
                        <asp:TextBox ID="txtAboutUsDescription" ValidateRequestMode="Disabled" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtAboutUsDescription.ClientID%>', {
                                    extraPlugins: 'tableresize'
                                });
                        </script>
                        <br />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="txtMetaTitle">Meta Title</label>
                        <asp:TextBox ID="txtMetaTitle" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" placeholder="Enter Meta Title"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMetaTitle" ForeColor="Red" runat="server" ControlToValidate="txtMetaTitle" Display="Dynamic"
                            ErrorMessage="Enter Meta Title." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-group">
                        <label for="txtMetaDescription">Meta Description</label>
                        <asp:TextBox ID="txtMetaDescription" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" placeholder="Enter Meta Description"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ControlToValidate="txtMetaDescription" Display="Dynamic"
                            ErrorMessage="Enter Meta Description." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <div>
                            <label>&nbsp;&nbsp;</label>
                        </div>
                        <%
                                if (SessionWrapper.UserPageDetails.CanAdd)
                                {%>
                        <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />
                        <% } %>
                    </div>
                </div>
            </div>
            <hr style="border-color: black !important;" />
            <div class="row" id="dvSubView" runat="server">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtExecutiveName">Executive Name</label>
                        <asp:TextBox ID="txtExecutiveName" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter Executive name" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvExecutiveName" ForeColor="Red" runat="server" ControlToValidate="txtExecutiveName" ValidationGroup="Sub"
                            ErrorMessage="Enter executive name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlDesignation">Designation</label>
                        <asp:DropDownList ID="ddlDesignation" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDesignation" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlDesignation" ValidationGroup="Sub"
                            ErrorMessage="Select designation." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="fuImage">Message</label>
                        <asp:TextBox ID="txtMessage" aria-describedby="emailHelp" TextMode="MultiLine" CssClass="form-control" placeholder="Enter message" ValidationGroup="Sub" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMessage" ForeColor="Red" runat="server" ControlToValidate="txtMessage" ValidationGroup="Sub"
                            ErrorMessage="Enter mesage." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="fuImage">Image Upload</label>
                        <asp:HiddenField ID="hfFilName" runat="server" />
                        <asp:FileUpload CssClass="form-control" ID="fuImage" runat="server" />
                        <asp:Image ID="imgProfile" Height="100px" Width="100px" Visible="false" runat="server" ValidationGroup="Profile" />
                        <p class="help-block">( Image should be 840px*963px )</p>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnSubAdd" CssClass="btn btn-primary " Text="Save" OnClick="btnSubAdd_Click" ValidationGroup="Sub" />
                        <asp:Button runat="server" ID="btnSubClear" CssClass="btn btn-primary " Text="Clear" OnClick="btnSubClear_Click" CausesValidation="false" />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            AllowPaging="true" AllowSorting="false" PageSize="10">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DesignationName" HeaderText="Executive Name" SortExpression="DesignationName" />
                                <asp:BoundField DataField="DesName" HeaderText="Designation Name" SortExpression="DesName" />
                                <asp:BoundField DataField="Message" HeaderText="Message" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>
                                        <a id="afile" href='<%# Eval("PhotoPath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                { %>
                                            <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                            <%}
                                                if (SessionWrapper.UserPageDetails.CanDelete)
                                                { %>
                                            <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                            <%} %>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>

        </div>
    </div>


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
