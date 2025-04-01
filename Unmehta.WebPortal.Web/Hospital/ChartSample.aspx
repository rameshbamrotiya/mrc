<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChartSample.aspx.cs" Inherits="Unmehta.WebPortal.Web.Hospital.ChartSample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <!-- Plugin styles -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/bundle.css")%>" type="text/css">


    <!-- Prism -->
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/Template/html/vendors/prism/prism.css")%>" type="text/css">

    <!-- Plugin scripts -->
    <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/bundle.js")%>"></script>


    <!-- Chartjs -->
    <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/charts/chartjs/chart.min.js")%>"></script>
    <div class="colors">
        <!-- To use theme colors with Javascript -->
        <div class="bg-primary"></div>
        <div class="bg-primary-bright"></div>
        <div class="bg-secondary"></div>
        <div class="bg-secondary-bright"></div>
        <div class="bg-info"></div>
        <div class="bg-info-bright"></div>
        <div class="bg-success"></div>
        <div class="bg-success-bright"></div>
        <div class="bg-danger"></div>
        <div class="bg-danger-bright"></div>
        <div class="bg-warning"></div>
        <div class="bg-warning-bright"></div>
    </div>

    <!-- Apex chart -->
    <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/charts/apex/apexcharts.min.js?"+DateTime.Now.ToString())%>"></script>


    <!-- Circle progress -->
    <script src="<%= ResolveUrl("~/Admin/Template/html/vendors/circle-progress/circle-progress.min.js?"+DateTime.Now.ToString())%>"></script>



    <script src="<%= ResolveUrl("~/Admin/Script/MainPage.js?"+DateTime.Now.ToString())%>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="card">
                <div class="card-body">

                    <asp:DropDownList ID="ddlChartType" runat="server" onchange="OnChangeChartType(this.value);">
                        <asp:ListItem Text="Bar" Value="Bar"></asp:ListItem>
                        <asp:ListItem Text="Pie" Value="Pie"></asp:ListItem>
                        <asp:ListItem Text="line" Value="line"></asp:ListItem>
                    </asp:DropDownList>
					<div id="chartContainer" style="height: 400px; width: 100%;"></div>
                    <div id="chartreport">
                        <canvas id="chartjs_one" class="" height="150"></canvas>

                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="<%= ResolveUrl("~/Admin/Script/App.js?"+DateTime.Now.ToString())%>"></script>
	<script src="<%= ResolveUrl("~/Hospital/assets/js/canvasjs.min.js?"+DateTime.Now.ToString())%>"></script>
    <script>


        $(document).ready(function () {

            //"line", "chartreport", "chartjs_one", "/Hospital/ChartSample.aspx/GetAllLineLineSubDetail"

            ChangeChartType("Bar", "chartreport", "chartjs_one", "/Hospital/ChartSample.aspx/GetAllSubDetail", null);
        });

        //window.onload = function () {

        //    var chart = new CanvasJS.Chart("chartContainer", {
        //        animationEnabled: true,
        //        theme: "light2",
        //        title: {
        //            text: "Department"
        //        },
        //        axisX: {
        //            valueFormatString: "YYYY",
        //            crosshair: {
        //                enabled: true,
        //                snapToDataPoint: true
        //            }
        //        },
        //        axisY: {
        //            title: "Number of Visits",
        //            includeZero: true,
        //            crosshair: {
        //                enabled: true
        //            }
        //        },
        //        toolTip: {
        //            shared: true
        //        },
        //        legend: {
        //            cursor: "pointer",
        //            verticalAlign: "bottom",
        //            horizontalAlign: "left",
        //            dockInsidePlotArea: true,
        //            itemclick: toogleDataSeries
        //        },
        //        data: [{
        //            type: "line",
        //            showInLegend: true,
        //            name: "Total Visit",
        //            //markerType: "square",
        //            //xValueFormatString: "DD MMM, YYYY",
        //            //color: "#F08080",
        //            dataPoints: [
		//				{ x: new Date(2012, 0, 3), y: 650 },
		//				{ x: new Date(2013, 0, 4), y: 700 },
		//				{ x: new Date(2014, 0, 5), y: 710 },
		//				{ x: new Date(2015, 0, 6), y: 658 },
		//				{ x: new Date(2016, 0, 7), y: 734 },
		//				{ x: new Date(2018, 0, 8), y: 963 },
		//				{ x: new Date(2019, 0, 9), y: 847 },
		//				{ x: new Date(2020, 0, 10), y: 853 }
        //            ]
        //        },
		//		{
		//		    type: "line",
		//		    showInLegend: true,
		//		    name: "Unique Visit",
		//		    //lineDashType: "dash",
		//		    dataPoints: [
		//				{ x: new Date(2012, 0, 3), y: 510 },
		//				{ x: new Date(2013, 0, 4), y: 560 },
		//				{ x: new Date(2014, 0, 5), y: 540 },
		//				{ x: new Date(2015, 0, 6), y: 558 },
		//				{ x: new Date(2016, 0, 7), y: 544 },
		//				{ x: new Date(2018, 0, 8), y: 693 },
		//				{ x: new Date(2019, 0, 9), y: 657 },
		//				{ x: new Date(2020, 0, 10), y: 663 }
		//		    ]
		//		}]
        //    });
        //    chart.render();

        //    function toogleDataSeries(e) {
        //        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
        //            e.dataSeries.visible = false;
        //        } else {
        //            e.dataSeries.visible = true;
        //        }
        //        chart.render();
        //    }

        //}


        function OnChangeChartType(value) {
            ChangeChartType(value, "chartreport", "chartjs_one", "/Hospital/ChartSample.aspx/GetAllSubDetail", null);
        }

    </script>
</body>
</html>
