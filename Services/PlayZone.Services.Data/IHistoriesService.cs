namespace PlayZone.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHistoriesService
    {
        Task AddVideoToHistoryAsync(string videoId, string userId);

        IEnumerable<T> GetVideosHistoryByUser<T>(string userId);

        Task DeleteFromHistoryAsync(string videoId, string userId);
    }
}
