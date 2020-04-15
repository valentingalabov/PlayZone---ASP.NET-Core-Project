namespace PlayZone.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using PlayZone.Services.Data;
    using PlayZone.Web.ViewModels;
    using PlayZone.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private const int ItemsPerPage = 6;

        private readonly IVideosService videosService;

        public HomeController(IVideosService videosService)
        {
            this.videosService = videosService;
        }

        // [Route("/")]
        public IActionResult Index(int page = 1)
        {
            var viewModel = new IndexViewModel();

            viewModel.AllVideos = this.videosService.GetAllVideos<IndexVideoViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);

            var count = this.videosService.GetAllVideosCount();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
