<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="Unmehta.WebPortal.Web.EventDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    UnMehta - EventDetails
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopStyle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <asp:HiddenField ID="startDate" runat="server" ClientIDMode="Static" Value="" />
    <div class="page-title">
        <img src="<%=strHeaderImage%>" class="img-fluid" alt="banner" />
        <div class="container-fluid">
            <ul class="page-breadcrumb">
                <li><a href="<%=ResolveUrl("~/") %>"><i class="fas fa-home"></i></a></li>
                <li>/</li>
                <li><a href="<%=ResolveUrl("~/Event") %>">Event</a></li>
                <li>/</li>
                <li>Event Details</li>
            </ul>
        </div>
    </div>
    <!-- Page Content -->

    <div class="content">
        <div class="container">
            <div class="row">
                <%=strListOfSubSectionDescription %>
            </div>
        </div>
    </div>
    <!-- /Page Content -->

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomJs" runat="server">
    <script>

        function strToDate(dtStr) {
            if (!dtStr) return null;


            dtStr = dtStr.replace("/", "-");
            dtStr = dtStr.replace("/", "-");
            let dateParts = dtStr.split("-");
            let timeParts = dateParts[2].split(" ")[1].split(":");
            dateParts[2] = dateParts[2].split(" ")[0];
            // month is 0-based, that's why we need dataParts[1] - 1
            return dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0], timeParts[0], timeParts[1], timeParts[2]);
        }


        function getTimeRemaining(endtime) {
            const total = strToDate(endtime) - Date.parse(new Date());
            const seconds = Math.floor((total / 1000) % 60);
            const minutes = Math.floor((total / 1000 / 60) % 60);
            const hours = Math.floor((total / (1000 * 60 * 60)) % 24);
            const days = Math.floor(total / (1000 * 60 * 60 * 24));
            debugger;
            return {
                total,
                days,
                hours,
                minutes,
                seconds
            };
        }

        function initializeClock(id, endtime) {
            const clock = document.getElementById(id);
            const daysSpan = clock.querySelector('.days');
            const hoursSpan = clock.querySelector('.hours');
            const minutesSpan = clock.querySelector('.minutes');
            const secondsSpan = clock.querySelector('.seconds');

            function updateClock() {
                const t = getTimeRemaining(endtime);

                daysSpan.innerHTML = t.days;
                hoursSpan.innerHTML = ('0' + t.hours).slice(-2);
                minutesSpan.innerHTML = ('0' + t.minutes).slice(-2);
                secondsSpan.innerHTML = ('0' + t.seconds).slice(-2);

                if (t.total <= 0) {
                    clearInterval(timeinterval);
                }
            }

            updateClock();
            const timeinterval = setInterval(updateClock, 1000);
        }

        const deadline = $("#startDate").val();
        initializeClock('clockdiv', deadline);
    </script>
</asp:Content>
