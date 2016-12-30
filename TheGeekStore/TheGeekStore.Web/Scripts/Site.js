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

// Action selctor
function changeViewAction(url) {
    // Loading
    $("#content").html("Loading... please wait!");

    // Ajax request
    $.ajax({
        type: 'GET',
        url: url,
        datatype: 'json',
        success: function (result) {
            $("#content").html(result);
        },
        error: function () {
            $("#content").html("ERROR!");
        }
    });
}

function changeViewEdit(url, id) {
    // Loading
    $("#content").html("Loading... please wait!");

    // Ajax request
    $.ajax({
        type: 'GET',
        url: url,
        data: {id: id},
        datatype: 'json',
        success: function (result) {
            $("#content").html(result);
        },
        error: function () {
            $("#content").html("ERROR!");
        }
    });
}

function changeFeatured(id, view) {
    var url = "/Admin/ChangeFeatured";
    $.ajax({
        type: 'GET',
        url: url,
        data: { id: id },
        datatype: 'json',
        success: function (result) {
            if (result === "True") {
                //changeView(view);
                $("#featured-btn-" + id).html("Unfeature");
                $("#featured-btn-" + id).removeClass("btn-success");
                $("#featured-btn-" + id).addClass("btn-warning");
                
            } else {
                $("#featured-btn-" + id).html("Feature");
                $("#featured-btn-" + id).removeClass("btn-warning");
                $("#featured-btn-" + id).addClass("btn-success");
                
            }
        },
        error: function () {
            alert("Whoops! Something went wrong...");
        }
    });
}

// =====================================================
// Delete Modal
// =====================================================
function showDeleteModal(id, action) {
    $("#modal-body-delete").html("Please wait! Loading...");
    $("#delete-item-id").html(id);

    $.ajax({
        type: 'GET',
        url: action,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        data: { Id: id },
        success: function (result) {
            $("#modal-body-delete").html(result);
        },
        error: function () {
            $("#modal-body-delete").html("Oops.. Something went wrong! :-(");
        }
    });
}

function deleteItem(action) {
    var id = $("#delete-item-id").html();

    $.ajax({
        type: 'POST',
        url: action,
        data: { Id: id },
        success: function (result) {
            location.reload();
        },
        error: function () {
            alert("Oops.. Something went wrong! :-(");
        }
    });
}

// ====================================================
// Customer Page
// ====================================================
// Menu selctor
function changeCustomerView(url, firstime) {
    // Loading
    $("#content").html("Loading... please wait!");
    // Set action
    var action = "/Customer/GetPartial" + url;
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

// Admin Details
function showDetailsModal(id, action) {
    console.log(action);
    $("#modal-details-body").html("Please wait! Loading...");
    
    $.ajax({
        type: 'GET',
        url: action,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        data: { Id: id },
        success: function (result) {
            $("#modal-details-body").html(result);
        },
        error: function () {
            $("#modal-details-body").html("Oops.. Something went wrong! :-(");
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

$(document)
    .ready(function() {
        updateCart();
    });

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
    var shipping = $("#shipping-cost").val();
    var currentAmount = $("#cart-item-count-" + productId).html();
    var resultAmount = +currentAmount + +amount;

    if (resultAmount > 0) {
        $.ajax({
            type: 'GET',
            url: action,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: { id: productId, amount: amount },
            success: function (result) {
                var data = JSON.parse(result);
                var priceCart = data.CartPrice.toFixed(2);
                var priceTotal = data.TotalPrice.toFixed(2);
                var countItem = data.Count;
                var total = +(priceCart + shipping);

                $("#cart-item-count-" + productId).html(countItem);
                $("#cart-item-totalprice-" + productId).html(priceTotal);
                $("#cart-price").html(priceCart);
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

// ====================================================
// Payment Page
// ====================================================
function updateCard(obj) {
    var cardnumber = $("#cardnumber").val();

    if (/^5[1-5]/.test(cardnumber)) {
        $("#cc-mastercard").removeClass().addClass("cc-shown");
        $("#cc-visa").removeClass().addClass("cc-faded");
        $("#cc-amex").removeClass().addClass("cc-faded");
    } else if (/^4/.test(cardnumber)) {
        $("#cc-mastercard").removeClass().addClass("cc-faded");
        $("#cc-visa").removeClass().addClass("cc-shown");
        $("#cc-amex").removeClass().addClass("cc-faded");
    }
    else if (/^3[47]/.test(cardnumber)) {
        $("#cc-mastercard").removeClass().addClass("cc-faded");
        $("#cc-visa").removeClass().addClass("cc-faded");
        $("#cc-amex").removeClass().addClass("cc-shown");
    } else {
        $("#cc-mastercard").removeClass().addClass("cc-shown");
        $("#cc-visa").removeClass().addClass("cc-shown");
        $("#cc-amex").removeClass().addClass("cc-shown");
    }
    $(obj).focus();
}

function processPayment() {
    // fake something here!
    $("#payment-button").addClass("disabled");
    $("#processing-payment-anim").removeClass();
    setTimeout(redirectToSuccess, 4000);
}
function redirectToSuccess() {
    window.location.href = "/checkout/CheckOutCart";
}

// ====================================================
// Search
// ====================================================
function TopSearchClear() {
    $("#search-top").val("");
    $("#search-results").html("");
}

function TopSearch() {
    var term = $("#search-top").val();
    var action = "/Home/Search";
    $("#search-results").html("");
    if (term.length > 0) {
        $("#search-results").html("<hr>");
        $.ajax({
            type: 'GET',
            url: action,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: { search: term },
            success: function(result) {
                var data = JSON.parse(result);
                $(data).each(function (i, obj) {
                    // <div class="search-result"><a href="/Product/ViewProduct/3">Some very very long product name Product</a></div>
                        $("#search-results")
                            .append(
                                '<div class="search-result"><a href="/Product/ViewProduct/' +
                                obj.productId +
                                '">' +
                                obj.Name +
                                '</a></div>'
                        );
                    });
            },
            error: function() {
                $("#search-results").html("Sorry someting went wrong!");
            }
        });
    }
}