$(document).ready(function () {

    setTimeout(function () {
        var $head = $('#dizzyjam-iframe').contents().find("head");
        $head.append($("<link/>",
            { rel: "stylesheet", href: "/Content/store.css", type: "text/css" }));
    }, 2000);

});