namespace PlayZone.Web.ViewModels.Chanels
{
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class ChanelTitleViewModel : IMapFrom<Chanel>
    {
        public string Title { get; set; }
    }
}