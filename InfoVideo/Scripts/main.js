var btn_click = function (e) {
    if ($(e).val().toString() === "") {
        return document.location.href = "../index.html";
    } else {
        return document.location.href = $(e).val();
    }
};

var user_login = function (e) {

    $("#head-panel-one").slideUp(500, function () { $("#head-panel-two").slideDown(500); });
  
   

};