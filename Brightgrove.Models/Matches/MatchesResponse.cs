namespace Brightgrove.Models.Matches
{
    public class MatchesResponse : BaseResponse
    {
        #region Public Properties

        public IList<MatchCompetitionItem> Competitions { get; set; } = new List<MatchCompetitionItem>();

        #endregion
    }
}