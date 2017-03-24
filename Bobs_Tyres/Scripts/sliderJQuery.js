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