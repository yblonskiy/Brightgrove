﻿
class MatchItemCard extends HTMLElement {
    constructor() {
        super();
        const template = document.getElementById('match-card-template');
        const templateContent = template.content;
        this.attachShadow({ mode: 'open' }).appendChild(templateContent.cloneNode(true));
    }

    connectedCallback() {

        this.shadowRoot.querySelector('.match-header').textContent = this.getAttribute('date');

        this.shadowRoot.querySelectorAll('.team-logo')[0].src = this.getAttribute('home-team-logo');
        this.shadowRoot.querySelectorAll('.team-logo')[0].alt = this.getAttribute('home-team-name');
        this.shadowRoot.querySelectorAll('.team-name')[0].textContent = this.getAttribute('home-team-name');
        this.shadowRoot.querySelectorAll('.team-score')[0].textContent = this.getAttribute('home-team-score');

        this.shadowRoot.querySelectorAll('.team-logo')[1].alt = this.getAttribute('away-team-logo');
        this.shadowRoot.querySelectorAll('.team-logo')[1].src = this.getAttribute('away-team-logo');
        this.shadowRoot.querySelectorAll('.team-name')[1].textContent = this.getAttribute('away-team-name');
        this.shadowRoot.querySelectorAll('.team-score')[1].textContent = this.getAttribute('away-team-score');

        // Add styles
        const style = document.createElement('style');

        style.textContent = `
            :host {
                width: 330px;
                position: relative;
                display: flex;
                flex-direction: column;
                min-width: 0;
                word-wrap: break-word;
                background-color: #fff;
                background-clip: border-box;
                border: 1px solid rgb(0 0 0 / .125);
                border-radius: .25rem;
                flex-shrink: 0;
                margin-left: 15px;
                margin-right: 15px;
                user-select: none;
                border-color: #198754 !important;
            }

            .match-body {
                box-sizing: border-box;
                flex: 1 1 auto;
                padding: 1rem 1rem 0.1rem 1rem;
                border-color: #6c757d !important;
                user-select: none;
            }

            .match-header {
                display: block;
                margin-inline-start: 0;
                margin-inline-end: 0;
                font-weight: 700;
                unicode-bidi: isolate;
                font-size: 1rem;
                line-height: 1.2;
                margin-bottom: 1.5rem !important;
                --bs-text-opacity: 1;
                color: #6c757d !important;
            }

            .team {
                --bs-gutter-x: 1.5rem;
                --bs-gutter-y: 0;
                display: flex;
                flex-wrap: wrap;
                margin-top: calc(var(--bs-gutter-y)* -1);
                margin-right: calc(var(--bs-gutter-x)* -.5);
                margin-bottom: 1rem !important;
            }

            .team .team-logo {
                flex: 0 0 auto;
                padding-right: calc(var(--bs-gutter-x)* .5);
                max-width: 17px;
                height: auto;
            }

            .team .team-name {
                flex: 0 0 auto;
                margin-right: 10px;
                width: 70%;
                height: auto;

                padding-right: calc(var(--bs-gutter-x)* .5);
                margin-top: var(--bs-gutter-y);

                margin-inline-start: 0;
                margin-inline-end: 0;
                font-weight: 700;
                unicode-bidi: isolate;
                font-size: 1rem;
                color: #414344 !important;
            }

            .team .team-score {
                flex: 0 0 auto;
                width: 10%;
                --bs-text-opacity: 1;
                color: #3e4042 !important;
                text-align: right;
            }
        `;

        this.shadowRoot.append(style);
    }
}

customElements.define('match-card', MatchItemCard);

class LeagueCard extends HTMLElement {
    constructor() {
        super();
        const template = document.getElementById('league-card-template');
        const templateContent = template.content;
        this.attachShadow({ mode: 'open' }).appendChild(templateContent.cloneNode(true));
    }

