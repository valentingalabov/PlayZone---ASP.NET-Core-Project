namespace PlayZone.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFavoritesServices
    {
        Task AddVideoToFavoriteAsync(string videoId, string userId);

        IEnumerable<T> GetFavoriteVideosByUser<T>(string userId);

        Task DeleteFromFavoritesAsync(string videoId, string userId);

        bool IsVideoExist(string videoId, string userId);
    }
}
