var colors = {
    primary: $('.colors .bg-primary').css('background-color'),
    primaryLight: $('.colors .bg-primary-bright').css('background-color'),
    secondary: $('.colors .bg-secondary').css('background-color'),
    secondaryLight: $('.colors .bg-secondary-bright').css('background-color'),
    info: $('.colors .bg-info').css('background-color'),
    infoLight: $('.colors .bg-info-bright').css('background-color'),
    success: $('.colors .bg-success').css('background-color'),
    successLight: $('.colors .bg-success-bright').css('background-color'),
    danger: $('.colors .bg-danger').css('background-color'),
    dangerLight: $('.colors .bg-danger-bright').css('background-color'),
    warning: $('.colors .bg-warning').css('background-color'),
    warningLight: $('.colors .bg-warning-bright').css('background-color'),
};

$(document).ready(function () {
    ClosePreloder();


    $('.clockpicker-demo').clockpicker({
        donetext: 'Done'
    });

    $('.datepicker-demo').datepicker({
        singleDatePicker: true,
        showDropdowns: true,
        dateFormat: 'dd/mm/yy'
    });
});

function RemoveImage(Element,Remvoe,hfField) {
    if (confirm('Are you sure want to delete? ')) {
        document.getElementById(Element).innerHTML = "";
        document.getElementById(hfField).value = "";
        document.getElementById(Remvoe).style.display = 'none';
    }
    return false;
}


function explodePie(e) {
    if (typeof (e.dataSeries.dataPoints[e.dataPointIndex].exploded) === "undefined" || !e.dataSeries.dataPoints[e.dataPointIndex].exploded) {
        e.dataSeries.dataPoints[e.dataPointIndex].exploded = true;
    } else {
        e.dataSeries.dataPoints[e.dataPointIndex].exploded = false;
    }
    e.chart.render();
}

function toogleDataSeries(e) {
    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
        e.dataSeries.visible = false;
    } else {
        e.dataSeries.visible = true;
    }
    chart.render();
}

function OpenPreloder() {
    $(".preloader").fadeIn(400, function () {
        setTimeout(function () {
        }, 500);
    });
}

function ClosePreloder() {
    $(".preloader").fadeOut(400, function () {
        setTimeout(function () {
        }, 500);
    });
}

function CreateDatePicker(date) {
    $(date).datepicker({
        singleDatePicker: true,
        showDropdowns: true,
        dateFormat: 'dd/mm/yy'
    });
}

function isNumberKey(e) {
    var result = false;
    try {
        var charCode = (e.which) ? e.which : e.keyCode;
        if ((charCode > 31) && (charCode >= 48 && charCode <= 57)) {
            result = true;
        }
    }
    catch (err) {
        //console.log(err);
    }
    return result;
}


function lettersOnly() {
    var charCode = event.keyCode;

    if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8)

        return true;
    else
        return false;
}

function CreateFromToDatePicker(fromDate, toDate) {
    $(fromDate).datepicker({
        singleDatePicker: true,
        showDropdowns: true,
        dateFormat: 'dd/mm/yy',
        onSelect: function (selected) {
            var dt = new Date(GetDateFromstring(selected));
            dt.setDate(dt.getDate());
            $(toDate).datepicker("option", "minDate", dt);
        }
    });

    $(toDate).datepicker({
        singleDatePicker: true,
        showDropdowns: true,
        dateFormat: 'dd/mm/yy',
        onSelect: function (selected) {
            var dt = new Date(GetDateFromstring(selected));
            dt.setDate(dt.getDate());
            $(fromDate).datepicker("option", "maxDate", dt);
        }
    });

    var fromDateVal = $(fromDate).val();
    var toDateVal = $(toDate).val();
    if (fromDateVal != "") {
        var dt = new Date(GetDateFromstring(fromDateVal));
        dt.setDate(dt.getDate());
        $(toDate).datepicker("option", "minDate", dt);
    }

    if (toDateVal != "") {
        var dt = new Date(GetDateFromstring(toDateVal));
        dt.setDate(dt.getDate());
        $(fromDate).datepicker("option", "minDate", dt);
    }
}

function ChangeChartType(value, dvchartreport, chartjs_Id, urlS, strData) {

    document.getElementById(dvchartreport).innerHTML = "";
    document.getElementById(dvchartreport).innerHTML = "<canvas id='" + chartjs_Id + "'><canvas>";
    var element = document.getElementById(chartjs_Id);

    $.ajax({
        type: "POST",
        url: urlS,
        data: strData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {


            if (value == "Pie") {

                var strData = result.d.split('||');
                var strLabel = strData[0].split('|');
                var strData = strData[1].split('|').map((n) => {
                    return parseInt(n, 10);
                });;

                new Chart(element, {
                    type: 'pie',
                    data: {
                        labels: strLabel,
                        datasets: [{
                            label: "Population (millions)",
                            borderWidth: 5,
                            backgroundColor: [
                                colors.primary,
                                colors.secondary,
                                colors.danger,
                                colors.warning,
                                colors.secondaryLight
                            ],
                            data: strData
                        }]
                    },
                    options: {
                        title: {
                            display: true,
                            text: 'Predicted world population (millions) in 2050'
                        }
                    }
                });
            }
            else if (value == "Bar") {

                var strData = result.d.split('||');
                var strLabel = strData[0].split('|');
                var strData = strData[1].split('|').map((n) => {
                    return parseInt(n, 10);
                });;

                new  CanvasJS.Chart(element, {
                    type: 'bar',
                    axisY: {
                        minimum: 0,
                    },
                    data: {
                        labels: strLabel,
                        datasets: [
                            {
                                label: "Population (millions)",
                                backgroundColor: [
                                    colors.primary,
                                    colors.secondary,
                                    colors.danger,
                                    colors.warning,
                                    colors.secondaryLight
                                ],
                                data: strData
                            }
                        ]
                    },
                    options: {
                        legend: { display: false },
                        title: {
                            display: true,
                            text: 'Predicted world population (millions) in 2050'
                        }
                    }
                });
            }
            else
            {
                debugger;

                var jsonModel = JSON.parse(result.d);

                var chart = new CanvasJS.Chart("chartContainer", {
                    animationEnabled: true,
                    theme: "light2",
                    title: {
                        text: "Department"
                    },
                    axisX: {

                        crosshair: {
                            enabled: true,
                            snapToDataPoint: true
                        }
                    },
                    axisY: {
                        title: "Number of Visits",
                        includeZero: true,
                        crosshair: {
                            enabled: true
                        }
                    },
                    toolTip: {
                        shared: true
                    },
                    legend: {
                        cursor: "pointer",
                        verticalAlign: "bottom",
                        horizontalAlign: "left",
                        dockInsidePlotArea: true,
                        itemclick: toogleDataSeries
                    },
                    data: jsonModel
                });
                chart.render();

                function toogleDataSeries(e) {
                    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                        e.dataSeries.visible = false;
                    } else {
                        e.dataSeries.visible = true;
                    }
                    chart.render();
                }
            }
        }
    });

}

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
