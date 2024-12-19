namespace Brightgrove.Models.Matches
{
	public class MatchCompetitionItem
    {
        #region Public Properties

        public MatchCompetition Competition { get; set; }

        public IList<MatchItem> Matches { get; set; } = new List<MatchItem>();

        #endregion
    }
}