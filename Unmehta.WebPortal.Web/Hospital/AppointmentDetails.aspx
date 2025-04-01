<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital.Master" AutoEventWireup="true" CodeBehind="AppointmentDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.Hospital.AppointmentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Appointment List</title>
    <!-- Favicon -->
    <link rel="shortcut icon" href="/Admin/Template/html/assets/media/image/favicon.png" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopJS" runat="server">
    <!-- App styles -->
    <link rel="stylesheet" href="/Admin/Template/html/assets/css/app.min.css" type="text/css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyForm" runat="server">
    <div class="container">
        
                    <h5>Appointment List</h5>
        <div class="card">
            <div class="card-body">
                <div class="">
                    <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                 AllowPaging="true" AllowSorting="false" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1  %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                                    <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" SortExpression="MiddleName" />
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                    <asp:BoundField DataField="MobileNumber" HeaderText="Mobile" SortExpression="MobileNumber" />
                                    <asp:BoundField DataField="EmailId" HeaderText="EmailId" SortExpression="EmailId" />
                                    <asp:BoundField DataField="AppointmentDate"  HeaderText="Date" SortExpression="AppointmentDate" />
                                    <asp:BoundField DataField="AppointmentTime" HeaderText="Time" SortExpression="AppointmentTime" />
                                    <asp:BoundField DataField="FollowUp" HeaderText="Is FollowUp" SortExpression="FollowUp" /> 

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                 <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                                     { %>
                                                <asp:LinkButton ID="ibtn_FollowUp" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_FollowUp_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%} %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                    
                </div>
                <div class="form-wrapper">
                    <a href="Appointment.aspx">Get Back To Appointment Page</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottomJs" runat="server">

</asp:Content>
