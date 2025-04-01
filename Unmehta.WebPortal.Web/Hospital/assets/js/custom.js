$(document).ready(function () {
  $('.toggle').click(function () {
    $('.sidebar-contact').toggleClass('active')
    $('.toggle').toggleClass('active')
  })
})

/*==========  Open and Close Popup   ==========*/
// open Mini Popup


// Close topbar

$(window).on("scroll", function () {
    var scroll = $(window).scrollTop();
    if (scroll < 245) {
        $(".header-sticky").removeClass("sticky");
    } else {
        $(".header-sticky").addClass("sticky");
    }
});

function injectAsidebar(jQuery) {
  jQuery.fn.asidebar = function asidebar(status) {
    switch (status) {
      case "open":
        var that = this;
        // fade in backdrop
        if ($(".aside-backdrop").length === 0) {
          $("body").append("<div class='aside-backdrop'></div>");
        }
        $(".aside-backdrop").addClass("in");


        function close() {
          $(that).asidebar.apply(that, ["close"]);
        }

        // slide in asidebar
        $(this).addClass("in");
        $(this).find("[data-dismiss=aside], [data-dismiss=asidebar]").on('click', close);
        $(".aside-backdrop").on('click', close);
        break;
      case "close":
        // fade in backdrop
        if ($(".aside-backdrop.in").length > 0) {
          $(".aside-backdrop").removeClass("in");
        }

        // slide in asidebar
        $(this).removeClass("in");
        break;
      case "toggle":
        if ($(this).attr("class").split(' ').indexOf('in') > -1) {
          $(this).asidebar("close");
        } else {
          $(this).asidebar("open");
        }
        break;
    }
  }
}

// support browser and node
if (typeof jQuery !== "undefined") {
  injectAsidebar(jQuery);
} else if (typeof module !== "undefined" && module.exports) {
  module.exports = injectAsidebar;
}
jQuery(function ($) {

  $(".sidebar-dropdown > a").click(function () {
    $(".sidebar-submenu").slideUp(200);
    if (
      $(this)
        .parent()
        .hasClass("active")
    ) {
      $(".sidebar-dropdown").removeClass("active");
      $(this)
        .parent()
        .removeClass("active");
    } else {
      $(".sidebar-dropdown").removeClass("active");
      $(this)
        .next(".sidebar-submenu")
        .slideDown(200);
      $(this)
        .parent()
        .addClass("active");
    }
  });

  $("#close-sidebar").click(function () {
    $(".page-wrapper").removeClass("toggled");
  });
  $("#show-sidebar").click(function () {
    $(".page-wrapper").addClass("toggled");
  });

});



// Modal Video JS
$('.js-modal-btn').modalVideo();

// Projects Slider JS
$('.gallary-slider').owlCarousel({
  loop: true,
  margin: 20,
  nav: true,
  dots: false,
  smartSpeed: 1000,
  autoplay: true,
  autoplayTimeout: 3000,
  autoplayHoverPause: true,
  navText: [
    "<i class='fas fa-angle-left'></i>",
    "<i class='fas fa-angle-right'></i>"
  ],
  responsive: {
    0: {
      items: 1,
    },
    600: {
      items: 2,
    },
    1000: {
      items: 4,
    }
  }
});

// Projects Slider JS
$('.cathlab-slider').owlCarousel({
  loop: true,
  margin: 20,
  nav: false,
  dots: true,
  smartSpeed: 1000,
  autoplay: true,
  autoplayTimeout: 3000,
  autoplayHoverPause: true,
  navText: [
    "<i class='fas fa-angle-left'></i>",
    "<i class='fas fa-angle-right'></i>"
  ],
  responsive: {
    0: {
      items: 1,
    },
    600: {
      items: 1,
    },
    1000: {
      items: 1,
    }
  }
});

// Projects Slider JS
$('.projects-slider').owlCarousel({
  loop: true,
  margin: 20,
  nav: true,
  dots: false,
  smartSpeed: 1000,
  autoplay: true,
  autoplayTimeout: 3000,
  autoplayHoverPause: true,
  navText: [
    "<i class='fas fa-angle-left'></i>",
    "<i class='fas fa-angle-right'></i>"
  ],
  responsive: {
    0: {
      items: 1,
    },
    600: {
      items: 2,
    },
    1000: {
      items: 4,
    }
  }
});

