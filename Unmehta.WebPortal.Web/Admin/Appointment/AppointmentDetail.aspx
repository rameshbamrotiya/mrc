<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AppointmentDetail.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Appointment.AppointmentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">

    <title>Appointment Detail</title>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">

    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Appointment Detail</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Appointment</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Appointment Detail</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card">
        <div class="card-body">
            <asp:HiddenField ID="hfId" runat="server" />
            <div class="row" id="dvMain" runat="server">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtFirstName">Select Department</label>
                        <asp:DropDownList ID="ddlSpecialization" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSpecialization_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlUnit">Select Unit  </label>
                        <asp:DropDownList ID="ddlUnit" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="exampleInputFile">
                            Select Doctor   
                        </label>
                        <asp:DropDownList ID="ddlDoctorList" CssClass="doc1 form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>


            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label>From Date </label>
                        <div class="cal-icon">
                            <asp:TextBox ID="txtFromDate" aria-describedby="from date" MaxLength="10" CssClass="form-control col-md-12 datepicker" placeholder="Enter From date" runat="server"></asp:TextBox>

                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <label>To Date </label>
                        <div class="cal-icon">
                            <%--<asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datetimepicker" placeholder="Select To Date"></asp:TextBox>--%>
                            <asp:TextBox ID="txtToDate" aria-describedby="to date" MaxLength="10" CssClass="form-control col-md-12 datepicker" placeholder="Enter To date" runat="server"></asp:TextBox>

                        </div>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-12 controls">
                    <div class="form-group">
                        <button runat="server" id="btn_Search" class="btn btn-primary" title="Search" onserverclick="btn_Search_ServerClick">
                            <i class="fa fa-search">&nbsp;Search</i>
                        </button>
                        <button runat="server" id="btn_SearchCancel" class="btn btn-inverse" title="Cancel" onserverclick="btn_SearchCancel_ServerClick">
                            <i class="fa fa-remove">&nbsp;Clear</i>
                        </button>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-md-12">
                    <h3>Appointment Detail </h3>
                </div>

                <div class="col-md-12">
                    <div class="form-group" style="overflow-x: scroll;">
                        <asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary right" OnClick="btnExport_Click" Text="Export" Style="float: right;" /><hr />


                        <asp:GridView ID="gView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                            CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--   <asp:TemplateField HeaderText="RegistarionId">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ibtn_View" CommandName="eView" runat="server" data-original-title="View" CssClass="btn btn-sm show-tooltip" OnClick="ibtn_View_Click" OnClientClick="SetTarget();"><i class="fa fa-search-plus fa-lg"></i><%# Eval("RegisrationId") %></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="PatientName" HeaderText="Patient Name" />
                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                                <asp:BoundField DataField="VisitTypeName" HeaderText="Visit Type" />
                                <asp:BoundField DataField="UNMId" HeaderText="UNM Id" />
                                <asp:BoundField DataField="ReasonForVisit" HeaderText="Reason For Visit" />
                                <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                                <asp:BoundField DataField="UnitName" HeaderText="Unit Name" />
                                <asp:BoundField DataField="SloteName" HeaderText="Slote Name" />
                                <asp:BoundField DataField="SlotDetail" HeaderText="Slote Detail" />
                                <asp:BoundField DataField="DoctorName" HeaderText="Doctor Name" />
                                <asp:BoundField DataField="strAppointmentDate" HeaderText="Appointment Date" />

                                <%--   <asp:TemplateField HeaderText="Signature Name">
                                    <ItemTemplate>
                                        <a id="afileSignaturePath" href='<%# Eval("SignaturePath") %>' target="_blank" runat="server" tooltip="View File" class="fa fa-search-plus"><%# Eval("SignatureName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Visit Status">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <%# Eval("IsVisit").Equals(true) ? "<span>Status: True</span>" : string.Empty %>
                                            <asp:LinkButton ID="LinkButton1" CommandName="eView" runat="server"
                                                data-original-title="Visit" CssClass="btn btn-sm show-tooltip"
                                                OnClick="lnkVisit_Click1"
                                                OnClientClick="return confirmVisit(this);"
                                                Visible='<%# !Eval("IsVisit").Equals(true) %>'
                                                data-patient-name='<%# Eval("PatientName") %>'
                                                data-mobile-number='<%# Eval("MobileNo") %>'>
                                                 <i class="fa fa-user-check"></i> Confirm Visit
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script>

        function GetDateFromstring(strDate) {
            var parts = strDate.split("/");
            var dt = new Date(parseInt(parts[2], 10),
                parseInt(parts[1], 10) - 1,
                parseInt(parts[0], 10));
            return dt;
        }

        function confirmVisit(linkButton) {
            var patientName = linkButton.getAttribute('data-patient-name');
            var mobileNumber = linkButton.getAttribute('data-mobile-number');
            var message = "Are you sure the patient " + patientName + " with mobile number " + mobileNumber + " is visiting?";
            return confirm(message);
        }

        $(document).ready(function () {
            ClosePreloder();
            var StartDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var EndDate = document.getElementById('<%=txtToDate.ClientID%>');
            //CreateFromToDatePicker(StartDate, EndDate);
            //CreateFromToDatePicker(StartDate, EndDate);
            CreateDatePicker(StartDate);
            CreateDatePicker(EndDate);
            //var StartDate = document.getElementById('bodyPart_txtStartDate');
            //var EndDate = document.getElementById('bodyPart_txtEndDate');
            //var txtInterviewdate = document.getElementById('bodyPart_txtInterviewdate');
            //CreateFromToDatePicker(StartDate, EndDate);
            //CreateDatePicker(txtInterviewdate);


            <%--$('#<%=grdUser.ClientID%>').DataTable({
                destroy: true,
                responsive: true
            });--%>
        });


        //function GetDateFromstring(strDate) {
        //    var parts = strDate.split("/");
        //    var dt = new Date(parseInt(parts[2], 10),
        //        parseInt(parts[1], 10) - 1,
        //        parseInt(parts[0], 10));
        //    return dt;
        //}


        function downloadURL(url) {
            debugger;
            url = window.location.protocol + "//" + window.location.host + url;

            var params = [
                'height=' + screen.height,
                'width=' + screen.width,
                'fullscreen=yes' // only works in IE, but here for completeness
            ].join(',');
            // and any other options from
            // https://developer.mozilla.org/en/DOM/window.open

            var popup = window.open(url, 'popup_window', params);
            popup.moveTo(0, 0);
        };


        //$('.datepicker').datepicker({
        //    format: 'dd/mm/yyyy',
        //    beforeShowDay: function (date) {
        //        dmy = date.getDate() + "-" + (date.getMonth() + 1) + "-" + date.getFullYear();
        //        if (disableDates.indexOf(dmy) != -1) {
        //            return false;
        //        }
        //        else {
        //            return true;
        //        }
        //    }
        //});


    </script>
</asp:Content>
