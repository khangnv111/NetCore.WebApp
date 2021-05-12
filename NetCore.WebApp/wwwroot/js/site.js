// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("a[href='#top']").click(function () {
    $("html, body").animate({ scrollTop: 1000 }, "slow");
    return false;
});

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
            $('#home').addClass('active');
            $("#list_article").html(data);
        },
    });
};

function PagingRelate(Id, page) {
    $.ajax({
        type: "GET",
        url: UrlRoot + '/Article/NewsRelation',
        cache: false,
        data: {
            Id: Id,
            Page: page,
        },
        dataType: "html",
        success: function (data) {
            $("#show_relationArticle").html(data);
        },
    });
};