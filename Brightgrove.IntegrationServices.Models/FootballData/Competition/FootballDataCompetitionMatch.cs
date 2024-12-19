namespace Brightgrove.IntegrationServices.Models.FootballData.Competition
{
    public class FootballDataCompetitionMatch
    {
        #region Public Properties

        public int Id { get; set; }

        public DateTime UtcDate { get; set; }

        public string Status { get; set; }

        public int Matchday { get; set; }

        public string Stage { get; set; }

        public DateTime LastUpdated { get; set; }

        public FootballDataMatchTeam HomeTeam { get; set; }

        public FootballDataMatchTeam AwayTeam { get; set; }

        public FootballDataMatchTime Score { get; set; }

        #endregion
    }
}