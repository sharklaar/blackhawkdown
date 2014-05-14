$(document).ready(function () {
    $('#globalNavButton').click(function () {
        var nav = $('#navigation');
        if (!nav.hasClass('open')) {
            nav.slideDown('fast');
            nav.addClass('open');
        } else {
            nav.slideUp('fast');
            nav.removeClass('open');
        }                
    })
})