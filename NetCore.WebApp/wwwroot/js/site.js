// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("a[href='#top']").click(function () {
    $("html, body").animate({ scrollTop: 1000 }, "slow");
    return false;
});

_documentHeight = document.body.clientHeight;
window.onload = function () {
    window.onscroll = function () {

        var _advTop = $(".advert_right").offset().top;
        var _windowTop = $(window).scrollTop();

        if (_windowTop > 157) {
            $(".advert_right").css("top", _windowTop + 2);
            $(".advert_left").css("top", _windowTop + 2);
        } else {
            $('.advert_right').css('top', 'auto').css('left', 'auto');
            $('.advert_left').css('top', 'auto').css('left', 'auto');
        }

        //if (_height >= 314) {
        //    $(".advert_right").css("top", 0);
        //    $(".advert_left").css("top", 0);
        //} else {
        //    $('.advert_right').css('top', 'auto').css('left', 'auto');
        //    $('.advert_left').css('top', 'auto').css('left', 'auto');
        //}
    };
};

//$(function () {
//    $("#advert_right").stickOnScroll({
//        topOffset: $("#header").outerHeight(),
//        footerElement: $("#footer"),
//        bottomOffset: 0
//    });
//});

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