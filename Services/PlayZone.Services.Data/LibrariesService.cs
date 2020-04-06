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
        private readonly IDeletableEntityRepository<FavoriteVideo> favoritesVideoRepository;

        public LibrariesService(
            IDeletableEntityRepository<VideoHistory> videoHistoryRepository,
            IDeletableEntityRepository<FavoriteVideo> favoritesVideoRepository)
        {
            this.videoHistoryRepository = videoHistoryRepository;
            this.favoritesVideoRepository = favoritesVideoRepository;
        }

        public async Task AddVideoToFavoriteAsync(string videoId, string userId)
        {
            var favoriteVideo = this.favoritesVideoRepository.AllWithDeleted()
                .FirstOrDefault(v => v.VideoId == videoId && v.UserId == userId);

            if (favoriteVideo != null)
            {
                if (favoriteVideo.IsDeleted == true)
                {
                    favoriteVideo.IsDeleted = false;
                    favoriteVideo.DeletedOn = null;
                }

                favoriteVideo.CreatedOn = DateTime.UtcNow;
            }
            else
            {
                favoriteVideo = new FavoriteVideo
                {
                    VideoId = videoId,
                    UserId = userId,
                };

                await this.favoritesVideoRepository.AddAsync(favoriteVideo);
            }

            await this.favoritesVideoRepository.SaveChangesAsync();
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

        public async Task DeleteFromLibrary(string videoId)
        {
            var videoToDelete = this.videoHistoryRepository.All().FirstOrDefault(x => x.VideoId == videoId);
            if (videoToDelete != null)
            {
                this.videoHistoryRepository.Delete(videoToDelete);
                await this.videoHistoryRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetFavoriteVideosByUser<T>(string userId)
        {
            var favoriteVideos = this.favoritesVideoRepository.All()
                .Where(u => u.UserId == userId)
                .OrderByDescending(v => v.CreatedOn)
                .To<T>()
                .ToList();

            return favoriteVideos;
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
