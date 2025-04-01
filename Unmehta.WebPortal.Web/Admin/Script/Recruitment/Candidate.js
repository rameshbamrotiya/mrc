'use strict';
$(document).ready(function () {
    var DateOfBirth = document.getElementById('bodyPart_txtDateOfBirth');
    CreateDatePicker(DateOfBirth);
    //$('#wizard1').steps({
    //    headerTag: 'h3',
    //    bodyTag: 'section',
    //    autoFocus: true,
    //    titleTemplate: '<span class="wizard-index">#index#</span> #title#'
    //});

    //$('#wizard2').steps({
    //    headerTag: 'h3',
    //    bodyTag: 'section',
    //    autoFocus: true,
    //    titleTemplate: '<span class="wizard-index">#index#</span> #title#'
        
    //});

});

function CreateDatePicker(date) {
    $(date).datepicker({
        singleDatePicker: true,
        showDropdowns: true,
        dateFormat: 'dd/mm/yy'
    });
}
