<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital.Master" AutoEventWireup="true" CodeBehind="DoctorAppointment.aspx.cs" Inherits="Unmehta.WebPortal.Web.Hospital.DoctorAppointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Appointment</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopJS" runat="server">
    <link rel="shortcut icon" href="/Admin/Template/html/assets/media/image/favicon.png" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">



    <style>
        body {
            background-color: #e9ecef;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">

        <div class="card">
            <div class="card-body">
                <div class="">

                    <h5>Get Appointment</h5>

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-4">
                                    <label>Specialization</label>
                                    <asp:RequiredFieldValidator ID="rfddlSpecialization" runat="server" Display="Dynamic" ErrorMessage="*" ValidationGroup="Main" ForeColor="Red" ControlToValidate="ddlSpecialization"></asp:RequiredFieldValidator>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSpecialization" class="form-control " runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSpecialization_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <label>Unit</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="*" ValidationGroup="Main" ForeColor="Red" ControlToValidate="ddlUnit"></asp:RequiredFieldValidator>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlUnit" class="form-control " OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-12" id="dvDoctorList" runat="server">
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!-- form -->

                    <!-- form -->

                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="dvFooter" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="myModalAppointment" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title" id="dv">Book Appointment</h4>
                    <button type="button" class="close myModalAppointment" data-bs-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <asp:HiddenField ID="hfDocId" runat="server" />
                    <div class="row">

                        <div class="col-12">
                            <label>Doctor Name</label>
                            <div class="form-group">
                                <label id="txtDocName" class="form-control "></label>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <div class="row" style="margin: auto 2%">


                                    <div class="col-6">
                                        <label>Appointment Date</label><asp:RequiredFieldValidator ID="rftxtAppointmentDate" runat="server" Display="Dynamic" ErrorMessage="*" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtAppointmentDate"></asp:RequiredFieldValidator>

                                        <div class="form-group">
                                            <asp:TextBox ID="txtAppointmentDate" AutoPostBack="true" OnTextChanged="txtAppointmentDate_TextChanged" class="form-control" placeholder="Appointment Date" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-6">
                                        <label>Appointment Time</label>
                                        <asp:RequiredFieldValidator ID="rfAppintmentTime" runat="server" Display="Dynamic" ErrorMessage="*" ValidationGroup="Main" ForeColor="Red" ControlToValidate="ddlAppintmentTime"></asp:RequiredFieldValidator>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlAppintmentTime" class="form-control " runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <label>Patient Name</label>
                                        <asp:RequiredFieldValidator ID="rfvPatientName" runat="server" Display="Dynamic" ErrorMessage="*" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtPatientName"></asp:RequiredFieldValidator>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtPatientName" class="form-control " MaxLength="100" placeholder="Patient name" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <label>Mobile</label>
                                        <asp:RequiredFieldValidator ID="rftxtMobile" runat="server" Display="Dynamic" ErrorMessage="*" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtMobile"></asp:RequiredFieldValidator>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtMobile" MaxLength="10" placeholder="Enter Mobile No" onkeypress="return isNumberKey(event)" class="form-control " runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <label>EmailId</label>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                            CssClass="validationmsg" SetFocusOnError="true" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Invalid email address" ValidationGroup="Main" ForeColor="Red" />
                                        <div class="form-group">
                                            <asp:TextBox ID="txtEmail" class="form-control " placeholder="EmailId" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <label>Visit Type</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="*" ValidationGroup="Main" ForeColor="Red" ControlToValidate="ddlVisitTYpe"></asp:RequiredFieldValidator>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlVisitTYpe" class="form-control " OnSelectedIndexChanged="ddlVisitTYpe_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-6" id="dvUDN" runat="server">
                                        <label>UDN Id</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" Enabled="false" ErrorMessage="*" ValidationGroup="Main" ForeColor="Red" ControlToValidate="txtUDNId"></asp:RequiredFieldValidator>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtUDNId" class="form-control " MaxLength="100" placeholder="UDN Id" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <label>Reason For Visit</label>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtReasonForVisit" class="form-control " TextMode="MultiLine" Rows="4" placeholder="Reason For Visit" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <span id="lblError" runat="server" style="/* float: right; */color: red; text-align: center; display: block; margin: 7px auto;"></span>
                                    </div>

                                    <div class="col-4">
                                        <asp:Button ID="btnAppoinment" runat="server" CssClass="btn btn-block btn-primary" OnClick="btnAppoinment_Click" ValidationGroup="Main" Text="Get Appointment" />
                                    </div>
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:PostBackTrigger ControlID="btnAppoinment" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                        <!-- ./ form -->

                    </div>

                    <!-- Modal footer -->
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-danger myModalAppointment" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script>

        function GetPageEvent() {

            function TosterMessage(message, strType) {
                toastr.options = {
                    timeOut: 4e3,
                    progressBar: !0,
                    showMethod: "slideDown",
                    hideMethod: "slideUp",
                    showDuration: 500,
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


            $("#<%=txtAppointmentDate.ClientID %>").datepicker({
                autoclose: true,
                dateFormat: 'dd/mm/yy',
                minDate:new Date()
                //beforeShowDay: function (date) {
                //    var dmy = date.getDate() + "-" + (date.getMonth() + 1) + "-" + date.getFullYear();
                //    return disableDates.indexOf(dmy) === -1;
                //}
            });

            var dvUDN = document.getElementById('<%=dvUDN.ClientID%>');

            $(".bookAppointment").click(function () {

                var docID = this.attributes["data-original-title"].value;
                var docIDName = this.attributes["data-original-name"].value;

                $("#<%=hfDocId.ClientID %>").val(docID);
                $("#<%=txtPatientName.ClientID %>").val("");
                $("#<%=ddlAppintmentTime.ClientID %>").val("");
                $("#<%=txtReasonForVisit.ClientID %>").val("");
                $("#<%=txtMobile.ClientID %>").val("");
                $("#<%=txtEmail.ClientID %>").val("");
                $("#<%=txtAppointmentDate.ClientID %>").val("");
                $("#<%=ddlVisitTYpe.ClientID %>").val("1");
                $("#<%=txtUDNId.ClientID %>").val("");

                if (dvUDN != null && dvUDN != undefined) {
                    dvUDN.style.display = "none";
                }

                $("#txtDocName").html(docIDName);
                $("#myModalAppointment").modal('show');
            });

            $(".myModalAppointment").click(function () {
                $("#<%=hfDocId.ClientID %>").val("");
                $("#<%=txtPatientName.ClientID %>").val("");
                $("#<%=ddlAppintmentTime.ClientID %>").val("");
                $("#<%=txtReasonForVisit.ClientID %>").val("");
                $("#<%=txtAppointmentDate.ClientID %>").val("");
                $("#<%=txtMobile.ClientID %>").val("");
                $("#<%=ddlVisitTYpe.ClientID %>").val("1");
                $("#<%=txtEmail.ClientID %>").val("");
                $("#<%=txtUDNId.ClientID %>").val("");

                if (dvUDN != null && dvUDN != undefined) {
                    dvUDN.style.display = "none";
                }
                $("#myModalAppointment").modal('hide');

            });
            $("#<%= ddlAppintmentTime.ClientID %> option[disabled]").wrapAll("<optgroup label='Unavailable'></optgroup>");

            // Get holidays list
         $.ajax({
                type: "POST",
                url: "/Hospital/Appointment.aspx/GetHolidayList",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var disableDates = data.d;
                    debugger
                    disableDates.push("16/06/2024")
                    $("#<%=txtAppointmentDate.ClientID %>").datepicker({
                        autoclose: true,
                        dateFormat: 'dd/mm/yy',
                        minDate: new Date(),
                        beforeShowDay: function (date) {
                            var dmy = (d.getMonth() + 1)
                            if (d.getMonth() < 9)
                                dmy = "0" + dmy;
                            dmy += "-";

                            if (d.getDate() < 10) dmy += "0";
                            dmy += d.getDate() + "-" + d.getFullYear();

                            console.log(dmy + ' : ' + ($.inArray(dmy, disableDates)));

                            if ($.inArray(dmy, disableDates) == -1) {
                                return [true, "", "Available"];
                            } else {
                                return [false, "", "unAvailable"];
                            }
                        }
                    });
                    
                },
                error: function (error) {
                    console.error("Error fetching holidays list: ", error);
                }
            });

            // Update datepicker when dropdown changes
            $("#<%= ddlUnit.ClientID %>").change(function () {
                var unitId = $(this).val();
                $.ajax({
                    type: "Get",
                    url: "/Hospital/Appointment.aspx/GetEnabledDays?unitId=" + unitId,
                    //data: JSON.stringify({ unitId: unitId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var enabledDays = response.d;

                        // Define the function for beforeShowDay
                        var beforeShowDayFunction = function (date) {
                            var day = date.getDay().toString();
                            return [enabledDays.indexOf(day) !== -1];
                        };

                        // Set the beforeShowDay function in the datepicker options
                        $("#body_txtAppointmentDate").datepicker("option", "beforeShowDay", beforeShowDayFunction);
                    },
                    error: function (error) {
                        console.error("Error fetching enabled days: ", error);
                    }
                });

            });
        }

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            GetPageEvent();
        });

        function EndRequestHandler(sender, args) {
            GetPageEvent();
        }

    </script>
</asp:Content>
