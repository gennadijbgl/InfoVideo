﻿// Generated by IcedCoffeeScript 108.0.9
(function() {
  var all_scripts, head_panel_click_open, head_panel_close, left_menu_update, load_parts, main_menu_update, rat, rating, slider_toggle, social_hover;

  head_panel_click_open = function() {
    if ($('.head-user-panel').hasClass('display-block')) {
      return head_panel_close();
    } else {
      $('.head-user-panel').addClass('display-block');
      return $('.head-user-panel').css("display", "block").css("opacity", "1").unbind("transitionend webkitTransitionEnd oTransitionEnd otransitionend");
    }
  };

  rat = 0;

  head_panel_close = function() {
    return $('.head-user-panel').css("opacity", "0").on('transitionend webkitTransitionEnd oTransitionEnd otransitionend', function() {
      $(".head-user-panel").css("display", "none");
      return $('.head-user-panel').removeClass('display-block');
    });
  };

  main_menu_update = function() {
    var hdr, mn, slider;
    mn = $('#menu');
    hdr = $('.body').height();
    slider = $('.header-slider').height();
    if ($(this).scrollTop() > hdr) {
      mn.addClass('menu-scrolled');
    } else {
      mn.removeClass('menu-scrolled');
    }
  };

  left_menu_update = function($) {
    $('#cssmenu li.active').addClass('open').children('ul').show();
    $('#cssmenu li.has-sub>a').on('click', function() {
      var element;
      $(this).removeAttr('href');
      element = $(this).parent('li');
      if (element.hasClass('open')) {
        element.removeClass('open');
        element.find('li').removeClass('open');
        element.find('ul').slideUp(200);
      } else {
        element.addClass('open');
        element.children('ul').slideDown(200);
        element.siblings('li').children('ul').slideUp(200);
        element.siblings('li').removeClass('open');
        element.siblings('li').find('li').removeClass('open');
        element.siblings('li').find('ul').slideUp(200);
      }
    });
  };

  social_hover = function() {
    return $("#git-social").on({
      mouseenter: function() {
        $("#git-social").css("color", "#ff00ae");
        return $("#git-social >li >i").toggleClass('zmdi-github zmdi-github-alt');
      },
      mouseleave: function() {
        $("#git-social").css("color", "inherit");
        return $("#git-social >li >i").toggleClass('zmdi-github zmdi-github-alt');
      }
    });
  };

  $(document).ready(function() {
    return all_scripts();
  });

  load_parts = function() {
    $("#header").load("header.html");
    return $("#footer").load("footer.html", all_scripts);
  };

  all_scripts = function() {
    ion.sound({
      sounds: [
        {
          name: "camera_flashing"
        }
      ],
      volume: 0.5,
      path: "/Media/Sounds/",
      preload: true,
      multiplay: true
    });
    slider_toggle();
    social_hover();
    main_menu_update();
    $('#profile').click(function(e) {
      head_panel_click_open();
      return e.stopPropagation();
    });
    left_menu_update(jQuery);
    $(window).scroll(main_menu_update);
    return $("input[name='rating']").change(rating);
  };

  rating = function() {
    var vl;
    vl = parseInt($('input[name="rating"]:checked').val(), 10);
    if (rat !== 0) {
      rat += vl;
      rat = rat / 2;
    } else {
      rat = vl;
    }
    $('input[name="rating"]').attr('checked', false);
    return $("#ratS").text(+rat.toFixed(2));
  };

  slider_toggle = function() {
    return $('#img').click(function() {
      ion.sound.play("camera_flashing");
      if ($('.header-slider').css('display') === 'none') {
        $(".header-slider").slideDown();
        return $('#img').removeClass('img-gray');
      } else {
        $('#img').addClass('img-gray');
        return $(".header-slider").slideUp();
      }
    });
  };

}).call(this);
