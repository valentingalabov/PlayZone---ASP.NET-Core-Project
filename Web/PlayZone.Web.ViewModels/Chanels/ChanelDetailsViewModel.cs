namespace PlayZone.Web.ViewModels.Chanels
{
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class ChanelDetailsViewModel : IMapFrom<Chanel>
    {
        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

    }
}
