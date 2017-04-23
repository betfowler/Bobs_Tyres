$(document).ready(function () {

    var verifyCallback = function (response) {
        alert("grecaptcha is ready!");
    };

    var expiredCallback = function () {
        alert("grecaptcha is expired!");
    };

});