// Menu integration
jQuery(document).ready(function () {
    //menu
    jQuery('header nav').meanmenu();
});


// Number counts
jQuery(document).ready(function ($) {
    $('.counter').counterUp({
        delay: 10,
        time: 1000
    });
});

// Popup
$(document).ready(function () {
    $('.popup-youtube, .popup-vimeo, .popup-gmaps').magnificPopup({
        //disableOn: 700,
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        fixedContentPos: false
    });
});

//--------------- feedback form validations -----------------//
$(document).ready(function () {
    $("#feedback_form").validate({
        rules: {
            name: {
                required: true
            },
            email: {
                required: true
            },
            subject: {
                required: true
            },
            message: {
                required: true
            },
        },
        messages: {
            name: {
                required: "Please enter your name"
            },
            email: {
                required: "Please enter your email "
            },
            subject: {
                required: "Please enter your subject "
            },
            message: {
                required: "Please enter your message "
            },

        }
    });
});
var results;
results = $('.select2-drop-active > .select2-results');
results.niceScroll({
    autohidemode: false,
    cursorcolor: "#4A4A4A",
    cursorwidth: 6
});





/*!
* jquery.counterup.js 1.0
*
* Copyright 2013, Benjamin Intal http://gambit.ph @bfintal
* Released under the GPL v2 License
*
* Date: Nov 26, 2013
*/(function (e) { "use strict"; e.fn.counterUp = function (t) { var n = e.extend({ time: 400, delay: 10 }, t); return this.each(function () { var t = e(this), r = n, i = function () { var e = [], n = r.time / r.delay, i = t.text(), s = /[0-9]+,[0-9]+/.test(i); i = i.replace(/,/g, ""); var o = /^[0-9]+$/.test(i), u = /^[0-9]+\.[0-9]+$/.test(i), a = u ? (i.split(".")[1] || []).length : 0; for (var f = n; f >= 1; f--) { var l = parseInt(i / n * f); u && (l = parseFloat(i / n * f).toFixed(a)); if (s) while (/(\d+)(\d{3})/.test(l.toString())) l = l.toString().replace(/(\d+)(\d{3})/, "$1,$2"); e.unshift(l) } t.data("counterup-nums", e); t.text("0"); var c = function () { t.text(t.data("counterup-nums").shift()); if (t.data("counterup-nums").length) setTimeout(t.data("counterup-func"), r.delay); else { delete t.data("counterup-nums"); t.data("counterup-nums", null); t.data("counterup-func", null) } }; t.data("counterup-func", c); setTimeout(t.data("counterup-func"), r.delay) }; t.waypoint(i, { offset: "100%", triggerOnce: !0 }) }) } })(jQuery);



//---------------- inline popup -------------//
$(document).ready(function () {
    $('.popup-with-move-anim').magnificPopup({
        type: 'inline',

        fixedContentPos: false,
        fixedBgPos: true,

        overflowY: 'auto',

        closeBtnInside: true,
        preloader: false,

        midClick: true,
        removalDelay: 300,
        mainClass: 'my-mfp-slide-bottom'
    });
});

// toggle content readmore expand 
$(".toggle_content").hide();
$(".toggle_block h4").click(function () {
    $(this).toggleClass("tgg_close").prev().slideToggle("medium");
    if ($(this).hasClass('tgg_close')) {
        $(this).text('Read Less');
    } else {
        $(this).text('Read More');
    }
});


// top Search drop down
$(document).ready(function () {
    $('.dropdown-toggle2').click(function (e) {
        $(".topSearchForm").toggle();
        $(".dropdown-toggle2").toggleClass('open');
        e.stopPropagation();
    });
});
$(document).click(function (e) {
    if (!$(e.target).is('.topSearchForm,.ui-autocomplete,.ui-corner-all, .topSearchForm *')) {
        $(".topSearchForm").hide();
        $(".dropdown-toggle2").removeClass('open');
    }
});

//scrollbar
$(".scroll").niceScroll({ cursorborder: "", cursorwidth: "3px", cursorcolor: "#333", boxzoom: false });



/* Rating for deals */
$(document).ready(function () {
    var options = {
        max_value: 6,
        step_size: 0.5,
        selected_symbol_type: 'hearts',
        url: 'http://localhost/test.php',
        initial_value: 3,
    }
    $(".rate").rate();

    $(".rate").rate("setFace", 2, '😊');
    $(".rate").rate("setFace", 1, '😒');

});

// Feedback footer & Raise a Request
$(document).ready(function () {
    $('#feedback_slide').click(function () {
        $(this).parent("#feedback").toggleClass("open");
    });

    $('#feedback_slide2').click(function () {
        $(this).parent("#feedback2").toggleClass("open");
    });

});




