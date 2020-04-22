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
        private readonly IHistoriesService historiesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VideosController(
                                ICategoriesService categoriesService,
                                IVideosService videosService,
                                IHistoriesService historiesService,
                                UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.videosService = videosService;
            this.historiesService = historiesService;
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

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.videosService.IsValidTitle(input.Title))
            {
                input.Categories = this.categoriesService.GetAllCategories<CategoryDropDownViewModel>();
                input.ExistingTitle = "This title already exist!";
                return this.View(input);
            }

            if (!this.videosService.IsValidUrl(input.Url))
            {
                input.Categories = this.categoriesService.GetAllCategories<CategoryDropDownViewModel>();
                input.ExsistingUrl = "This url already exist!";
                return this.View(input);
            }

            var videoId = await this.videosService.CreateVideoAsync(input.Title, input.Url, input.Description, input.CategoryId, user.Id, user.ChanelId);

            return this.RedirectToAction("Details", new { Id = videoId });
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = this.videosService.GetVideoById<VideoDetailsViewModel>(id);

            var userId = this.userManager.GetUserId(this.User);

            if (viewModel.UserId == userId)
            {
                viewModel.IsCreator = true;
            }

            if (userId != null)
            {
                await this.historiesService.AddVideoToHistoryAsync(id, userId);
            }

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.videosService.IsOwner(id, userId))
            {
                return this.RedirectToAction("Details", new { Id = id });
            }

            var viewModel = this.videosService.GetVideoById<VideoEditInputModel>(id);
            string url = $"https://www.youtube.com/watch?v={viewModel.Url}";
            viewModel.Categories = this.categoriesService.GetAllCategories<CategoryDropDownViewModel>();
            viewModel.Url = url;
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(VideoEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.videosService.IsValidTitleAfterEdit(input.Id, input.Title))
            {
                input.Categories = this.categoriesService.GetAllCategories<CategoryDropDownViewModel>();
                input.ExistingTitle = "This title already exist!";

                return this.View(input);
            }

            if (!this.videosService.IsValidUrlAfterEdit(input.Id, input.Url))
            {
                input.Categories = this.categoriesService.GetAllCategories<CategoryDropDownViewModel>();
                input.ExistingUrl = "This url already exist!";
                return this.View(input);
            }

            await this.videosService.EditVideoAsync(input.Id, input.Title, input.Url, input.Description, input.CategoryId);

            return this.RedirectToAction("Details", new { Id = input.Id });
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.videosService.IsOwner(id, userId))
            {
                return this.RedirectToAction("Details", new { Id = id });
            }

            await this.videosService.Delete(id);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult ConfirmationDelete(string id)
        {
            var viewModel = this.videosService.GetVideoById<ConfirmationDeleteViewModel>(id);

            return this.View(viewModel);
        }
    }
}
