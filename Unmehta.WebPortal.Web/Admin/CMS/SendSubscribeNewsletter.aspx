<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="SendSubscribeNewsletter.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.CMS.SendSubscribe_Newsletter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Send Subscribe Newsletter </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div"  class="alert fade in col-md-12 ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        }

        function Checkall(Checkbox) {
            var GridView1 = document.getElementById("<%=gView.ClientID %>");
            for (i = 1; i < GridView1.rows.length; i++) {
                GridView1.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }

        function Validate(sender, args) {
            var gridView = document.getElementById("<%=gView.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");

            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>
    <style type="text/css">
        .register-box-body {
            background-color: #ecf0f5 !important;
        }

        .content-header > h1 {
            margin: 0;
            font-size: 30px !important;
            text-align: center !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Send Subscribe Newsletter</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Admin</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">SendSubscribeNewsletter</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="content-wrapper">

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
                    <!-- Bootstrap alert -->
                    <div class="row">
                        <div class="col-md-12" id="tblSearch" style="margin-bottom: 10px;">
                            <div class="form-group">
                                <div class="row">
                                    <%-- <div class="col-md-3 controls">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-search"></i></span>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search By Application Id"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 controls">
                                        <div class="form-group">
                                            <button runat="server" id="btn_Search" class="btn btn-primary" title="Search" causesvalidation="false" onserverclick="btn_Search_ServerClick">
                                                <i class="fa fa-search">&nbsp;Search</i>
                                            </button>
                                            <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" causesvalidation="false" onserverclick="btn_SearchCancel_ServerClick">
                                                <i class="fa fa-remove">&nbsp;Cancel</i>
                                            </button>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-6 controls">
                                        <div class="form-group">
                                            <label for="exampleInputFile">Select Document Name<span class="req-field">*</span></label>
                                            <asp:DropDownList ID="ddlDocument" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="txtAcademicsName">Mail Subject<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtsubject" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUserName" CssClass="validationmsg" runat="server" ControlToValidate="txtsubject" ValidationGroup="main"
                                                ErrorMessage="Enter Mail Subject." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="txtAcademicDesc">Mail Body Description<span class="req-field">*</span></label>
                                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control"></asp:TextBox>
                                            <script type="text/javascript">
                                                CKEDITOR.dtd.$removeEmpty['i'] = false;
                                                var editor = CKEDITOR.replace('<%=txtDescription.ClientID%>', {
                                                    extraPlugins: 'tableresize'
                                                });
                                            </script>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group" style="overflow-x: scroll;">
                                    <%--<asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>--%>
                                    <button runat="server" id="btnExportToExcel" class="btn btn-primary" onserverclick="btnExportToExcel_ServerClick" title="Export To Excel" causesvalidation="false">
                                        <i class="fa fa-file-excel-o">&nbsp;Export To Excel</i>
                                    </button>
                                    <asp:Button ID="btnSendCallLetter" runat="server" CssClass="btn btn-primary right" OnClick="btnSendCallLetter_Click" Text="Send News Letter" Style="float: right;" />
                                    <%--<asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary right" OnClick="btnExport_Click" Text="Export" Visible="false" Style="float: right;" />--%><hr />
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please select at least one record."
                                        ClientValidationFunction="Validate" ForeColor="Red"></asp:CustomValidator>
                                    <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                        DataSourceID="sqlds" AllowPaging="false" AllowSorting="true" PageSize="10" OnPageIndexChanging="gView_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select All">
                                                <HeaderTemplate>
                                                    <%--<label>Select All</label>--%>
                                                    <asp:CheckBox ID="chkSelectAll" AutoPostBack="true" ToolTip="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id" HeaderText="Id" />
                                            <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                            <asp:BoundField DataField="EmailId" HeaderText="Email" />
                                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No." />
                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.dtpicker').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });

            $('.timepicker').timepicker({
                defaultTime: '',
                minuteStep: 1,
                disableFocus: true,
                template: 'dropdown',
                showMeridian: false
            })
        });

        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
