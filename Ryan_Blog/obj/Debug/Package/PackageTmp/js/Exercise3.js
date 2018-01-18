$(document).ready(function () {
    $(".input-fb").focus(function () {
        $(this).css("outline-color", "#388E8E");
    });
    $("#submit-fb").click(function () {
        var one = parseInt($("input[name=1st-fb]").val());
        var two = parseInt($("input[name=2nd-fb]").val());
        var oneF = parseFloat($("input[name=1st-fb]").val());
        var twoF = parseFloat($("input[name=2nd-fb]").val());

        if ($.trim($("#numbers-fb").text()) != "") {
            $("#numbers-fb").empty();
        }
        if ((isNaN(one)) || (isNaN(two))) {
            $("#error-fb").empty();
            $("#error-fb").append("<p>You must submit 2 numbers!</p>");
        }
        else if (((one < 1) || (one > 100)) || ((two < 1) || (two > 100))) {
            $("#error-fb").empty();
            $("#error-fb").append("<p>Your numbers must be between 1-100!</p>");
        }
        else if (((oneF % 1) != 0) || ((twoF % 1) != 0)) {
            $("#error-fb").empty();
            $("#error-fb").append("<p>You must enter whole numbers!</p>");
        }
        else {
            if (((one >= 1) && (one <= 100)) && ((two >= 1) && (two <= 100))) {
                $("#error-fb").empty();
                for (i = 1; i < 101; i++) {
                    if ((i % one === 0) && (i % two === 0)) {
                        $("#numbers-fb").append("<p>FizzBuzz</p>");
                    }
                    else if (i % one === 0) {
                        $("#numbers-fb").append("<p>Fizz</p>");
                    }
                    else if (i % two === 0) {
                        $("#numbers-fb").append("<p>Buzz</p>");
                    }
                    else {
                        $("#numbers-fb").append("<p>" + i + "</p>");
                    }
                }
            }
        }
    });
    $("#again-fb").click(function () {
        $("#numbers-fb").empty();
        $("#error-fb").empty();
        $("#form-fb")[0].reset();
    });
});