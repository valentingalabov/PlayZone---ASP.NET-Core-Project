namespace PlayZone.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVideosService
    {
        IEnumerable<T> GetAllVideos<T>(int? take, int skip = 0);

        int GetAllVideosCount();

        Task<string> CreateVideoAsync(string title, string url, string description, int categoryId, string userId, string chanelId);

        T GetVideoById<T>(string id);

        bool IsValidVideo(string title, string url);
    }
}
