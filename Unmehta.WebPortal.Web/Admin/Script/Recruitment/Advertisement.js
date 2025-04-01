
$(document).ready(function () {
    ClosePreloder();
    GetGriDData();
    var StartDate = document.getElementById('bodyPart_txtStartDate');
    var EndDate = document.getElementById('bodyPart_txtEndDate');
    CreateFromToDatePicker(StartDate, EndDate);
});


function GetDateFromstring(strDate) {
    var parts = strDate.split("/");
    var dt = new Date(parseInt(parts[2], 10),
                      parseInt(parts[1], 10) - 1,
                      parseInt(parts[0], 10));
    return dt;
}

function downloadURL(url) {
    
    url = window.location.protocol + "//" + window.location.host + url;
    //url = url.replace("//", "/");
    //var hiddenIFrameID = 'hiddenDownloader',
    //    iframe = document.getElementById(hiddenIFrameID);
    //if (iframe === null) {
    //    iframe = document.createElement('iframe');
    //    iframe.id = hiddenIFrameID;
    //    iframe.style.display = 'none';
    //    document.body.appendChild(iframe);
    //}
    //iframe.src = url;
    var params = [
    'height=' + screen.height,
    'width=' + screen.width,
    'fullscreen=yes' // only works in IE, but here for completeness
    ].join(',');
    // and any other options from
    // https://developer.mozilla.org/en/DOM/window.open

    var popup = window.open(url, 'popup_window', params);
    popup.moveTo(0, 0);
};

function GetEditData(Data) {

    var strData = Data.split('|');
    var StartDate = document.getElementById('bodyPart_txtStartDate');
    var EndDate = document.getElementById('bodyPart_txtEndDate');
    CreateFromToDatePicker(StartDate, EndDate);
    document.getElementById("bodyPart_hfRowId").value = strData[0];
    document.getElementById("bodyPart_txtPostName").value = strData[1];
    document.getElementById("bodyPart_txtPostDesc").value = strData[2].replace("_", "\"").replace("__", "<").replace("___", ">");
    document.getElementById("bodyPart_ddlAdvertisementCode").value = strData[3];
    document.getElementById("bodyPart_txtPostCode").value = strData[4];
    document.getElementById("bodyPart_ddlPostType").value = strData[5];
    document.getElementById("bodyPart_ddlRecruitmentType").value = strData[6];
    document.getElementById("bodyPart_hfFilName").value = strData[7];
    document.getElementById("bodyPart_txtAgeTo").value = strData[8];
    document.getElementById("bodyPart_txtStartDate").value = strData[9];
    document.getElementById("bodyPart_txtStartTime").value = strData[10];
    document.getElementById("bodyPart_txtEndDate").value = strData[11];
    document.getElementById("bodyPart_txtEndTime").value = strData[12];
    document.getElementById("bodyPart_chkEnable").checked = strData[13].toLowerCase() == "true" ? 1 : 0;

    //theHtml = $.parseHTML("ABC&nbsp;&nbsp;&nbsp;&nbsp;ABC Description");

    var urlS = "/Admin/Recruitment/Advertisement.aspx/GetAllEducationSubDetailById";

    $.ajax({
        type: "POST",
        url: urlS,
        data: "{'id':'" + strData[0] + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            
            var strData = result.d.split('|');
            strData.forEach(BindCheckBoxs);
        }
    });

    var urlS = "/Admin/Recruitment/Advertisement.aspx/GetAllSourceSubDetailById";

    $.ajax({
        type: "POST",
        url: urlS,
        data: "{'id':'" + strData[0] + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            
            var strData = result.d.split('|');
            strData.forEach(BindCheckBoxss);
        }
    });

    var urlSs = "/Admin/Recruitment/Advertisement/GetAllSubDetailById";
    $.ajax({
        type: "POST",
        url: urlSs,
        data: "{'id':'" + strData[0] + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            
            var strData = result.d.split('|');
            strData.forEach(BindCheckBox);
        }
    });

}

function BindCheckBoxss(item, index, arr) {
    
    var divName = "#test2_" + item.replace(" ", "_");
    $(divName + ' :checkbox').each(function () {

        var element = this.id;

        $("#" + element).prop('checked', true);
        //selected.push($(this).attr('name'));
    });
}

function BindCheckBoxs(item, index, arr) {
    
    var divName = "#test1_" + item.replace(" ", "_");
    $(divName + ' :checkbox').each(function () {

        var element = this.id;

        $("#" + element).prop('checked', true);
        //selected.push($(this).attr('name'));
    });
}
function BindCheckBox(item, index, arr) {
    var divName = "#test_" + item.replace(" ", "_");
    $(divName + ' :checkbox').each(function () {

        var element = this.id;

        $("#" + element).prop('checked', true);
        //selected.push($(this).attr('name'));
    });
}

function RemoveData(id) {
    if (confirm("Are you sure want to delete ? ")) {
        var urlS = "/Admin/Recruitment/Advertisement.aspx/RemoveDetailById";
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
        url: "/Admin/Recruitment/Advertisement.aspx/GetGridView",
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