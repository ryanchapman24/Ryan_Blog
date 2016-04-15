$(document).ready(function () {
    $(".input-f").focus(function () {
        $(this).css("outline-color", "#388E8E");
    });
    $("#submit-f").click(function () {
        var one = parseInt($("input[name=1st-f]").val());
        var oneF = parseFloat($("input[name=1st-f]").val());
        var factorial = 1;

        if ($.trim($("#numbers-f").text()) != "") {
            $("#numbers-f").empty();
        }
        if (isNaN(one)) {
            $("#error-f").empty();
            $("#error-f").append("<p>You must submit a number!</p>");
        }
        else if (one < 0) {
            $("#error-f").empty();
            $("#error-f").append("<p>Your number must be positive!</p>");
        }
        else if ((oneF % 1) != 0) {
            $("#error-f").empty();
            $("#error-f").append("<p>You must enter a whole number!</p>");
        }
        else if (one === 1) {
            $("#error-f").empty();
            $("#numbers-f").append("<p>1! = 1.</p>");
        }
        else if (one === 0) {
            $("#error-f").empty();
            $("#numbers-f").append("<p>0! = 1..... for some odd reason...</p>");
        }
        else {
            if (one >= 0) {
                $("#error-f").empty();
                for (i = one; i > 1; i--) {
                    factorial *= i;
                }
                $("#numbers-f").prepend("<p>" + one + "! = " + factorial + ".</p>");
            }
        }
    });
    $("#again-f").click(function () {
        $("#numbers-f").empty();
        $("#error-f").empty();
        $("#form-f")[0].reset();
    });
});