namespace Brightgrove.IntegrationServices.Models.FootballData.Match
{
    public class FootballDataMatchesItem
    {
        #region Public Properties

        public FootballDataMatchesCompetition Competition { get; set; }

        public int Id { get; set; }

        public DateTime UtcDate { get; set; }

        public string Status { get; set; }

        public int Matchday { get; set; }

        public string Stage { get; set; }

        public DateTime LastUpdated { get; set; }

        public FootballDataMatchTeam HomeTeam { get; set; }

        public FootballDataMatchTeam AwayTeam { get; set; }

        public FootballDataMatchScore Score { get; set; }

        #endregion
    }
}