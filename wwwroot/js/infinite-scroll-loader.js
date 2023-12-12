let page = 1;
let isLoading = false;

function isBottom() {
    return window.innerHeight + window.scrollY >= document.body.offsetHeight;
}

function loadMoreData() {
    if (isLoading) return;
    isLoading = true;
    page++;

    let region = $('#region').val();
    let errorCount = $('#errorCount').val();
    let seed = $('#seed').val();

    $.ajax({
        url: `/FakeGenerator/LoadMoreData?page=${page}&region=${region}&errorCount=${errorCount}&seed=${seed}`,
        method: 'GET',
        success: function (data) {
            $('#userDataTable tbody').append(data);
            isLoading = false;
        },
        error: function (error) {
            console.error('Error loading more data:', error);
            isLoading = false;
        }
    });
}

window.addEventListener('scroll', function () {
    if (isBottom()) {
        loadMoreData();
    }
});