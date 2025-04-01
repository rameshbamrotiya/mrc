function isNumberKey(e) {
    var keyCode = e.keyCode || e.which;
    var regex = /^\d+$/;
    //Validate TextBox value against the Regex.
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (isValid)
        return true;
    else
        return false;
};

$(document).ready(function () {

    $('.number').keypress(function(e){
        var keyCode = e.keyCode || e.which;
        var regex = /^\d+$/;
        //Validate TextBox value against the Regex.
        var isValid = regex.test(String.fromCharCode(keyCode));
        if (!isValid)
            e.preventDefault();
    });

$('.number').on("paste", function (e) {
    var currentid = $(this).attr('id');
    setTimeout(function () {
        var num = $('#' + currentid).val();
        var regexwp = new RegExp("^[0-9\b]+$");
        if (!regexwp.test(num)) {
            $('#' + currentid).val('');
        }
    }, 100);

});

var regexglobalvalidation = /^[A-Za-z0-9 _  @#/.,-:(){}]*[A-Za-z0-9  @#/.,-:(){}][A-Za-z0-9 _  @#/.,-:(){}]*$/;

$("input,textarea").keypress(function (e) {
    var keyCode = e.keyCode || e.which;

    //Validate TextBox value against the Regex.
    var isValid = regexglobalvalidation.test(String.fromCharCode(keyCode));
    if (!isValid) {
        e.preventDefault();
    }

    return isValid;
});

$("input,textarea").on("paste", function (e) {
    var currentid = $(this).attr('id');
    setTimeout(function () {
        var num = $('#' + currentid).val();
        if (!regexglobalvalidation.test(num)) {
            $('#' + currentid).val('');
        }
    }, 100);
});

});

function OnlyAllowedAlphabets(e) {
    var keyCode = e.keyCode || e.which;
    var regex = /^[A-Za-z]+$/;
    //Validate TextBox value against the Regex.
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (isValid) 
        return true;
    else
        return false;
}

function OnlyAllowedAlphabetsSpaceNumbers(e) {
    var keyCode = e.keyCode || e.which;
    var regex = /^[A-Za-z0-9 _ ]*[A-Za-z0-9 ][A-Za-z0-9 _ ]*$/;
    //var regex = /^[A-Za-z0-9 _  @#/-]*[A-Za-z0-9  @#/-][A-Za-z0-9 _  @#/-]*$/; // Some Special Char allowed
    //Validate TextBox value against the Regex.
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (isValid) 
        return true;
    else
        return false;
}

function OnlyAllowedAlphabetsSpaceNumbersPaste(obj) {
    if (window.event.clipboardData.getData('Text').match(/^[A-Za-z0-9 _ ]*[A-Za-z0-9 ][A-Za-z0-9 _ ]*$/))
        return true;
    else
        return false;
}

function OnlyAllowedAlphabetsSpaceNumbers(e) {
    var keyCode = e.keyCode || e.which;
    var regex = /^[A-Za-z0-9 _ ]*[A-Za-z0-9 ][A-Za-z0-9 _ ]*$/;
    //var regex = /^[A-Za-z0-9 _  @#/-]*[A-Za-z0-9  @#/-][A-Za-z0-9 _  @#/-]*$/; // Some Special Char allowed
    //Validate TextBox value against the Regex.
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (isValid)
        return true;
    else
        return false;
}
function NotAllowedSpecialChar(e) {
    var keyCode = e.keyCode || e.which;
    if (!String.fromCharCode(keyCode).match(/^[^*|\":<>[\]{}`\\()';@&$]+$/)) 
        return false;
    else
        return true;
}


function OnlyAllowedlettersWithSpaceOnly(e) {
    var keyCode = e.keyCode || e.which;
    var regex = /^[a-zA-Z\s]*$/; 
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (isValid)
        return true;
    else
        return false;
}

function OnlyAllowedlettersWithSpaceOnlyPaste(obj) {
    if (window.event.clipboardData.getData('Text').match(/^[a-zA-Z\s]*$/))
        return true;
    else
        return false;
}


function NotAllowedSpecialCharPaste(obj) {
    if (window.event.clipboardData.getData('Text').match(/^[^*|\":<>[\]{}`\\()';@&$]+$/))
        return true;
    else
        return false;
}