// video Slider JS
$('.video-slider').owlCarousel({
  loop: true,
  margin: 20,
  nav: true,
  dots: false,
  smartSpeed: 1000,
  autoplay: true,
  autoplayTimeout: 3000,
  autoplayHoverPause: true,
  navText: [
    "<i class='fas fa-angle-left'></i>",
    "<i class='fas fa-angle-right'></i>"
  ],
  responsive: {
    0: {
      items: 1,
    },
    600: {
      items: 1,
    },
    1000: {
      items: 1,
    }
  }
});

// Testimonials Slider JS

$('.testimonials-slider').owlCarousel({
  loop: true,
  margin: 30,
  nav: false,
  dots: false,
  smartSpeed: 1000,
  autoplay: true,
  autoplayTimeout: 3000,
  autoplayHoverPause: true,
  navText: [
    "<i class='fas fa-angle-left'></i>",
    "<i class='fas fa-angle-right'></i>"
  ],
  responsive: {
    0: {
      items: 1,
    },
    600: {
      items: 1,
    },
    1000: {
      items: 2,
    }
  }
});


// Testimonials Slider JS

$('.upcomingevent-slider').owlCarousel({
  loop: true,
  margin: 30,
  nav: false,
  dots: false,
  smartSpeed: 1000,
  autoplay: true,
  autoplayTimeout: 3000,
  autoplayHoverPause: true,
  navText: [
    "<i class='fas fa-angle-left'></i>",
    "<i class='fas fa-angle-right'></i>"
  ],
  responsive: {
    0: {
      items: 1,
    },
    600: {
      items: 1,
    },
    1000: {
      items: 1,
    }
  }
});

// career Slider JS

$('.career-slider').owlCarousel({
  loop: true,
  margin: 30,
  nav: false,
  dots: false,
  smartSpeed: 1000,
  autoplay: true,
  autoplayTimeout: 3000,
  autoplayHoverPause: true,
  navText: [
    "<i class='fas fa-angle-left'></i>",
    "<i class='fas fa-angle-right'></i>"
  ],
  responsive: {
    0: {
      items: 1
    },
    600: {
      items: 2
    },
    800: {
      items: 2
    },
    1024: {
      items: 3
    },
    1100: {
      items: 3
    },
    1200: {
      items: 4
    }
  }
});

/*-------------------------------------
      OwlCarousel
  -------------------------------------*/
$('.rs-carousel').each(function () {
  var owlCarousel = $(this),
    loop = owlCarousel.data('loop'),
    items = owlCarousel.data('items'),
    margin = owlCarousel.data('margin'),
    stagePadding = owlCarousel.data('stage-padding'),
    autoplay = owlCarousel.data('autoplay'),
    autoplayTimeout = owlCarousel.data('autoplay-timeout'),
    smartSpeed = owlCarousel.data('smart-speed'),
    dots = owlCarousel.data('dots'),
    nav = owlCarousel.data('nav'),
    navSpeed = owlCarousel.data('nav-speed'),
    xsDevice = owlCarousel.data('mobile-device'),
    xsDeviceNav = owlCarousel.data('mobile-device-nav'),
    xsDeviceDots = owlCarousel.data('mobile-device-dots'),
    smDevice = owlCarousel.data('ipad-device'),
    smDeviceNav = owlCarousel.data('ipad-device-nav'),
    smDeviceDots = owlCarousel.data('ipad-device-dots'),
    smDevice2 = owlCarousel.data('ipad-device2'),
    smDeviceNav2 = owlCarousel.data('ipad-device-nav2'),
    smDeviceDots2 = owlCarousel.data('ipad-device-dots2'),
    mdDevice = owlCarousel.data('md-device'),
    centerMode = owlCarousel.data('center-mode'),
    HoverPause = owlCarousel.data('hoverpause'),
    mdDeviceNav = owlCarousel.data('md-device-nav'),
    mdDeviceDots = owlCarousel.data('md-device-dots');
  owlCarousel.owlCarousel({
    loop: (loop ? true : false),
    items: (items ? items : 4),
    lazyLoad: true,
    center: (centerMode ? true : false),
    autoplayHoverPause: (HoverPause ? true : false),
    margin: (margin ? margin : 0),
    //stagePadding: (stagePadding ? stagePadding : 0),
    autoplay: (autoplay ? true : false),
    autoplayTimeout: (autoplayTimeout ? autoplayTimeout : 1000),
    smartSpeed: (smartSpeed ? smartSpeed : 250),
    dots: (dots ? true : false),
    nav: (nav ? true : false),
    navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
    navSpeed: (navSpeed ? true : false),
    responsiveClass: true,
    responsive: {
      0: {
        items: (xsDevice ? xsDevice : 1),
        nav: (xsDeviceNav ? true : false),
        dots: (xsDeviceDots ? true : false),
        center: false,
      },
      576: {
        items: (smDevice2 ? smDevice2 : 2),
        nav: (smDeviceNav2 ? true : false),
        dots: (smDeviceDots2 ? true : false),
        center: false,
      },
      768: {
        items: (smDevice ? smDevice : 3),
        nav: (smDeviceNav ? true : false),
        dots: (smDeviceDots ? true : false),
        center: false,
      },
      992: {
        items: (mdDevice ? mdDevice : 4),
        nav: (mdDeviceNav ? true : false),
        dots: (mdDeviceDots ? true : false),
      }
    }
  });
});




