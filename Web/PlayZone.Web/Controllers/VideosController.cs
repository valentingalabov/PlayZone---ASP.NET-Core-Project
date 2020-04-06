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
        private readonly IVideosService videosService;
        private readonly ILibrariesService librariesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VideosController(
                                ICategoriesService categoriesService,
                                IVideosService videosService,
                                ILibrariesService librariesService,
                                UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.videosService = videosService;
            this.librariesService = librariesService;
            this.userManager = userManager;
        }

        [HttpGet]
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
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.ChanelId == null)
            {
                return this.Redirect("/Chanels/Create");
            }

            if (!this.videosService.IsValidVideo(input.Title, input.Url))
            {
                return this.View("Title and Url must be Unique!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var videoId = await this.videosService.CreateVideoAsync(input.Title, input.Url, input.Description, input.CategoryId, user.Id, user.ChanelId);

            return this.RedirectToAction("Details", new { Id = videoId });
        }

        public async Task<IActionResult> Details(string id)
        {
            var videoViewModel = this.videosService.GetVideoById<VideoDetailsViewModel>(id);

            var userId = this.userManager.GetUserId(this.User);

            if (userId != null)
            {
                await this.librariesService.AddVideoToHistoryAsync(id, userId);
            }

            if (videoViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(videoViewModel);
        }
    }
}
