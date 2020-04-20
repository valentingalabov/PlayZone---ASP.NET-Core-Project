namespace PlayZone.Web.ViewModels.Chanels
{
    using AutoMapper;
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class ChanelDetailsViewModel : IMapFrom<Chanel>
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public bool IsCreator { get; set; }

        public string EmbedChanelImageUrl => $"http://res.cloudinary.com/dqh6dvohu/image/upload/c_thumb,g_center,h_339,w_958/{this.ImageUrl}";
    }
}
