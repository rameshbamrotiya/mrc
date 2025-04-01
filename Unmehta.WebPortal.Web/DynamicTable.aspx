<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DynamicTable.aspx.cs" Inherits="Unmehta.WebPortal.Web.DynamicTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Columns
                </td>
                <td>
                    <asp:TextBox ID="txtColumns" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Rows
                </td>
                <td>
                    <asp:TextBox ID="txtRows" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSubmit" OnClick="GenerateGridView" Text="Submit" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvData" runat="server" />
                    <asp:Button ID="btnGetData" OnClick="btnGetData_Click" Text="Submit" runat="server" />
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
