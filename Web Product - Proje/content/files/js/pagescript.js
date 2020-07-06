
$(document).ready(function () {
    //imza title
    $('[data-toggle="tooltip"]').tooltip();

    //Hata Sayfasi Sayac
    $(function () {
        var saniye = $("#sayachidden").val();
        var sayacYeri = $(".404sayac span");
        if (saniye != null) {
            $.sayimiBaslat = function () {
                if (saniye > 1) {
                    saniye--;
                    sayacYeri.text(saniye);
                } else {
                    $("div.404sayac").text("Yonlendiriliyorsunuz..");
                    $(location).attr('href', '/');
                }
            }

            sayacYeri.text(saniye);
            setInterval("$.sayimiBaslat()", 1000);
        }
    });

});

//arama
$("#aramabutton").click(function () {
    window.location = "/urun-ara/" + $("#aramaurunadi").val();
});

//slider
$('.carousel').carousel({
    interval: 2000,
    keyboard: true,
    touch: true
})
//Popup
$(document).ready(function () {
    if ($("#popupgoster").val() == "True") {
        $('#sitemodal').modal('show');
    }
})
//owl slider
$(document).ready(function () {
    $('.owl1').owlCarousel({
        loop: true,
        //stagePadding: 50, //sag ve sol basi yarim gosterir
        autoplay: true,
        nav: true,
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                nav: true,
                dots: false
            },
            400: {
                items: 2,
                nav: true,
                dots: false
            },
            600: {
                items: 3,
                nav: true,
                dots: false
            },
            768: {
                items: 2,
                nav: true,
                dots: false
            },
            992: {
                items: 3,
                nav: false,
                dots: true
            },
            1200: {
                items: 4,
                nav: true,
                dots: true
            }
        }
    })
})