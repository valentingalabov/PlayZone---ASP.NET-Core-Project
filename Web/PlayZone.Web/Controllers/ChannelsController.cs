namespace PlayZone.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlayZone.Data.Models;
    using PlayZone.Services.Data;
    using PlayZone.Web.ViewModels.Channels;

    public class ChannelsController : BaseController
    {
        private const int ItemsPerPage = 6;

        private readonly IChannelsService channelsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChannelsController(IChannelsService channelsService, UserManager<ApplicationUser> userManager)
        {
            this.channelsService = channelsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ChannelCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user.ChannelId != null)
            {
                return this.RedirectToAction("Details", new { Id = user.ChannelId });
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.channelsService.IsValidChannel(input.Title))
            {
                input.ExistTitle = "This channel title already exist!";
                return this.View();
            }

            var channelId = await this.channelsService.CreateChannelAsync(input.Title, input.Description, user);

            return this.RedirectToAction("Details", new { Id = channelId });
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = this.channelsService.GetChannelById<ChannelDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Create");
            }

            var user = await this.userManager.GetUserAsync(this.User);
            if (user != null)
            {
                viewModel.IsCreator = this.channelsService.IsOwner(id, user.ChannelId);
            }

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

            await this.channelsService.UploadAsync(file, id);

            return this.RedirectToAction("Details", new { Id = id });
        }

        public IActionResult Description(string id)
        {
            var viewModel = this.channelsService.GetChannelById<ChannelDescriptionViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult Videos(string id, int page = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }

            var viewModel = new AllVideosByChannelViewModel();
            viewModel.Channel = this.channelsService.GetChannelById<ChannelViewModel>(id);
            viewModel.Videos = this.channelsService.GetVieosByChannel<VideoByChannelViewModel>(id, ItemsPerPage, (page - 1) * ItemsPerPage);
            var count = this.channelsService.GetAllVideosByChannelCount(id);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.channelsService.IsOwner(id, user.ChannelId))
            {
                return this.RedirectToAction("Details", new { Id = id });
            }

            var viewModel = this.channelsService.GetChannelById<ChannelEditInputModel>(id);

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(ChannelEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.channelsService.IsValidChannelAfterEdit(input.Id, input.Title))
            {
                input.ExistTitle = "This channel title already exist!";
                return this.View(input);
            }

            await this.channelsService.EditChannelAsync(input.Id, input.Title, input.Description);

            return this.RedirectToAction("Details", new { input.Id });
        }
    }
}
