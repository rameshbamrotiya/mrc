<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="PrintApplication.aspx.cs" Inherits="Unmehta.WebPortal.Web.Admin.Recruitment.PrintApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Candidate Details</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headCss" runat="server">
    <script type="text/javascript">


        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            var mywindow = window.open();
            var is_chrome = Boolean(mywindow.chrome);
            mywindow.document.write(printContents);
            if (is_chrome) {
                setTimeout(function () { // wait until all resources loaded 
                    mywindow.document.close(); // necessary for IE >= 10
                    mywindow.focus(); // necessary for IE >= 10
                    mywindow.print();  // change window to winPrint
                    mywindow.close();// change window to winPrint
                }, 250);
            }
            else {
                mywindow.document.close(); // necessary for IE >= 10
                mywindow.focus(); // necessary for IE >= 10

                mywindow.print();
                mywindow.close();
            }

            document.body.innerHTML = originalContents;
            document.forms[0].target = "_blank";

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_header" runat="server">
    <!-- begin::page-header -->
    <div class="page-header">
        <div class="container-fluid d-sm-flex justify-content-between">
            <h4>Candidate Details </h4>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">Recruitment</a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Candidate Details </li>
                </ol>
            </nav>
        </div>
    </div>
    <!-- end::page-header -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyPart" runat="server">
    <div class="card">
        <div class="card-body">
            
                    <button runat="server" id="btnLogOut" class="btn btn-primary" onserverclick="btnLogOut_ServerClick" causesvalidation="false" title="Search">
                        Back
                    </button>
            <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary submit-btn" OnClientClick="printDiv('printableArea')" Text="Print" />
        <%--<button type="button" id="print" class="btn btn-primary" onclick="printDiv('printableArea')">Print</button>--%>
        <%--<button type="button" id="print" class="btn btn-primary" runat="server" onserverclick="print_ServerClick">Print</button>--%>        
        <div id="printableArea" runat="server">
            <div id="headerDiv" runat="server">
                <table cellspacing="0" cellpadding="6" width="100%">
                    <tr>
                        <td style="width: 15%;"></td>
                        <td class="style29" style="width: 70%; text-align: center;">
                            <img id="imgLogo" src="<%=ResolveUrl("~/Hospital/assets/img/logo.png") %>" height="50" width="80" style="text-align: center;" />
                            <p style="margin: 0px">
                                U. N. Mehta Institute of Cardiology & Research Centre
                                <br />
                                (Affiliated to B J Medical Collage, Ahmedabad)
                            </p>
                            <%--<p style="margin: 0px; font-size: 14px;">Block No. 2, 2nd Floor, C & D Wing, Karmayogi Bhavan Sector - 10 A, Gandhinagar - 382010 Gujarat.</p>--%>
                            <br />
                        </td>
                        <td>
                            <p>
                                <img id="ImgAppi" runat="server" height="109" width="101" align="right" />
                            </p>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td class="style29" style="text-align: center; font-size: 16pt; width: 50%;">Application for the post of <span id="Label1" runat="server" text=""></span>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="basicInfoDiv" runat="server" style="page-break-inside: initial !important;">
                <br />
                <table cellspacing="0" cellpadding="3" border='0' width="100%">
                    <tr>
                        <td class="style1" style="font-size: 11pt;"><b>Application Confirmation Number :</b>
                        </td>
                        <td class="srlast" style="font-size: 11pt;">
                            <span id="lblApplicationNo" runat="server" text="[applicationnumber]"></span>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="font-size: 11pt;"><b>Applicant’s Name (In Block 
                    Letters) : </b>
                        </td>
                        <td class="srlast" style="font-size: 11pt;">
                            <span id="firstname" runat="server" text=""></span>
                            <br />
                        </td>
                    </tr>
                    <tr runat="server">
                        <td class="style1" style="font-size: 11pt;"><b>Father’s /Husband’s Name (In Block Letters) :</b>
                        </td>
                        <td class="style52" style="font-size: 11pt;">
                            <span id="Ffirstname" runat="server" text=""></span>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="font-size: 11pt;"><b>Residential address for correspondence (In Block Letters) : </b>
                        </td>
                        <td class="style52" style="font-size: 11pt;">
                            <span id="address" runat="server" text="" cssclass="style1"></span>
                            <span id="lblpin" runat="server" text="" visible="False"></span>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="font-size: 11pt;"><b>Mobile Number :</b></td>
                        <td class="style52" style="font-size: 11pt;">
                            <span id="LblMobNo" runat="server" text=""></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="font-size: 11pt;"><b>E-mail id : </b>
                        </td>
                        <td class="style52" style="font-size: 11pt;">
                            <span id="lblmail" runat="server" text=""></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="font-size: 11pt;"><b>Date of Birth (DD/MM/YYYY) : </b>
                        </td>
                        <td class="style52" style="font-size: 11pt;">
                            <span id="LblDob" runat="server" text=""></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="font-size: 11pt;"><b>Gender :</b>
                        </td>
                        <td class="style52" style="font-size: 11pt;">
                            <span id="lblgen" runat="server" text=""></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="font-size: 11pt;"><b>Caste category: (Whether belongs to ST/OBC/UR) : </b>
                        </td>
                        <td class="style52" style="font-size: 11pt;">
                            <span id="LblCast" runat="server" text=""></span>
                        </td>
                    </tr>
                </table>
                <span id="tblLanguage" runat="server"></span>
            </div>
            <div id="experienceDiv" runat="server">                
                <span id="tableedu" runat="server"></span>
                <div runat="server" id="Divexp">
                    <span id="tableexp" runat="server"></span>
                </div>
                <br />
                <span id="lblexpre" runat="server"></span>
            </div>
            <div id="finalDiv" runat="server">
                <br />
                Declaration:       
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     I,            
                <span id="LblFnameRe" runat="server" text=""></span>
                &nbsp;declare that I fulfill all the conditions of eligibility regarding age limit
        and Education Qualification and other qualification etc., for the post of            
                <span id="lblpostre" runat="server" text="" style="font-weight: bold;"></span>.       
            </p>
                <p>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        I hereby certify that the foregoing information are true and correct to the best of my knowledge.I have not suppressed any material fact or factual information in the above statement.If in case, I have given wrong information or suppressed any material fact or factual information, my candidature is liable to be cancelled or my service is also liable to be terminated without giving any notice or reasons thereof. I know that, for such suppression, I shall be liable for criminal prosecution.                  
                </p>
                <p>
                    &nbsp;           
                </p>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%; white-space: nowrap;">
                            <div>Date: <span id="lblDate1" runat="server" text="[dated]"></span></div>
                        </td>
                        <td style="width: 50%;">
                            <img id="imgsignappi" align="left;" runat="server" height="39" width="140" style="text-align: right; margin-left: 600px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%;"></td>
                        <td style="width: 50%; text-align: right;">
                            <span id="lBLfNAMESIGN" runat="server" text="[applicantname]" style="text-align: right;"></span>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footerJS" runat="server">
</asp:Content>
