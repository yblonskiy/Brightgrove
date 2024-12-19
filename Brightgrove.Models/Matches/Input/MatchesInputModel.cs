namespace Brightgrove.Models.Matches.Input
{
	public class MatchesInputModel
    {
        #region Public Properties

        public List<string> CompetitionIds { get; set; } = new List<string>();

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public List<string> MatchIds { get; set; } = new List<string>();

        public string? Status { get; set; }

        #endregion
    }
}