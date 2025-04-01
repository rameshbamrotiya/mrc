<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/StudentAdmission.Master" AutoEventWireup="true" CodeBehind="StudentCourseSequence.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.StudentCourseSequence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <%--<section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h1>Student Details</h1>
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>">Home</a></li>
                <li>/</li>
                <li>Student Details</li>
            </ul>
        </div>
    </section>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="contact-area register-card-body pt-50 pb-70">
        <div class="container">
            <div class="row justify-content-md-center">
                <div class="col-lg-12">
                    <section class="content">
                        <div class="card">
                            <div class="section-main-title" style="background-color: #c20e3e; text-align: center;">
                                <h2 style="margin-top: 10px; color: white;">Student Course Details</h2>
                                <h3>
                                    <asp:Label ID="lblPostName" runat="server" Style="text-align: right !important; color: red;"></asp:Label>
                                </h3>
                            </div>
                            <div class="row col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="ddlDepartment">Department</label>
                                        <asp:DropDownList ID="ddlcoursename" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvDepartment" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlcoursename"
                                        ErrorMessage="Select course." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3" style="margin-top:33px; text-align: center;">
                                    <div class="form-group">
                                        <button runat="server" id="btnAdd" class="btn btn-primary btn_general" tabindex="4" title="Final Submit" onserverclick="btnAdd_ServerClick" visible="true">
                                            Add Course
                                        </button>
                                        <%--<button class="fa fa-plus-circle" data-toggle="tooltip" style="cursor: pointer; margin-top: 36px" title="Add" id="btnAdd" runat="server" onserverclick="btnAdd_ServerClick" />--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>From<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtFrom" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" style="text-align: center;">
                                    <div class="form-group">
                                        <label>To<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtTo" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" style="margin-top: 33px; text-align: center;">
                                    <button runat="server" id="btnreplace" class="btn btn-primary btn_general" tabindex="4" title="Final Submit" onserverclick="Button1_ServerClick" visible="true">
                                        Replace
                                    </button>
                                </div>
                            </div>
                            <div class="card-body">
                                <!-- Bootstrap alert -->
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:GridView ID="gView" OnRowCommand="gView_RowCommand" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,StudentId,MasterCourseId" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1  %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                                                    <asp:TemplateField HeaderText="Action" Visible="true">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="ibtn_Delete" ToolTip="Delete" CommandName="eDelete" OnClientClick='<%# Eval("CourseName", "return confirm(\"Are you sure want to delete : {0} ? \")") %>' runat="server" SkinID="lDelete" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnk_UP" CausesValidation="false" ToolTip="Page Up"
                                                                    CommandArgument='<%# Eval("StudentId") + "," + Eval("CourseSequence") + "," + Eval("MasterCourseId") + "," + "up"%>' runat="server" data-original-title="Page Up" CssClass="btn btn-sm show-tooltip">
                                                                    <i class="fa fa-arrow-circle-up"></i>
                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="lnk_Dwn" CausesValidation="false" ToolTip="Page Down"
                                                                    CommandArgument='<%# Eval("StudentId") + "," + Eval("CourseSequence") +  "," + Eval("MasterCourseId") + "," + "down" %>'
                                                                    runat="server" data-original-title="Page Down" CssClass="btn btn-sm show-tooltip">
                                                                    <i class="fa fa-arrow-circle-down"></i>
                                                                </asp:LinkButton>
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
                        <div class="row" style="padding-top: 5px !important;">
                            <!-- /.col -->
                            <div class="col-md-5">
                                <div class="form-group">
                                </div>
                            </div>
                            <div class="col-xs-2">
                                <%--<button runat="server" id="btnProfessionalDivPrivious" class="btn btn-primary btn_general" onserverclick="btnProfessionalDivPrivious_ServerClick" causesvalidation="false" title="Search">
                                    Previous
                                </button>--%>
                                <button runat="server" id="btnFinalSubmit" class="btn btn-primary btn_general" tabindex="4" title="Final Submit" onserverclick="btnFinalSubmit_ServerClick" visible="true">
                                    Final Submit
                                </button>
                                <%--<button runat="server" id="btnPrint" class="btn btn-primary btn_general" onserverclick="btnPrint_ServerClick" causesvalidation="false" visible="false" title="Search">
                                    Print
                                </button>--%>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                </div>
                            </div>
                            <!-- /.col -->
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
</asp:Content>
