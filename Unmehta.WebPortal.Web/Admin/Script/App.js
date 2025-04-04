﻿"use strict";
! function (t) {
    var n = t(window),
        o = t("body");
    feather.replace({
        "stroke-width": 1
    }), t(document).on("click", '[data-toggle="fullscreen"]', function () {
        return t(this).toggleClass("active-fullscreen"), document.fullscreenEnabled ? t(this).hasClass("active-fullscreen") ? document.documentElement.requestFullscreen() : document.exitFullscreen() : alert("Your browser does not support fullscreen."), !1
    }), t(document).on("click", ".overlay", function () {
        t.removeOverlay(), o.hasClass("hidden-navigation") && t(".navigation .navigation-menu-body").niceScroll().remove(), o.removeClass("navigation-show")
    }), t.createOverlay = function () {
        t(".overlay").length < 1 && (o.addClass("no-scroll").append('<div class="overlay"></div>'), t(".overlay").addClass("show"))
    }, t.removeOverlay = function () {
        o.removeClass("no-scroll"), t(".overlay").remove()
    }, t("[data-backround-image]").each(function (a) {
        t(this).css("background", "url(" + t(this).data("backround-image") + ")")
    }),
     n.on("load", function () {
        setTimeout(function () {
            t(".navigation .navigation-menu-body ul li a").each(function () {
                var a = t(this);
                a.next("ul").length && a.append('<i class="sub-menu-arrow ti-angle-up"></i>')
            }), t(".navigation .navigation-menu-body ul li.open>a>.sub-menu-arrow").removeClass("ti-plus").addClass("ti-minus").addClass("rotate-in")
        }, 200)
    }), t(document).on("click", "[data-nav-target]", function () {
        var a = t(this),
            e = a.data("nav-target");
        o.hasClass("navigation-toggle-one") && o.addClass("navigation-show"), t(".navigation .navigation-menu-body .navigation-menu-group > div").removeClass("open"), t(".navigation .navigation-menu-body .navigation-menu-group " + e).addClass("open"), t("[data-nav-target]").removeClass("active"), a.addClass("active")//, a.tooltip("hide")
    }), t(document).on("click", ".navigation-toggler a", function () {
        return n.width() < 1200 ? (t.createOverlay(), o.addClass("navigation-show")) : o.hasClass("navigation-toggle-one") || o.hasClass("navigation-toggle-two") ? o.hasClass("navigation-toggle-one") && !o.hasClass("navigation-toggle-two") ? (o.addClass("navigation-toggle-two"), o.removeClass("navigation-toggle-one")) : !o.hasClass("navigation-toggle-one") && o.hasClass("navigation-toggle-two") && (o.removeClass("navigation-toggle-two"), o.removeClass("navigation-toggle-one")) : o.addClass("navigation-toggle-one"), !1
    }), t(document).on("click", ".header-toggler a", function () {
        return t(".header ul.navbar-nav").toggleClass("open"), !1
    }), t(document).on("click", "*", function (a) {
        !t(a.target).is(t(".navigation, .navigation *, .navigation-toggler *")) && o.hasClass("navigation-toggle-one") && o.removeClass("navigation-show")
    }), t(document).on("click", "*", function (a) {
        t(a.target).is(".header ul.navbar-nav, .header ul.navbar-nav *, .header-toggler, .header-toggler *") || t(".header ul.navbar-nav").removeClass("open")
    }), window.addEventListener("load", function () {
        var a = document.getElementsByClassName("needs-validation");
        Array.prototype.filter.call(a, function (e) {
            e.addEventListener("submit", function (a) {
                !1 === e.checkValidity() && (a.preventDefault(), a.stopPropagation()), e.classList.add("was-validated")
            }, !1)
        })
    }, !1);
    var a = t(".table-responsive-stack");

    function e() {
        n.width() < 768 ? t(".table-responsive-stack").each(function (a) {
            t(this).find(".table-responsive-stack-thead").show(), t(this).find("thead").hide()
        }) : t(".table-responsive-stack").each(function (a) {
            t(this).find(".table-responsive-stack-thead").hide(), t(this).find("thead").show()
        })
    }
    a.find("th").each(function (a) {
        t(".table-responsive-stack td:nth-child(" + (a + 1) + ")").prepend('<span class="table-responsive-stack-thead">' + t(this).text() + ":</span> "), t(".table-responsive-stack-thead").hide()
    }), a.each(function () {
        var a = 100 / t(this).find("th").length + "%";
        t(this).find("th, td").css("flex-basis", a)
    }), e(), window.onresize = function (a) {
        e()
    }, t(document).on("click", '[data-toggle="search"], [data-toggle="search"] *', function () {
        return t(".header .header-body .header-search").show().find(".form-control").focus(), !1
    }), t(document).on("click", ".close-header-search, .close-header-search svg", function () {
        return t(".header .header-body .header-search").hide(), !1
    }), t(document).on("click", "*", function (a) {
        t(a.target).is(t('.header, .header *, [data-toggle="search"], [data-toggle="search"] *')) || t(".header .header-body .header-search").hide()
    }), t(document).on("click", ".accordion.custom-accordion .accordion-row a.accordion-header", function () {
        var a = t(this);
        return a.closest(".accordion.custom-accordion").find(".accordion-row").not(a.parent()).removeClass("open"), a.parent(".accordion-row").toggleClass("open"), !1
    });
    var i, s = t(".table-responsive");
    if (s.on("show.bs.dropdown", function (a) {
            i = t(a.target).find(".dropdown-menu"), o.append(i.detach());
            var e = t(a.target).offset();
            i.css({
        display: "block",
        top: e.top + t(a.target).outerHeight(),
        left: e.left,
        width: "184px",
                "font-size": "14px"
    }), i.addClass("mobPosDropdown")
    }), s.on("hide.bs.dropdown", function (a) {
            t(a.target).append(i.detach()), i.hide()
    }), t(document).on("click", ".chat-app-wrapper .btn-chat-sidebar-open", function () {
            return t(".chat-app-wrapper .chat-sidebar").addClass("chat-sidebar-opened"), !1
    }), t(document).on("click", "*", function (a) {
            t(a.target).is(".chat-app-wrapper .chat-sidebar, .chat-app-wrapper .chat-sidebar *, .chat-app-wrapper .btn-chat-sidebar-open, .chat-app-wrapper .btn-chat-sidebar-open *") || t(".chat-app-wrapper .chat-sidebar").removeClass("chat-sidebar-opened")
    }), t(document).on("click", ".navigation ul li a", function () {
            var a = t(this);
            if (a.next("ul").length) {
                var e = a.find(".sub-menu-arrow");
                return e.toggleClass("rotate-in"), a.next("ul").toggle(200), a.parent("li").siblings().find("ul").not(a.parent("li").find("ul")).slideUp(200), a.next("ul").find("li ul").slideUp(200), a.next("ul").find("li>a").find(".sub-menu-arrow").removeClass("ti-minus").addClass("ti-plus"), a.next("ul").find("li>a").find(".sub-menu-arrow").removeClass("rotate-in"), a.parent("li").siblings().not(a.parent("li").find("ul")).find(">a").find(".sub-menu-arrow").removeClass("ti-minus").addClass("ti-plus"), a.parent("li").siblings().not(a.parent("li").find("ul")).find(">a").find(".sub-menu-arrow").removeClass("rotate-in"), e.hasClass("rotate-in") ? setTimeout(function () {
                    e.removeClass("ti-plus").addClass("ti-minus")
    }, 200) : e.removeClass("ti-minus").addClass("ti-plus"), !o.hasClass("horizontal-side-menu") && 1200 <= n.width() && setTimeout(function (a) {
                    t(".navigation .navigation-menu-body").getNiceScroll().resize()
    }, 300), !1
    }
    }), t("body.small-navigation .navigation").hover(function (a) {
            o.hasClass("small-navigation") && !o.hasClass("horizontal-navigation") && !o.hasClass("hidden-navigation") && 992 <= n.width() && t(".navigation .navigation-menu-body").niceScroll()
    }, function () {
            t(".navigation .navigation-menu-body").getNiceScroll().remove(), t(".navigation ul").attr("style", null)
    }), t(document).on("click", ".dropdown-menu", function (a) {
            a.stopPropagation()
    }), t("#exampleModal").on("show.bs.modal", function (a) {
            var e = t(a.relatedTarget).data("whatever"),
                n = t(this);
            n.find(".modal-title").text("New message to " + e), n.find(".modal-body input").val(e)
    }),
    //    t('[data-toggle="tooltip"]').tooltip({
    //    container: "body"
    //}),
        t('[data-toggle="popover"]').popover(), t(".carousel").carousel(), 992 <= n.width()) {
        t(".card-scroll").niceScroll(), t(".table-responsive").niceScroll(), t(".app-block .app-content .app-lists").niceScroll(), t(".app-block .app-sidebar .app-sidebar-menu").niceScroll(), t(".chat-block .chat-sidebar .chat-sidebar-content").niceScroll();
        var l = t(".chat-block .chat-content .messages");
        l.length && (l.niceScroll({
            horizrailenabled: !1
        }), l.getNiceScroll(0).doScrollTop(l.get(0).scrollHeight, -1))
    } !o.hasClass("small-navigation") && !o.hasClass("horizontal-navigation") && !o.hasClass("hidden-navigation") && 992 <= n.width() && t(".navigation .navigation-menu-body").niceScroll(), t(".dropdown-menu ul.list-group").niceScroll(), t(document).on("click", ".chat-block .chat-content .mobile-chat-close-btn a", function () {
        return t(".chat-block .chat-content").removeClass("mobile-open"), !1
    })
}(jQuery);