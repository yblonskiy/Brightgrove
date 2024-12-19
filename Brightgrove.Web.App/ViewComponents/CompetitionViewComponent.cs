namespace Brightgrove.Web.App.ViewComponents
{
    public class CompetitionViewComponent : ViewComponent
    {
        public CompetitionViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(MatchCompetitionItem competitionItem)
        {
            return View(competitionItem);
        }
    }
}