$(document).ready(function () {
    $('#errorSlider').on('input', function () {
        $('#errorCount').val($(this).val());
    });
    $('#errorCount').on('input', function () {
        var inputValue = parseFloat($(this).val());
        if (!isNaN(inputValue) && inputValue >= 0 && inputValue <= 1000) {
            $('#errorSlider').val(inputValue);
        }
    });
});