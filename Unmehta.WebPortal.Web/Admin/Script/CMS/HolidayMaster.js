
$(document).ready(function () {
    ClosePreloder();
    GetGriDData();
    var StartDate = document.getElementById('bodyPart_txtHolidayDate');
    CreateDatePicker(StartDate);

});

//$(function () {
//    var disabledDate = ['2021-4-1', '2021-4-15', '2021-4-3'];
//    $('#bodyPart_txtHolidayDate').datetimepicker({
//        disabledDates: disabledDate
//    });
//});

function GetDateFromstring(strDate) {
    var parts = strDate.split("/");
    var dt = new Date(parseInt(parts[2], 10),
                      parseInt(parts[1], 10) - 1,
                      parseInt(parts[0], 10));
    return dt;
}


function GetEditData(Data) {

    var strData = Data.split('|');
    document.getElementById("bodyPart_hfID").value = strData[0];
    document.getElementById("bodyPart_txtholidaydesc").value = strData[1];
    document.getElementById("bodyPart_txtHolidayDate").value = strData[2];
    document.getElementById("bodyPart_ddlActiveInactive").checked = strData[3];

}

function RemoveData(id) {
    if (confirm("Are you sure want to delete ? ")) {
        var urlS = "/Admin/CMS/Holiday.aspx/RemoveDetailById";
        $.ajax({
            type: "POST",
            url: urlS,
            data: "{'id':'" + id + "'}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var returnedstring = data.d;
                var strData = returnedstring.split('|');
                document.getElementById("bodyPart_hfID").value = "0";
                TosterMessage(strData[0], strData[1]);
                GetGriDData();
            }
        });
    }
}

function GetGriDData() {
    $.ajax({
        type: "POST",
        url: "/Admin/CMS/Holiday.aspx/GetGridView",
        //data: "{'id':'1'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var returnedstring = data;
            document.getElementById("bodyPart_gvInnerGridView").innerHTML = returnedstring.d;
            $('#gvEduQuaData').DataTable({
                destroy: true,
                responsive: true
            });
        }
    });
}