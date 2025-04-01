<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="Unmehta.WebPortal.Web.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - Statistics 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
	<style>
		.fixTableHead {
      overflow-y: auto;
      max-height: 500px;
    }
    .fixTableHead thead th {
      position: sticky;
      top: 0;
    }
    table {
      border-collapse: collapse;        
      width: 100%;
    }
    th,
    td {
      padding: 8px 15px;
      border: 2px solid #529432;
    }
    th {
      background: #ABDD93;
    }
	</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
        <div class="container-fluid">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li>Statistics</li>
            </ul>
        </div>
    </div>
    <div class="content">
			<div class="container">
				<div class="row">
					<%=strStatisticsMain %>
					<!-- Doctor Details Tab -->
					<%--<div class="col-lg-12">
						<!-- Tab Menu -->						
						<nav class="user-tabs mb-4">
							<ul class="nav nav-tabs nav-tabs-bottom">
								<li class="nav-item">
									<a class="nav-link active" href="#dap_Faculty_1" data-toggle="tab">Data Summary</a>
								</li>
								<li class="nav-item">
									<a class="nav-link " href="#dap_introduction_1" data-toggle="tab">EMCS Scheme</a>
								</li>
								<li class="nav-item">
									<a class="nav-link" href="#dap_Services_1" data-toggle="tab">Patient Benevolent Scheme Data</a>
								</li>
							</ul>
						</nav>						
						<!-- Tab Content -->
						<div class="tab-content">
							<!-- EMCS Scheme Content -->
							<div role="tabpanel" id="dap_introduction_1" class="tab-pane fade ">
								<div class="row">
									<div class="table-responsive">
                                        <%=strEMCSChartTable %>										
									</div>
								</div>
							</div>
							<!-- /EMCS Scheme Content -->
							<!-- Data Summary Content -->
							<div role="tabpanel" id="dap_Services_1" class="tab-pane fade">
								<div class="accordion md-accordion accordion-blocks" id="accordionscheme" role="tablist" aria-multiselectable="true">								
                                        <%=strSchemaChartTable %>
								</div>
							</div>
							<!-- /Data Summary Content -->
							<!-- Data Summary Content -->
							<div role="tabpanel" id="dap_Faculty_1" class="tab-pane fade active show">
								<div class="accordion md-accordion accordion-blocks" id="accordionschemedata" role="tablist" aria-multiselectable="true">									
                                        <%=strDepartmentAndOtherChartTable %>
								</div>
							</div>
							<!-- /Data Summary Content -->
						</div>
					</div>--%>

				</div>
			</div>
		</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
</asp:Content>
