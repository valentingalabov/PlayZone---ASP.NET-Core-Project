namespace PlayZone.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PlayZone.Data.Common.Repositories;
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class HistoriesService : IHistoriesService
    {
        private readonly IDeletableEntityRepository<VideoHistory> videoHistoryRepository;

        public HistoriesService(IDeletableEntityRepository<VideoHistory> videoHistoryRepository)
        {
            this.videoHistoryRepository = videoHistoryRepository;
        }

        public async Task AddVideoToHistoryAsync(string videoId, string userId)
        {
            var videoHistory = this.videoHistoryRepository.AllWithDeleted()
                .FirstOrDefault(h => h.VideoId == videoId && h.UserId == userId);

            if (videoHistory != null)
            {
                if (videoHistory.IsDeleted == true)
                {
                    videoHistory.IsDeleted = false;
                    videoHistory.DeletedOn = null;
                }

                videoHistory.CreatedOn = DateTime.UtcNow;
            }
            else
            {
                videoHistory = new VideoHistory
                {
                    UserId = userId,
                    VideoId = videoId,
                };

                await this.videoHistoryRepository.AddAsync(videoHistory);
            }

            await this.videoHistoryRepository.SaveChangesAsync();
        }

        public async Task DeleteFromHistoryAsync(string videoId, string userId)
        {
            var videoToDelete = this.videoHistoryRepository.All()
                .FirstOrDefault(v => v.VideoId == videoId && v.UserId == userId);

            if (videoToDelete != null)
            {
                this.videoHistoryRepository.Delete(videoToDelete);
                await this.videoHistoryRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetVideosHistoryByUser<T>(string userId)
        {
            var historyVideos = this.videoHistoryRepository.All()
                .Where(u => u.UserId == userId)
                .OrderByDescending(v => v.CreatedOn)
                .To<T>()
                .ToList();

            return historyVideos;
        }
    }
}
