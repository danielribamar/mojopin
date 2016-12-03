$(document).ready(function () {

    console.log('INIT');

    $('.vermais').click(function (event) {
        event.preventDefault();

        var e = $('.vermais');

        var page = $(e).data("page");
        console.log('get page ' + page);

        $.ajax({
            url: "/umbraco/api/Search/GetFeed/" + page,
            type: 'GET',
            success: function (result) {
                console.log(result);
            }
        });
    });
});