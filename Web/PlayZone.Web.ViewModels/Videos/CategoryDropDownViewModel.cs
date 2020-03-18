namespace PlayZone.Web.ViewModels.Videos
{
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}