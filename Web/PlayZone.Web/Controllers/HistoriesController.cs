namespace PlayZone.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlayZone.Data.Models;
    using PlayZone.Services.Data;
    using PlayZone.Web.ViewModels.Histories;

    [Authorize]
    public class HistoriesController : BaseController
    {
        private readonly IHistoriesService historiesService;
        private readonly UserManager<ApplicationUser> userManager;

        public HistoriesController(IHistoriesService historiesService, UserManager<ApplicationUser> userManager)
        {
            this.historiesService = historiesService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var userId = this.userManager.GetUserId(this.User);

            var viewModel = new HistoryLibraryViewModel
            {
                Videos = this.historiesService.GetVideosHistoryByUser<HistoryVideoViewModel>(userId),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> DeleteFromHistory(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.historiesService.DeleteFromHistoryAsync(id, userId);

            return this.RedirectToAction("All");
        }
    }
}
