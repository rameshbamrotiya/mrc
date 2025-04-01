$(document).ready(function () {
    ClosePreloder();
   
});



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