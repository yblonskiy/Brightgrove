
window.addEventListener('load', () => {

    var btnRecentMatches = document.getElementById('btnRecentMatches');
    var btnUpcomingMatches = document.getElementById('btnUpcomingMatches');

    btnRecentMatches.addEventListener('click', () => {

        document.getElementById('pnlRecentCompetitions').style.display = 'block';
        document.getElementById('pnlUpcomingCompetitions').style.display = 'none';

        document.getElementById('btnRecentMatches').className = 'btn btn-sm btn-success';
        document.getElementById('btnUpcomingMatches').className = 'btn btn-sm btn-outline-success';

    }, false);

    btnUpcomingMatches.addEventListener('click', () => {

        document.getElementById('pnlUpcomingCompetitions').style.display = 'block';
        document.getElementById('pnlRecentCompetitions').style.display = 'none';

        document.getElementById('btnRecentMatches').className = 'btn btn-sm btn-outline-success';
        document.getElementById('btnUpcomingMatches').className = 'btn btn-sm btn-success';

    }, false);

    document.querySelectorAll('.carousel-container').forEach(container => {
        initCarousel(container);
    });

    document.getElementById('pnlRecentCompetitions').style.display = 'none';
});

function initCarousel(container) {
    const carousel = container.querySelector('.carousel');
    let isMouseDown = false;
    let startX;
    let scrollLeft;

    container.addEventListener('mousedown', (e) => {
        isMouseDown = true;
        startX = e.pageX - container.offsetLeft;
        scrollLeft = carousel.offsetLeft;
        container.style.cursor = 'grabbing';
    });

    container.addEventListener('mouseup', () => {
        isMouseDown = false;
        container.style.cursor = 'grab';
    });

    container.addEventListener('mousemove', (e) => {
        if (!isMouseDown) return;
        e.preventDefault();
        const x = e.pageX - container.offsetLeft;
        const walk = (x - startX) * 2;

        const maxScrollLeft = carousel.scrollWidth - container.offsetWidth + 20;
        const currentScrollLeft = scrollLeft + walk;

        if (currentScrollLeft <= 0 && currentScrollLeft >= -maxScrollLeft) {
            carousel.style.left = `${currentScrollLeft}px`;
        }
    });

    container.addEventListener('touchstart', (e) => {
        isMouseDown = true;
        startTouchX = e.touches[0].pageX - container.offsetLeft;
        scrollLeft = carousel.offsetLeft;
    });

    container.addEventListener('touchmove', (e) => {
        if (!isMouseDown) return;

        e.preventDefault();

        const touchX = e.touches[0].pageX - container.offsetLeft;
        const walk = (touchX - startTouchX) * 2;

        const maxScrollLeft = carousel.scrollWidth - container.offsetWidth + 20;
        const currentScrollLeft = scrollLeft + walk;

        if (currentScrollLeft <= 0 && currentScrollLeft >= -maxScrollLeft) {
            carousel.style.left = `${currentScrollLeft}px`;
        }
    });

    container.addEventListener('touchend', () => {
        isMouseDown = false;
    });

    function resetPosition() {
        const totalWidth = carousel.scrollWidth;
        const containerWidth = container.offsetWidth;
        if (parseFloat(window.getComputedStyle(carousel).left) <= -totalWidth) {
            carousel.style.left = `-${totalWidth - containerWidth}px`;
        } else if (parseFloat(window.getComputedStyle(carousel).left) >= 0) {
            carousel.style.left = `0px`;
        }
    }

    setInterval(resetPosition, 100);
}