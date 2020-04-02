namespace PlayZone.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PlayZone.Data.Common.Repositories;
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class LibrariesService : ILibrariesService
    {
        private readonly IDeletableEntityRepository<VideoHistory> videoHistoryRepository;

        public LibrariesService(IDeletableEntityRepository<VideoHistory> videoHistoryRepository)
        {
            this.videoHistoryRepository = videoHistoryRepository;
        }

        public async Task AddVideoToHistoryAsync(string videoId, string userId)
        {
            var history = this.videoHistoryRepository.AllWithDeleted()
                .FirstOrDefault(h => h.VideoId == videoId && h.UserId == userId);

            if (history != null)
            {
                if (history.IsDeleted == true)
                {
                    history.IsDeleted = false;
                    history.DeletedOn = null;
                }

                history.CreatedOn = DateTime.UtcNow;
                await this.videoHistoryRepository.SaveChangesAsync();
            }
            else
            {
                var videoHistory = new VideoHistory
                {
                    UserId = userId,
                    VideoId = videoId,
                };

                await this.videoHistoryRepository.AddAsync(videoHistory);
                await this.videoHistoryRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteFromLibrary(string videoId)
        {
            var videoToDelete = this.videoHistoryRepository.All().FirstOrDefault(x => x.VideoId == videoId);
            if (videoToDelete != null)
            {
                this.videoHistoryRepository.Delete(videoToDelete);
                await this.videoHistoryRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetVideosHistoryByUser<T>(string userId)
        {
            var videos = this.videoHistoryRepository.All()
                .Where(u => u.UserId == userId)
                .OrderByDescending(v => v.CreatedOn)
                .To<T>()
                .ToList();

            return videos;
        }
    }
}
