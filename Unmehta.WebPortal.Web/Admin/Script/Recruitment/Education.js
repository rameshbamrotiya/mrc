$(document).ready(function () {
    ClosePreloder();
    GetGriDData();
});

function GetEditData(Data) {
    var strData = Data.split('|');
    document.getElementById("bodyPart_hfRowId").value = strData[0];
    document.getElementById("bodyPart_txtQualificationName").value = strData[1];
    document.getElementById("bodyPart_ddlGraduateOrPostGraduate").value = strData[2];
    document.getElementById("bodyPart_chkEnable").checked = strData[3].toLowerCase() == "true" ? 1 : 0;
}

function RemoveData(id) {
    if (confirm("Are you sure want to delete ? ")) {
        var urlS = "/Admin/Recruitment/Education.aspx/RemoveDetailById";
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
        url: "/Admin//Recruitment/Education.aspx/GetGridView",
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