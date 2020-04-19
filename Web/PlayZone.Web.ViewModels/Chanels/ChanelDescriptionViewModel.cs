namespace PlayZone.Web.ViewModels.Chanels
{
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class ChanelDescriptionViewModel : IMapFrom<Chanel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
