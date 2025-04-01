<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="StudentMasterData.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Student.StudentMasterData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Master Data</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Student Master Data</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Student</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Student Master Data</li>
                </ol>
            </nav>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $(<%=lstApplicationStatus.ClientID%>).SumoSelect();
        });
        $(document).ready(function () {
            $(<%=lstCourceList.ClientID%>).SumoSelect();
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <section class="content">
        <div class="card" style="display: none">
            <div class="card-body">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="input-group">
                                    <button runat="server" id="btn_Exceldata" class="btn btn-primary" onserverclick="btn_Exceldata_ServerClick" title="Generate Excel File">
                                        <i class="fa fa-plus-square">&nbsp;Generate Excel File</i>
                                    </button>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
        <div class="card">
            <div class="card-body">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputFile">Course</label>
                                    <%--<asp:DropDownList ID="ddlCourceList" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                                    <asp:ListBox ID="lstCourceList" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputFile">Application Status</label>
                                    <%--<asp:DropDownList ID="ddlapplication" CssClass="form-control select2" runat="server">
                                    </asp:DropDownList>--%>
                                    <asp:ListBox ID="lstApplicationStatus" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>

                            <%-- <div class="col-md-3">
                                <div class="form-group">
                                    <label for="exampleInputFile">Payment Status<span class="req-field">*</span></label>
                                    <asp:DropDownList ID="ddlpayment" CssClass="form-control"  runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>--%>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="txtStartDate">Start Date</label>
                                    <asp:TextBox ID="txtStartDate" aria-describedby="emailHelp" CssClass="form-control datepicker-demo" placeholder="Enter start date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="txtEndDate">End Date</label>
                                    <asp:TextBox ID="txtEndDate" aria-describedby="emailHelp" CssClass="form-control datepicker-demo" placeholder="Enter end date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="txtPaymentStatus">Payment Status</label>
                                    <asp:TextBox ID="txtPaymentStatus" CssClass="form-control" placeholder="Enter Payment Status" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="txtStartDate">Transaction Start Date</label>
                                    <asp:TextBox ID="txtTransactionStartDate" aria-describedby="emailHelp" CssClass="form-control datepicker-demo" placeholder="Enter Transaction start date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="txtEndDate">Transaction End Date</label>
                                    <asp:TextBox ID="txtTransactionEndDate" aria-describedby="emailHelp" CssClass="form-control datepicker-demo" placeholder="Enter Transaction end date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <button runat="server" id="btn_Search" class="btn btn-primary" onserverclick="btn_Search_ServerClick" title="Search">
                                        <i class="fa fa-search">&nbsp;Search</i>
                                    </button>
                                    <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel">
                                        <i class="fa fa-remove">&nbsp;Cancel</i>
                                    </button>
                                </div>
                            </div>
                            <div class="col-md-12">
                               
                                        <div class="form-group" style="overflow-x: scroll;">
                                            <asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                            <%--<asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary right" OnClick="btnExport_Click" Text="Export" Style="float: right;" />--%><hr />
                                            <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,StudentId,CourseId,FirstName,RegistrationId,PresentPhoneM" OnRowDataBound="gView_RowDataBound" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                                                <Columns>

                                                    <asp:BoundField DataField="SRNO" HeaderText="Sr.No" />
                                                    <asp:TemplateField HeaderText="Download">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSendPaymentSMS" CommandName="eView" runat="server" data-original-title="View" CssClass="mr-2 btn submit-btn print_btn" OnClick="lnkSendPaymentSMS_Click">Send SMS</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkSendCorrectionSMS" CommandName="eView" runat="server" data-original-title="View" CssClass="mr-2 btn submit-btn print_btn" OnClick="lnkSendCorrectionSMS_Click">Send SMS</asp:LinkButton>
                                                            <asp:LinkButton ID="ibtn_Download" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip" OnClick="ibtn_Download_Click" OnClientClick="SetTarget();"><i class="fa fa-download fa-lg"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkPriview" CommandName="eView" runat="server" data-original-title="View" CssClass="mr-2 btn submit-btn print_btn" OnClick="lnkPriview_Click" Text="Print"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:BoundField DataField="ApplicationStatus" HeaderText="Application&nbsp;Status" />
                                                    <asp:BoundField DataField="PaymentStatus" HeaderText="Payment&nbsp;Status" />
                                                    <asp:BoundField DataField="CourseName" HeaderText="CourseName" />
                                                    <asp:BoundField DataField="RegistrationId" HeaderText="Registration&nbsp;Id" />
                                                    <asp:BoundField DataField="FirstName" HeaderText="Full&nbsp;Name" />

                                                    <asp:BoundField DataField="PresentFullAddress" HeaderText="Present&nbsp;Address" />
                                                    <asp:BoundField DataField="PresentPhoneM" HeaderText="Phone Number (M)" />
                                                    <asp:BoundField DataField="PresentPhoneR" HeaderText="Phone Number (R)" />

                                                    <asp:BoundField DataField="PermentFullAddress" HeaderText="Permanent Address" />
                                                    <asp:BoundField DataField="ParmenentPhoneM" HeaderText="Phone Number (M)" />
                                                    <asp:BoundField DataField="ParmenentPhoneR" HeaderText="Phone Number (R)" />

                                                    <asp:BoundField DataField="MaritalStatus" HeaderText="Marital Status" />
                                                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                                    <asp:BoundField DataField="DateOfBirth" HeaderText="Date&nbsp;Of&nbsp;Birth" ItemStyle-Wrap="false" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="CastName" HeaderText="Caste Name" />
                                                    <asp:BoundField DataField="ReligionName" HeaderText="Religion Name" />
                                                    <asp:BoundField DataField="PlaceOfBirth" HeaderText="PlaceOfBirth" />


                                                    <%--<asp:TemplateField HeaderText="FULLNAME" SortExpression="FirstName">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("FirstName")+ " " + Eval("Mobile")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                 
                            </div>
                            <div class="col-md-12">
                                <div class="input-group">
                                    <button runat="server" id="btnexport" class="btn btn-primary" visible="false" onserverclick="btnexport_ServerClick" title="Generate Excel File">
                                        <i class="fa fa-plus-square">&nbsp;Generate Excel File</i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
