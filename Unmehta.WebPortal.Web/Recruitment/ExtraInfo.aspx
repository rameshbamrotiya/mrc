<%@ Page Title="" Language="C#" MasterPageFile="~/Recruitment/Career.Master" AutoEventWireup="true" CodeBehind="ExtraInfo.aspx.cs" Inherits="Unmehta.WebPortal.Web.Recruitment.ExtraInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Top" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Header" runat="server">
    <%-- <section class="page-title" style="background-image: url(assets/img/breadcum.jpg);">
        <div class="auto-container">
            <h1>Other Information</h1>
            <ul class="page-breadcrumb">
                <li><a href="index.html">Recruitment</a></li>
                <li>/</li>
                <li>Other Information</li>
            </ul>
        </div>
    </section>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <div class="col-md-7 col-lg-12">
            <!-- Basic Information -->
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Other Information</h4>
                    <div class="row form-row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Extra Curricular Activities/Hobbies: <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtExtraActivities" TextMode="MultiLine" runat="server" class="form-control txtletterUpper"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Membership of any professional/social bodies: <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtMemberShip" TextMode="MultiLine" runat="server" class="form-control txtletterUpper"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Do you suffer from any chronic illness? If yes, give details:</label>
                                <asp:TextBox ID="txtChronicillness" TextMode="MultiLine" runat="server" class="form-control txtletterUpper"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Has any court of law in India or abroad ever convicted you or/and any offence has been registered aginast you? If yes, give details(submit certified copy of FIR)</label>
                                <asp:TextBox ID="txtLawOffence" TextMode="MultiLine" runat="server" class="form-control txtletterUpper"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Describe yourself as a person in about 20 words:</label>
                                <asp:TextBox ID="txtselfDetails" TextMode="MultiLine" runat="server" class="form-control txtletterUpper"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Highlight your biggest achievement so far ad why?</label>
                                <asp:TextBox ID="txtAchievement" TextMode="MultiLine" runat="server" class="form-control txtletterUpper"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Tell us the most frustraing experience so far. What do you think about your biggest failure?</label>
                                <asp:TextBox ID="txtFrustratingExperinece" TextMode="MultiLine" runat="server" class="form-control txtletterUpper"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>What would you like to do in the next one year and where would you like to see yourself in the next three years?</label>
                                <asp:TextBox ID="txtFutreVision" TextMode="MultiLine" runat="server" class="form-control txtletterUpper"></asp:TextBox>

                            </div>
                        </div>


                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Contact Person in Case of Emergency(Person name)</label>
                                <asp:TextBox ID="txtEmergencyContact" runat="server" class="form-control txtletterUpper"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Contact No</label>
                                <asp:TextBox ID="txtEmergencyContactNo" runat="server" onkeypress="return isNumberKey(event)"  class="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Known person in UNMICRC, if any</label>
                                <asp:TextBox ID="txtPersoninUNMICR" runat="server" class="form-control txtletterUpper"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Basic Information -->
            <!-- Education -->
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Please give refrences of two persons who know you professionally and Socially:</h4>
                    <div class="education-info">
                        <div class="row form-row education-cont">
                            <div class="col-12 col-md-12 col-lg-12">
                                <div class="row form-row">
                                    <div class="col-12 col-md-6 col-lg-4">
                                        <div class="form-group">
                                            <asp:HiddenField ID="hfReferralId" runat="server" />
                                            <label>Person Name</label>
                                            <asp:TextBox ID="txtKnownPersonName"  onkeypress="return lettersWithSpaceOnly(event)"  runat="server" class="form-control txtletterUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4">
                                        <div class="form-group">
                                            <label>Position</label>
                                            <asp:TextBox ID="txtPosition" runat="server" class="form-control txtletterUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4">
                                        <div class="form-group">
                                            <label>Address</label>
                                            <asp:TextBox ID="txtAddress" runat="server" class="form-control txtletterUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4">
                                        <div class="form-group">
                                            <label>Telephone No.</label>
                                            <asp:TextBox ID="txtTelePhone" runat="server" onkeypress="return isNumberKey(event)"  class="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-4">
                                        <div class="form-group mb-0">
                                            <label>RelationShip</label>
                                            <asp:TextBox ID="txtRelatioship" runat="server"  onkeypress="return lettersWithSpaceOnly(event)" class="form-control txtletterUpper"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-4">
                                        <div class="form-group mb-0">
                                            <label>Years Known</label>
                                            <asp:TextBox ID="txtYearsKnown" runat="server" onkeypress="return isNumberKey(event)"  class="form-control txtletterUpper"></asp:TextBox>

                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <asp:Button ID="btnKnowPersonDetails" runat="server" CssClass="btn btn-primary submit-btn" OnClick="btnKnowPersonDetails_Click" Style="float: right;" Text="Add Details" />
                                    </div>
                                </div>
                                <div class="row form-row" style="padding-top: 5px !important;">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblCount" runat="server" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                            <asp:GridView ID="grdReferalDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="id" CssClass="table table-bordered table-hover table-striped" EmptyDataText="Record does not exist..."
                                                >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr no" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center"
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
                                                                <asp:LinkButton ID="lbtnEdit" CausesValidation="false" runat="server" OnClick="lbtnEdit_Click" data-original-title="Edit" CssClass="btn btn-sm show-tooltip"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnDelete" CommandName="eDelete" runat="server" SkinID="lDelete" OnClick="lbtnDelete_Click" CausesValidation="false" OnClientClick="javascript:return confirm('Are you sure you want to delete ?');" data-original-title="Delete" CssClass="btn btn-sm btn-danger show-tooltip"><i class="fa fa-trash"></i></asp:LinkButton>
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
                </div>
            </div>
            <div class="submit-section submit-btn-bottom" style="padding-top: 5px !important;">
                <div class="row form-row">
                    <div class="col-md-5">
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnPrivious" runat="server" Text="Previous" CssClass="btn btn-primary submit-btn" OnClick="btnPrivious_Click" CausesValidation="false" />
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary submit-btn" Style="" OnClick="btnSubmit_Click" Text="Next" />
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
