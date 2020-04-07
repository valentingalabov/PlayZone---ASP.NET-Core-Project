namespace PlayZone.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PlayZone.Data.Common.Repositories;
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class FavoritesService : IFavoritesServices
    {
        private readonly IDeletableEntityRepository<FavoriteVideo> favoritesVideosRepository;

        public FavoritesService(IDeletableEntityRepository<FavoriteVideo> favoritesVideosRepository)
        {
            this.favoritesVideosRepository = favoritesVideosRepository;
        }

        public async Task AddVideoToFavoriteAsync(string videoId, string userId)
        {
            var favoriteVideo = this.favoritesVideosRepository.AllWithDeleted()
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

                await this.favoritesVideosRepository.AddAsync(favoriteVideo);
            }

            await this.favoritesVideosRepository.SaveChangesAsync();
        }

        public async Task DeleteFromFavoritesAsync(string videoId, string userId)
        {
            var videoToDelete = this.favoritesVideosRepository.All()
                 .FirstOrDefault(v => v.VideoId == videoId && v.UserId == userId);

            if (videoToDelete != null)
            {
                this.favoritesVideosRepository.Delete(videoToDelete);
                await this.favoritesVideosRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetFavoriteVideosByUser<T>(string userId)
        {
            var favoriteVideos = this.favoritesVideosRepository.All()
                .Where(u => u.UserId == userId)
                .OrderByDescending(v => v.CreatedOn)
                .To<T>()
                .ToList();

            return favoriteVideos;
        }
    }
}
