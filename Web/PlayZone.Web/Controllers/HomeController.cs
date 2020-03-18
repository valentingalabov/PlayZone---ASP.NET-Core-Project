namespace PlayZone.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using PlayZone.Services.Data;
    using PlayZone.Services.Mapping;
    using PlayZone.Web.ViewModels;
    using PlayZone.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IVideosService videosService;

        public HomeController(IVideosService videosService)
        {
            this.videosService = videosService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Videos = this.videosService.GetAllVieos<IndexVideosViewModel>(),
            };

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
