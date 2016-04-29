"use strict";

var btn_click = function btn_click(e) {
    if ($(e).val().toString() === "") {
        return document.location.href = "../index.html";
    } else {
        return document.location.href = $(e).val();
    }
};

var user_login_show = function user_login_show(e) {

    $("#head-panel-one").slideUp(300, function () {
        $("#head-panel-two").slideDown(300);
    });
};

var user_login_hide = function user_login_hide(e) {
    $("#head-panel-two").slideUp(300, function () {
        $("#head-panel-one").slideDown(300);
    });
};
var id = -1;
var OnSuccess = function OnSuccess(data) {
    $('.dest').empty();
    $('#item' + id).empty();
    {
        $('#item' + id).append(data); // добавляем данные в список
    }
};

$(document).ready(function () {

    $('.test').hover(function (e) {
        id = e.toElement.id;
        $.ajax({
            url: "/Account/JsonSearch",
            data: { email: $(this).html() }
        }).done(OnSuccess);
    }, function (e) {});

    $('#btn-reg-ajax').click(function () {
        var dataArray = $.makeArray($(".head-user-content input").filter(':visible').serializeArray());
        $.ajax({
            url: "/Account/LoginAjax",
            data: JSON.stringify(dataArray),
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (data) {
            if (data) location.reload();else {
                $($($(".head-user-content").find("span"))[0]).empty();
                $($($(".head-user-content").find("span"))[0]).append("Логіна не існуе");
            }
        });
    });
});

var validate = function validate() {

    var c = $("#content .field-validation-error").filter(function () {
        return $(this).text().length > 0;
    });
    c = $.grep(c, function (a) {
        return true;
    });
    c.forEach(function (t) {
        var idT = $(t).attr("data-valmsg-for");
        $("#content #" + idT).css("background-color", "pink");
    });
};

$("#Logo").change(function () {
    readURL(this);
});

var readURL = function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#logo-prew').attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
};

