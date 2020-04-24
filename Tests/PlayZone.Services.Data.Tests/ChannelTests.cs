namespace PlayZone.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.EntityFrameworkCore;
    using PlayZone.Data;
    using PlayZone.Data.Models;
    using PlayZone.Data.Repositories;
    using Xunit;

    public class ChannelTests
    {
        [Fact]
        public async Task IsValidChanelTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            var channelRepository = new EfDeletableEntityRepository<Channel>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));
            var videoRepository = new EfDeletableEntityRepository<Video>(new ApplicationDbContext(options.Options));
            var cloudinaryRepository = new Cloudinary(cloudinaryUrl: "channel test cloudinary");

            await channelRepository.AddAsync(this.CreateChannel());

            await channelRepository.SaveChangesAsync();

            var service = new ChannelsService(channelRepository, imageRepository, videoRepository, cloudinaryRepository);

            Assert.False(service.IsValidChannel("Enduro"));
        }


        public Channel CreateChannel()
        {
            return new Channel
            {
                Title = "Enduro",
                Description = "Desc of channel",
                Image = null,
                UserId = null,
            };
        }
    }
}
