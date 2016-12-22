// ====================================================
// Run on all pages always
// ====================================================
// Load categories to menu
//$(document).ready(function() {
//    var action = "/Category/GetCategories/";
//    $.ajax({
//        type: 'GET',
//        url: action,
//        contentType: "application/json; charset=utf-8",
//        datatype: "json",
//        success: function (result) {
//            var data = JSON.parse(result);
//            $(data).each(function (i, obj) {
//                // Add to menu
//                var link = '<a href=""';
//                $("#CategoriesMenu").append("<li>" + link + "</li>");
//            });
//        },
//        error: function () {
//            alert("Whoopsies!");
//        }
//    });
//})

// ====================================================
// Login Page
// ====================================================
function LoginAs(user) {
    // Fill fields
    if (user === "User") {
        $("#login-username").val("user@tgs.dk");
        $("#login-password").val("User123");
    }
    if (user === "Admin") {
        $("#login-username").val("admin@tgs.dk");
        $("#login-password").val("Admin123");
    }

    // Login
    $("#login-submit").click();
}

// ====================================================
// Admin Page
// ====================================================
// Menu selctor
function changeView(url, firstime) {
    // Loading
    $("#content").html("Loading... please wait!");
    // Set action
    var action = "/Admin/GetPartial" + url;
    // Active tab
    if (firstime === true)
        $("#" + url).addClass("active");

    // Ajax request
    $.ajax({
        type: 'GET',
        url: action,
        datatype: 'json',
        success: function (result) {
            $("#content").html(result);
        },
        error: function () {
            $("#content").html("ERROR!");
        }
    });
}

function chnageFeatured(id, view) {
    var url = "/Admin/ChangeFeatured";
    $.ajax({
        type: 'GET',
        url: url,
        data: { id: id },
        datatype: 'json',
        success: function (result) {
            if (result === "True") {
                //changeView(view);
                $("#featured-btn-" + id).removeClass("btn-success");
                $("#featured-btn-" + id).addClass("btn-warning");
                $("#featured-btn-" + id).val("Unfeature");
            } else {
                $("#featured-btn-" + id).removeClass("btn-warning");
                $("#featured-btn-" + id).addClass("btn-success");
                $("#featured-btn-" + id).val("Feature");
            }
        },
        error: function () {
            alert("Whoops! Something went wrong...");
        }
    });


}

// ====================================================
// Menu Cart
// ====================================================
function updateCart() {
    var action = "/Cart/GetCartInfo/";
    $.ajax({
        type: 'GET',
        url: action,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (result) {
            if (result !== "") {
                var data = JSON.parse(result);
                $("#cart-itemscount").html("" + data.ItemsInCart + "");
                $("#cart-amounttotal").html(data.TotalPrice.toFixed(2));
            } else {
                $("#cart-itemscount").html("0");
                $("#cart-amounttotal").html("0.00");
            }
        },
        error: function () {
            $("#cart-itemscount").html("0");
            $("#cart-amounttotal").html("0.00");
        }
    });
}

$(document).ready(function() {
    updateCart();
})

// ====================================================
// Add to Cart
// ====================================================
function addToCart(productId) {
    var action = "/Cart/AddToCart/";
    $.ajax({
        type: 'GET',
        url: action,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        data: {id: productId},
        success: function (result) {
            if (result !== true) {
                updateCart();
            } else {
                alert("Sorry someting went wrong!");
            }
        },
        error: function () {
            alert("Sorry someting went wrong!");
        }
    });
}

// ====================================================
// adjust product amount in cart
// ====================================================
function adjustAmount(productId, amount) {
    var action = "/Cart/AdjustAmount/";
    var shipping = $("#shipping-cost").html();
    var currentAmount = $("#cart-item-count-" + productId).html();
    var resultAmount = +currentAmount + amount;

    if (resultAmount > 0) {
        $.ajax({
            type: 'GET',
            url: action,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: { id: productId, amount: amount },
            success: function (result) {
                var data = JSON.parse(result);
                var total = (+data.CartPrice + +shipping);
                $("#cart-item-count-" + productId).html(data.Count);
                $("#cart-item-totalprice-" + productId).html(data.TotalPrice.toFixed(2));
                $("#cart-price").html(data.CartPrice.toFixed(2));
                $("#cart-total-price").html(total.toFixed(2));
                updateCart();
            },
            error: function () {
                alert("Sorry someting went wrong!");
            }
        });
    }

    if (resultAmount > 1) {
        $("#amount-minus-" + productId).removeClass("btn-warning");
        $("#amount-minus-" + productId).addClass("btn-primary");
    } else {
        $("#amount-minus-" + productId).removeClass("btn-primary");
        $("#amount-minus-" + productId).addClass("btn-warning");
    }
}