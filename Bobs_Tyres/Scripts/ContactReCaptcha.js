$(document).ready(function () {

    var recaptcha1;
    var recaptcha2;

    var myCallBack = function () {
        recaptcha1 = grecaptcha.render('recaptcha1', {
            'sitekey': '6LdquhwUAAAAAN9IEqlL-Z_H9kbPVTQYG38wFXWD',
            'theme' : 'light'
        });

        recaptcha2 = grecaptcha.render('recaptcha2', {
            'sitekey': '6LdquhwUAAAAAN9IEqlL-Z_H9kbPVTQYG38wFXWD',
            'theme': 'dark'
        })
    };

    var verifyCallback = function (response) {
        alert("grecaptcha is ready!");
    };

    var expiredCallback = function () {
        alert("grecaptcha is expired!");
    };

});