// top Search drop down
$(document).ready(function () {
    $('.email-signup-link').click(function (e) {
        //$(".email-signup").slideToggle('slow');
        $('.email-signup').toggle('slide', { direction: 'right' }, 500);
        //$(".email-signup").toggleClass('open');
        $('.feedback_inner').hide();
        e.stopPropagation();

    });
});
$(document).click(function (e) {
    if (!$(e.target).is('.email-signup, .email-signup *')) {
        $(".email-signup").hide();
        //$(".email-signup").removeClass('open');
    }
});

// bottom feedback
$(document).ready(function () {
    $('.feedback-link').click(function (e) {
        //$(".email-signup").slideToggle('slow');
        $('.feedback_inner').slideToggle('fast');
        //$(".email-signup").toggleClass('open');
        $('.email-signup').hide();
        e.stopPropagation();

    });
});
$(document).click(function (e) {
    if (!$(e.target).is('.feedback_inner, .feedback_inner *')) {
        $(".feedback_inner").hide();
        //$(".email-signup").removeClass('open');
    }
});





// Show / Hide div
$(document).ready(function () {
    $(".slidingDiv").hide();
    $(".show_hide").show();

    $("body").not(".show_hide,.slidingDiv").click(function () {
        $(".slidingDiv").fadeOut("slow");
    });

    $(".show_hide").click(function (e) {
        $(".slidingDiv").slideToggle('slow');
        e.stopPropagation();
    });

});


// Drop down Select select box
$(function () {
    // turn the element to select2 select style
    $('#e1').select2();
    $('#e2').select2();
});

// about us accordians 
$(document).ready(function () {
    var animTime = 300,
        clickPolice = false;

    $(document).on('touchstart click', '.acc-btn', function () {
        if (!clickPolice) {
            clickPolice = true;

            var currIndex = $(this).index('.acc-btn'),
                targetHeight = $('.acc-content-inner').eq(currIndex).outerHeight();

            $('.acc-btn h1').removeClass('selected');
            $(this).find('h1').addClass('selected');

            $('.acc-content').stop().animate({ height: 0 }, animTime);
            $('.acc-content').eq(currIndex).stop().animate({ height: targetHeight }, animTime);

            setTimeout(function () { clickPolice = false; }, animTime);
        }

    });

});


// add multiple div or inputs 
$(document).ready(function () {
    var max_fields = 10; //maximum input boxes allowed
    var wrapper = $(".subinputs1"); //Fields wrapper
    var add_button = $(".add_field_button"); //Add button ID

    var x = 1; //initlal text box count
    $(add_button).click(function (e) { //on add input button click
        e.preventDefault();
        if (x < max_fields) { //max input box allowed
            x++; //text box increment
            $(wrapper).append('<ul>  <li>    <label>Event Start Date</label>    <input type="text" name="event" placeholder="Event Start Date">  </li>  <li>    <label>Event Start Time <span>*</span></label>    <input type="text" name="event" placeholder="Event Start Time">  </li>  <li>    <label>Event End Date</label>    <input type="text" name="event" placeholder="Event End Date">  </li>  <li>    <label>Event End Time <span>*</span></label>    <input type="text" name="event" placeholder="Event End Time"></li><a href="#" class="remove_field">X</a></ul>'); //add input box
        }
    });

    $(wrapper).on("click", ".remove_field", function (e) { //user click on remove text
        e.preventDefault(); $(this).parent('ul').remove(); x--;
    })
});


// add multiple div or inputs 
$(document).ready(function () {
    var max_fields = 10; //maximum input boxes allowed
    var wrapper = $(".subinputs2"); //Fields wrapper
    var add_button = $(".add_field_button2"); //Add button ID

    var x = 1; //initlal text box count
    $(add_button).click(function (e) { //on add input button click
        e.preventDefault();
        if (x < max_fields) { //max input box allowed
            x++; //text box increment
            $(wrapper).append('<ul><li><div class="event-date"><label>Event Start Date <span>*</span></label><input type="text" name="Email" placeholder="DD/MM/YYYY" id="datetimepicker1"></div><div class="event-date"><label>Event Start Time <span>*</span></label><input type="text" name="Email" placeholder="HH:MM" id="datetimepicker3"></div></li><li><div class="event-date"><label>Event End Date <span>*</span></label><input type="text" name="Email" placeholder="DD/MM/YYYY" id="datetimepicker2"></div><div class="event-date"><label>Event End Time <span>*</span></label><input type="text" name="Email" placeholder="HH:MM" id="datetimepicker4"></div></li><a href="#" class="remove_field2">X</a></ul>'); //add input box
        }
    });

    $(wrapper).on("click", ".remove_field2", function (e) { //user click on remove text
        e.preventDefault(); $(this).parent('ul').remove(); x--;
    })
});







//---------------- Go Top Button -------------//
$(window).scroll(function () {
    if ($(this).scrollTop() > 100) {
        $('#btn-scrollup').fadeIn();
    } else {
        $('#btn-scrollup').fadeOut();
    }
});
$('#btn-scrollup').click(function () {
    $("html, body").animate({ scrollTop: 0 }, 600);
    return false;
});


