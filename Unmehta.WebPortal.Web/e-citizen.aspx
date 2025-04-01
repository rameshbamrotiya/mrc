<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="e-citizen.aspx.cs" Inherits="Unmehta.WebPortal.Web.e_citizen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - E-citizen
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
    <style>
        .validate {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <!-- Breadcrumb -->
    <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
        <div class="container">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li>E-citizen</li>
            </ul>
        </div>
    </div>
    <!-- /Breadcrumb -->
    <div class="content">
        <div class="container">
            <div class="row">
                <!-- Doctor Details Tab -->
                <div class="col-lg-9">
                    <div class="card">
                        <div class="card-body pt-0">
                            <div class="tab-content opdtiming">
                                <div class="tab-pane active" id="tab_a">
                                    <!-- Tab Menu -->
                                    <div class="widget about-widget">
                                        <h4 class="widget-title">Patient Feedback</h4>
                                        <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
                                        <p>
                                            <%-- Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
												tempor incididunt ut labore et dolore
												magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco
												laboris nisi ut aliquip ex ea commodo
												consequat. Duis aute irure dolor in reprehenderit in voluptate velit
												esse cillum dolore eu fugiat nulla
												pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa
												qui officia deserunt mollit anim id est
												laborum.--%>
                                            <%=strFeedback%>
                                        </p>
                                    </div>
                                    <p style="color: red;">Fields marked with * are mandatory</p>

                                    <%--   <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="fbform" CssClass="validate" />--%>
                                    <div class="row form-row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtfullname" autocomplete="false" class="form-control" placeholder="Full Name *" runat="server" ValidationGroup="fbform"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvfullname" runat="server" ControlToValidate="txtfullname" ErrorMessage="Full Name" ValidationGroup="fbform" CssClass="validate"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtEmail" autocomplete="false" class="form-control" placeholder="Email *" runat="server" ValidationGroup="fbform"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ValidationGroup="fbform" CssClass="validate"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtMno" autocomplete="false" class="form-control" TextMode="Number" MaxLength="15" placeholder="Mobile Number *" runat="server" ValidationGroup="fbform"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMno" runat="server" ControlToValidate="txtMno" ErrorMessage="Mobile Number" ValidationGroup="fbform" CssClass="validate"></asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="REFmoblie" runat="server" ValidationGroup="fbform"
                                                    ErrorMessage="Please enter valid mobile no" ControlToValidate="txtMno" CssClass="validate" ValidationExpression="^[\d+]{0,15}$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlVisitType" class="form-control" runat="server">
                                                    <asp:ListItem Value="0" Selected="True"> -- Select Visit Type --</asp:ListItem>
                                                    <asp:ListItem Value="OPD">OPD</asp:ListItem>
                                                    <asp:ListItem Value="IPD">IPD</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvvisittype" runat="server" ControlToValidate="ddlVisitType" ErrorMessage="Select Visit Type" InitialValue="0" ValidationGroup="fbform" CssClass="validate"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtVisitNumber" class="form-control" autocomplete="false" MaxLength="3" TextMode="Number"  placeholder="Visit Number *" runat="server" ValidationGroup="fbform"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvVisitNumber" runat="server" ControlToValidate="txtVisitNumber" ErrorMessage="Visit Number" ValidationGroup="fbform" CssClass="validate"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="regexvisitnumber" runat="server" ValidationGroup="fbform"
                                                    ErrorMessage="Please enter valid Visit Number" ControlToValidate="txtVisitNumber" CssClass="validate" ValidationExpression="^[\d+]{0,3}$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtCountry" class="form-control" autocomplete="false" placeholder="Country *" runat="server" ValidationGroup="fbform"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="txtCountry" ErrorMessage="Country" ValidationGroup="fbform" CssClass="validate"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="regexpcountry" CssClass="validate" runat="server" ErrorMessage="Only upper and lower case A-Z entered here." ValidationGroup="fbform" ControlToValidate="txtCountry" ValidationExpression="^[A-Za-z]*$" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtState" class="form-control" autocomplete="false" placeholder="State *" runat="server" ValidationGroup="fbform"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtState" ErrorMessage="State" ValidationGroup="fbform" CssClass="validate"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="regexstate" CssClass="validate" runat="server" ErrorMessage="Only upper and lower case A-Z entered here." ValidationGroup="fbform" ControlToValidate="txtState" ValidationExpression="^[A-Za-z]*$" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtCity" class="form-control" autocomplete="false" placeholder="City *" runat="server" ValidationGroup="fbform"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="tfvCity" runat="server" ControlToValidate="txtCity" ErrorMessage="City" ValidationGroup="fbform" CssClass="validate"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="regexcity" CssClass="validate" runat="server" ErrorMessage="Only upper and lower case A-Z entered here." ValidationGroup="fbform" ControlToValidate="txtCity" ValidationExpression="^[A-Za-z]*$" />
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtMsg" autocomplete="false" class="form-control" TextMode="MultiLine" Rows="5" placeholder="Message *" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group text-center">
                                                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" runat="server" ValidationGroup="fbform" Text="Submit" />
                                                <asp:Button ID="btnReset" OnClick="btnReset_Click" CssClass="btn btn-primary" runat="server" ValidationGroup="fbform" Text="Reset" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /Tab Menu -->
                                </div>
                                <div class="tab-pane" id="tab_b">
                                    <div class="widget about-widget">
                                        <h4 class="widget-title">Right to Information</h4>
                                        <p>
                                            <%-- Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
												tempor incididunt ut labore et dolore
												magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco
												laboris nisi ut aliquip ex ea commodo
												consequat. Duis aute irure dolor in reprehenderit in voluptate velit
												esse cillum dolore eu fugiat nulla
												pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa
												qui officia deserunt mollit anim id est
												laborum.--%>


                                            <%=strRTIDesc%>
                                        </p>
                                    </div>
                                    <div class="details-listview">
                                        <ul>
                                            <%-- <li>
                                                <h4>UNMICRC Right to Information</h4>
                                                <a href="#" class="btn btn-outline-primary">Click Here</a>
                                            </li>--%>
                                            <asp:Repeater ID="rptRTI" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <h4><%#Eval("DocTitle")%></h4>
                                                        <a href="<%# Page.ResolveUrl( Eval("DocURL").ToString())%>" target="_blank" class="btn btn-outline-primary">Click Here</a>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab_c">
                                    <div class="widget about-widget">
                                        <h4 class="widget-title">Committee</h4>
                                        <p>
                                            <%--  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
												tempor incididunt ut labore et dolore
												magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco
												laboris nisi ut aliquip ex ea commodo
												consequat. Duis aute irure dolor in reprehenderit in voluptate velit
												esse cillum dolore eu fugiat nulla
												pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa
												qui officia deserunt mollit anim id est
												laborum.--%>

                                            <%=strcomitteeDesc%>
                                        </p>
                                    </div>
                                    <div class="details-listview">
                                        <ul>
                                            <asp:Repeater ID="rptCommitte" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <h4><%#Eval("DocTitle")%></h4>
                                                        <a href="<%# Page.ResolveUrl( Eval("DocURL").ToString())%>" target="_blank" class="btn btn-outline-primary">Click Here</a>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <%-- <li>
                                                <h4>Anti-Ragging Committee</h4>
                                                <a href="#" class="btn btn-outline-primary">Click Here</a>
                                            </li>
                                            <li>
                                                <h4>Ethics Committee</h4>
                                                <a href="#" class="btn btn-outline-primary">Click Here</a>
                                            </li>--%>
                                        </ul>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab_d">
                                    <div class="widget about-widget">

                                        <%=strHospitalGuide%>
                                        <%--<h4 class="widget-title">Patient Hospital Guide</h4>

                                        <a href="https://online.flippingbook.com/view/377113257/" class="fbo-embed"
                                            data-fbo-id="377113257" data-fbo-ratio="16:9" data-fbo-lightbox="yes" data-fbo-width="100%"
                                            data-fbo-height="auto" data-fbo-version="1" style="max-width: 100%">Untitled-1</a>
                                        <script async defer
                                            src="https://online.flippingbook.com/EmbedScriptUrl.aspx?m=redir&hid=377113257"></script>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="sidebar">
                        <div class="card widget-categories">
                            <div class="card-header">
                                <h4 class="card-title">Quick Links</h4>
                            </div>
                            <div class="card-body">
                                <ul class="categories nav nav-pills nav-stacked flex-column">
                                    <li><a href="#tab_a" class="active" data-toggle="pill">Patient Feedback</a></li>
                                    <li><a href="#tab_b" data-toggle="pill">Right to Information</a></li>
                                    <li><a href="#tab_c" data-toggle="pill">Committee</a></li>
                                    <li><a href="#tab_d" data-toggle="pill">Patient Hospital Guide</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Doctor Details Tab -->
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
    <%=strPackagesModels%>
</asp:Content>
