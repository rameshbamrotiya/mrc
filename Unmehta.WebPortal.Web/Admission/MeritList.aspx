<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/LTEStudent.Master" AutoEventWireup="true" CodeBehind="MeritList.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.MeritList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="form-group" style="overflow-x: scroll;">
                <asp:GridView ID="gView" runat="server" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
</asp:Content>
