$(document).ready(function () {
    $('#generateSeed').click(function () {
        var randomSeed = Math.floor(Math.random() * 10000);
        $('#seed').val(randomSeed);
    });
});