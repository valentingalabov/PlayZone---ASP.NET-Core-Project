namespace PlayZone.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlayZone.Data;
    using PlayZone.Data.Models;
    using PlayZone.Data.Repositories;
    using Xunit;

    public class VideosServiceTests
    {
        private readonly EfDeletableEntityRepository<Video> videoRepository;
        private readonly VideosService service;

        private readonly Video video;

        public VideosServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            this.videoRepository = new EfDeletableEntityRepository<Video>(new ApplicationDbContext(options.Options));

            this.service = new VideosService(this.videoRepository);

            this.video = new Video
            {
                Id = "id",
                CategoryId = 1,
                ChannelId = "channelId",
                UserId = "userId",
                Description = "Description",
                Url = "mDC8ZSKTWKc&t=1s",
                Title = "Title",
            };
        }

        [Fact]
        public async Task CreateVideoAsyncWorkCorrectTest()
        {
            await this.service.CreateVideoAsync(this.video.Title, this.video.Url, this.video.Description, this.video.CategoryId, this.video.UserId, this.video.ChannelId);

            Assert.Single(this.videoRepository.All());
        }

        [Fact]
        public async Task CreateVideoAsyncReturnCoorectValueTest()
        {
            var result = await this.service.CreateVideoAsync(this.video.Title, this.video.Url, this.video.Description, this.video.CategoryId, this.video.UserId, this.video.ChannelId);
            var videoId = this.videoRepository.All().Select(v => v.Id).FirstOrDefault();

            Assert.Equal(videoId, result);
        }

        [Fact]
        public async Task IsValidVideoWorkCorrectWhenVideoExistTest()
        {
            await this.videoRepository.AddAsync(this.video);
            await this.videoRepository.SaveChangesAsync();

            var result = this.service.IsValidVideo(this.video.Title, "TestedUrlVideo");

            Assert.False(result);
        }

        [Fact]
        public async Task IsValidVideoWorkCorrectWhenVideoNotExistTest()
        {
            await this.videoRepository.AddAsync(this.video);
            await this.videoRepository.SaveChangesAsync();

            var result = this.service.IsValidVideo("TestTitle", "TestedUrlVideo");

            Assert.True(result);
        }

        [Fact]
        public async Task GetAllVideosCountWorkCorrectTest()
        {
            await this.videoRepository.AddAsync(this.video);
            await this.videoRepository.SaveChangesAsync();

            var result = this.service.GetAllVideosCount();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task IsOwnerWrokCorrectTest()
        {
            await this.videoRepository.AddAsync(this.video);
            await this.videoRepository.SaveChangesAsync();

            var result = this.service.IsOwner(this.video.Id, this.video.UserId);

            Assert.True(result);
        }

        [Fact]
        public async Task IsOwnerWrokCorrectIfUserIsNotOwnerTest()
        {
            await this.videoRepository.AddAsync(this.video);
            await this.videoRepository.SaveChangesAsync();

            var result = this.service.IsOwner(this.video.Id, "testId");

            Assert.False(result);
        }

        [Fact]
        public async Task EditVideoAsyncWorkCorrectTest()
        {
            await this.videoRepository.AddAsync(this.video);
            await this.videoRepository.SaveChangesAsync();

            await this.service.EditVideoAsync(this.video.Id, "NewTitle", "NewUrl", "NewDescription", 2);

            Assert.Equal("NewTitle", this.video.Title);
        }

        [Fact]
        public async Task EditVideoAsyncWorkCorrectWhenNotFindIdTest()
        {
            await this.videoRepository.AddAsync(this.video);
            await this.videoRepository.SaveChangesAsync();

            await this.service.EditVideoAsync("testId", "NewTitle", "NewUrl", "NewDescription", 2);

            Assert.Equal("Title", this.video.Title);
        }

        [Fact]
        public void GetShortUrlWorkCorrectTest()
        {
            var result = this.service.GetShortUrl("https://www.youtube.com/watch?v=mDC8ZSKTWKc&t=1s");

            Assert.Equal("mDC8ZSKTWKc&t=1s", result);
        }

        [Fact]
        public async Task DeleteWorkCorrectTest()
        {
            await this.videoRepository.AddAsync(this.video);
            await this.videoRepository.SaveChangesAsync();

            await this.service.Delete(this.video.Id);

            Assert.Equal(0, this.videoRepository.All().Count());
        }

        [Fact]
        public async Task DeleteNotDeleteWhenNotFindTest()
        {
            await this.videoRepository.AddAsync(this.video);
            await this.videoRepository.SaveChangesAsync();

            await this.service.Delete("testId");

            Assert.Single(this.videoRepository.All());
        }

        [Fact]
        public async Task IsValidUrlAfterEditWorkCorrectTest()
        {
            await this.videoRepository.AddAsync(this.video);

            await this.videoRepository.AddAsync(new Video
            {
                Id = "id1",
                CategoryId = 2,
                ChannelId = "channelId1",
                UserId = "userId1",
                Description = "Description1",
                Url = "mDC8ZSKTWKc&t=2s",
                Title = "Title1",
            });

            await this.videoRepository.SaveChangesAsync();

            var result = this.service.IsValidUrlAfterEdit(this.video.Id, "https://www.youtube.com/watch?v=mDC8ZSKTWKc&t=2s");

            Assert.False(result);
        }

        [Fact]
        public async Task IsValidTitleAfterEditWorkCorrectTest()
        {
            await this.videoRepository.AddAsync(this.video);

            await this.videoRepository.AddAsync(new Video
            {
                Id = "id1",
                CategoryId = 2,
                ChannelId = "channelId1",
                UserId = "userId1",
                Description = "Description1",
                Url = "mDC8ZSKTWKc&t=2s",
                Title = "Title1",
            });

            await this.videoRepository.SaveChangesAsync();

            var result = this.service.IsValidTitleAfterEdit("id1", this.video.Title);

            Assert.False(result);
        }

        [Fact]
        public async Task IsValidTitleWorkCorrectTest()
        {
            await this.videoRepository.AddAsync(this.video);

            await this.videoRepository.AddAsync(new Video
            {
                Id = "id1",
                CategoryId = 2,
                ChannelId = "channelId1",
                UserId = "userId1",
                Description = "Description1",
                Url = "mDC8ZSKTWKc&t=2s",
                Title = "Title1",
            });

            await this.videoRepository.SaveChangesAsync();

            var result = this.service.IsValidTitle(this.video.Title);

            Assert.False(result);
        }

        [Fact]
        public async Task IsValidUrlCorrectWorkTest()
        {
            await this.videoRepository.AddAsync(this.video);
            await this.videoRepository.SaveChangesAsync();

            var result = this.service.IsValidUrl("https://www.youtube.com/watch?v=mDC8ZSKTWKc&t=3s");

            Assert.True(result);
        }
    }
}
