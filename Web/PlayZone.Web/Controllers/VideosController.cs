namespace PlayZone.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlayZone.Data.Models;
    using PlayZone.Services.Data;
    using PlayZone.Web.ViewModels.Videos;

    public class VideosController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IVideosService videosService;

        public VideosController(ICategoriesService categoriesService,
             UserManager<ApplicationUser> userManager,
             IVideosService videosService)
        {
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.videosService = videosService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new VideoCreateInputModel
            {
                Categories = this.categoriesService.GetAllCategories<CategoryDropDownViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(VideoCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var videoId = await this.videosService.CreateVideoAsync(input.Title, input.Url, input.Description, input.CategoryId, user.Id);

            return this.Redirect("Index");
        }
    }
}
