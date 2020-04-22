namespace PlayZone.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using PlayZone.Data.Common.Repositories;
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class ChannelsService : IChannelsService
    {
        private readonly IDeletableEntityRepository<Channel> channelRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly IDeletableEntityRepository<PlayZone.Data.Models.Video> videoReposiroty;
        private readonly Cloudinary cloudinary;

        public ChannelsService(
            IDeletableEntityRepository<Channel> channelRepository,
            IDeletableEntityRepository<Image> imageRepository,
            IDeletableEntityRepository<PlayZone.Data.Models.Video> videoReposiroty,
            Cloudinary cloudinary)
        {
            this.channelRepository = channelRepository;
            this.imageRepository = imageRepository;
            this.videoReposiroty = videoReposiroty;
            this.cloudinary = cloudinary;
        }

        public bool IsValidChannel(string title)
        {
            if (this.channelRepository.All().Any(c => c.Title == title))
            {
                return false;
            }

            return true;
        }

        public async Task<string> CreateChannelAsync(string title, string description, ApplicationUser user)
        {
            var channel = new Channel
            {
                Title = title,
                Description = description,
                UserId = user.Id,
            };

            user.ChannelId = channel.Id;

            await this.channelRepository.AddAsync(channel);
            await this.channelRepository.SaveChangesAsync();

            return channel.Id;
        }

        public async Task UploadAsync(IFormFile file, string id)
        {
            var currentImage = this.imageRepository.All().FirstOrDefault(c => c.ChannelId == id);

            if (currentImage != null)
            {
                this.cloudinary.DeleteResources(currentImage.CloudinaryPublicId);
                this.imageRepository.HardDelete(currentImage);
                await this.imageRepository.SaveChangesAsync();
            }

            byte[] destinationImage;
            ImageUploadResult result;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                destinationImage = memoryStream.ToArray();
            }

            using (var destinationStream = new MemoryStream(destinationImage))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, destinationStream),
                };
                result = await this.cloudinary.UploadAsync(uploadParams);
            }

            var imageUrl = result.Uri.AbsoluteUri.Replace("http://res.cloudinary.com/dqh6dvohu/image/upload/", string.Empty);
            var publicId = result.PublicId;

            var currentChannel = this.channelRepository.All().Where(c => c.Id == id).FirstOrDefault();

            await this.CreateImage(imageUrl, publicId, currentChannel);
        }

        public T GetChannelById<T>(string id)
        {
            var channel = this.channelRepository.All()
                .Where(c => c.Id == id).To<T>()
                .FirstOrDefault();

            return channel;
        }

        public async Task CreateImage(string url, string cloudinaryPublicId, Channel currentChannel)
        {
            var image = new Image
            {
                Url = url,
                CloudinaryPublicId = cloudinaryPublicId,
                ChannelId = currentChannel.Id,
                Channel = currentChannel,
            };

            await this.imageRepository.AddAsync(image);

            currentChannel.ImageId = image.Id;

            await this.imageRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetVieosByChannel<T>(string id, int? take, int skip = 0)
        {
            var videos = this.videoReposiroty.All()
                .Where(v => v.ChannelId == id)
                .OrderByDescending(v => v.CreatedOn)
                .Skip(skip);

            return videos.Take(take.Value)
                         .To<T>()
                         .ToList();
        }

        public int GetAllVideosByChannelCount(string id)
        {
            return this.videoReposiroty.All()
                 .Where(c => c.ChannelId == id)
                 .Count();
        }

        public bool IsOwner(string channelId, string userChannelId)
        {
            if (userChannelId == channelId)
            {
                return true;
            }

            return false;
        }

        public async Task EditChannelAsync(string id, string title, string description)
        {
            var channel = this.channelRepository.All()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (channel != null)
            {
                channel.Title = title;
                channel.Description = description;
            }

            await this.channelRepository.SaveChangesAsync();
        }

        public bool IsValidChaneAfterEdit(string channelId, string title)
        {
            var channels = this.channelRepository.All()
                .Where(c => c.Id != channelId)
                .ToList();

            if (channels.Any(c => c.Title == title))
            {
                return false;
            }

            return true;
        }
    }
}
