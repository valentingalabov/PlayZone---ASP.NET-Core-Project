namespace PlayZone.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.EntityFrameworkCore;
    using PlayZone.Data;
    using PlayZone.Data.Models;
    using PlayZone.Data.Repositories;
    using PlayZone.Services.Mapping;
    using PlayZone.Web.ViewModels.Channels;
    using Xunit;

    public class ChannelTests
    {
        private readonly EfDeletableEntityRepository<Channel> channelRepository;
        private readonly EfDeletableEntityRepository<Image> imageRepository;
        private readonly EfDeletableEntityRepository<Video> videoRepository;
        private readonly Cloudinary cloudinary;
        private readonly ChannelsService service;

        private readonly ApplicationUser user;

        private readonly Channel channel1;
        private readonly Channel channel2;

        public ChannelTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            var acc = new Account();
            acc.Cloud = "CloudName";

            this.channelRepository = new EfDeletableEntityRepository<Channel>(new ApplicationDbContext(options.Options));
            this.imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));
            this.videoRepository = new EfDeletableEntityRepository<Video>(new ApplicationDbContext(options.Options));
            this.cloudinary = new Cloudinary(acc);

            this.service = new ChannelsService(this.channelRepository, this.imageRepository, this.videoRepository, this.cloudinary);

            this.user = new ApplicationUser
            {
                UserName = "asd@abv.bg",
                Email = "asd@abv.bg",
            };

            this.channel1 = new Channel
            {
                Title = "Enduro",
                Description = "Desc of channel",
                Image = null,
                UserId = this.user.Id,
            };

            this.channel2 = new Channel
            {
                Title = "Music",
                Description = "Music desc",
                Image = null,
                UserId = this.user.Id,
            };
        }

        [Fact]
        public async Task IsValidChanelTest()
        {
            await this.AddChannelsToRepository();

            Assert.False(this.service.IsValidChannel(this.channel1.Title));
            Assert.True(this.service.IsValidChannel("Valid"));
        }

        [Fact]
        public async Task CreateChannelTest()
        {
            await this.service.CreateChannelAsync(this.channel1.Title, this.channel1.Description, this.user);

            Assert.Single(this.channelRepository.All());
        }

        [Fact]
        public async Task CreateChannelReturnCorrectIdTest()
        {
            var result = await this.service.CreateChannelAsync(this.channel1.Title, this.channel1.Description, this.user);

            var expectedResult = this.channelRepository.All().Select(c => c.Id).FirstOrDefault();

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task GetChannelByIdTest()
        {
            await this.AddChannelsToRepository();

            AutoMapperConfig.RegisterMappings(typeof(ChannelDescriptionViewModel).Assembly);

            var expectedChanel = this.channelRepository.All().To<ChannelDescriptionViewModel>().FirstOrDefault(c => c.Title == this.channel1.Title);

            var result = this.service.GetChannelById<ChannelDescriptionViewModel>(this.channel1.Id);

            Assert.Equal(expectedChanel.Id, result.Id);
        }

        [Fact]
        public async Task CreateImageTest()
        {
            await this.service.CreateImage("354684314", "123", this.channel1);

            Assert.Single(this.imageRepository.All());
        }

        [Fact]
        public async Task GetVieosByChannelTest()
        {
            await this.AddChannelsToRepository();

            await this.videoRepository.AddAsync(new Video
            {
                Title = "Video",
                Description = "video description",
                Url = "https://youtube.com//",
                UserId = this.user.Id,
                ChannelId = this.channel1.Id,
            });

            await this.videoRepository.AddAsync(new Video
            {
                Title = "newVideo",
                Description = "video description",
                Url = "https://youtube.comasdaasf//",
                UserId = this.user.Id,
                ChannelId = this.channel1.Id,
            });

            AutoMapperConfig.RegisterMappings(typeof(VideoByChannelViewModel).Assembly);

            var result = this.service.GetVieosByChannel<VideoByChannelViewModel>(this.channel1.Id, 5);

            var expectedVideos = this.videoRepository.All();

            Assert.Equal(expectedVideos.Count(), result.Count());
        }

        [Fact]
        public async Task GetAllVideosByChannelCountTest()
        {
            await this.AddChannelsToRepository();

            await this.videoRepository.AddAsync(new Video
            {
                Title = "Video",
                Description = "video description",
                Url = "https://youtube.com//",
                UserId = this.user.Id,
                ChannelId = this.channel1.Id,
                Channel = this.channel1,
            });

            await this.videoRepository.AddAsync(new Video
            {
                Title = "newVideo",
                Description = "video description",
                Url = "https://youtube.comasdaasf//",
                UserId = this.user.Id,
                ChannelId = this.channel1.Id,
                Channel = this.channel1,
            });

            var result = this.service.GetAllVideosByChannelCount(this.channel1.Id);

            Assert.Equal(2, result);
        }

        private async Task AddChannelsToRepository()
        {
            await this.channelRepository.AddAsync(this.channel1);

            await this.channelRepository.AddAsync(this.channel2);

            await this.channelRepository.SaveChangesAsync();
        }
    }
}
