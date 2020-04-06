namespace PlayZone.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlayZone.Data.Models;
    using PlayZone.Services.Data;
    using PlayZone.Web.ViewModels.Libraries;
    using PlayZone.Web.ViewModels.Libraries.Favorite;
    using PlayZone.Web.ViewModels.Libraries.History;

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

            var viewModel = new HistoryLibraryViewModel
            {
                Videos = this.librariesService.GetVideosHistoryByUser<HistoryVideoViewModel>(userId),
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

        [Authorize]
        public async Task<IActionResult> AddVideoToFavorites(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.librariesService.AddVideoToFavoriteAsync(id, userId);

            return this.RedirectToAction("Details", "Videos", new { Id = id });
        }

        [Authorize]
        public IActionResult Favorites()
        {
            var userId = this.userManager.GetUserId(this.User);

            var viewModel = new FavoriteLibraryViewModel
            {
                Videos = this.librariesService.GetFavoriteVideosByUser<FavoriteVideoViewModel>(userId),
            };

            return this.View(viewModel);
        }
    }
}
