namespace Brightgrove.Web.App.ViewComponents
{
    public class MatchItemViewComponent : ViewComponent
    {
        public MatchItemViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(MatchItem matchItem)
        {
            return View(matchItem);
        }
    }
}