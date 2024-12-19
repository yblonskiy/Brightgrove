
$(document).ready(function () {

    $('#btnRecentMatches').click(function () {

        $('#pnlRecentCompetitions').show();
        $('#pnlUpcomingCompetitions').hide();

        $('#btnRecentMatches').attr('class', 'btn btn-sm btn-success');
        $('#btnUpcomingMatches').attr('class', 'btn btn-sm btn-outline-success');

        return false;
    });

    $('#btnUpcomingMatches').click(function () {

        $('#pnlUpcomingCompetitions').show();
        $('#pnlRecentCompetitions').hide();

        $('#btnRecentMatches').attr('class', 'btn btn-sm btn-outline-success');
        $('#btnUpcomingMatches').attr('class', 'btn btn-sm btn-success');

        return false;
    });

});

$(window).on('load', function () {

    InitCarousel();

    $('#pnlRecentCompetitions').hide();

});

function InitCarousel() {

    $('.carousel-container').each(function () {

        let isMouseDown = false;
        let startX, startScroll;

        const carousel = $(this).find('.carousel');
        const containerWidth = $(this).width();
        const totalWidth = carousel[0].scrollWidth;

        $(this).on('dragstart', function (e) {
            e.preventDefault();
        });

        $(this).on('mousedown', function (e) {
            isMouseDown = true;
            $(this).css('cursor', 'grabbing');
            startX = e.pageX;
            startScroll = carousel.position().left;
        });

        // Подія mousemove: коли рухається миша
        $(this).on('mousemove', function (e) {
            if (!isMouseDown) return;
            e.preventDefault();
            const moveX = e.pageX - startX;
            const newPosition = startScroll + moveX;

            if (newPosition <= 0 && newPosition >= containerWidth - totalWidth) {
                carousel.css('transform', 'translateX(' + newPosition + 'px)');
            }
        });

        $(this).on('mouseup', function () {
            isMouseDown = false;
            $(this).css('cursor', 'grab');

            const currentLeft = carousel.position().left;
            const scrollDistance = Math.round(currentLeft / 350) * 350;
            carousel.animate({
                transform: 'translateX(' + scrollDistance + 'px)'
            }, 200);
        });

        $(this).on('mouseleave', function () {
            if (isMouseDown) {
                isMouseDown = false;
                $(this).css('cursor', 'grab');

                const currentLeft = carousel.position().left;
                const scrollDistance = Math.round(currentLeft / 350) * 350;
                carousel.animate({
                    transform: 'translateX(' + scrollDistance + 'px)'
                }, 200);
            }
        });

        $(this).on('touchstart', function (e) {
            isTouching = true;
            const touch = e.touches[0];
            startX = touch.pageX;
            startScroll = carousel.position().left;
        });

        $(this).on('touchmove', function (e) {
            if (!isTouching) return;
            e.preventDefault();
            const touch = e.touches[0];
            const moveX = touch.pageX - startX;
            const newPosition = startScroll + moveX;

            if (newPosition <= 0 && newPosition >= containerWidth - totalWidth) {
                carousel.css('transform', 'translateX(' + newPosition + 'px)');
            }
        });

        $(this).on('touchend', function () {
            isTouching = false;
            const currentLeft = carousel.position().left;
            const scrollDistance = Math.round(currentLeft / 350) * 350;
            carousel.animate({
                transform: 'translateX(' + scrollDistance + 'px)'
            }, 200);
        });
    });
}
