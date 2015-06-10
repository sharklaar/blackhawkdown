$(document).ready(function () {
    $('#globalNavButton').click(function () {
        var nav = $('#navigation');
        var navButton = $('#globalNavButton');
        if (!nav.hasClass('open')) {
            nav.slideDown('fast');
            nav.addClass('open');
            navButton.addClass('open');

        } else {
            nav.slideUp('fast');
            nav.removeClass('open');
            navButton.removeClass('open');
        }
    })
})