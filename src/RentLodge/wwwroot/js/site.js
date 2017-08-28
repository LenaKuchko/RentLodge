$('.datepicker').pickadate({
    selectMonths: true, // Creates a dropdown to control month
    selectYears: 15, // Creates a dropdown of 15 years to control year,
    today: 'Today',
    clear: 'Clear',
    close: 'Ok',
    closeOnSelect: false // Close upon selecting a date,
});

// Initialize collapse button
$(".button-collapse").sideNav();

$(document).ready(function () {
    $('select').material_select();
});

$('#AditionalInfo').val('New Text');
$('#AditionalInfo').trigger('autoresize');