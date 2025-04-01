<%@ Page Title="" Language="C#" MasterPageFile="~/Recruitment/Career.Master" AutoEventWireup="true" CodeBehind="ResearchTeachingPosts.aspx.cs" Inherits="Unmehta.WebPortal.Web.Recruitment.ResearchTeachingPosts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Teaching Posts Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h1>Teaching Posts Details</h1>
            <ul class="page-breadcrumb">
                <li><a href="index.html">Home</a></li>
                <li>/</li>
                <li>Teaching Posts Details</li>
            </ul>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <div class="col-md-7 col-lg-12">
            <!-- Work Performance -->
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Work Performance</h4>
                    <hr />
                    <div class="row form-row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>(A) Independent work with result :</label>
                                <asp:TextBox ID="txtIndependentWorkResult" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control txtletterUpper" placeholder="Independent work with result"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>(B) Work under supervision :</label>
                                <asp:TextBox ID="txtWorkUnderSupervision" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control txtletterUpper" placeholder="Work under supervision"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Work Performance-->
            <!-- Details Of Research Paper Publication / Acceptance (Indexed Journal)-->
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Details Of Research Paper Publication / Acceptance (Indexed Journal) :</h4>
                    <hr />
                    <div class="row form-row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:HiddenField ID="hfIndexJournalId" runat="server" Value="0" />
                                <label>Name Of The Journal</label>
                                <asp:TextBox ID="txtNameOfTheJournal" runat="server" CssClass="form-control txtletterUpper" placeholder="Name Of The Journal" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvNameOfTheJournal" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtNameOfTheJournal"
                                    ErrorMessage="Enter name of the journal." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Topic</label>
                                <asp:TextBox ID="txtTopic" runat="server" CssClass="form-control txtletterUpper" placeholder="Topic" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvTopic" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtTopic"
                                    ErrorMessage="Enter topic." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Month</label>
                                <asp:DropDownList ID="ddlMonth" CssClass="form-control select" runat="server">
                                    <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                    <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvMonth" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="ddlMonth"
                                    ErrorMessage="Select month." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Year</label>
                                <asp:DropDownList ID="ddlYear" CssClass="form-control select" runat="server">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvYear" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="ddlYear"
                                    ErrorMessage="Select year." InitialValue="Select" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>National / International <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtNationalInternational" runat="server" CssClass="form-control txtletterUpper" placeholder="National / International" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvDesignation" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtNationalInternational"
                                    ErrorMessage="Enter national / international." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Publication / Acceptance <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtPublicationAcceptance" runat="server" CssClass="form-control txtletterUpper" placeholder="Publication / Acceptance"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvPublicationAcceptance" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtPublicationAcceptance"
                                    ErrorMessage="Enter publication / acceptance." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Button ID="btnAddIndexedJournal" runat="server" CssClass="btn btn-primary submit-btn" Style="float: right;" Text="Add Indexed Journal" OnClick="btnAddIndexedJournal_Click" />
                        </div>
                    </div>
                    <div class="row form-row" style="padding-top: 5px !important;">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lblCount" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                <asp:GridView ID="gvIndexedJournal" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,Month" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                    AllowPaging="true" AllowSorting="false" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NameOfTheJournal" HeaderText="Name Of The Journal" />
                                        <asp:BoundField DataField="Topic" HeaderText="Topic" />
                                        <asp:BoundField DataField="MonthName" HeaderText="Month" />
                                        <asp:BoundField DataField="Year" HeaderText="Year" />
                                        <asp:BoundField DataField="NationalInternational" HeaderText="National / International" />
                                        <asp:BoundField DataField="PublicationAcceptance" HeaderText="Publication / Acceptance" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="lbtnIndexedJournalEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="lbtnIndexedJournalEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnIndexedJournalDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="lbtnIndexedJournalDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash"></i></asp:LinkButton>
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
            <!-- /Details Of Research Paper Publication / Acceptance (Indexed Journal)-->
            <!-- Details Of Research Paper Publication / Acceptance (Non-Indexed Journal)-->
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Details Of Research Paper Publication / Acceptance (Non-Indexed Journal) :</h4>
                    <hr />
                    <div class="row form-row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:HiddenField ID="hfNonIndexJournalId" runat="server" Value="0" />
                                <label>Name Of The Journal</label>
                                <asp:TextBox ID="txtNonIndexNameOfTheJournal" runat="server" CssClass="form-control txtletterUpper" placeholder="Name Of The Journal" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvNonIndexNameOfTheJournal" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtNonIndexNameOfTheJournal"
                                    ErrorMessage="Enter name of the journal." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Month</label>
                                <asp:DropDownList ID="ddlNonIndexMonth" CssClass="form-control select" runat="server">
                                    <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                    <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvNonIndexMonth" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="ddlNonIndexMonth"
                                    ErrorMessage="Select month." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Year</label>
                                <asp:DropDownList ID="ddlNonIndexYear" CssClass="form-control select" runat="server">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvNonIndexYear" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="ddlNonIndexYear"
                                    ErrorMessage="Select year." InitialValue="Select" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>National / International <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtNonIndexNationalInternational" runat="server" CssClass="form-control txtletterUpper" placeholder="National / International" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvNonIndexNationalInternational" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtNonIndexNationalInternational"
                                    ErrorMessage="Enter national / international." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Publication / Acceptance <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtNonIndexPublicationAcceptance" runat="server" CssClass="form-control txtletterUpper" placeholder="Publication / Acceptance" onkeypress="return lettersWithSpaceOnly(event);"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvNonIndexPublicationAcceptance" CssClass="validationmsg" ForeColor="Red" runat="server" ControlToValidate="txtNonIndexPublicationAcceptance"
                                    ErrorMessage="Enter publication / acceptance." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Button ID="btnAddNonIndexedJournal" runat="server" CssClass="btn btn-primary submit-btn" Style="float: right;" Text="Add Non-Indexed Journal" OnClick="btnAddNonIndexedJournal_Click" />
                        </div>
                    </div>
                    <div class="row form-row" style="padding-top: 5px !important;">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                <asp:GridView ID="gvNonIndexedJournal" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,Month" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                    AllowPaging="true" AllowSorting="false" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1  %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NameOfTheJournal" HeaderText="Name Of The Journal" />
                                        <asp:BoundField DataField="MonthName" HeaderText="Month" />
                                        <asp:BoundField DataField="Year" HeaderText="Year" />
                                        <asp:BoundField DataField="NationalInternational" HeaderText="National / International" />
                                        <asp:BoundField DataField="PublicationAcceptance" HeaderText="Publication / Acceptance" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="lbtnNonIndexedJournalEdit" CausesValidation="false" runat="server" data-original-title="Edit" OnClick="lbtnNonIndexedJournalEdit_Click" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnNonIndexedJournalDelete" CommandName="eDelete" runat="server" SkinID="lDelete" CausesValidation="false" OnClick="lbtnNonIndexedJournalDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash"></i></asp:LinkButton>
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
            <!-- /Details Of Research Paper Publication / Acceptance (Indexed Journal)-->
            <!-- Final Bolck -->
            <div class="card">
                <div class="card-body">
                    <div class="row form-row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label><b>Note :</b> For Additional Research Paper please mention in column - other additional information</label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Conference attendance and paper presentation / Speaker : (For teaching posts)</label>
                                <asp:TextBox ID="txtConferenceAttendance" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control txtletterUpper" placeholder="Conference attendance and paper presentation / Speaker : (For teaching posts)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Final Bolck -->
            <div class="submit-section submit-btn-bottom" style="padding-top: 5px !important;">
                <div class="row form-row">
                    <div class="col-md-5">
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnPrivious" runat="server" Text="Previous" CssClass="btn btn-primary submit-btn" OnClick="btnPrivious_Click" CausesValidation="false" />
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnSubmit_Click" CausesValidation="false" />
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Bottom" runat="server">
</asp:Content>
