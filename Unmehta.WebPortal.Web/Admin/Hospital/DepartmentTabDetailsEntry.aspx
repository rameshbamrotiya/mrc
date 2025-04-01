<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DepartmentTabDetailsEntry.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.DepartmentTabDetailsEntry" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Department Tab Entry </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Department Tab Entry</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Department Tab Entry</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <script src="<%= ResolveUrl("~/ckeditor/ckeditor.js") %>"></script>
    <div class="card" id="divForm" runat="server">
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:HiddenField ID="hfID" runat="server" />
                        <label for="ddllanguage">Language</label>
                        <asp:DropDownList ID="ddlLanguage" CssClass="form-control" runat="server" ValidationGroup="Profile" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvLanguage" CssClass="validationmsg" runat="server" ControlToValidate="ddlLanguage" ValidationGroup="Profile"
                            ErrorMessage="Enter select language" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <div>
                            <label>&nbsp;&nbsp;</label>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <div>
                            <label>&nbsp;&nbsp;</label>
                        </div>
                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary " Text="Save All Details" OnClick="btnType5Save_Click1" CausesValidation="false" />
                        <asp:Button runat="server" ID="btnBack" CssClass="btn btn-primary " Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
                    </div>
                </div>

            </div>
        </div>

        <div class="card-body">
            <div class="row">
                <div class="col-md-12">
                    <h6 class="card-title">CkEditor Type 1</h6>
                    <div class="form-group">
                        <label for="txtType1Information">Description<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType1Information" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtType1Information.ClientID%>', {
                                extraPlugins: 'tableresize'
                            });
                        </script>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtType1SquanceNo">Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType1SquanceNo" runat="server" CssClass="form-control" ValidationGroup="CkEditor" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <div class="card-body">
            <div class="row">
                <div class="col-md-12">
                    <h6 class="card-title">Ul Description Pop-up Image With Type 2</h6>
                    <asp:HiddenField ID="hfType2RowNo" runat="server" />
                    <asp:HiddenField ID="hfType2Command" runat="server" value="0"/>
                    <asp:HiddenField ID="hfType2Id" runat="server" value="0" />
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtSquanceNo">Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType2SquanceNo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="txtBlogName">Short Description</label>
                        <asp:TextBox ID="txtType2ShortDescription" runat="server" CssClass="form-control" ValidationGroup="UlDescriptionImageWithPopup" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvType2ShortDescription" CssClass="validationmsg" runat="server" ControlToValidate="txtType2ShortDescription" ValidationGroup="UlDescriptionImageWithPopup"
                            ErrorMessage="Enter Short Description" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                          <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtType2ShortDescription.ClientID%>', {
                                extraPlugins: 'tableresize'
                            });
                        </script>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtType2SubSquanceNo">Detail Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType2SequenceRowNo" runat="server" CssClass="form-control" ValidationGroup="UlDescriptionImageWithPopup" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validationmsg" runat="server" ControlToValidate="txtType2SequenceRowNo" ValidationGroup="UlDescriptionImageWithPopup"
                            ErrorMessage="Enter Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator2" ControlToValidate="txtType2SequenceRowNo" ValidationGroup="UlDescriptionImageWithPopup" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtSquanceNo">Pop-up Image Upload<span class="req-field">*</span></label>
                        <asp:HiddenField ID="hfType2PopUpImage" runat="server" />
                        <asp:Label ID="lblType2PopUpImage" runat="server" Text=""></asp:Label>
                        <a onclick="return RemoveImage('bodyPart_lblType2PopUpImage','bodyPart_aRemoveType2PopUpImage','bodyPart_hfType2PopUpImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveType2PopUpImage" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>                            
                        <asp:FileUpload ID="fuType2PopupImage" runat="server" />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="txtPopupDesc">Pop-up Description<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType2PopupDesc" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtType2PopupDesc.ClientID%>', {
                                extraPlugins: 'tableresize'
                            });
                        </script>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <div>
                            <label>&nbsp;&nbsp;</label>
                        </div>
                        <asp:Button runat="server" ID="btnType2Save" CssClass="btn btn-primary " Text="Save" OnClick="btnType2Save_Click" ValidationGroup="UlDescriptionImageWithPopup" />
                        <asp:Button runat="server" ID="btnType2Clear" CssClass="btn btn-primary " Text="Clear" OnClick="btnType2Clear_Click" CausesValidation="false" />
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:GridView ID="gvType2" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PopupBasicShortDesc" HeaderText="Short Description" SortExpression="PopupBasicShortDesc" />
                                <asp:BoundField DataField="SequanceNo" HeaderText="Sequance No" SortExpression="SequanceNo" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>
                                        <a id="afile" href='<%# Eval("PopupImageName") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="ibtn_Type2Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Type2Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="ibtn_Type2Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Type2Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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

        <div class="card-body">
            <div class="row">

                <div class="col-md-12">
                    <h6 class="card-title">Statistics Type 3</h6>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtType3SequanceNo">Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType3SequanceNo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                 <asp:HiddenField ID="hfType3RowNo" runat="server" />
                 <asp:HiddenField ID="hfType3Command" runat="server" value="0"/>
                 <asp:HiddenField ID="hfType3Id" runat="server" value="0" />
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtType3Information">Statistics<span class="req-field">*</span></label>
                        <asp:DropDownList ID="ddlType3Statistics" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtType3SubSquanceNo">Detail Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType3SubSquanceNo" runat="server" CssClass="form-control" ValidationGroup="UlDescriptionImageWithPopup" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validationmsg" runat="server" ControlToValidate="txtType3SubSquanceNo" ValidationGroup="Statistics"
                            ErrorMessage="Enter Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator3" ControlToValidate="txtType3SubSquanceNo" ValidationGroup="Statistics" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <div class="form-group">
                        <div>
                            <label>&nbsp;&nbsp;</label>
                        </div>
                        <asp:Button runat="server" ID="btnType3Save" CssClass="btn btn-primary " Text="Save" OnClick="btnType3Save_Click" ValidationGroup="Statistics" />
                        <asp:Button runat="server" ID="btnType3Clear" CssClass="btn btn-primary " Text="Clear" OnClick="btnType3Clear_Click" CausesValidation="false" />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:GridView ID="gvType3" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PopupBasicShortDesc" HeaderText="Short Description" SortExpression="PopupBasicShortDesc" />
                                <asp:BoundField DataField="SequanceNo" HeaderText="Sequance No" SortExpression="SequanceNo" />
                               
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="ibtn_Type3Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Type3Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="ibtn_Type3Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Type3Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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

        <div class="card-body">
            <div class="row">

                <div class="col-md-12">
                    <h6 class="card-title">Slider Type 5</h6>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtSquanceNo">Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType5SquanceNo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">

                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtSquanceNo">Image Upload<span class="req-field">*</span></label>
                        <asp:HiddenField ID="hfType5PopUpImage" runat="server" />
                        <asp:Label ID="lblType5PopUpImage" runat="server" Text=""></asp:Label>
                        <a onclick="return RemoveImage('bodyPart_lblType5PopUpImage','bodyPart_aRemoveType5PopUpImage','bodyPart_hfType5PopUpImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveType5PopUpImage" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>                            
                        
                        <asp:FileUpload ID="fuType5PopupImage" runat="server" ValidationGroup="slider" />
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="form-group">
                        <div>
                            <label>&nbsp;&nbsp;</label>
                        </div>
                        <asp:Button runat="server" ID="btnType5Save" CssClass="btn btn-primary " Text="Save" OnClick="btnType5Save_Click" ValidationGroup="slider" />
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:GridView ID="gvType5" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            AllowPaging="true" PageSize="10" OnPageIndexChanging="gvType5_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>
                                        <a id="afile" href='<%# Eval("PopupImageName") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="ibtn_Type5Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Type5Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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

        <div class="card-body">
            <div class="row">

                <div class="col-md-12">
                    <h6 class="card-title">Accordion Type 6</h6>
                    <asp:HiddenField ID="hfType6Id" runat="server"  value="0" />
                    <asp:HiddenField ID="hfType6Command" runat="server" value="0"/>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtSquanceNo">Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType6SquanceNo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtType6AccordionTitle">Accordion Title</label>
                        <asp:TextBox ID="txtType6AccordionTitle" runat="server" CssClass="form-control" ValidationGroup="Accordion"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validationmsg" runat="server" ControlToValidate="txtType6AccordionTitle" ValidationGroup="Accordion"
                            ErrorMessage="Enter Accordion Title" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

                    </div>
                </div>

                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtType2SubSquanceNo">Detail Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType6SequenceRowNo" runat="server" CssClass="form-control" ValidationGroup="UlDescriptionImageWithPopup" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validationmsg" runat="server" ControlToValidate="txtType6SequenceRowNo" ValidationGroup="Accordion"
                            ErrorMessage="Enter Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtType6SequenceRowNo" ValidationGroup="Accordion" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtSquanceNo">Image Upload<span class="req-field">*</span></label>
                        <asp:HiddenField ID="hfType6PopUpImage" runat="server" />
                        <asp:Label ID="lblType6PopUpImage" runat="server" Text=""></asp:Label>
                        <a onclick="return RemoveImage('bodyPart_lblType6PopUpImage','bodyPart_aRemoveType6PopUpImage','bodyPart_hfType6PopUpImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveType6PopUpImage" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>                            
                        <asp:FileUpload ID="fuType6PopupImage" runat="server" />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="txtPopupDesc">Description<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType6Description" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtType6Description.ClientID%>', {
                                extraPlugins: 'tableresize'
                            });
                        </script>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <div>
                            <label>&nbsp;&nbsp;</label>
                        </div>
                        <asp:Button runat="server" ID="btnType6Save" CssClass="btn btn-primary " Text="Save" OnClick="btnType6Save_Click" ValidationGroup="Accordion" />
                        <asp:Button runat="server" ID="btnType6clear" CssClass="btn btn-primary " Text="Clear" OnClick="btnType6Clear_Click" CausesValidation="false" />
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:GridView ID="gvType6" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            AllowPaging="true" PageSize="10" OnPageIndexChanging="gvType6_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SequanceNo" HeaderText="Sequance No" SortExpression="SequanceNo" />
                                <asp:BoundField DataField="PopupBasicShortDesc" HeaderText="Accordion Title" SortExpression="PopupBasicShortDesc" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>
                                        <a id="afile" href='<%# Eval("PopupImageName") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="ibtn_Type6Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Type6Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="ibtn_Type6Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Type6Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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

        <div class="card-body">
            <div class="row">

                <div class="col-md-12">
                    <h6 class="card-title">Image With Pop-up Type 7</h6>
                    <asp:HiddenField ID="hfType7Id" runat="server"  value="0"/>
                    <asp:HiddenField ID="hfType7Command" runat="server" value="0"/>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtSquanceNo">Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType7SquanceNo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtBlogName">Title</label>
                        <asp:TextBox ID="txtType7Title" runat="server" CssClass="form-control" ValidationGroup="ImageWithPopup"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validationmsg" runat="server" ControlToValidate="txtType7Title" ValidationGroup="ImageWithPopup"
                            ErrorMessage="Enter Title" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtBlogName">Short Description</label>
                        <asp:TextBox ID="txtType7ShortDescription" runat="server" CssClass="form-control" ValidationGroup="ImageWithPopup"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator130" CssClass="validationmsg" runat="server" ControlToValidate="txtType7ShortDescription" ValidationGroup="ImageWithPopup"
                            ErrorMessage="Enter Short Description" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtType7SubSquanceNo">Detail Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType7SequenceRowNo" runat="server" CssClass="form-control" ValidationGroup="ImageWithPopup" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator321" CssClass="validationmsg" runat="server" ControlToValidate="txtType7SequenceRowNo" ValidationGroup="ImageWithPopup"
                            ErrorMessage="Enter Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator321" ControlToValidate="txtType7SequenceRowNo" ValidationGroup="ImageWithPopup" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtSquanceNo">Pop-up Image Upload<span class="req-field">*</span></label>
                        <asp:HiddenField ID="hfType7PopUpImage" runat="server" />
                        <asp:Label ID="lblType7PopUpImage" runat="server" Text=""></asp:Label>
                        <a onclick="return RemoveImage('bodyPart_lblType7PopUpImage','bodyPart_aRemoveType7PopUpImage','bodyPart_hfType7PopUpImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveType7PopUpImage" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>                            
                        <asp:FileUpload ID="fuType7PopupImage" runat="server" />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="txtPopupDesc">Pop-up Description<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType7PopupDesc" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtType7PopupDesc.ClientID%>', {
                                extraPlugins: 'tableresize'
                            });
                        </script>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <div>
                            <label>&nbsp;&nbsp;</label>
                        </div>
                        <asp:Button runat="server" ID="btnType7Save" CssClass="btn btn-primary " Text="Save" OnClick="btnType7Save_Click" ValidationGroup="ImageWithPopup" />
                        <asp:Button runat="server" ID="btnType7Clear" CssClass="btn btn-primary " Text="Clear" OnClick="btnType7Clear_Click" CausesValidation="false" />
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:GridView ID="gvType7" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            AllowPaging="true" PageSize="10" OnPageIndexChanging="gvType7_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PopupBasicShortDesc" HeaderText="Short Description" SortExpression="PopupBasicShortDesc" />
                                <asp:BoundField DataField="SequanceNo" HeaderText="Sequance No" SortExpression="SequanceNo" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>
                                        <a id="afile" href='<%# Eval("PopupImageName") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="ibtn_Type7Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Type7Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="ibtn_Type7Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Type7Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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

        <div class="card-body">
            <div class="row">

                <div class="col-md-12">
                    <h6 class="card-title">Image With Description Left Right Type 8</h6>
                    <asp:HiddenField ID="hfType8Command" runat="server" value="0"/>
                    <asp:HiddenField ID="hfType8Id" runat="server" />
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtSquanceNo">Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType8SquanceNo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">

                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtType8SubSquanceNo">Detail Sequence No<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType8SequenceRowNo" runat="server" CssClass="form-control" ValidationGroup="ImageWithDescriptionLeftRight" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" CssClass="validationmsg" runat="server" ControlToValidate="txtType8SequenceRowNo" ValidationGroup="ImageWithDescriptionLeftRight"
                            ErrorMessage="Enter Sequence No" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator21" ControlToValidate="txtType8SequenceRowNo" ValidationGroup="ImageWithDescriptionLeftRight" runat="server" Type="Integer" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" MinimumValue="1" MaximumValue="200" ErrorMessage="Range between 1 to 200 "></asp:RangeValidator>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtSquanceNo">Pop-up Image Upload<span class="req-field">*</span></label>
                        <asp:HiddenField ID="hfType8PopUpImage" runat="server" />
                        <asp:Label ID="lblType8PopUpImage" runat="server" Text=""></asp:Label>
                        <a onclick="return RemoveImage('bodyPart_lblType8PopUpImage','bodyPart_aRemoveType8PopUpImage','bodyPart_hfType8PopUpImage');" class="fa fa-trash-o btn btn-primary"  id="aRemoveType8PopUpImage" runat="server" style="margin-left: 5px; cursor:pointer;">Remove</a>                            
                        <asp:FileUpload ID="fuType8PopupImage" runat="server" />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="txtPopupDesc">Pop-up Description<span class="req-field">*</span></label>
                        <asp:TextBox ID="txtType8PopupDesc" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtType8PopupDesc.ClientID%>', {
                                extraPlugins: 'tableresize'
                            });
                        </script>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <div>
                            <label>&nbsp;&nbsp;</label>
                        </div>
                        <asp:Button runat="server" ID="btnType8Save" CssClass="btn btn-primary " Text="Save" OnClick="btnType8Save_Click" ValidationGroup="ImageWithDescriptionLeftRight" />
                        <asp:Button runat="server" ID="btnType8Clear" CssClass="btn btn-primary " Text="Clear" OnClick="btnType8Clear_Click" CausesValidation="false" />
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:GridView ID="gvType8" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                            AllowPaging="true" PageSize="10" OnPageIndexChanging="gvType8_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SequanceNo" HeaderText="Sequance No" SortExpression="SequanceNo" />
                                <asp:TemplateField HeaderText="View File">
                                    <ItemTemplate>
                                        <a id="afile" href='<%# Eval("PopupImageName") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-picture-o"></a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="ibtn_Type8Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Type8Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="ibtn_Type8Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Type8Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
