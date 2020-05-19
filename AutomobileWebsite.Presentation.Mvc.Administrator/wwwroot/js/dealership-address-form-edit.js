$(document).ready(function () {
    var previousSelectedValue;
    var previousDealershipAddressValue;

    $("#dealership-select")
        .on('focus', function () {
            previousSelectedValue = $(this).val();
        })
        .change(function () {
            ClearFormSubmitMessage();

            var baseUrl = $("base").attr("href");
            var dealershipAddressId;
            var dealershipId = 0;

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

                        dealershipAddressId = 0;
                        dealershipId = $("#dealership-select").val();
                    }

                    if (dealershipAddresses.length == 1) {
                        dealershipAddressId = dealershipAddresses[0].dealershipAddressId;
                    }

                    $.each(dealershipAddresses, function (i, dealershipAddress) {
                        $('#dealership-address-select').append($('<option>', {
                            value: dealershipAddress.dealershipAddressId,
                            text: dealershipAddress.street + ", " + dealershipAddress.city + ", " + dealershipAddress.stateAbbreviation + " " + dealershipAddress.zipCode
                        }));
                    });

                    $.get(baseUrl + "Dealerships/GetDealershipAddressById", { dealershipAddressId: dealershipAddressId, dealershipId: dealershipId })
                        .done(function (dealershipAddress) {
                            $("#dealership-address-form-container").html(dealershipAddress);
                        })
                        .fail(function () {
                            alert("An error has occurred");
                            $("#dealership-select").val(previousSelectedValue);
                        });

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

    $("#dealership-address-select")
        .on('focus', function () {
            previousDealershipAddressValue = $(this).val();
        })
        .change(function () {
            ClearFormSubmitMessage();

            var baseUrl = $("base").attr("href");

            $.get(baseUrl + "Dealerships/GetDealershipAddressById", { dealershipAddressId: $(this).val() })
                .done(function (dealershipAddress) {
                    $("#dealership-address-form-container").html(dealershipAddress);

                    previousDealershipAddressValue = $("#dealership-address-select").val();
                })
                .fail(function () {
                    alert("An error has occurred");
                    $("#dealership-address-select").val(previousDealershipAddressValue);
                });
        });
});

function ClearFormSubmitMessage() {
    $("#success-message").text("");
    $("#error-message").text("");
}