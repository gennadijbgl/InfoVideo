
rat = 0

head_panel_click_open = ->
  if $('.head-user-panel').hasClass('display-block')
    head_panel_close()
  else
    $('.head-user-panel').addClass 'display-block'
    $('.head-user-panel').css("display", "block").css("opacity", "1").unbind("transitionend webkitTransitionEnd oTransitionEnd otransitionend");


head_panel_close1 = ->
  $('.head-user-panel').css "opacity", "0"
  .on 'transitionend webkitTransitionEnd oTransitionEnd otransitionend'
  , -> $(".head-user-panel").css "display", "none" ; $('.head-user-panel').removeClass 'display-block'   


main_menu_update = ->
  mn = $('#menu')
  hdr = $('.body').height()
  slider = $('.header-slider').height()
  if $(this).scrollTop() > hdr
    mn.addClass 'menu-scrolled'
  else
    mn.removeClass 'menu-scrolled'
  return

left_menu_update =  ($) ->
  $('#cssmenu li.active').addClass('open').children('ul').show()
  $('#cssmenu li.has-sub>a').on 'click', ->
    $(this).removeAttr 'href'
    element = $(this).parent('li')
    if element.hasClass('open')
      element.removeClass 'open'
      element.find('li').removeClass 'open'
      element.find('ul').slideUp 200
    else
      element.addClass 'open'
      element.children('ul').slideDown 200
      element.siblings('li').children('ul').slideUp 200
      element.siblings('li').removeClass 'open'
      element.siblings('li').find('li').removeClass 'open'
      element.siblings('li').find('ul').slideUp 200
    return
  return

social_hover = ->
  $("#git-social").on 
    mouseenter: ->
      $("#git-social").css "color","#ff00ae"
      $("#git-social >li >i").toggleClass 'zmdi-github zmdi-github-alt'
    mouseleave: ->
      $("#git-social").css "color","inherit"
      $("#git-social >li >i").toggleClass 'zmdi-github zmdi-github-alt'

$(document).ready ->
  #load_parts()
  all_scripts()


load_parts = ->
  $("#header").load "header.html" 
  $("#footer").load "footer.html", all_scripts

all_scripts = ->
  ion.sound({
    sounds: [
        {
            name: "camera_flashing"
        }
    ],
    volume: 0.5,
    path: "Media/Sounds/",
    preload: true,
    multiplay:true
  })
  slider_toggle()
  social_hover()
  main_menu_update()
  $('#profile').click (e) ->
    head_panel_click_open()
    e.stopPropagation()
  left_menu_update(jQuery)
  $(window).scroll main_menu_update
  $(document).click (e)->
    head_panel_close()  
  $("input[name='rating']").change rating


rating = ->
  vl = parseInt($('input[name="rating"]:checked').val(), 10)
  if rat != 0
    rat += vl
    rat = rat / 2
  else 
    rat = vl


 
  $('input[name="rating"]').attr('checked', false)
  $("#ratS").text +rat.toFixed(2)


  
slider_toggle = ->
  $('#img').click ->
    ion.sound.play("camera_flashing")
    if $('.header-slider').css('display') == 'none' 
      $(".header-slider").slideDown() 
      $('#img').removeClass 'img-gray'    
    else
      $('#img').addClass 'img-gray'
      $(".header-slider").slideUp() 

