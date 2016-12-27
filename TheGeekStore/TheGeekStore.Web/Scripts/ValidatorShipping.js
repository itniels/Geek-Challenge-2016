function validateShipping() {
    //vars
    var isValid = true;
    var name = $("#input-name").val();
    var phone = $("#input-phone").val();
    var att = $("#input-att").val();
    var street1 = $("#input-street1").val();
    var street2 = $("#input-street2").val();
    var postal = $("#input-postal").val();
    var city = $("#input-city").val();
    var state = $("#input-state").val();
    var country = $("#input-country").val();

    // Name
    if (name.length === 0)
        isValid = false;
    // Phone
    if (phone.length === 0)
        isValid = false;
    // Street
    if (street1.length === 0)
        isValid = false;
    // Postal
    if (postal.length === 0)
        isValid = false;
    // City
    if (city.length === 0)
        isValid = false;
    // Country
    if (country.length === 0)
        isValid = false;

    // Change Submit buttons
    if (isValid) {
        $(":submit").removeClass("disabled");
    } else {
        $(":submit").addClass("disabled");
    }
}