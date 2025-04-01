$(document).ready(function () {
    ClosePreloder();
    GetGriDData();
});

function GetEditData(Data) {
    var strData = Data.split('|');
    document.getElementById("bodyPart_hfRowId").value = strData[0];
    document.getElementById("bodyPart_txtCastName").value = strData[1];
    document.getElementById("bodyPart_chkEnable").checked = strData[2].toLowerCase() == "true" ? 1 : 0;
}

function RemoveData(id) {
    if (confirm("Are you sure want to delete ? ")) {
        var urlS = "/Admin/Recruitment/Cast.aspx/RemoveDetailById";
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
        url: "/Admin/Recruitment/Cast.aspx/GetGridView",
        //data: "{'id':'1'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var returnedstring = data;
            document.getElementById("bodyPart_gvInnerGridView").innerHTML = returnedstring.d;
            $('#gvCastData').DataTable({
                destroy: true,
                responsive: true
            });
        }
    });
}