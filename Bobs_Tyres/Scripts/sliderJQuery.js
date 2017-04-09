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

function starLoad(n) {
    $(document).ready(function () {
        console.log(n);
    })
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

function initMap() {
    var myLatLng = { lat: 50.881291, lng: -2.789033 };
    var map = new google.maps.Map(document.getElementById('googleMap'), {
        zoom: 17,
        center: myLatLng
    });
    var marker = new google.maps.Marker({
        position: myLatLng,
        map: map,
        title: "We're here!"
    });
}

function showImage(imageID) {
    $("#" + imageID).css({ "filter": "grayscale(0%)", "border": "2px solid #ccc" });
}

function hideImage(imageID) {
    if ($("#Image").val() != $("#" + imageID).attr("value")) {
        $("#" + imageID).css({ "filter": "grayscale(100%)", "border": "1px solid #e6e6e6" });
    }
}

function chooseImage(imageID) {
    $("#Image").val($("#" + imageID).attr("value"));
    $(".newsExistingImage").css({ "filter": "grayscale(100%)", "border": "1px solid #e6e6e6" })
    $("#imageDisplay img").attr("src", "/Content/Images/LatestNews/" + $("#" + imageID).attr("value"));
    $("#" + imageID).css({ "filter": "grayscale(0%)", "border": "2px solid green" });
}

function buttonlink() {
    var selectedValue = $("#buttonDropDown :selected").val();
    
    if (selectedValue == "noLink") {
        $("#ButtonLink").val("");
        $(".hiddenButtonLink").css("display", "none");
    }
    else {
        $("#ButtonLink").val("");
        $(".hiddenButtonLink").css("display", "block");
    }
}
