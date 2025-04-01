$(document).ready(function () {
    ClosePreloder();
    var divForm = document.getElementById("bodyPart_divForm");
    var divGrid = document.getElementById("bodyPart_divGrid");
    divGrid.style.display = 'block';
    divForm.style.display = 'none';

    GetGriDData();
    var el = document.getElementById('foo');
    el.onclick = showFoo;
});

function showFoo() {
    var divForm = document.getElementById("bodyPart_divForm");
    var divGrid = document.getElementById("bodyPart_divGrid");
    divForm.style.display = 'block';
    divGrid.style.display = 'none';

    return false;
}

function GetEditData(Data) {
    var strData = Data.split('|');
    showFoo();
    document.getElementById("bodyPart_hfRowId").value = strData[0];
    document.getElementById("bodyPart_ddlLanguage").value = strData[1];
    document.getElementById("bodyPart_txtExecutiveName").value = strData[2];
    document.getElementById("bodyPart_ddlDesignation").value = strData[3];
    document.getElementById("bodyPart_txtMessage").value = strData[4];
    document.getElementById("bodyPart_chkEnable").checked = strData[5].toLowerCase() == "true" ? 1 : 0;
    document.getElementById("bodyPart_hfFilName").value = strData[6];
}

function RemoveData(id) {
    if (confirm("Are you sure want to delete ? ")) {
        var urlS = "/Admin/Hospital/AboutUs.aspx/RemoveDetailById";
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

function downloadURL(url, fileName) {
    url = window.location.protocol + "//" + window.location.host + url;
    var link = document.createElement("a");
    // If you don't know the name or want to use
    // the webserver default set name = ''
    link.setAttribute('download', fileName);
    link.href = url;
    document.body.appendChild(link);
    link.click();
    link.remove();
}

function GetGriDData() {
    $.ajax({
        type: "POST",
        url: "/Admin/Hospital/AboutUs.aspx/GetGridView",
        //data: "{'id':'1'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var returnedstring = data;
            document.getElementById("bodyPart_gvInnerGridView").innerHTML = returnedstring.d;
            $('#gvAboutUsData').DataTable({
                destroy: true,
                responsive: true
            });
        }
    });
}