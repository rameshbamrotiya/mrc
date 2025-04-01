<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/LTEStudent.Master" AutoEventWireup="true" CodeBehind="Course.aspx.cs" Inherits="Unmehta.WebPortal.Web.Course" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Advertisement Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">    
    <script type="text/javascript">
        function OpenModel() {
            $("#btnOPenModel").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <%--<section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container" style="width: 100%;padding-left: 12%;">
            <h1>Apply Now</h1>
            <ul class="page-breadcrumb" style="width: 23% !important;z-index:0;">
                <li><a href="<%=ResolveUrl("~/") %>">Home</a></li>
                <li>/</li>
                <li>Apply Now</li>
            </ul>

        </div>
    </section>--%>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="">
        <div class="col-md-12 col-lg-12">
            <!-- Basic Information -->
            <div class="card">
                <div class="card-body">
                    <div class="content-details-area pt-50 pb-50">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="section-main-title">
                                    <h2>Course List</h2>
                                    <hr />
                                    <asp:GridView ID="gvCourse" runat="server" AutoGenerateColumns="False" RowStyle-Font-Bold="true" DataKeyNames="Id,IsEnableApply" CssClass="table table-bordered table-hover table-striped table-responsive" OnRowDataBound="gvCourse_RowDataBound"
                                        EmptyDataText="Record does not exist...">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr&nbsp;No" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                                            <asp:TemplateField HeaderText="Apply&nbsp;Course" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <asp:Label ID="lblPaymentStatus" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:LinkButton ID="lnkDetails" OnClick="lnkDetails_Click" runat="server" CssClass="btn mr-2 details_btn" CausesValidation="false" Text="Details"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkApply" runat="server" OnClick="lnkApply_Click" CssClass="btn mr-2 submit-btn apply_btn" Text="Apply&nbsp;Now" CausesValidation="false"></asp:LinkButton>
                                                        <asp:LinkButton ID="ibtn_Payment"  OnClick="ibtn_Payment_Click" runat="server" CssClass="mr-2 btn submit-btn payment_btn" Text="Payment"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkPriview" CommandName="eView" runat="server" data-original-title="View" CssClass="mr-2 btn submit-btn print_btn"  OnClick="lnkPriview_Click" Text="Print" ></asp:LinkButton>
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
            </div>
        </div>
    </div>
    
    <button type="button" id="btnOPenModel" class="btn btn-primary mr-2 mb-2" data-toggle="modal" style="display:none" data-target="#exampleModalsasd" runat="server">Launch demo modal</button>
    <div class="modal" tabindex="-1" role="dialog" id="exampleModalsasd">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:HiddenField ID="hfcourseId" runat="server" />
                    <h5 class="modal-title" id="hTitle" runat="server">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body" id="dvDetails" runat="server">
                    <p>Modal body text goes here.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn close_btn" data-dismiss="modal">Close</button>
                    <asp:LinkButton ID="btnReg" runat="server" class="btn apply_btn" OnClick="btnReg_Click">Apply</asp:LinkButton>
                 <%--   <button type="button" class="btn btn-primary">Save changes</button>--%>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
    <script type="text/javascript" language="javascript">

        function TosterMessage(message, strType) {
            toastr.options = {
                timeOut: 4e3,
                progressBar: !0,
                showMethod: "slideDown",
                hideMethod: "slideUp",
                showDuration: 700,
                hideDuration: 200,
                positionClass: "toast-top-center"
            };

            switch (strType) {
                case "success": toastr.success(message); break;
                case "error": toastr.error(message); break;
                case "warning": toastr.warning(message); break;
                case "info": toastr.info(message); break;
            }
        }

        // attach the event binding function to every partial update
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }

            function TosterMessage(message, strType) {
                toastr.options = {
                    timeOut: 4e3,
                    progressBar: !0,
                    showMethod: "slideDown",
                    hideMethod: "slideUp",
                    showDuration: 700,
                    hideDuration: 200,
                    positionClass: "toast-top-center"
                };

                switch (strType) {
                    case "success": toastr.success(message); break;
                    case "error": toastr.error(message); break;
                    case "warning": toastr.warning(message); break;
                    case "info": toastr.info(message); break;
                }
            }
        });

    </script>
</asp:Content>
