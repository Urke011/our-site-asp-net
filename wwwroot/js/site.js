// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Wrap all widget code into anonymous function expression to avoid conflicts
// that may occur with another code on the page (module pattern).

(function ($, rootUrl, templateUrl) {
    "use strict";


    function fixShadowProblem() {
        $(".navbar-toggler").on("click", function () {
            let ariaExpandedVal = document.getElementById('navbar-toggler').getAttribute('aria-expanded');
            if (ariaExpandedVal == "false") {
                $(".leider-nicht-navbar-expand-md").addClass("shadow");
            }
        });
    }

    window.addEventListener("scroll", function (event) {
        var scroll = this.scrollY;
        let ariaExpandedVal = document.getElementById('navbar-toggler').getAttribute('aria-expanded');
        if (scroll == 0 && ariaExpandedVal == "true") {
            $(".leider-nicht-navbar-expand-md").css("-webkit-box-shadow", "0 3px 8px 0 rgb(128 128 128 / 25%)");
        }

    })

    function fixOverlap() {
        $(".overlap-top-big").each(function () {
            let h = $(this).height();
            $(this).css("margin-top", "-" + h * 2 + "px");
            $(this).css("margin-bottom", h - 10 + "px");
            $(this).css("width", "");
            $(this).attr("style", "margin-top: -" + h * 2 + "px !important");
            $(this).css("margin-bottom", h - 10 + "px");
            $(this).css("width", "");
        });

        $(".overlap-top").each(function () {
            let h = $(this).height();
            $(this).css("margin-top", "-" + h * 1.5 + "px");
            $(this).css("margin-bottom", h * 0.5 - 5 + "px");
            $(this).css("width", "");
            $(this).attr("style", "margin-top: -" + h * 1.5 + "px !important");
            $(this).css("margin-bottom", h * 0.5 - 5 + "px");
            $(this).css("width", "");
        });

        $(".overlap-top-small").each(function () {
            let h = $(this).height();
            $(this).css("margin-top", "-" + (h + 5) + "px");
            $(this).css("margin-bottom", "0px");
            $(this).css("width", "");
            $(this).attr("style", "margin-top: -" + (h + 5) + "px !important");
            $(this).css("margin-bottom", "0px");
            $(this).css("width", "");
        });

        $(".overlap-top-xsmall").each(function () {
            let h = $(this).height();
            $(this).css("margin-top", "-" + (h * 0.6 + 2) + "px");
            $(this).css("margin-bottom", "0px");
            $(this).css("width", "");
            $(this).attr("style", "margin-top: -" + (h * 0.6 + 2) + "px !important");
            $(this).css("margin-bottom", "0px");
            $(this).css("width", "");
        });

        if ($(window).width() < 1200) {
            $(".overlap-no-mobile").each(function () {
                let h = $(this).height();
                $(this).css("margin-top", "1px");
                $(this).css("margin-bottom", "1px");
                $(this).css("width", "auto");
                $(this).attr("style", "margin-top: 1px !important");
                $(this).css("margin-bottom", "1px");
                $(this).css("width", "auto");
            });
        }
    }

    function galleryInit() {
        let counter = 0;
        $(".gallery").attr("data-toggle", "modal");
        $(".gallery").attr("data-target", "#exampleModal");
        let width = 0;
        $(".gallery img").each(function () {
            $(this).attr("data-target", "#carouselExample");
            $(this).attr("data-slide-to", counter);
            $(this).css("object-fit", "cover");
            width += $(this).width();
            //$(this).height($(this).width() * 0.8);
            counter++;
        });
        if (counter > 0) {
            width = width / counter;
            let first = true;
            let html = "";
            $(".gallery img").each(function () {
                $(this).width(width);
                $(this).height(width * 0.75);
                html += '<div class="carousel-item ';
                if (first) {
                    first = false;
                    html += " active ";
                }
                html += ' "><img class="d-block w-100" src="' + $(this).attr("src") + '"></div>';
            });
            $("#gallery-carousel").html(html);
        } //if(counter>0){
    }

    let rotatorIndex = 0;
    let rotatorMax = 0;

    function rotatorInit() {
        let counter = 0;
        let height = 0;
        $(".m-rotator tr").each(function () {
            $(this).attr("rotator-index", counter);
            $(this).addClass("shadow");
            $(this).addClass("rotator-object");
            $(this).addClass("m-3");
            counter++;

        });
        rotatorMax = counter;
        if (counter < 0) return;
        let max = 0;
        $(".m-rotator td img")
            .parent()
            .each(function () {
                max = Math.max(max, Math.max($(this).height(), $(this).width()));
            });
        $(".m-rotator td img").each(function () {
            $(this).height(max);
            $(this).width(max);
            $(this).css("max-height", "290px");
            $(this).css("object-fit", "cover");
        });
        $(
            '<tr><td class="p-2 pr-3" colspan="2" align="right" nowrap><nobr>\
          <a id="rotator-left" href="#"><img width="30" height="30" alt="<<" src="' +
            rootUrl +
            '/images/icons/arrow-left.svg"></a>\
          &nbsp; <a id="rotator-right" href="#"><img width="30" height="30" alt=">>" src="' +
            rootUrl +
            '/images/icons/arrow-right.svg"></a>\
          </nobr></td></tr>'
        ).insertBefore($(".m-rotator tr").first());
        $("#rotator-left").click(function () {
            $(".rotator-object").hide();
            rotatorIndex--;
            if (rotatorIndex >= rotatorMax) {
                rotatorIndex = 0;
            }
            if (rotatorIndex < 0) {
                rotatorIndex = rotatorMax - 1;
            }
            $(".rotator-object[rotator-index=" + rotatorIndex + "]").show();
            return false;
        });
        $("#rotator-right").click(function () {
            $(".rotator-object").hide();
            rotatorIndex++;
            if (rotatorIndex >= rotatorMax) {
                rotatorIndex = 0;
            }
            if (rotatorIndex < 0) {
                rotatorIndex = rotatorMax - 1;
            }
            $(".rotator-object[rotator-index=" + rotatorIndex + "]").show();
            return false;
        });
        let gcounter = -1;
        $(".m-rotator tr").each(function () {
            if (gcounter <= 0) {
                $(this).show();
            } else {
                $(this).hide();
            }
            gcounter++;
        });
    }

    function checkAlert() {
        $("#privacy_alert").removeClass("show");
        $("#privacy_alert").hide();
        let privacy = getCookie("privacy");
        if (privacy === undefined || privacy == null) {
            privacy = 0;
        }
        privacy = parseInt(privacy);
        if (privacy < 1) {
            $("#privacy_alert").show();
            $("#privacy_alert").addClass("show");
            $("#privacy_alert").on("closed.bs.alert", function () {
                setCookie("privacy", 1, 365);
            });
        }
    }


    function updateLinksWithBackImages() {
        $('a[back]').each(function () {
            let content = $(this).html();
            let title = $(this).attr('title');
            let url = $(this).attr('href');
            let back = $(this).attr('back');
            if ((!back.startsWith('http://')) && (!back.startsWith('https://'))) {
                if (back.startsWith('/')) {
                    back = rootUrl + back;
                } else {
                    back = rootUrl + '/' + back;
                }
            }
            let from_right = $(this).attr('dir') == 'rtl';
            let align_class = from_right ? " float-right" : " float-left";
            let new_html = '<div class="mathema-link-with-background" style="background-image: url(\'' + back + '\');">';
            new_html += '<div class="card h-100 mathema-link-child-block ' + align_class + '">';
            if ((typeof title) != 'undefined') {
                new_html += '<div class="mathema-link-block-title font-special text-red">' + title + '</div>';
            }
            if ((typeof content) != 'undefined') {
                new_html += '<div class="mathema-link-block-content font-normal text-black small">' + content + '</div>';
            }
            new_html += '</div>'; //card
            new_html += '</div>'; //div with background
            $(this).css('display', 'block');
            $(this).html(new_html);
            $(this).removeAttr('title');
            $(this).removeAttr('back');
            $(this).removeAttr('dir');
        })
    }

    //<!-- Matomo -->
    function initStat() { //<!-- Matomo -->
        try {
            let u = "https://piwik.mathema.de/";
            _paq.push(['setTrackerUrl', u + 'matomo.php']);
            _paq.push(['setSiteId', '1']);
            let d = document,
                g = d.createElement('script'),
                s = d.getElementsByTagName('script')[0];
            g.type = 'text/javascript';
            g.async = true;
            g.defer = true;
            g.src = u + 'matomo.js';
            s.parentNode.insertBefore(g, s);
        } catch (e) {
            console.log('Matomo Error');
        }
    } //<!-- End Matomo Code -->


    // Jobs, Jobs, Jobs - Widget
    function checkJobWidgetExists() {
        try {
            if ($('#psJobWidget').length) {
                $('#psJobWidget').html('');
                let newScript = document.createElement('script');
                let firstScript = document.getElementsByTagName('script')[0];
                newScript.type = 'text/javascript';
                newScript.async = true;
                newScript.defer = true;
                newScript.src = 'https://mathema.jobbase.io/widget/iframe.js?config=nigg9qm0';
                firstScript.parentNode.insertBefore(newScript, firstScript);
            }
        } catch (e) {
            console.log('Jobs, Jobs, Jobs Error');
        }
    }


    function initTimeline() {
        //show only first 3 div from loop, rest hide
        $('.mathema_timeline .container').slice(3).hide();
        $('#show_more_timeline_items').click(event => {
            // Call the "smoothScroll" function
            if ($('#show_more_timeline_items').text() == "Mehr anzeigen") {
                smoothScrollDown(event);
            } else {
                smoothScrollUp(event);
            }
        });
    }

    // Jobs, Jobs, Jobs - Widget

    $(document).ready(function () {
        checkAlert();
        galleryInit();
        rotatorInit();
        fixOverlap();
        window.addEventListener("resize", function (event) {
            fixOverlap();
        });
        fixShadowProblem();

        updateLinksWithBackImages();
        initTimeline();

        //ALWAYS LAST AFTER All
        initStat();
        checkJobWidgetExists();
    });



    function setCookie(name, value, days) {
        let expires = "";
        if (days) {
            let date = new Date();
            date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    }

    function getCookie(name) {
        let nameEQ = name + "=";
        let ca = document.cookie.split(";");
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) == " ") c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    function eraseCookie(name) {
        document.cookie = name + "=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;";
    }

    function smoothScrollDown(event) {
        event.preventDefault();
        $("#show_more_timeline_items").text("Weniger anzeigen");
        $('.toggleIcons').attr('src', 'images/icons/toggleUpRed.svg');
        $('.mathema_timeline .container').show();

    }
    function smoothScrollUp(event) {
        event.preventDefault();
        $("#show_more_timeline_items").text("Mehr anzeigen");
        $('.toggleIcons').attr('src', 'images/icons/toggleDownRed.svg');
        $('.mathema_timeline .container').slice(3).hide();
        const targetId = event.currentTarget.getAttribute("href");
        document.querySelector(targetId).scrollIntoView({
            behavior: "smooth",
            block: "end"
        });
    }
    $(document).keyup(function (e) {
        if (e.keyCode == 27) { // escape key maps to keycode `27`
            $('.navbar-slide-content-item').removeClass('show');
            $("button").attr("aria-expanded", "false");
        }
    });

 

})(jQuery, rootUrl, templateUrl);




