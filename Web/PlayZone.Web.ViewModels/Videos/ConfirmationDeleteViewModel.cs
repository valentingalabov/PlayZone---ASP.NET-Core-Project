namespace PlayZone.Web.ViewModels.Videos
{
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class ConfirmationDeleteViewModel : IMapFrom<Video>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ChanelTitle { get; set; }

        public string Url { get; set; }

        public string EmbeddUrl => $"https://www.youtube.com/embed/{this.Url}";
    }
}
