$(document).ready(validatePayment());

function validatePayment() {
    console.log("validating payment...");
    var isValid = true;
    var cc = $("#cardnumber").val();
    var month = $("#month").val();
    var year = $("#year").val();
    var cvv = $("#cvv").val();

    // CC
    if (cc.length === 0) {
        isValid = false;
    }
    // Month
    if (month.isNaN()|| month === "" || +month <= 0 || +month > 12) {
        isValid = false;
    }
    // Year
    if (year.isNaN() || year === "" || +year < 16) {
        isValid = false;
    }
    // CVV
    if (cvv.isNaN() || cvv === "" || cvv > 999) {
        isValid = false;
    }

    // Button
    if (isValid) {
        $("#payment-button").removeClass("disabled");
    } else {
        $("#payment-button").addClass("disabled");
    }
}

function fillMastercard() {
    var cc = "5555555555554444";
    var m = "06";
    var y = "19";
    var cvv = "123";
    $("#cardnumber").val(cc);
    $("#month").val(m);
    $("#year").val(y);
    $("#cvv").val(cvv);
    updateCard();
    validatePayment();
}

function fillVisa() {
    var cc = "4111111111111111";
    var m = "08";
    var y = "20";
    var cvv = "123";
    $("#cardnumber").val(cc);
    $("#month").val(m);
    $("#year").val(y);
    $("#cvv").val(cvv);
    updateCard();
    validatePayment();
}

function fillAmex() {
    var cc = "378282246310005";
    var m = "01";
    var y = "21";
    var cvv = "123";
    $("#cardnumber").val(cc);
    $("#month").val(m);
    $("#year").val(y);
    $("#cvv").val(cvv);
    updateCard();
    validatePayment();
}