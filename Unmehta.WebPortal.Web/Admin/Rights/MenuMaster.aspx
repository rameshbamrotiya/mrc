<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuMaster.aspx.cs" MasterPageFile="~/Admin/Admin.Master" Inherits="Pages_MenuMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>DocumentUpload Master
        <small>Add Document for global.</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li class="active">DocumnetUpload Master</li>
            </ol>
        </section>


        <!-- Main content -->
        <section class="content">
            <div class="box">
                <div class="box-body">
                    <!-- Bootstrap alert -->
                    <div class="row">
                        <div class="form-group col-md-12">
                            <div class="messagealert" id="alert_container">
                            </div>
                        </div>
                    </div>
                    <!-- END Bootstrap alert -->
                    <asp:Panel ID="pnlView" runat="server">
                        <div class="row">
                            <div class="col-md-6" id="tblSearch">
                                <div class="form-group">
                                    <div class="col-md-9 controls">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-search"></i></span>

                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="serachText form-control" placeholder="Search here..."></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:Button runat="server" ID="btn_Search"  class="btn btn-primary" title="Search" />
                                                <%--<asp:Button runat="server" id="btn_Search" class="btn btn-primary" title="Search">--%>
                                                <i class="fa fa-search">&nbsp;Search</i>

                                            </span>
                                            <span class="input-group-btn">
                                                <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel">
                                                    <i class="fa fa-remove">&nbsp;Cancel</i>
                                                </button>
                                            </span>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                        <br />
                        <br />

                    </asp:Panel>



                </div>
            </div>
        </section>
    </div>
</asp:Content>