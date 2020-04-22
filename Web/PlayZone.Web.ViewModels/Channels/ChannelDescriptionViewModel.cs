namespace PlayZone.Web.ViewModels.Channels
{
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class ChannelDescriptionViewModel : IMapFrom<Channel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
