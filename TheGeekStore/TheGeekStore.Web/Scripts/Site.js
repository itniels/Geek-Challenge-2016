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
            if (result == "True") {
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