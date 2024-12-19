namespace Brightgrove.Web.App.Models.Matches
{
    public class CompetitionsMatchesResponse
    {
        public IList<MatchCompetitionItem> RecentCompetitions { get; set; } = new List<MatchCompetitionItem>();

        public IList<MatchCompetitionItem> UpcomingCompetitions { get; set; } = new List<MatchCompetitionItem>();
    }
}
