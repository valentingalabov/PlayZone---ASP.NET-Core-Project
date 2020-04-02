namespace PlayZone.Web.ViewModels.Videos
{
    using System;

    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class VideoDetailsViewModel : IMapFrom<Video>
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string ChanelTitle { get; set; }

        public string ChanelId { get; set; }

        public string ChanelImageUrl { get; set; }

        public string EmbedVideoUrl => $"https://www.youtube.com/embed/{this.Url}?autoplay=1";

        public string EmbedChanelImageUrl => $"http://res.cloudinary.com/dqh6dvohu/image/upload/w_100,c_fill,ar_1:1,g_auto,r_max,bo_2px_solid_blue,b_rgb:ffffff/{this.ChanelImageUrl}";

        public DateTime CreatedOn { get; set; }
    }
}
