
$(document).ready(function () {
    ClosePreloder();
    GetGriDData();
    var StartDate = document.getElementById('bodyPart_txtStartDate');
    var EndDate = document.getElementById('bodyPart_txtEndDate');
    var PublishDate = document.getElementById('bodyPart_txtpublishdate');
    CreateFromToDatePicker(StartDate, EndDate);
    CreateDatePicker(PublishDate);
});


function GetDateFromstring(strDate) {
    var parts = strDate.split("/");
    var dt = new Date(parseInt(parts[2], 10),
                      parseInt(parts[1], 10) - 1,
                      parseInt(parts[0], 10));
    return dt;
}

function GetEditData(Data) {

    var strData = Data.split('|');
    var StartDate = document.getElementById('bodyPart_txtStartDate');
    var EndDate = document.getElementById('bodyPart_txtEndDate');
    var PublishDate = document.getElementById('bodyPart_txtpublishdate');
    CreateDatePicker(PublishDate);
    CreateFromToDatePicker(StartDate, EndDate);
    document.getElementById("bodyPart_hfRowId").value = strData[0];
    document.getElementById("bodyPart_txtPostCode").value = strData[1];
    document.getElementById("bodyPart_txtStartDate").value = strData[2];
    document.getElementById("bodyPart_txtStartTime").value = strData[3];
    document.getElementById("bodyPart_txtEndDate").value = strData[4];
    document.getElementById("bodyPart_txtEndTime").value = strData[5];
    document.getElementById("bodyPart_chkEnable").checked = strData[6].toLowerCase() == "true" ? 1 : 0;
    document.getElementById("bodyPart_hfFilName").value = strData[7];
    document.getElementById("bodyPart_txtpublishdate").value = strData[8];

}

function RemoveData(id) {
    if (confirm("Are you sure want to delete ? ")) {
        var urlS = "/Admin/Recruitment/AdvertisementCode.aspx/RemoveDetailById";
        $.ajax({
            type: "POST",
            url: urlS,
            data: "{'id':'" + id + "'}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var returnedstring = data.d;
                var strData = returnedstring.split('|');
                document.getElementById("bodyPart_hfRowId").value = "0";
                TosterMessage(strData[0], strData[1]);
                GetGriDData();
            }
        });
    }
}

function GetGriDData() {
    $.ajax({
        type: "POST",
        url: "/Admin/Recruitment/AdvertisementCode.aspx/GetGridView",
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