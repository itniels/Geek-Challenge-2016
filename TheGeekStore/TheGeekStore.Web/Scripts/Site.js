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
function changeView(target) {
    $.ajax()
}