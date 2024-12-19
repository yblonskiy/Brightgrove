namespace Brightgrove.Models.Matches
{
	public class MatchScore
    {
        #region Public Properties

        public string Winner { get; set; }

        public string Duration { get; set; }

        public MatchTime FullTime { get; set; }

        public MatchTime HalfTime { get; set; }

        #endregion
    }
}