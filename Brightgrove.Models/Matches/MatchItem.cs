namespace Brightgrove.Models.Matches
{
	public class MatchItem
    {
        #region Public Properties

        public int Id { get; set; }

        public DateTime UtcDate { get; set; }

        public string MatchDate { get; set; }

        public string Status { get; set; }

        public int Matchday { get; set; }

        public string Stage { get; set; }

        public DateTime LastUpdated { get; set; }

        public MatchTeam HomeTeam { get; set; }

        public MatchTeam AwayTeam { get; set; }

        public MatchScore Score { get; set; }

        #endregion
    }
}