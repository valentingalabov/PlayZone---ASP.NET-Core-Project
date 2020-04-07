namespace PlayZone.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlayZone.Data.Models;
    using PlayZone.Services.Data;
    using PlayZone.Web.ViewModels.Favorites;

    [Authorize]
    public class FavoritesController : BaseController
    {
        private readonly IFavoritesServices favoritesService;
        private readonly UserManager<ApplicationUser> userManager;

        public FavoritesController(IFavoritesServices favoritesService, UserManager<ApplicationUser> userManager)
        {
            this.favoritesService = favoritesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AddVideoToFavorites(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.favoritesService.AddVideoToFavoriteAsync(id, userId);

            return this.RedirectToAction("Details", "Videos", new { Id = id });
        }

        public IActionResult All()
        {
            var userId = this.userManager.GetUserId(this.User);

            var viewModel = new FavoriteLibraryViewModel
            {
                Videos = this.favoritesService.GetFavoriteVideosByUser<FavoriteVideoViewModel>(userId),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> DeleteFromFavorites(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.favoritesService.DeleteFromFavoritesAsync(id, userId);

            return this.RedirectToAction("All");
        }
    }
}
