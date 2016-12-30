// ===============================================================
// People looking at this
// ===============================================================
function srPeopleLookingAtProduct(pid) {
    // Ref the hub
    var myhub = $.connection.productPageHub;
    // Event handler(s)
    myhub.client.CurrentlyViewing = function (id, count) {
        if (pid === id)
            if (count === 1)
                $("#people-looking").html(count + " person");
            else
                $("#people-looking").html(count + " people");
    };
    myhub.client.TimesPurchased = function (id, purchased, inStock) {
        if (pid === id)
                $("#times-purchased").html(purchased + " times");
                $("#instock").html(inStock + " pcs");
    };
    myhub.client.ReadyToBuy = function (id, count) {
        if (pid === id)
            if (count === 1)
                $("#people-readytobuy").html(count + " person");
            else
                $("#people-readytobuy").html(count + " people");
    };

    $.connection.hub.start().done(function () {
        myhub.server.loadedPage(pid);
    });
}