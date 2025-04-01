$(document).ready(function () {
    ClosePreloder();
    GetGriDData();
});
function GetEditData(Data) {
    
    var strData = Data.split('|');
    document.getElementById("bodyPart_hfRowId").value = strData[0];
    document.getElementById("bodyPart_txtStateName").value = strData[1];
}
function GetGriDData() {
    $.ajax({
        type: "POST",
        url: "/Admin/Recruitment/State.aspx/GetGridView",
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