    connectedCallback() {
        const matches = JSON.parse(this.getAttribute('matches'));

        const carousel = this.shadowRoot.querySelector('.carousel');

        this.shadowRoot.querySelector('.league-header').textContent = this.getAttribute('league-header');

        matches.forEach(match => {
            const matchCard = document.createElement('match-card');

            matchCard.setAttribute('date', match.MatchDate);

            matchCard.setAttribute('home-team-logo', match.HomeTeam.Crest);
            matchCard.setAttribute('home-team-name', match.HomeTeam.Name);

            matchCard.setAttribute('away-team-logo', match.AwayTeam.Crest);
            matchCard.setAttribute('away-team-name', match.AwayTeam.Name);

            if (match.Score.Winner != null) {
                matchCard.setAttribute('home-team-score', match.Score.FullTime.Home);
                matchCard.setAttribute('away-team-score', match.Score.FullTime.Away);
            }

            carousel.appendChild(matchCard);
        });

        // Add styles
        const style = document.createElement('style');

        style.textContent = `
            :host {
                margin-bottom: 1.5rem !important;
                border-color: #6c757d !important;
                position: relative;
                display: flex;
                flex-direction: column;
                min-width: 0;
                word-wrap: break-word;
                background-color: #fff;
                background-clip: border-box;
                border: 1px solid rgb(0 0 0 / .125);
                border-radius: .25rem;
            }

            .league-header {
                border-radius: calc(.25rem - 1px) calc(.25rem - 1px) 0 0;
                --bs-text-opacity: 1;
                color: #198754;
                padding: .5rem 1rem;
                margin-bottom: 0;
                background-color: rgb(0 0 0 / .03);
                border-bottom: 1px solid rgb(0 0 0 / .125);
            }

            .league-body {
                flex: 1 1 auto;
                padding: 1rem 1rem;
                border-color: #6c757d !important;
            }

            .carousel-container {
                overflow: hidden;
                width: 100%;
                position: relative;
                cursor: grab;
            }

            .carousel {
                display: flex;
                transition: transform 0.3s ease;
                position: relative;
            }
        `;

        this.shadowRoot.append(style);

        initCarousel(this.shadowRoot.querySelector('.carousel-container'));
    }
}

customElements.define('league-card', LeagueCard);

class CompetitionCarousel extends HTMLElement {
    constructor() {
        super();

        this.attachShadow({ mode: 'open' });

        JSON.parse(this.getAttribute('competitions')).forEach(competition => {

            if (competition.Matches.length > 0) {
                const leagueCard = document.createElement('league-card');

                leagueCard.setAttribute('league-header', competition.Competition.Name);
                leagueCard.setAttribute('matches', JSON.stringify(competition.Matches));

                this.shadowRoot.appendChild(leagueCard);
            }
        });

        // Add styles
        const style = document.createElement('style');

        style.textContent = `
            :host {
                flex: 1 0 0%;
                width: 100%;
                max-width: 100%;
                padding-right: calc(var(--bs-gutter-x)* .5);
                padding-left: calc(var(--bs-gutter-x)* .5);
                margin-top: var(--bs-gutter-y);
            }
        `;

        this.shadowRoot.append(style);
    }
}

customElements.define('competition-carousel', CompetitionCarousel);

window.addEventListener('load', () => {

    document.getElementById('btnRecentMatches').addEventListener('click', () => {

        document.getElementById('pnlRecentCompetitions').style.display = 'block';
        document.getElementById('pnlUpcomingCompetitions').style.display = 'none';

        document.getElementById('btnRecentMatches').className = 'btn btn-sm btn-success';
        document.getElementById('btnUpcomingMatches').className = 'btn btn-sm btn-outline-success';

    }, false);

    document.getElementById('btnUpcomingMatches').addEventListener('click', () => {

        document.getElementById('pnlUpcomingCompetitions').style.display = 'block';
        document.getElementById('pnlRecentCompetitions').style.display = 'none';

        document.getElementById('btnRecentMatches').className = 'btn btn-sm btn-outline-success';
        document.getElementById('btnUpcomingMatches').className = 'btn btn-sm btn-success';

    }, false);

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