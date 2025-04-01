<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/LTEStudent.Master" AutoEventWireup="true" CodeBehind="ExtraInfofroAdmission.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admission.ExtrInfofroAdmission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <style>
        input[type=checkbox], input[type=radio] {
            box-sizing: border-box;
            padding: 2px;
            margin-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-md-7 col-lg-12">
            <asp:HiddenField ID="hdfExtrainfo" runat="server" />
            <!-- Basic Information -->
            <div class="card card-info">
                <div class="card-header">
                    <h3 class="card-title"><i class="fab fa-accusoft"></i>Other Information
                    </h3>
                </div>
                <asp:Panel ID="PanelOtherInformation" runat="server" Enabled="false">
                    <div class="card-body">
                        <div class="row form-row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Known person in UNMICRC, if any : <span class="text-danger">*</span></label>
                                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="RBLknowperson" AutoPostBack="true" OnSelectedIndexChanged="knowperson_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="no" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblUNMICRCPerson"  Font-Bold="true" Text="Please mention name" Visible="false" runat="server"></asp:Label>
                                    <asp:TextBox ID="txtUNMICRCPerson" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Do you suffer from any chronical illness?  If yes, give details: <span class="text-danger">*</span></label>
                                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="RBLilleness" AutoPostBack="true" OnSelectedIndexChanged="RBLilleness_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="no" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblChronicillness" Font-Bold="true" Text="Please add details" Visible="false" runat="server"></asp:Label>
                                    <asp:TextBox ID="txtChronicillness" runat="server" class="form-control" Visible="false"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Your current accommodation :</label>
                                    <%--<asp:TextBox ID="txtEmergencyContactNo" runat="server" onkeypress="return isNumberKey(event)" class="form-control"></asp:TextBox>--%>
                                    <asp:RadioButtonList ID="rblaccommodation" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Own" Text="Own"></asp:ListItem>
                                        <asp:ListItem Value="Rental" Text="Rental"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Extra curricular Activities / Hobbies :</label>
                                    <asp:TextBox ID="txtextraactivities" runat="server" class="form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Interest in Social Activities :(Specify)</label>
                                    <asp:TextBox ID="txtsocialactivities" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <asp:UpdatePanel ID="upAnnexure1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <label>How did you heard about this Course?</label>
                                            <table class="table table-bordered table-hover">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBoxList Width="100%" RepeatDirection="Horizontal" runat="server" ID="chk1" OnSelectedIndexChanged="chk1_SelectedIndexChanged" AutoPostBack="true" />

                                                        <div class="col-md-9" id="CLRemarks" runat="server" visible="false">
                                                            <div class="form-group mb-0">
                                                                <label>If other please specify:</label>
                                                                <asp:TextBox TextMode="MultiLine" ID="txtcourseheard1" runat="server" class="form-control"></asp:TextBox>
                                                                <%--<textarea id="txtcourseheard" runat="server" class="form-control"></textarea>--%>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--<asp:TextBox ID="txtothercourse" runat="server" class="form-control"></asp:TextBox>--%>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="chk1" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row col-md-12" id="DivOtherInformationMain" runat="server" visible="false">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application Status</label>
                                    <asp:Label ID="LblstatusOI" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-10" id="DivRemerksOI" runat="server">
                                <div class="form-group">
                                    <label>Remerks</label>
                                    <asp:TextBox ID="txtRemerksOI" ReadOnly="true" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="7" style="height:140px; background: yellow;"></asp:TextBox>
                                    <%--<asp:Label ID="LblRemerksFMI" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <!-- /Basic Information -->
            <div class="card card-info">
                <div class="card-header">
                    <h3 class="card-title"><i class="fas fa-gavel"></i>Has any court of law in India or abroad ever convicted you? If yes, give details.</h3>
                </div>
                <asp:Panel ID="PanelLawinindia" runat="server" Enabled="false">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>
                                        Has any court of law in India or abroad ever convicted you? If yes, give details. 
                                    <asp:RadioButtonList ID="rblcourtoflaw" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="rblcourtoflaw_SelectedIndexChanged" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="YES" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Selected="True" Value="NO" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList></label>
                                    <asp:TextBox Visible="false" ID="txtcourtoflaw" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>

                                </div>
                            </div>

                        </div>
                        <div class="row col-md-12" id="DivLawinindiaMain" runat="server" visible="false">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application Status</label>
                                    <asp:Label ID="LblLStatusIN" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4" id="DivRemerksLIN" runat="server">
                                <div class="form-group">
                                    <label>Remerks</label>
                                    <asp:TextBox ID="txtRemerksLIN" ReadOnly="true" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="7" style="height:140px; background: yellow;"></asp:TextBox>
                                    <%--<asp:Label ID="LblRemerksFMI" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="card card-info">
                <div class="card-header">
                    <h3 class="card-title"><i class="fas fa-ambulance"></i> Emergency Details</h3>
                </div>
                <asp:Panel ID="PanelEmergencyDetails" runat="server" Enabled="false">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Blood Group </label>
                                    <asp:DropDownList ID="ddlbloodgroup" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Select Blood Group" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                                        <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                                        <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                                        <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                                        <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                                        <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                                        <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>
                                        <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                                        <asp:ListItem Text="Don't know" Value="Don't know"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtbloodgroup" runat="server" class="form-control" placeholder="Enter Blood Group"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtbloodgroup" CssClass="validationmsg" runat="server" ControlToValidate="txtbloodgroup"
                                    ErrorMessage="Enter Blood Group." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Allergic to </label>
                                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="RBLAllergicto" AutoPostBack="true" OnSelectedIndexChanged="RBLAllergicto_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="no" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label></label>
                                    <asp:TextBox ID="txtmajorillness" runat="server" class="form-control" placeholder="Enter Allergic to" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Last Major illness/Surgery </label>
                                    <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="RBLSurgery" AutoPostBack="true" OnSelectedIndexChanged="RBLSurgery_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="no" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label></label>
                                    <asp:TextBox ID="txtsurgeryinfo" TextMode="MultiLine" runat="server" class="form-control" Visible="false" placeholder="Enter Last Major illness/Surgery"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-3" runat="server" id="DIVSurgeryMonth" visible="false">
                                <div class="form-group">
                                    <label for="exampleInputFile">Surgery Month<span class="req-field">*</span></label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Select Month" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Jan" Value="Jan"></asp:ListItem>
                                            <asp:ListItem Text="Feb" Value="Feb"></asp:ListItem>
                                            <asp:ListItem Text="Mar" Value="Mar"></asp:ListItem>
                                            <asp:ListItem Text="Apr" Value="Apr"></asp:ListItem>
                                            <asp:ListItem Text="May" Value="May"></asp:ListItem>
                                            <asp:ListItem Text="Jun" Value="Jun"></asp:ListItem>
                                            <asp:ListItem Text="Jul" Value="Jul"></asp:ListItem>
                                            <asp:ListItem Text="Aug" Value="Aug"></asp:ListItem>
                                            <asp:ListItem Text="Sep" Value="Sep"></asp:ListItem>
                                            <asp:ListItem Text="Oct" Value="Oct"></asp:ListItem>
                                            <asp:ListItem Text="Nov" Value="Nov"></asp:ListItem>
                                            <asp:ListItem Text="Dec" Value="Dec"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-3" runat="server" id="DIVSurgeryYear" visible="false">
                                <div class="form-group">
                                    <label for="ddlHistoryYear">Surgery Year</label>
                                    <asp:DropDownList ID="ddlExtrInfoYear" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvExtraInfoYear" InitialValue="-1" ForeColor="Red" runat="server" ControlToValidate="ddlExtrInfoYear"
                                    ErrorMessage="Select Passing year." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Contact person in case of emergency</label>
                                    <asp:TextBox ID="txtconperson" runat="server" class="form-control" placeholder="Enter Contact person in case of emergency"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtconperson" CssClass="validationmsg" runat="server" ControlToValidate="txtconperson"
                                        ErrorMessage="Enter Contact person in case of emergency." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Contact No</label>
                                    <asp:TextBox ID="txtcontactno" runat="server" class="form-control" placeholder="Enter Contact No" onkeypress="return isNumberKey(event)" MaxLength="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtcontactno" CssClass="validationmsg" runat="server" ControlToValidate="txtcontactno"
                                        ErrorMessage="Enter Contact No." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Relation</label>
                                    <asp:TextBox ID="txtrelation" runat="server" class="form-control" placeholder="Enter Relation"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvrelation" CssClass="validationmsg" runat="server" ControlToValidate="txtrelation"
                                        ErrorMessage="Enter Relation." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Address</label>
                                    <asp:TextBox ID="txtaddress" TextMode="MultiLine" runat="server" placeholder="Enter Address" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtaddress" CssClass="validationmsg" runat="server" ControlToValidate="txtaddress"
                                        ErrorMessage="Enter Address." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row col-md-12" id="DivEmergencyDetailsMain" runat="server" visible="false">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application Status</label>
                                    <asp:Label ID="LblStatusED" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-10" id="DivRemerksED" runat="server">
                                <div class="form-group">
                                    <label>Remerks</label>
                                    <asp:TextBox ID="txtRemerksEd" ReadOnly="true" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="7" style="height:140px; background: yellow;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <!-- Education -->
            <div class="card card-info" style="display:none;">
                <div class="card-header">
                    <h3 class="card-title"><i class="fas fa-asterisk"></i>Please give references of persons who know you professionally and Socially
                    </h3>
                </div>
                <asp:Panel ID="PanelReferences" runat="server" Enabled="false">
                    <div class="card-body">
                        <div class="education-info">
                            <div class="row form-row education-cont">
                                <div class="col-12 col-md-12 col-lg-12">
                                    <div class="row form-row">
                                        <div class="col-12 col-md-6 col-lg-3">
                                            <div class="form-group">
                                                <asp:HiddenField ID="hfReferralId" runat="server" />
                                                <label>Person Name</label>
                                                <asp:TextBox ID="txtKnownPersonName" onkeypress="return lettersWithSpaceOnly(event)" placeholder="Enter Person Name" runat="server" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtKnownPersonName" CssClass="validationmsg" runat="server" ControlToValidate="txtKnownPersonName"
                                                    ErrorMessage="Enter Person Name." SetFocusOnError="true" ValidationGroup="btnKnowPersonDetails" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-6 col-lg-3">
                                            <div class="form-group">
                                                <label>Position</label>
                                                <asp:TextBox ID="txtPosition" runat="server" class="form-control" placeholder="Enter Position"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtPosition" CssClass="validationmsg" runat="server" ControlToValidate="txtPosition"
                                                    ErrorMessage="Enter Position." SetFocusOnError="true" ValidationGroup="btnKnowPersonDetails" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>


                                        <div class="col-12 col-md-12 col-lg-3">
                                            <div class="form-group mb-0">
                                                <label>RelationShip</label>
                                                <asp:TextBox ID="txtRelatioship" runat="server" onkeypress="return lettersWithSpaceOnly(event)" placeholder="Enter RelationShip" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtRelatioship" CssClass="validationmsg" runat="server" ControlToValidate="txtRelatioship"
                                                    ErrorMessage="Enter RelationShip." SetFocusOnError="true" ValidationGroup="btnKnowPersonDetails" ForeColor="Red"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="col-12 col-md-12 col-lg-3">
                                            <div class="form-group mb-0">
                                                <label>Years Known</label>
                                                <asp:TextBox ID="txtYearsKnown" runat="server" onkeypress="return isNumberKey(event)" placeholder="Enter Years Known" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtYearsKnown" CssClass="validationmsg" runat="server" ControlToValidate="txtYearsKnown"
                                                    ErrorMessage="Enter Years Known." SetFocusOnError="true" ValidationGroup="btnKnowPersonDetails" ForeColor="Red"></asp:RequiredFieldValidator>

                                            </div>

                                        </div>
                                        <div class="col-12 col-md-6 col-lg-3">
                                            <div class="form-group">
                                                <label>Mobile No</label>
                                                <asp:TextBox ID="txtTelePhone" runat="server" TextMode="Phone" MaxLength="10" placeholder="Enter Mobile No" onkeypress="return isNumberKey(event)" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtTelePhone" CssClass="validationmsg" runat="server" ControlToValidate="txtTelePhone"
                                                    ErrorMessage="Enter Mobile No." SetFocusOnError="true" ValidationGroup="btnKnowPersonDetails" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-8 col-lg-6">
                                            <div class="form-group">
                                                <label>Address</label>
                                                <asp:TextBox ID="txtAddressreferral" TextMode="MultiLine" runat="server" placeholder="Enter Address" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtAddressreferral" CssClass="validationmsg" runat="server" ControlToValidate="txtAddressreferral"
                                                    ErrorMessage="Enter Address." SetFocusOnError="true" ValidationGroup="btnKnowPersonDetails" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-2 col-lg-2">
                                            <div class="form-group">
                                                <br />
                                                <asp:Button ID="btnKnowPersonDetails" CausesValidation="true" ValidationGroup="btnKnowPersonDetails" runat="server" CssClass="btn btn-success submit-btn" OnClick="btnKnowPersonDetails_Click" Style="float: right;" Text="Add Details" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row form-row" style="padding-top: 5px !important;">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblCount" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                <asp:GridView ID="grdReferalDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist...">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr&nbspno" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1  %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Name" HeaderText="Person Name" />
                                                        <asp:BoundField DataField="Position" HeaderText="Position" />
                                                        <asp:BoundField DataField="MobileNO" HeaderText="Mobile No." />
                                                        <asp:BoundField DataField="RelationShip" HeaderText="RelationShip" />
                                                        <asp:BoundField DataField="Address" HeaderText="Address" />
                                                        <asp:BoundField DataField="YearsKnown" HeaderText="Years Known" />
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <div class="btn-group">
                                                                    <asp:LinkButton ID="lbtnEdit" CausesValidation="false" runat="server" OnClick="lbtnEdit_Click" data-original-title="Edit" CssClass="btn btn-sm show-tooltip">Edit</asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnDelete" CommandName="eDelete" runat="server" SkinID="lDelete" OnClick="lbtnDelete_Click" CausesValidation="false" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip">Delete</asp:LinkButton>
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
                        <div class="row col-md-12" id="DivReferencesMain" runat="server" visible="false">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application Status</label>
                                    <asp:Label ID="LblStatusReferences" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-10" id="DivRemerksReferences" runat="server">
                                <div class="form-group">
                                    <label>Remerks</label>
                                    <asp:TextBox ID="txtRemerksReferences" ReadOnly="true" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="7" style="height:140px; background: yellow;"></asp:TextBox>
                                    <%--<asp:Label ID="LblRemerksFMI" runat="server" CssClass="form-control" BackColor="LightGray"></asp:Label>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="submit-section submit-btn-bottom" style="padding-top: 5px !important; margin-bottom: 5px;">
                <div class="row form-row">
                    <div class="col-md-5">
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnPrivious" runat="server" Text="Previous" CssClass="btn btn-info submit-btn" OnClick="btnPrivious_Click" CausesValidation="false" />
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-secondary submit-btn" Style="" OnClick="btnSubmit_Click" Text="Next" />
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
    <script type="text/javascript">
        document.addEventListener('contextmenu', (e) => {
            e.preventDefault();
        }
          );
        document.onkeydown = function (e) {
            if (event.keyCode == 123) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'C'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
                return false;
            }
        }
    </script>
</asp:Content>