// Back To Top JS
$(function () {
  $(window).on('scroll', function () {
    var scrolled = $(window).scrollTop();
    if (scrolled > 100) $('.go-top').addClass('active');
    if (scrolled < 100) $('.go-top').removeClass('active');
  });
  $('.go-top').on('click', function () {
    $('html, body').animate({ scrollTop: '0' }, 500);
  });
});




//=== History Carousel ===


$('.medical-departments-carousel').owlCarousel({
  loop: true,
  margin: 30,
  nav: true,
  dots: false,
  autoplayHoverPause: false,
  autoplay: 6000,
  smartSpeed: 700,
  navText: ['<i class="fas fa-angle-left"></i>', '<i class="fas fa-angle-right"></i>'],
  responsive: {
    0: {
      items: 1
    },
    600: {
      items: 2
    },
    800: {
      items: 2
    },
    1024: {
      items: 3
    },
    1100: {
      items: 3
    },
    1200: {
      items: 4
    }
  }
})


"use strict";

//===RevolutionSliderActiver===
//===RevolutionSliderActiver===
function revolutionSliderActiver() {

  if ($('.rev_slider_wrapper #slider1').length) {
    // alert($("#topheader").height());
    // alert(window.innerHeight - ($("#topheader").height()));
    $("#slider1").revolution({

      sliderType: "standard",
      sliderLayout: "auto",
      delay: 5000,
      startwidth: 1500,
      //startheight: window.innerHeight - ($("#topheader").height()),
      startheight: 600,
      navigationType: "bullet",
      navigationArrows: "0",
      navigationStyle: "preview4",
      dottedOverlay: 'yes',
      hideTimerBar: "off",
      onHoverStop: "off",
      navigation: {
        arrows: { enable: true }
      },
      gridwidth: [1170],
      gridheight: [750]
    });
  };
}


// Testimonial Carousel Five
if ($('.testimonial-carousel-5').length) {
  $('.testimonial-carousel-5').owlCarousel({
    loop: true,
    margin: 30,
    nav: false,
    smartSpeed: 1000,
    autoplay: 500,
    navText: ['<span class="fas fa-arrow-up"></span>', '<span class="fas fa-arrow-down"></span>'],
    responsive: {
      0: {
        items: 1
      },
      480: {
        items: 1
      },
      600: {
        items: 1
      },
      800: {
        items: 1
      },
      1200: {
        items: 1
      }

    }
  });
}
// Dom Ready Function
jQuery(document).ready(function () {
  (function ($) {
    // add your functions
    revolutionSliderActiver();


  })(jQuery);
});

$(document).ready(function () {
  $('ul.nav li.dropdown').hover(function () {
    $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(200);
  }, function () {
    $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(200);
  });
});

$(window).on('load', function () {
  $("#loading").delay(300).fadeOut(300);
  $("#loading-center").on('click', function () {
    $("#loading").fadeOut(300);
  })
})





//=== Accordion ===
if ($('.accordion-inner').length) {
  $('.accordion-inner .acc-btn').click(function () {
    if ($(this).hasClass('active') !== true) {
      $('.accordion-inner .acc-btn').removeClass('active');
    }

    if ($(this).next('.acc-content').is(':visible')) {
      $(this).removeClass('active');
      $(this).next('.acc-content').slideUp(500);
    }

    else {
      $(this).addClass('active');
      $('.accordion-inner .acc-content').slideUp(500);
      $(this).next('.acc-content').slideDown(500);
    }
  });
}


