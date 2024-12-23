namespace Brightgrove.Web.App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMatchService _matchService;

        public CompetitionsMatchesResponse CompetitionsMatches { get; set; }

        public IndexModel(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task OnGet()
        {
            CompetitionsMatches = await _matchService.GetMatches();
        }
    }
}