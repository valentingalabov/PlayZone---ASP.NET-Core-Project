namespace PlayZone.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PlayZone.Data.Models;

    public interface IVideosService
    {
        IEnumerable<T> GetAllVideos<T>(int? take, int skip = 0);

        int GetAllVideosCount();

        Task<string> CreateVideoAsync(string title, string url, string description, int categoryId, string userId, string channelId);

        T GetVideoById<T>(string id);

        string GetShortUrl(string url);

        bool IsValidUrlAfterEdit(string id, string url);

        bool IsValidTitleAfterEdit(string id, string title);

        bool IsValidTitle(string title);

        bool IsValidUrl(string url);

        bool IsOwner(string videoId, string userId);

        Task EditVideoAsync(string videoId, string title, string url, string description, int categoryId);

        Task Delete(string videoId);
    }
}
