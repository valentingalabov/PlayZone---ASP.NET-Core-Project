namespace PlayZone.Web.ViewModels.Favorites
{
    using System.Collections.Generic;

    public class FavoriteLibraryViewModel
    {
        public IEnumerable<FavoriteVideoViewModel> Videos { get; set; }
    }
}
