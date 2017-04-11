$(document).ready(function () {
    var reviewAverage = $("#reviewaverage").text();
    for (var i = 1; i <= reviewAverage; i++) {
        $("#star" + i).html("&#9733");
    }

    if ($("#rating").attr('value') != null) {
        var n = $("#rating").attr('value');
        for (var i = 1; i <= n; i++) {
            $("#star" + i).html("&#9733");
        }
    
    }

});