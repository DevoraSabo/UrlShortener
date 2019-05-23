$(() => {
    $('#submit').on('click', function () {
        const fullUrl = $("#url").val();
        $.get('/home/UrlShortener', { url : fullUrl }, hashedUrl => {
            $(".well").append(`<a href="http://localhost:52137/${hashedUrl}">Go To Your Url</a>`);
            $("#url").val("");
        });
    });

});