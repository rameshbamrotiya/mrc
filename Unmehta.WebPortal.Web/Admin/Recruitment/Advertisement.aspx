<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Advertisement.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Recruitment.Advertisement" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Advertisement Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Advertisement</h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Recruitment</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Advertisement</li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">

    <script src="<%= ResolveUrl("~/ckeditor/ckeditor.js") %>"></script>
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddlRecruitmentType">Recruitment Type</label>
                        <asp:DropDownList ID="ddlRecruitmentType" OnSelectedIndexChanged="ddlRecruitmentType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddlDepartments">Departments</label>
                        <asp:DropDownList ID="ddlAdvertisementType" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtAgeTo">No of opening</label>
                        <asp:TextBox ID="txtnoopning" aria-describedby="emailHelp" TextMode="Number" CssClass="form-control" placeholder="Enter No of oppning" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddlAdvertisementCode">Advertisement Code</label>
                        <asp:DropDownList ID="ddlAdvertisementCode" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAdvertisementCode_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        <%--<asp:DropDownList ID="ddlAdvertisementCode" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlAdvertisementCode_SelectedIndexChanged"></asp:DropDownList>--%>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtAgeTo">Post Code</label>
                        <asp:TextBox ID="txtPostCode" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter Post Code" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <asp:HiddenField ID="hfRowId" runat="server" />
                    <div class="form-group">
                        <label for="txtAdvertisementName">Post Name</label>
                        <asp:TextBox ID="txtPostName" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter advertisement name" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="ddlPostType">Post Type</label>
                        <asp:DropDownList ID="ddlPostType" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <label for="exampleInputFile">Add NewIcon?<span class="req-field">*</span></label>
                    <div class="input-group">
                        <asp:RadioButton ID="rbtnYES" Checked="true" GroupName="IdocNew" runat="server" Text="Yes" ValidationGroup="tender" />
                        &nbsp;&nbsp;
                                                        <asp:RadioButton ID="rbtnNO" runat="server" GroupName="IdocNew" Text="No" ValidationGroup="tender" />
                    </div>
                    <span style="visibility: hidden;">&nbsp;</span>
                </div>
                <div class="col-md-3" style="display: none;">
                    <div class="form-group">
                        <label for="ddlDesignation">Designation</label>
                        <asp:DropDownList ID="ddldesignation" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">

                        <label for="txtPostDesc">General Description</label>
                        <textarea id="txtPostDesc" runat="server" name="editor1">This is sample text</textarea>
                        <%--<asp:TextBox ID="txtPostDesc" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter advertisement description" runat="server"></asp:TextBox>--%>
                    </div>

                </div>
                <script type="text/javascript">
                    CKEDITOR.dtd.$removeEmpty['i'] = false;
                    var editor = CKEDITOR.replace('<%=txtPostDesc.ClientID%>', {
                        extraPlugins: 'tableresize'
                    });
                </script>
                <div id="diviswalkin" class="card-body" runat="server" visible="false">
                    <div class="row col-md-12">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtStartDate">Interview Date</label>

                                <div class="row">
                                    <asp:TextBox ID="txtInterviewdate" aria-describedby="emailHelp" CssClass="form-control col-md-10" placeholder="Enter Interview date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtStartDate">Interview Time</label>

                                <div class="row">
                                    <asp:TextBox ID="txtInterviewTime" aria-describedby="emailHelp" CssClass="form-control col-md-10 clockpicker" placeholder="Enter Interview start Time" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtStartDate">Reporting Time</label>

                                <div class="row">
                                    <asp:TextBox ID="txtReportingtime" aria-describedby="emailHelp" CssClass="form-control col-md-10" placeholder="Enter Reporting start Time" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtAgeTo">Max Age</label>
                        <asp:TextBox ID="txtAgeTo" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter Max age" TextMode="Number" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtStartDate">Start Date</label>

                        <div class="row">
                            <asp:TextBox ID="txtStartDate" aria-describedby="emailHelp" CssClass="form-control col-md-6" placeholder="Enter start date" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtStartTime" aria-describedby="emailHelp" CssClass="form-control col-md-6 clockpicker-demo" placeholder="Enter start Time" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtEndDate">End Date</label>
                        <div class="row">
                            <asp:TextBox ID="txtEndDate" aria-describedby="emailHelp" CssClass="form-control col-md-6" placeholder="Enter end date" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtEndTime" aria-describedby="emailHelp" CssClass="form-control col-md-6 clockpicker-demo" placeholder="Enter start Time" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlQualification">Advertisement Source</label>
                        <asp:DataList ID="dlAdvertisementSource" runat="server" RepeatDirection="Vertical">
                            <ItemTemplate>
                                <div id="test2_<%#Eval("AdvertisementName").ToString().Replace(" ","_")%>" style="margin-left: 5px;">
                                    <asp:Label ID="LblEducationDetailName" Visible="false" runat="server" Text='<%# Eval("AdvertisementName") %>'></asp:Label>
                                    <asp:CheckBox ID="chkRow" runat="server" Font-Bold="true" Text='<%#Eval("AdvertisementName")%>' value='<%#Eval("AdvertisementName")%>' />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlQualification">Min. Education Qualification</label>
                        <asp:DataList ID="dlChkEducationType" runat="server" RepeatDirection="Vertical">
                            <ItemTemplate>
                                <div id="test1_<%#Eval("TypeName").ToString().Replace(" ","_")%>" style="margin-left: 5px;">
                                    <asp:Label ID="LblEducationDetailName" Visible="false" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                                    <asp:CheckBox ID="chkRow" runat="server" Font-Bold="true" Text='<%#Eval("TypeName")%>' value='<%#Eval("TypeName")%>' />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="dlCheckList">Select Visible Education in Form</label>
                        <asp:DataList ID="dlCheckList" runat="server" RepeatDirection="Vertical">
                            <ItemTemplate>
                                <div id="test_<%#Eval("EducationDetailName").ToString().Replace(" ","_")%>" style="margin-left: 5px;">
                                    <asp:Label ID="LblEducationDetailName" Visible="false" runat="server" Text='<%# Eval("EducationDetailName") %>'></asp:Label>
                                    <asp:CheckBox ID="chkRow" runat="server" Font-Bold="true" Text='<%#Eval("EducationDetailName")%>' value='<%#Eval("EducationDetailName")%>' />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group form-check">
                        <label for="dlCheckList">Gender:</label>
                        <%--<br />
                        <br />--%>
                        <asp:CheckBoxList ID="ChkGEnder" runat="server">
                            <asp:ListItem Value="1" Text="Male"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Female"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Trans-gender"></asp:ListItem>
                        </asp:CheckBoxList>
                        <%-- <asp:CheckBox Font-Bold="true" ID="chkmale" runat="server" value="1" />
                        <label style="font-weight: bold;" class="form-check-label" for="chkEnable">Male</label>
                        <asp:CheckBox Font-Bold="true" ID="chkfemale" runat="server" value="2" />
                        <label style="font-weight: bold;" class="form-check-label" for="chkEnable">Female</label>
                        <asp:CheckBox Font-Bold="true" ID="chkother" runat="server" value="3" />
                        <label style="font-weight: bold;" class="form-check-label" for="chkEnable">Transcender</label>--%>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="fuAboutAdvertisement">Advertisement File</label>

                        <asp:FileUpload ID="fuAboutAdvertisement" runat="server" />
                        <asp:Label ID="lblInnerImage" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hfInnerImage" runat="server" />
                        <a onclick="return RemoveImage('bodyPart_lblInnerImage','bodyPart_aRemoveInner','bodyPart_hfInnerImage');" id="aRemoveInner" class="fa fa-trash-o btn btn-primary" runat="server" style="margin-left: 5px; cursor: pointer;">Remove</a>

                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtage">Age Limit Calculate Start Date</label>

                        <asp:TextBox ID="txtAgeLimitCalOn" aria-describedby="emailHelp" CssClass="form-control col-md-12" placeholder="Enter Age Limit date" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtminexpe">Min Experience</label>

                        <asp:TextBox ID="txtMinExp" aria-describedby="emailHelp" TextMode="Number" CssClass="form-control col-md-12" placeholder="Enter Age Limit date" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtAgeTo">Max Experience</label>
                        <asp:TextBox ID="txtmaxexp" aria-describedby="emailHelp" CssClass="form-control" placeholder="Enter Max Experience" TextMode="Number" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtPublishDate">Publish Date</label>

                        <div class="row">
                            <asp:TextBox ReadOnly="true" ID="txtpublishdate" aria-describedby="emailHelp" CssClass="form-control col-md-12" placeholder="Enter publish date" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group form-check">
                        <br />
                        <br />
                        <asp:CheckBox ID="chkEnable" runat="server" />
                        <label class="form-check-label" for="chkEnable">Active</label>
                    </div>
                </div>
                <div class="col-md-3">
                    <label>&nbsp;</label>
                    <label>&nbsp;</label>
                    <div class="form-group">
                        <% if (SessionWrapper.UserPageDetails.CanAdd)
                            { %>
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <%} %>
                        <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <iframe id="my_iframe" style="display: none;"></iframe>
            </div>
        </div>
    </div>
    <div class="card">
        <div class="card-body">
            <div>
                <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..." AllowSorting="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1  %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AdvertisementName" HeaderText="Name" SortExpression="AdvertisementName" />
                        <asp:BoundField DataField="MaxAge" HeaderText="Max Age" SortExpression="MaxAge" />
                        <asp:BoundField DataField="PublishDate" HeaderText="PublishDate" SortExpression="PublishDate" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                        <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
                        <%--<asp:BoundField DataField="IsNewIcon" HeaderText="IsNewIcon" SortExpression="IsNewIcon" />--%>
                        <asp:BoundField DataField="IsActive" HeaderText="Visable" SortExpression="IsActive" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <div class="btn-group">
                                    <% if (SessionWrapper.UserPageDetails.CanUpdate)
                                        { %>
                                    <asp:LinkButton ID="ibtn_Edit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="ibtn_Edit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                    <%}
                                        if (SessionWrapper.UserPageDetails.CanDelete)
                                        { %>
                                    <asp:LinkButton ID="ibtn_Delete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="ibtn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this role?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                    <%} %>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
    <script>


        $(document).ready(function () {
            ClosePreloder();
            var StartDate = document.getElementById('bodyPart_txtStartDate');
            var EndDate = document.getElementById('bodyPart_txtEndDate');
            var txtInterviewdate = document.getElementById('bodyPart_txtInterviewdate');
            CreateFromToDatePicker(StartDate, EndDate);
            CreateDatePicker(txtInterviewdate);

            $('#<%=txtInterviewTime.ClientID%>').clockpicker({
                twelvehour: true,
                donetext: 'DONE', // done button text
                autoclose: false, // auto close when minute is selected
                afterDone: function () {
                    $('#<%=txtInterviewTime.ClientID%>').val($('#<%=txtInterviewTime.ClientID%>').val().slice(0, -2) + ' ' + $('#<%=txtInterviewTime.ClientID%>').val().slice(-2));
                }
            });

            $('#<%=txtReportingtime.ClientID%>').clockpicker({
                twelvehour: true,
                donetext: 'DONE', // done button text
                autoclose: false, // auto close when minute is selected
                afterDone: function () {
                    $('#<%=txtReportingtime.ClientID%>').val($('#<%=txtReportingtime.ClientID%>').val().slice(0, -2) + ' ' + $('#<%=txtReportingtime.ClientID%>').val().slice(-2));
                }
            });

            $('#<%=grdUser.ClientID%>').DataTable({
                destroy: true,
                responsive: true
            });
        });


        function GetDateFromstring(strDate) {
            var parts = strDate.split("/");
            var dt = new Date(parseInt(parts[2], 10),
                              parseInt(parts[1], 10) - 1,
                              parseInt(parts[0], 10));
            return dt;
        }


        function downloadURL(url) {
            debugger;
            url = window.location.protocol + "//" + window.location.host + url;
            //url = url.replace("//", "/");
            //var hiddenIFrameID = 'hiddenDownloader',
            //    iframe = document.getElementById(hiddenIFrameID);
            //if (iframe === null) {
            //    iframe = document.createElement('iframe');
            //    iframe.id = hiddenIFrameID;
            //    iframe.style.display = 'none';
            //    document.body.appendChild(iframe);
            //}
            //iframe.src = url;
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
    </script>
</asp:Content>
