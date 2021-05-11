// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var UrlRoot = window.location.origin;

function PageOnlineSupport(CurrentPage) {
    $.ajax({
        type: "GET",
        url: UrlRoot + "/Article/DonateOnlinePart",
        cache: false,
        data: {
            Page: CurrentPage
        },
        dataType: "html",
        success: function (data) {
            $("#list_article").html(data);
        },
    });
}