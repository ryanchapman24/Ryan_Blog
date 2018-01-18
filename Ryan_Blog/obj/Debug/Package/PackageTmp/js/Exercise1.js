$(document).ready(function () {
    $(".input-ng").focus(function () {
        $(this).css("outline-color", "#388E8E");
    });
   
    $("#again-ng").click(function () {
        $("#numbers-ng").empty();
        $("#error-ng").empty();
        $("#form-ng")[0].reset();
    });

    $("#submit-ng").click(function () {
        var one = parseFloat($("input[name=1st-ng]").val());
        var two = parseFloat($("input[name=2nd-ng]").val());
        var three = parseFloat($("input[name=3rd-ng]").val());
        var four = parseFloat($("input[name=4th-ng]").val());
        var five = parseFloat($("input[name=5th-ng]").val());
        var lowest = Math.min(one, two, three, four, five);
        var highest = Math.max(one, two, three, four, five);
        var average = Math.round(((one + two + three + four + five) / 5) * 1000) / 1000;
        var sum = Math.round((one + two + three + four + five) * 1000) / 1000;
        var product = Math.round((one * two * three * four * five) * 1000) / 1000;

        if ($.trim($("#numbers-ng").text()) != "") {
            $("#numbers-ng").empty();
        }

        if ((!(isNaN(one))) && (!(isNaN(two))) && (!(isNaN(three))) && (!(isNaN(four))) && (!(isNaN(five)))) {
            $("#numbers-ng").append("<p>LOWEST: " + lowest + "</p>");
            $("#numbers-ng").append("<p>HIGHEST: " + highest + "</p>");
            $("#numbers-ng").append("<p>AVERAGE: " + average + "</p>");
            $("#numbers-ng").append("<p>SUM: " + sum + "</p>");
            $("#numbers-ng").append("<p>PRODUCT: " + product + "</p>");
            $("#error-ng").empty();
        }

        else {
            $("#error-ng").empty();
            $("#error-ng").append("<p>You must submit 5 numbers!</p>");
        }
    });
});