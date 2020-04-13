namespace PlayZone.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<IndexVideoViewModel> AllVideos { get; set; }
    }
}
