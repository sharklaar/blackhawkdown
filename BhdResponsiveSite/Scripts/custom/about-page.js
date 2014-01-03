$(document).ready(function () {
    $('.read-more').click(function (event) {
        var section = '.' + event.target.id;
        var sectionElement = $(section);
        if ($(this).hasClass('closed')) {
            sectionElement.animate({
                height: "100%"
            });
            sectionElement.addClass('open');
            sectionElement.removeClass('closed');

            $(this).text("READ LESS...");
            $(this).removeClass('closed');
            $(this).addClass('open');
        } else {
            sectionElement.animate({
                height: '4.5em'
            });
            sectionElement.addClass('closed');
            sectionElement.removeClass('open');

            $(this).text("READ MORE...");
            $(this).removeClass('open');
            $(this).addClass('closed');
        }

        event.preventDefault();
    })
})