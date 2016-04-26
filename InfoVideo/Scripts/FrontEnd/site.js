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

var OnSuccess = function OnSuccess(data) {
    var results = $('#results'); // получаем нужный элемент
    results.empty(); //очищаем элемент
    for (var i = 0; i < data.length; i++) {
        results.append('<li>' + data + '</li>'); // добавляем данные в список
    }
};

$(document).ready(function () {
    $('.test').hover(function (e) {

        $.ajax({
            url: "/Account/JsonSearch",
            data: { email: $(this).html() }
        }).done(OnSuccess);
    }, function (e) {});
});

