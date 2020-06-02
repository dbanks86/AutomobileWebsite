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

            $.get(baseUrl + "api/Dealerships/GetDealershipAddressesByDealershipId", { dealershipId: $(this).val() })
                .done(function (dealershipAddresses) {
                    $("#dealership-address-select option").remove();

                    if (dealershipAddresses.length > 1) {
                        $('#dealership-address-select').append($('<option>', {
                            value: "",
                            text: "Select Address",
                            disabled: true,
                            selected: true
                        }));
                    }

                    $.each(dealershipAddresses, function (i, dealershipAddress) {
                        $('#dealership-address-select').append($('<option>', {
                            value: dealershipAddress.dealershipAddressId,
                            text: dealershipAddress.street + ", " + dealershipAddress.city + ", " + dealershipAddress.stateAbbreviation + " " + dealershipAddress.zipCode
                        }));
                    });

                    $('#dealership-address-select.input-validation-error').addClass('input-validation-valid');
                    $('#dealership-address-select.input-validation-error').removeClass('input-validation-error');

                    $('#dealership-address-select-error-message').addClass('field-validation-valid');
                    $('#dealership-address-select-error-message').empty();

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