/*==========   Add active class to accordions   ==========*/
$('.accordion__header').on('click', function () {
  $(this).parent('.accordion-item').toggleClass('opened');
  $(this).parent('.accordion-item').siblings().removeClass('opened');
})
$('.accordion__title').on('click', function (e) {
  e.preventDefault()
});


/*==========   Add active class to accordions   ==========*/
$('.accordion__header_faq').on('click', function () {
  $(this).parent('.accordion-item-faq').toggleClass('opened');
  $(this).parent('.accordion-item-faq').siblings().removeClass('opened');
})
$('.accordion__title_faq').on('click', function (e) {
  e.preventDefault()
});
// Search JS

$(document).ready(function () {
  $("#exampleModal").modal('show');
});
$(function () {
  $('.search_btn').on('click', function () {
    $(".search_open, .search_close").toggleClass("search_open search_close");
    $('.search').toggleClass('search_active');
    $('.go_btn').toggleClass('search_active_go_btn');
  });
});

$(function () {
  $('[data-toggle="tooltip"]').tooltip();
});

function explodePie(e) {
    if (typeof (e.dataSeries.dataPoints[e.dataPointIndex].exploded) === "undefined" || !e.dataSeries.dataPoints[e.dataPointIndex].exploded) {
        e.dataSeries.dataPoints[e.dataPointIndex].exploded = true;
    } else {
        e.dataSeries.dataPoints[e.dataPointIndex].exploded = false;
    }
    e.chart.render();
}

function toogleDataSeries(e) {
    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
        e.dataSeries.visible = false;
    } else {
        e.dataSeries.visible = true;
    }
    chart.render();
}

// Onclick video open

if ($('.video-bg-img').length > 0) {
    $('.video-bg-img').on('click', function (e) {
        var id = $(this).attr('data-video-id').replace('vid-', '');
        var format = $(this).attr('data-video-format');
        var title = $(this).children("span").html();
        var descrip = $(this).children("em").html();
        if (format == "youtube") {
            var vid = '<iframe src="https://www.youtube.com/embed/' + id + '?autoplay=1&loop=0&muted=0&rel=0" frameborder="0" allowfullscreen allow=autoplay></iframe>';
        }
        if (format == "vimeo") {
            var vid = '<iframe src="//player.vimeo.com/video/' + id + '?autoplay=1&loop=0&muted=0&rel=0" frameborder="0" allowfullscreen allow=autoplay></iframe>';
        }
        jQuery(this).html(vid);
    });
}

function ShowPopup() {
    $("#exampleModalfat").modal("show");
}

function ShowPopupSubscribeNewsletter() {
    $("#SubscribeNewsletter").modal("show");
}

function ShowAlertPopupSubscribeNewsletter() {
    //$("#SubscribeNewsletter").modal("close");
    $("#AlertSubscribeNewsletter").modal("show");
}

function isNumberKey(e) {
    var result = false;
    try {
        var charCode = (e.which) ? e.which : e.keyCode;
        if ((charCode > 31) && (charCode >= 48 && charCode <= 57)) {
            result = true;
        }
    }
    catch (err) {
        //console.log(err);
    }
    return result;
}

function isNumberKeyWithDot(e) {
    var result = false;
    try {
        var charCode = (e.which) ? e.which : e.keyCode;
        if ((charCode > 31) && (charCode >= 48 && charCode <= 57) || charCode == 46) {
            result = true;
        }
    }
    catch (err) {
        //console.log(err);
    }
    return result;
}

function lettersOnly() {
    var charCode = event.keyCode;

    if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8)

        return true;
    else
        return false;
}

$('.name-val').keypress(function (e) {
    var regex = new RegExp("^[a-zA-Z ]+$");
    var strigChar = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(strigChar)) {
        return true;
    }
    return false
});

$('.decimal').keyup(function () {
    var val = $(this).val();
    if (isNaN(val)) {
        val = val.replace(/[^0-9\.]/g, '');
        if (val.split('.').length > 2)
            val = val.replace(/\.+$/, "");
    }
    $(this).val(val);
});

function lettersWithSpaceOnly() {
    var charCode = event.keyCode;
    if (val.split('.').length > 2)
        val = val.replace(/\.+$/, "");
    if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)

        return true;
    else
        return false;
}

function runScript(e) {
    if (e.keyCode == 13) {
        $("#<%=btnSearchBox.ClientID%>").click(); //jquery
        //document.getElementById("btnSearchBox").click(); //javascript
    }
}

