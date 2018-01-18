$(document).ready(function () {
    $(".input-p").focus(function () {
        $(this).css("outline-color", "#388E8E");
    });
    $("#submit-p").click(function () {
        var one = $("input[name=1st-p]").val().toUpperCase();
        var oneR = one.split('').reverse().join('');
        var numberCheck = parseInt(one);
       
        if ($.trim($("#answer-p").text()) != "") {
            $("#answer-p").empty();
        }
        if ((one === "")) {
            $("#error-p").empty();
            $("#error-p").append("<p>You did not submit anything!</p>");
        }
        else if (isNaN(numberCheck) === false) {
            $("#error-p").empty();
            $("#error-p").append("<p>Numbers are invalid!</p>");
        }
        else {
            $("#error-p").empty();
            if (one === oneR) {
                $("#answer-p").append("<p>'" + one + "' is a palindrome!</p>");
            }
            else {
                $("#answer-p").append("<p>'" + one + "' is not a palindrome.</p>");
            }
        }        
    });
    $("#again-p").click(function () {
        $("#answer-p").empty();
        $("#error-p").empty();
        $("#form-p")[0].reset();
    });
});
