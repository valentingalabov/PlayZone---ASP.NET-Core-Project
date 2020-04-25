namespace PlayZone.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlayZone.Data;
    using PlayZone.Data.Models;
    using PlayZone.Data.Repositories;
    using PlayZone.Services.Mapping;
    using PlayZone.Web.ViewModels.Favorites;
    using Xunit;

    public class FavoritesServiceTest
    {
        private readonly EfDeletableEntityRepository<FavoriteVideo> favoritesRepository;
        private readonly FavoritesService service;

        private readonly FavoriteVideo favoriteVideo;

        public FavoritesServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            this.favoritesRepository = new EfDeletableEntityRepository<FavoriteVideo>(new ApplicationDbContext(options.Options));

            this.service = new FavoritesService(this.favoritesRepository);

            this.favoriteVideo = new FavoriteVideo
            {
                UserId = "user1",
                VideoId = "video1",
            };
        }

        [Fact]
        public async Task AddVideoToFavoriteAsyncWorkCorrectTest()
        {
            await this.service.AddVideoToFavoriteAsync("vidoeId", "userId");

            Assert.Single(this.favoritesRepository.All());
        }

        [Fact]
        public async Task AddVideoToFavoriteNotAddSameRecordTest()
        {
            await this.service.AddVideoToFavoriteAsync("vidoeId", "userId");
            await this.service.AddVideoToFavoriteAsync("vidoeId", "userId");

            Assert.Single(this.favoritesRepository.All());
        }

        [Fact]
        public async Task DeleteFromFavoritesAsyncWorkCorrentTest()
        {
            await this.favoritesRepository.AddAsync(this.favoriteVideo);
            await this.favoritesRepository.SaveChangesAsync();

            await this.service.DeleteFromFavoritesAsync(this.favoriteVideo.VideoId, this.favoriteVideo.UserId);

            Assert.Empty(this.favoritesRepository.All());
        }

        [Fact]
        public async Task DeleteFromFavoritesAsyncNotDeleteIfNotExistedTest()
        {
            await this.favoritesRepository.AddAsync(this.favoriteVideo);
            await this.favoritesRepository.SaveChangesAsync();

            await this.service.DeleteFromFavoritesAsync("test1", "test2");

            Assert.Single(this.favoritesRepository.All());
        }

        [Fact]
        public async Task GetFavoriteVideosByUserWorkCorrectTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(FavoriteVideoViewModel).Assembly);
            await this.favoritesRepository.AddAsync(this.favoriteVideo);
            await this.favoritesRepository.SaveChangesAsync();

            var videos = this.service.GetFavoriteVideosByUser<FavoriteVideoViewModel>("user1");

            Assert.Single(videos);
        }

        [Fact]
        public async Task GetFavoriteVideosByUserOrderCorrectTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(FavoriteVideoViewModel).Assembly);
            await this.favoritesRepository.AddAsync(new FavoriteVideo
            {
                UserId = "user1",
                VideoId = "video1",
            });
            await this.favoritesRepository.AddAsync(new FavoriteVideo
            {
                UserId = "user1",
                VideoId = "video2",
            });
            await this.favoritesRepository.SaveChangesAsync();

            var videos = this.service.GetFavoriteVideosByUser<FavoriteVideoViewModel>("user1");

            Assert.Equal(2, videos.Count());
        }

        [Fact]
        public async Task IsVideoExistWorkCorrectTest()
        {
            await this.favoritesRepository.AddAsync(this.favoriteVideo);
            await this.favoritesRepository.SaveChangesAsync();

            Assert.True(this.service.IsVideoExist(this.favoriteVideo.VideoId, this.favoriteVideo.UserId));
            Assert.False(this.service.IsVideoExist("test", this.favoriteVideo.UserId));
        }

        public class ViewModel : IMapFrom<FavoriteVideo>
        {
            public string UserId { get; set; }

            public string VideoId { get; set; }
        }
    }
}
