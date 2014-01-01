$(document).ready(function () {
    $('.read-more').click(function (event) {
        var section = '.' + event.target.id;

        if ($(this).hasClass('closed')) {
            $(section).height('100%');
            $(this).text("READ LESS...");
            $(this).removeClass('closed');
            $(this).addClass('open');
        } else {
            $(section).height('142px');
            $(this).text("READ MORE...");
            $(this).removeClass('open');
            $(this).addClass('closed');
        }
    })
})