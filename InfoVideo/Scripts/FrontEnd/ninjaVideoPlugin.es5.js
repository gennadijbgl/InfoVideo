﻿/* Ninja Slider Video Plugin v2015.10.21. Copyright www.menucool.com */
"use strict";

function nsVideoPlugin() {
    for (var k = 0, j = 0, i = 0, c = this.getLis(), b = 0; b < c.length; b++) for (var e = c[b].getElementsByTagName("*"), d = 0; d < e.length; d++) if (e[d].className == "video") {
        var a = e[d].getElementsByTagName("iframe");
        if (a.length) {
            a = a[0];
            c[b].player = a;
            var h = a.src.toLowerCase();
            if (h.indexOf("player.vimeo") != -1) {
                a.tp = "VM";
                addVimeoEventHandlers(this, c[b], a);
            } else if (h.indexOf("youtube.com") != -1) {
                a.tp = "YT";
                extendSliderFunForYoutube.call(this, c[b]);
            }
        } else {
            a = e[d].getElementsByTagName("video");
            if (!a.length) a = e[d].getElementsByTagName("audio");
            if (a.length) {
                a = a[0];
                c[b].player = a;
                a.tp = "VA";
                addVAEventHandlers(this, a);
            }
        }
        var g = this;
        if (c[b].player) {
            c[b].onclick = function () {
                g.playVideo && g.playVideo(this.player);
            };
            if (c[b].player.tp != "VA") {
                var f = document.createElement("div");
                f.className = "stopVideoIcon";
                f.onclick = function (a) {
                    g.unloadPlayer();
                    a.stopPropagation();
                };
                e[d].appendChild(f);
            }
        }
    }
    this.playVideo = function (a, b) {
        this.iframe != a && this.unloadPlayer();
        if (!b) b = 0;
        setSliderWhenOnPlay(this);
        switch (a.tp) {
            case "VM":
                if (this.playVideo_VM) this.playVideo_VM(a);else if (b < 25) {
                    var c = this;
                    setTimeout(function () {
                        c.playVideo(a, ++b);
                    }, 500);
                } else this.next();
                break;
            case "YT":
                if (this.playVideo_YT) this.playVideo_YT(a);else if (b < 25) {
                    var c = this;
                    setTimeout(function () {
                        c.playVideo(a, ++b);
                    }, 500);
                } else this.next();
                break;
            default:
                this.playVideo_VA(a);}
    };
    this.playAutoVideo = function (a) {
        if (a.player) {
            var c = a.player.getAttribute("data-autoplay"),
                b = 0;
            if (c == "true") b = 1;else if (c == "1" && !a.player.AP) {
                b = 1;
                a.player.AP = 1;
            }
            if (b) {
                this.stop();
                this.playVideo(a.player);
            }
        }
    };
    this.unloadPlayer = function () {
        var a = this.iframe;
        if (a) {
            a.parentNode.style.zIndex = "auto";
            switch (a.tp) {
                case "VM":
                    this.unloadPlayer_VM(a);
                    break;
                case "YT":
                    this.unloadPlayer_YT(a);
                    break;
                default:
                    this.unloadPlayer_VA(a);}
        }
    };
    this.setIframeSize = function () {
        for (var e = this.getLis(), d = 0; d < e.length; d++) {
            var a = e[d].player;
            if (a) {
                var c = a.getAttribute("data-width");
                if (c) var b = a.getAttribute("data-height");else {
                    var f = a.parentNode.parentNode.parentNode;
                    c = f.offsetWidth;
                    b = f.offsetHeight;
                }
                a.setAttribute("width", c);
                b && a.setAttribute("height", b);
            }
        }
    };
    this.setIframeSize();
}

function addEvent(a, c, b) {
    if (a.addEventListener) a.addEventListener(c, b, false);else a.attachEvent && a.attachEvent("on" + c, b);
}

function setSliderWhenOnPlay(a) {
    a.stop();
    a.iframe = a.getLis()[a.getIndex()].player;
    set_ZIndex(a.iframe, 1);
}

function set_ZIndex(a, b) {
    a.parentNode.style.zIndex = b ? "1964" : "auto";
}

