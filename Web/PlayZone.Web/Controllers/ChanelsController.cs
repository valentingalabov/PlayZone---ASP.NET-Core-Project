namespace PlayZone.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlayZone.Data.Models;
    using PlayZone.Services.Data;
    using PlayZone.Web.ViewModels.Chanels;

    public class ChanelsController : BaseController
    {
        private readonly IChanelsService chanelsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChanelsController(IChanelsService chanelsService, UserManager<ApplicationUser> userManager)
        {
            this.chanelsService = chanelsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ChanelCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user.ChanelId != null)
            {
                return this.RedirectToAction("Details", new { Id = user.ChanelId });
            }

            if (!this.ModelState.IsValid || !this.chanelsService.IsValidChanel(input.Title))
            {
                return this.View(input);
            }

            var chanelId = await this.chanelsService.CreateChanelAsync(input.Title, input.Description, user);

            return this.RedirectToAction("Details", new { Id = chanelId });
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Create");
            }

            var user = await this.userManager.GetUserAsync(this.User);
            if (user.ChanelId == id)
            {
                return this.RedirectToAction("OwnerDetails", new { Id = id });
            }

            var viewModel = this.chanelsService.GetChanelById<ChanelDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult OwnerDetails(string id)
        {
            var viewModel = this.chanelsService.GetChanelById<ChanelDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult ImageUpload()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ImageUpload(IFormFile file, string id)
        {
            if (file == null || id == null || file.Length > 10485760)
            {
                return this.View();
            }

            await this.chanelsService.UploadAsync(file, id);

            return this.RedirectToAction("Details", new { Id = id });
        }

        public IActionResult Description(string description)
        {
            var viewModel = new ChanelDescriptionViewModel
            {
                Description = description,
            };

            return this.View(viewModel);
        }

        public IActionResult Videos(string id)
        {
            var chanel = this.chanelsService.GetChanelById<ChanelViewModel>(id);

            var viewModel = new AllVideosByChanelViewModel
            {
                Videos = this.chanelsService.GetAllVieos<VideoByChanelViewModel>(id),
                Chanel = chanel,
            };

            return this.View(viewModel);
        }
    }
}
