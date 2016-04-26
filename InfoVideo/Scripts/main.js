var btn_click = function (e) {
    if ($(e).val().toString() === "") {
        return document.location.href = "../index.html";
    } else {
        return document.location.href = $(e).val();
    }
};

var user_login_show = function (e) {

    $("#head-panel-one").slideUp(300, function () { $("#head-panel-two").slideDown(300); });
  
};

var user_login_hide = function (e) {

    $("#head-panel-two").slideUp(300, function () { $("#head-panel-one").slideDown(300); });

};

var OnSuccess = function (data,e) {

    var results = $('#results'); // получаем нужный элемент
    results.empty(); //очищаем элемент

    for (var i = 0; i < data.length; i++) {
        results.append('<li>' + data + '</li>'); // добавляем данные в список
    }
}

$(document).ready(function () {
    $('.test')
         .hover(
             function (e) {
          
                 $.ajax({
                     url: "/Account/JsonSearch",
                     data: { email: $(this).html() }
                 })
                     .done(OnSuccess);

             },
             function (e) {

             });
});
