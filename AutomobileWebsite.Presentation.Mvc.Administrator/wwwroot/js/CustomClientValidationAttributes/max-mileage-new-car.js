var maxMileageNewCar = "maxmileagenewcar";

$.validator.addMethod(maxMileageNewCar, function (value, element, params) {
    var cbIsNew = $("#is-new");

    if (value < 0) {
        this.settings.messages[element.name][maxMileageNewCar] = function () { return element.name + " must be greater than zero" };
        return false;
    }

    if (cbIsNew.prop("checked") && value > parseInt(params.maxmileage)) {
        this.settings.messages[element.name][maxMileageNewCar] = function () { return element.name + " for new cars cannot be greater than " + params.maxmileage};
        return false;
    }

    return true;
});

$.validator.unobtrusive.adapters.add(maxMileageNewCar, ['maxmileage'], function (options) {
    // NOTE: params array of validator.unobtrusive.adapters.add MUST BE LOWERCASE and spelled just like constructor parameter of CustomValidate Attribute C# object
    options.rules[maxMileageNewCar] = options.params;
    options.messages[maxMileageNewCar] = options.message;
});