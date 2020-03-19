namespace PlayZone.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
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

        public IActionResult Details(string id)
        {
            var viewModel = this.chanelsService.GetChanelById<ChanelDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
