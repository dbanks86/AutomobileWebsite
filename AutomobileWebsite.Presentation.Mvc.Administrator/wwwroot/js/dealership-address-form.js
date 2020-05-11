$(document).ready(function () {
    $(".btn").click(function () {
        ClearFormSubmitMessage();
    });
});

function ClearFormSubmitMessage() {
    $("#success-message").text("");
    $("#error-message").text("");
}