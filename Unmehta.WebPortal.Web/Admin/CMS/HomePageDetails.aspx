<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="HomePageDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.HomePageDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Home Page</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Home Page Details</li>
                </ol>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/ckeditor/ckeditor.js") %>"></script>
    <%--Ref : Scheme , GovBodyMaster--%>
    <asp:Panel ID="pnlEntry" runat="server">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Language<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlLanguage" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged1" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Left Video Title<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtLeftVideoTitle" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLeftVideoTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtLeftVideoTitle"
                                ErrorMessage="Enter Left Video Title" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Left Video Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlvideouploadleft" AutoPostBack="true" OnSelectedIndexChanged="ddlvideoupload_SelectedIndexChanged" CssClass="form-control" TabIndex="6" runat="server" Style="width: 100%">
                                <asp:ListItem Value="True" Selected="True" Text="Internal Link"></asp:ListItem>
                                <asp:ListItem Value="False" Text="External Link"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div id="internalvideoLeft" runat="server">
                            <div class="form-group">
                                <label for="exampleInputFile">Left Select Video<span class="req-field">*</span></label>
                                <asp:FileUpload ID="fuDocUploadLeft" runat="server" TabIndex="7" CssClass="form-control" />
                                <asp:Label ID="lblLeftVideo" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hfLeftVideo" runat="server" />
                                <a onclick="return RemoveImage('bodyPart_lblLeftVideo','bodyPart_aRemoveLeftVideo','bodyPart_hfLeftVideo');" class="fa fa-trash-o btn btn-primary"  id="aRemoveLeftVideo" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                        
                            </div>
                        </div>
                        <div id="externallinkLeft" runat="server" visible="false">
                            <div class="form-group">
                                <label for="exampleInputFile">Left Video URL [Youtube video url]<span class="req-field">*</span></label>
                                <span>Youtube URl Must Be embed</span>
                                <asp:TextBox ID="txtLeftVideoURL" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLeftVideoURL" CssClass="validationmsg" runat="server" ControlToValidate="txtLeftVideoURL"
                                    ErrorMessage="Enter Left Video URL" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Select Left thumbnail<span class="req-field">*</span></label>
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuLeftThumbNail" runat="server" CssClass="form-control" />
                            <p class="help-block">( Image should be 1200px*800px )</p>
                            <asp:Label ID="lblLeftImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfLeftImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblLeftImage','bodyPart_aRemoveLeft','bodyPart_hfLeftImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveLeft" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Left Video ReadMore<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlLeftVideoReadMore" CssClass="form-control" AutoPostBack="false" runat="server">
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="rfvLeftVideoReadMore" CssClass="validationmsg" runat="server" InitialValue="0" ControlToValidate="ddlLeftVideoReadMore"
                                ErrorMessage="Select Left Video ReadMore" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="exampleInputFile">Right Video Title<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtRightVideoTitle" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRightVideoTitle" CssClass="validationmsg" runat="server" ControlToValidate="txtRightVideoTitle"
                                ErrorMessage="Enter Right Video Title" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="exampleInputFile">Right Video Status<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlvideouploadRight" AutoPostBack="true" OnSelectedIndexChanged="ddlvideouploadRight_SelectedIndexChanged" CssClass="form-control" TabIndex="6" runat="server" Style="width: 100%">
                                <asp:ListItem Value="True" Selected="True" Text="Internal Link"></asp:ListItem>
                                <asp:ListItem Value="False" Text="External Link"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div id="internalvideoRight" runat="server">
                            <div class="form-group">
                                <label for="exampleInputFile">Right Select Video<span class="req-field">*</span></label>
                                <asp:FileUpload ID="fuDocUploadRight" runat="server" TabIndex="7" CssClass="form-control" />
                                <asp:Label ID="lblRightVideo" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hfRightVideo" runat="server" />
                                <a onclick="return RemoveImage('bodyPart_lblRightVideo','bodyPart_aRemoveRightVideo','bodyPart_hfRightVideo');" class="fa fa-trash-o btn btn-primary"  id="aRemoveRightVideo" runat="server" style="margin-Right: 5px; cursor:pointer;">Remove</a>
                            </div>
                        </div>
                        <div id="externallinkRight" runat="server" visible="false">
                            <div class="form-group">
                                <label for="exampleInputFile">Right Video URL [Youtube video url]<span class="req-field">*</span></label>
                                <span>Youtube URl Must Be embed</span>
                                <asp:TextBox ID="txtRightVideoURL" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvRightVideoURL" CssClass="validationmsg" runat="server" ControlToValidate="txtRightVideoURL"
                                    ErrorMessage="Enter Right Video URL" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Select Right thumbnail<span class="req-field">*</span></label>
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuRightThumbNail" runat="server" CssClass="form-control" />
                            <p class="help-block">( Image should be 1200px*800px )</p>
                            <asp:Label ID="lblRightImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfRightImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblRightImage','bodyPart_aRemoveRight','bodyPart_hfRightImage');" id="aRemoveRight" class="fa fa-trash-o btn btn-primary"  runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Right Video ReadMore<span class="req-field">*</span></label>
                            <asp:DropDownList ID="ddlRightVideoReadMore" CssClass="form-control" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRightVideoReadMore" CssClass="validationmsg" runat="server" ControlToValidate="ddlRightVideoReadMore"
                                ErrorMessage="select Right Video ReadMore" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Select Image For OPD<span class="req-field">*</span></label>                            
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuOpdImage" runat="server" CssClass="form-control" />

                            <asp:Label ID="lblOpdImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfOpdImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblOpdImage','bodyPart_aRemoveOpdImage','bodyPart_hfOpdImage');" id="aRemoveOpdImage" class="fa fa-trash-o btn btn-primary"  runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                       
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">OPD Day<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtOpdDay" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvOpdDay" CssClass="validationmsg" runat="server" ControlToValidate="txtOpdDay"
                                ErrorMessage="Enter OPD Day" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Select Image For IPD<span class="req-field">*</span></label>                            
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuIPDImage" runat="server" CssClass="form-control" />
                            
                            <asp:Label ID="lblIPDImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfIPDImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblIPDImage','bodyPart_aRemoveIPDImage','bodyPart_hfIPDImage');" id="aRemoveIPDImage" class="fa fa-trash-o btn btn-primary"  runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                       

                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">IPD Day<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtIpdDay" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvIpdDay" CssClass="validationmsg" runat="server" ControlToValidate="txtIpdDay"
                                ErrorMessage="Enter IPD Day" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Select Image For Surgery<span class="req-field">*</span></label>                            
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuSurgeryImage" runat="server" CssClass="form-control" />
                            
                            <asp:Label ID="lblSurgeryImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfSurgeryImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblSurgeryImage','bodyPart_aRemoveSurgeryImage','bodyPart_hfSurgeryImage');" id="aRemoveSurgeryImage" class="fa fa-trash-o btn btn-primary"  runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Surgery Day<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtSurgeryDay" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSurgeryDay" CssClass="validationmsg" runat="server" ControlToValidate="txtSurgeryDay"
                                ErrorMessage="Enter Surgery Day" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Select Image For Procedures<span class="req-field">*</span></label>                            
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuProceduresImage" runat="server" CssClass="form-control" />
                            <asp:Label ID="lblProcedureImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfProcedureImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblProcedureImage','bodyPart_aRemoveProcedureImage','bodyPart_hfProcedureImage');" id="aRemoveProcedureImage" class="fa fa-trash-o btn btn-primary"  runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Procedures Day<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtProceduresDay" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvProceduresDay" CssClass="validationmsg" runat="server" ControlToValidate="txtProceduresDay"
                                ErrorMessage="Enter Procedures Day" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Select Image For Investigation<span class="req-field">*</span></label>                            
                            <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="fuInvestigationImage" runat="server" CssClass="form-control" />
                               <asp:Label ID="lblInvestigationImage" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfInvestigationImage" runat="server" />
                            <a onclick="return RemoveImage('bodyPart_lblInvestigationImage','bodyPart_aRemoveInvestigationImage','bodyPart_hfInvestigationImage');" id="aRemoveInvestigationImage" class="fa fa-trash-o btn btn-primary"  runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputFile">Investigations Day<span class="req-field">*</span></label>
                            <asp:TextBox ID="txtInvestigationsDay" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvestigationsDay" CssClass="validationmsg" runat="server" ControlToValidate="txtInvestigationsDay"
                                ErrorMessage="Enter Investigation sDay" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <label for="txtCastName">&nbsp;</label>
                        <% if (SessionWrapper.UserPageDetails.CanAdd)
                            { %>
                        <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-primary " Text="Save" OnClick="btn_Save_Click" />
                        <%}%>
                        <button runat="server" id="btn_Cancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_Cancel_ServerClick" causesvalidation="false">
                            <i class="fa fa-remove">&nbsp;Cancel</i>
                        </button>
                        <asp:HiddenField ID="hdnHome_ID" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
