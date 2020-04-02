namespace PlayZone.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlayZone.Data.Models;
    using PlayZone.Services.Data;
    using PlayZone.Web.ViewModels.Libraries;

    public class LibrariesController : BaseController
    {
        private readonly ILibrariesService librariesService;
        private readonly UserManager<ApplicationUser> userManager;

        public LibrariesController(ILibrariesService librariesService, UserManager<ApplicationUser> userManager)
        {
            this.librariesService = librariesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult History()
        {
            var userId = this.userManager.GetUserId(this.User);

            var viewModel = new HistoryViewModel
            {
                Videos = this.librariesService.GetVideosHistoryByUser<VideoHistoryViewModel>(userId),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> DeleteFromHistory(string id)
        {
            await this.librariesService.DeleteFromLibrary(id);

            var userId = this.userManager.GetUserId(this.User);

            return this.RedirectToAction("History");
        }
    }
}
