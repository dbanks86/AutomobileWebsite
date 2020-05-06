$(document).ready(function () {
    var previousSelectedValue;

    $(".btn").click(function () {
        ClearFormSubmitMessage();
    });

    $("#dealership-select")
        .on('focus', function () {
            previousSelectedValue = $(this).val();
        })
        .change(function () {
            ClearFormSubmitMessage();

            var baseUrl = $("base").attr("href");

            $.get(baseUrl + "api/Dealerships/GetDealershipById", { dealershipId: $(this).val() })
                .done(function (dealership) {
                    $("#dealership-name").val(dealership.dealershipName);
                    $("#website-url").val(dealership.websiteUrl);
                    $("#is-active").prop({ "checked": dealership.isActive });

                    $('.input-validation-error').addClass('input-validation-valid');
                    $('.input-validation-error').removeClass('input-validation-error');

                    $('.field-validation-error').addClass('field-validation-valid');
                    $('.field-validation-error').empty();

                    previousSelectedValue = $("#dealership-select").val();
                })
                .fail(function () {
                    alert("An error has occurred");
                    $("#dealership-select").val(previousSelectedValue);
                });
        });
});

function ClearFormSubmitMessage() {
    $("#success-message").text("");
    $("#error-message").text("");
}