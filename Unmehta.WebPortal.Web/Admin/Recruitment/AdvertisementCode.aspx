<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AdvertisementCode.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Recruitment.AdvertisementCode" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Advertisement Code</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Advertisement Code</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Recruitment</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Advertisement Code</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="">
                    <asp:HiddenField ID="hfRowId" runat="server" />
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtAgeTo">Post Code</label>
                        <asp:TextBox ID="txtPostCode" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter Post Code" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtStartDate">Start Date</label>
                        <div class="row">
                            <asp:TextBox ID="txtStartDate" aria-describedby="emailHelp" CssClass="form-control col-md-6" placeholder="Enter Start Date" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtStartTime" aria-describedby="emailHelp" CssClass="form-control col-md-6 clockpicker-demo" placeholder="Enter Start Time" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtEndDate">End Date</label>
                        <div class="row">
                            <asp:TextBox ID="txtEndDate" aria-describedby="emailHelp" CssClass="form-control col-md-6" placeholder="Enter End Date" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtEndTime" aria-describedby="emailHelp" CssClass="form-control col-md-6 clockpicker-demo" placeholder="Enter Start Time" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtpublishdate">Publish Date</label>
                        <div class="row">
                            <asp:TextBox ID="txtpublishdate" aria-describedby="emailHelp" CssClass="form-control col-md-6" placeholder="Enter Publish Date" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="fuAboutAdvertisement">General Instruction</label>
                        <asp:HiddenField ID="hfFilName" runat="server" />
                        <asp:FileUpload accept=".pdf,.doc,.docx" ID="fuAboutAdvertisement" runat="server" />
                    </div>
                </div>
                <div class="col-md-3">
                    <label for="exampleInputFile">Add NewIcon?<span class="req-field">*</span></label>
                    <div class="input-group">
                        <asp:RadioButton ID="rbtnYES" Checked="true" GroupName="IdocNew" runat="server" Text="Yes" ValidationGroup="tender" />
                        &nbsp;&nbsp;
                                                        <asp:RadioButton ID="rbtnNO" runat="server" GroupName="IdocNew" Text="No" ValidationGroup="tender" />
                    </div>
                    <span style="visibility: hidden;">&nbsp;</span>
                </div>
                <div class="col-md-4">
                    <div class="form-group form-check">
                        <br />
                        <br />
                        <asp:CheckBox ID="chkEnable" runat="server" />
                        <label class="form-check-label" for="chkEnable">Active</label>
                    </div>
                </div>
                
                <div class="col-md-12">
                    <div class="form-group">

                        <label for="txtPostDesc">General Description</label>
                        <textarea id="txtPostDesc" runat="server" name="editor1">This is sample text</textarea>
                        
                        <script type="text/javascript">
                            CKEDITOR.dtd.$removeEmpty['i'] = false;
                            var editor = CKEDITOR.replace('<%=txtPostDesc.ClientID%>', {
                                extraPlugins: 'tableresize'
                            });
                        </script>

                        <%--<asp:TextBox ID="txtPostDesc" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter advertisement description" runat="server"></asp:TextBox>--%>
                    </div>

                </div>
            </div>

            <div class="row">
                <div class="col-md-3">
                    <label>&nbsp;</label>
                    <label>&nbsp;</label>
                    <div class="form-group">
                        <% if (SessionWrapper.UserPageDetails.CanAdd)
                            { %>
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <%} %>
                        <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-12" id="gvInnerGridView" runat="server">
                    <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." AllowSorting="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1  %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AdvertisementCode" HeaderText="Code" SortExpression="AdvertisementCode" />
                        <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="PublishDate" HeaderText="PublishDate" SortExpression="PublishDate" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="IsActive" HeaderText="Visable" SortExpression="IsActive" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <div class="btn-group">
                                    <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                        { %>
                                    <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                    <%}
                                        if (SessionWrapper.UserPageDetails.CanDelete)
                                        { %>
                                    <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
     <script>


        $(document).ready(function () {
            ClosePreloder();
            var fromDate = document.getElementById('bodyPart_txtStartDate');
            var toDate = document.getElementById('bodyPart_txtEndDate');
            //var publishdate = document.getElementById('bodyPart_txtpublishdate');
            var ageLimitCalOn = document.getElementById('bodyPart_txtAgeLimitCalOn');
            var interviewdate = document.getElementById('bodyPart_txtInterviewdate')
            $(ageLimitCalOn).datepicker({
                singleDatePicker: true,
                showDropdowns: true,
                dateFormat: 'dd/mm/yy'
            });
            $(publishdate).datepicker({
                singleDatePicker: true,
                showDropdowns: true,
                dateFormat: 'dd/mm/yy'
            });

            $(fromDate).datepicker({
                singleDatePicker: true,
                showDropdowns: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    var dt = new Date(GetDateFromstring(selected));
                    dt.setDate(dt.getDate());
                    $(toDate).datepicker("option", "minDate", dt);
                }
            });
            $(interviewdate).datepicker({
                singleDatePicker: true,
                showDropdowns: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    var dt = new Date(GetDateFromstring(selected));
                    dt.setDate(dt.getDate());
                    $(toDate).datepicker("option", "minDate", dt);
                }
            });
            $(toDate).datepicker({
                singleDatePicker: true,
                showDropdowns: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    var dt = new Date(GetDateFromstring(selected));
                    dt.setDate(dt.getDate());
                    $(fromDate).datepicker("option", "maxDate", dt);
                }
            });
        });


        function GetDateFromstring(strDate) {
            var parts = strDate.split("/");
            var dt = new Date(parseInt(parts[2], 10),
                              parseInt(parts[1], 10) - 1,
                              parseInt(parts[0], 10));
            return dt;
        }


    </script>
    <script src="<%=ResolveUrl("~/Admin/Script/Recruitment/AdvertisementCode.js?"+DateTime.Now.ToString("hhmmssttff")) %>"></script>
</asp:Content>
