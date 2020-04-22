namespace PlayZone.Web.ViewModels.Channels
{
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class ChannelViewModel : IMapFrom<Channel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public Image Image { get; set; }
    }
}
