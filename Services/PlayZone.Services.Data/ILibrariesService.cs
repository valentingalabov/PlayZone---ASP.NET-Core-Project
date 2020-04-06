namespace PlayZone.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILibrariesService
    {
        Task AddVideoToHistoryAsync(string videoId, string userId);

        IEnumerable<T> GetVideosHistoryByUser<T>(string userId);

        Task DeleteFromLibrary(string videoId);

        Task AddVideoToFavoriteAsync(string videoId, string userId);

        IEnumerable<T> GetFavoriteVideosByUser<T>(string userId);
    }
}
