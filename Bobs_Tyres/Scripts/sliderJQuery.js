function plusSlides(n, numOfSlides, currentSlide) {
    slideIndex = parseInt(currentSlide) + n;

    //hide current slide
    $(".news" + currentSlide).css("display", "none");

    if (slideIndex < 1) {
        slideIndex = numOfSlides;
    }
    if (slideIndex > numOfSlides) {
        slideIndex = 1;
    }

    //show next slide
    $(".news" + slideIndex).css("display", "block")
}

function selectSlide(selected) {
    //hide all
    $(".news").css("display", "none");
    slideIndex = parseInt(selected);
    $(".news" + slideIndex).css("display", "block")
}

function starHover(n) {
    for (var i = 1; i <= 5; i++) {
        $("#star" + i).html("&#9734");
    }
    for (var i = 1; i <= n; i++)
    {
        $("#star" + i).html("&#9733");
    }
}

function starOut(n) {
    for (var i = 1; i <= n; i++) {
        $("#star" + i).html("&#9734");
    }
    var selected = $("#rating").attr("value");
    console.log(selected);
    if (selected != null) {
        for (var i = 1; i <= selected; i++) {
            $("#star" + i).html("&#9733");
        }
    }
}

function selectStar(n) {
    for (var i = 1; i <= 5; i++) {
        $("#star" + i).html("&#9734");
    }
    for (var i = 1; i <= n; i++) {
        $("#star" + i).html("&#9733");
    }
    $("#rating").attr("value", n);
}