function extendSliderFunForVimeo() {
    this.postToIframe = function (d, c, a) {
        this.iframe = d;
        if (window.JSON) {
            var b = { method: c };
            if (a) b.value = a;
            this.iframe.contentWindow.postMessage(JSON.stringify(b), this.iframe.getAttribute("src").split("?")[0]);
        }
    };
    this.playVideo_VM = function (a) {
        this.postToIframe(a, "play");
    };
    this.unloadPlayer_VM = function (a) {
        window.JSON && a.contentWindow.postMessage(JSON.stringify({ method: "pause" }), a.getAttribute("src").split("?")[0]);
    };
    this.backToStartFrame = function () {
        if (this.iframe && window.JSON) this.iframe.src = this.iframe.getAttribute("src");
    };
}

function addVimeoEventHandlers(a, h, b) {
    !a.playVideo_VM && extendSliderFunForVimeo.call(a);
    addEvent(window, "message", d);

    function d(a) {
        if (a.data) try {
            var b = JSON.parse(a.data);
            switch (b.event) {
                case "ready":
                    f();
                    break;
                case "play":
                    g();
                    break;
                case "finish":
                    e();}
        } catch (c) {}
    }

    function f() {
        c("addEventListener", "play");
        c("addEventListener", "finish");
    }

    function g() {
        a.iframe = b;
        setSliderWhenOnPlay(a);
    }

    function e() {
        if (a.iframe == b) {
            set_ZIndex(a.iframe, 0);
            a.backToStartFrame();
            setTimeout(function () {
                a.next();
            }, 120);
        }
    }

    function c(d, a) {
        if (b && window.JSON) {
            var c = { method: d };
            if (a) c.value = a;
            b.contentWindow.postMessage(JSON.stringify(c), b.getAttribute("src").split("?")[0]);
        }
    }
}

function extendSliderFunForYoutube(c) {
    if (!this.unloadPlayer_YT) {
        var b = document.createElement("script");
        b.src = "https://www.youtube.com/iframe_api";
        var a = document.getElementsByTagName("script")[0];
        a.parentNode.insertBefore(b, a);
    }
    c.player.src += "&origin=" + location.protocol + "//" + location.hostname + (location.port ? ":" + location.port : "");
    window._zSlider = this;
    this.unloadPlayer_YT = function (a) {
        var b = a.parentNode.parentNode.yplayer;
        if (b && typeof b.pauseVideo == "function") b.pauseVideo();else {
            a.src = a.getAttribute("src");
            a.parentNode.parentNode.yplayer = new YT.Player(a.id, { events: { onStateChange: onYTPlayerStateChange } });
        }
    };
}

function onYTPlayerStateChange(a) {
    if (a.data == 1) setSliderWhenOnPlay(_zSlider);else if (a.data == 0) {
        setTimeout(function () {
            _zSlider.iframe.src = _zSlider.iframe.getAttribute("src");
            _zSlider.iframe.parentNode.parentNode.yplayer = new YT.Player(_zSlider.iframe.id, { events: { onStateChange: onYTPlayerStateChange } });
        }, 500);
        _zSlider.next();
    }
}

function onYouTubePlayerAPIReady() {
    for (var c = _zSlider.getLis(), a = 0; a < c.length; a++) {
        var b = c[a].player;
        if (b && b.tp == "YT") {
            b.id = _zSlider.sliderId + "-ifr" + a;
            c[a].yplayer = new YT.Player(b.id, { events: { onStateChange: onYTPlayerStateChange } });
        }
    }
    _zSlider.playVideo_YT = function (a) {
        a.parentNode.parentNode.yplayer && a.parentNode.parentNode.yplayer.playVideo();
    };
}

function extendSliderFunForhtml5VA() {
    this.playVideo_VA = function (a) {
        a.play && a.play();
    };
    this.unloadPlayer_VA = function (a) {
        a.pause && a.pause();
    };
}

function addVAEventHandlers(c, a) {
    !c.playVideo_VA && extendSliderFunForhtml5VA.call(c);
    var b = { handleEvent: function handleEvent(a) {
            a.preventManipulation && a.preventManipulation();
            switch (a.type) {
                case "click":
                    a.stopPropagation();
                    break;
                case "play":
                    this.play();
                    break;
                case "ended":
                    this.ended();
                    break;
                case "loadedmetadata":
                    this.loaded();}
        }, play: function play() {
            setSliderWhenOnPlay(c);
        }, ended: function ended() {
            set_ZIndex(a, 0);
            c.next();
            a.currentTime = 0;
        }, loaded: function loaded() {} };
    addEvent(a, "click", b);
    addEvent(a, "play", b);
    addEvent(a, "ended", b);
    addEvent(a, "loadedmetadata", b);
}

