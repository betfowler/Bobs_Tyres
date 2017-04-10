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

function noImage() {
    $(".urlLink").css("display", "none");
    $(".uploadImage").css("display", "none");
    $(".existingImg").css("display", "none");
    $("#dropdownTitle").text("No Image");
    $("#Image").val("");
}

function imageUrl() {
    $(".urlLink").css("display", "block");
    $(".uploadImage").css("display", "none");
    $(".existingImg").css("display", "none");
    $("#dropdownTitle").text("Url link to image");
    $("#Image").val("");
}

function uploadImage(){
    $(".urlLink").css("display", "none");
    $(".uploadImage").css("display", "block");
    $(".existingImg").css("display", "none");
    $("#dropdownTitle").text("Upload image");
    $("#Image").val("");
}

function existingImage(){
    $(".urlLink").css("display", "none");
    $(".uploadImage").css("display", "none");
    $(".existingImg").css("display", "block");
    $("#dropdownTitle").text("Use existing image");
    $("#Image").val("");
}

function removeAjax(imageName) {
    $.ajax({
        type: "POST",
        url: "/Accounts/AjaxRemoveImage",
        data: '{imageName: "' + imageName +'"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $(".alert-success").css("display", "block");
            $("#successMessage").text("Image removed.");
            $(".uploadedImage").css("display", "none");
            $(".dropdown").css("display", "block");
            

        },
        failure: function (response) {
            $(".alert-danger").css("display", "block");
            $("#errorMessage").text("Unable to remove image.");
        },
        error: function (response) {
            $(".alert-danger").css("display", "block");
            $("#errorMessage").text("Unable to remove image.");
        }
    });
}

function setImageVal(imageName, folder) {
    $("#Image").attr('value', imageName);
    $("#folderLocation").attr('value', folder)
}