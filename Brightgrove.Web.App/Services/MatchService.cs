﻿namespace Brightgrove.Web.App.Services
{
    public class MatchService : IMatchService
    {
        #region Private Members

        private readonly ILoggerFactory _loggerFactory;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<WebApiServiceAgentSettings> _webApiSettings;

        #endregion

        #region Constructor

        public MatchService(ILoggerFactory loggerFactory,
                            IHttpClientFactory httpClientFactory,
                            IOptions<WebApiServiceAgentSettings> webApiSettings)
        {
            _loggerFactory      = loggerFactory;
            _httpClientFactory  = httpClientFactory;
            _webApiSettings     = webApiSettings;
        }

        #endregion

        public async Task<CompetitionsMatchesResponse> GetMatches()
        {
            var result = new CompetitionsMatchesResponse();

            var matchServiceAgent = new MatchServiceAgent(_loggerFactory.CreateLogger<MatchServiceAgent>(), _webApiSettings, _httpClientFactory);

            var matches = await matchServiceAgent.GetMatches(new Brightgrove.Models.Matches.Input.MatchesInputModel
            {
                CompetitionIds  =  new List<string> 
                { 
                    CompetitionCode.PrimeiraLiga,
                    CompetitionCode.PremierLeague,
                    CompetitionCode.Eredivisie,
                    CompetitionCode.Bundesliga,
                    CompetitionCode.SerieA,
                    CompetitionCode.PrimeraDivision,
                    CompetitionCode.CampeonatoBrasileiroSerieA
                },
                DateFrom        = DateTime.Now.AddDays(-4),
                DateTo          = DateTime.Now.AddDays(6)
            });

            result.RecentCompetitions = matches.Competitions
                 .Select(s => {

                     var competition    = s.Competition;
                     var matches        = s.Matches.Where(x => x.Status == "FINISHED").ToList();

                     matches.ForEach(e => e.MatchDate = e.UtcDate.ToLocalTime().ToString("dd.MM | HH:mm"));

                     return new MatchCompetitionItem
                     {
                         Competition    = competition,
                         Matches        = matches
                     };
                 })
                .ToList();

            result.UpcomingCompetitions = matches.Competitions
                 .Select(s => {

                     var competition    = s.Competition;
                     var matches        = s.Matches.Where(x => x.Status == "TIMED").ToList();

                     matches.ForEach(e => e.MatchDate = e.UtcDate.ToLocalTime().ToString("dd.MM | HH:mm"));

                     return new MatchCompetitionItem
                     {
                         Competition    = competition,
                         Matches        = matches
                     };
                 })
               .ToList();

            return result;
        }
    }
}