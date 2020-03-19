namespace PlayZone.Web.ViewModels.Videos
{
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;
    using System;

    public class VideoDetailsViewModel : IMapFrom<Video>
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
