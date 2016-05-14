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

var ajaxT = function ajaxT(data, status, xhr) {

    if (data.success) {
        location.reload();
    }
};

$(document).ready(function () {

    $('#type').keyup(function (event) {
        $('#searchForm').submit();
    });

    $('.test').hover(function (e) {
        id = e.toElement.id;
        $.ajax({
            url: "/Account/JsonSearch",
            data: { email: $(this).html() }
        }).done(OnSuccess);
    }, function (e) {});

    $('.menu-switch').unbind('click').click(function () {

        $(this).toggleClass("zmdi-menu");
        $(this).toggleClass("zmdi-close");

        var p = $(this.parentElement);

        if (p.closest(".news")) p.children("*:not(.card-menu, .menu-switch, img)").slideToggle();else p.find(".from-form").slideToggle();

        $(this.parentElement).find(".card-menu").slideToggle();
    });

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

    $('#stat-ajax-btn').unbind('click').click(function () {
        $.ajax({
            url: "/Histories/GetByDate",
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                yearDisplay: $("input[name='yearDisplay']").val()
            }
        }).done(function (data) {
            $("#stat-ajax").empty();
            var l = $.map(data, function (obj, i) {
                return obj.Month;
            });
            var d = $.map(data, function (obj, i) {
                return obj.Count;
            });
            var dataS = {
                labels: l,
                datasets: [{
                    label: "",

                    data: d
                }]
            };
            var myBarChart = new Chart($("#myChart"), {
                type: 'line',
                data: dataS

            });

            //  $("#stat-ajax").append(data);
        }).error(function (data, t, b) {
            $("#stat-ajax").empty();
            $("#stat-ajax").append(data.responseText);
            alert(data.responseText);
        });
    });

    $('#stat-ajax-btn-pop').unbind('click').click(function () {
        $.ajax({
            url: "/Histories/GetByVideo",
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            dataType: "json"

        }).done(function (data) {
            $("#stat-ajax").empty();
            var l = $.map(data, function (obj, i) {
                return obj.Title;
            });
            var d = $.map(data, function (obj, i) {
                return obj.Count;
            });
            var dataS = {
                labels: l,
                datasets: [{
                    data: d,
                    backgroundColor: ["#FF6384", "#4BC0C0", "#FFCE56", "#E7E9ED", "#36A2EB"],
                    hoverBackgroundColor: ["#FF6384", "#36A2EB", "#FFCE56"]
                }]
            };
            var myBarChart = new Chart($("#myChart"), {
                type: 'pie',
                data: dataS
            });

            //  $("#stat-ajax").append(data);
        }).error(function (data, t, b) {
            $("#stat-ajax").empty();
            $("#stat-ajax").append(data.responseText);
            alert(data.responseText);
        });
    });
});

var buy = function buy(e) {

    var b = $(e.closest("form")).find(".anim").show();
    var c = $(e.closest("form")).find(".from-form").add($(e.closest("form")).find(".main")).add($(e).find(".card-menu")).css("opacity", "0.5");
    var d = $(e.closest("form")).find("h2");

    $.ajax({
        url: e.closest("form").action,
        type: e.closest("form").method,
        data: $(e.closest("form")).serialize(),
        success: function success(result) {
            {
                if (result.success) {

                    d.toggleClass('greenI').delay(700).queue(function (next) {
                        d.toggleClass('greenI');
                        next();
                    }).delay(700);
                    c.css("opacity", "1");
                    b.hide();
                } else {
                    d.toggleClass('redI').delay(700).queue(function (next) {
                        d.toggleClass('redI');
                        next();
                    }).delay(700);
                    c.css("opacity", "1");
                    b.hide();
                }
            }
        },
        error: function error(err) {
            alert(err.responseText);
        }

    });
};

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

