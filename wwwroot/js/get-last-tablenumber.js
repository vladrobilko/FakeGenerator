$(document).ready(function () {
    function getLastNumberInTable() {
        let lastColumn = $('#userDataTable tbody tr td:first-child');
        let lastNumber = lastColumn.last().text();
        return lastNumber;
    }

    $('form').submit(function () {
        $('#usersCount').val(getLastNumberInTable());
    });
});