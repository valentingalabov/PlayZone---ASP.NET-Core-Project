namespace PlayZone.Web.ViewModels.Channels
{
    using System.Collections.Generic;

    public class AllVideosByChannelViewModel
    {
        public IEnumerable<VideoByChannelViewModel> Videos { get; set; }

        public ChannelViewModel Channel { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
