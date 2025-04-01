<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SQLExecuter.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Hospital.SQLExecuter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>SQL Executer</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">

    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>SQL Executer</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">CMS</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">SQL Executer</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card">
        <div class="card-body">
            <div class="card-body">
                <h4>SQL Executer</h4>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="txtSqlQuery">SQL Description</label>
                            <asp:TextBox ID="txtSqlQuery" TextMode="MultiLine" Rows="10" Columns="100" runat="server" CssClass="form-control" placeholder="Enter Meta Description" style="height: 49%;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rftxtSqlQuery" ForeColor="Red" runat="server" ControlToValidate="txtSqlQuery" Display="Dynamic"
                                ErrorMessage="Enter Sql Query." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <div>
                                <label>&nbsp;&nbsp;</label>
                            </div>
                            <asp:Button runat="server" ID="btnExecute" CssClass="btn btn-primary " Text="Execute" OnClick="btnExecute_Click" />
                            <asp:Button runat="server" ID="btnClear" CssClass="btn btn-primary " Text="Clear" OnClick="btnClear_Click" />

                        </div>
                    </div>
                    <div >
                        <div id="strHtmlData" runat="server" >
                        </div>
                    </div>
                </div>
            </div>
        </div>
      </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script>
        <%=strScript%>
    </script>
</asp:Content>
