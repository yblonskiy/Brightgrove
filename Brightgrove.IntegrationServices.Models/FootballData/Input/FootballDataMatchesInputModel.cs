namespace Brightgrove.IntegrationServices.Models.FootballData.Input
{
    public class FootballDataMatchesInputModel
    {
        #region Public Properties

        public List<string> CompetitionIds { get; set; } = new List<string>();

        public DateTime? DateFrom { get; set; }

		public DateTime? DateTo { get; set; }

        public List<string> MatchIds { get; set; } = new List<string>();

		public string? Status { get; set; }

        #endregion

        #region Public Methods

        public string ToGetRequestString()
        {
            var result = new StringBuilder();

            if (CompetitionIds.Count > 0)
            {
                result.Append($"competitions={string.Join(',', CompetitionIds)}&");
            }

            if (DateFrom != null)
            {
                result.Append($"dateFrom={DateFrom.Value.ToString("yyyy-MM-dd")}&");
            }

            if (DateTo != null)
            {
                result.Append($"dateTo={DateTo.Value.ToString("yyyy-MM-dd")}&");
            }

            if (MatchIds.Count > 0)
            {
                result.Append($"ids={string.Join(',', MatchIds)}&");
            }

            if (!string.IsNullOrEmpty(Status))
            {
                result.Append($"status={Status}&");
            }

            return result.ToString().TrimEnd('&');
        }

        #endregion
    }
}