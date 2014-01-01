$(document).ready(function () {
    $('.read-more').click(function (event) {
        var section = '.' + event.target.id;

        if ($(this).hasClass('closed')) {
            $(section).animate({
                height: "100%"
            });

            $(this).text("READ LESS...");
            $(this).removeClass('closed');
            $(this).addClass('open');
        } else {
            $(section).animate({
                height: '142px'
            });

            $(this).text("READ MORE...");
            $(this).removeClass('open');
            $(this).addClass('closed');
        }

        event.preventDefault();
    })
})