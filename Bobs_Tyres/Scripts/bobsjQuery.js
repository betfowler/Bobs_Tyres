$(document).ready(function () {
    var reviewAverage = $("#reviewaverage").text();
    for (var i = 1; i <= reviewAverage; i++) {
        $("#star" + i).html("&#9733");
    }
});