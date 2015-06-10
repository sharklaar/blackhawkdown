$(document).ready(function () {
    $('#globalNavButton').click(function () {
        var nav = $('#navigation');
        var navButton = $('#globalNavButton');
        if (!nav.hasClass('open')) {
            nav.slideDown();
            nav.addClass('open');
            navButton.addClass('open');

        } else {
            nav.slideUp();
            nav.removeClass('open');
            navButton.removeClass('open');
        }
    })
})