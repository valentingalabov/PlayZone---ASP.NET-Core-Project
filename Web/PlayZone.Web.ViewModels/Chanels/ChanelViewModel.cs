namespace PlayZone.Web.ViewModels.Chanels
{
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class ChanelViewModel : IMapFrom<Chanel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public Image Image { get; set; }
    }
}
