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


    public class ChanelsService : IChanelsService
    {
        private readonly IDeletableEntityRepository<Chanel> chanelRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly IDeletableEntityRepository<PlayZone.Data.Models.Video> videoReposiroty;
        private readonly Cloudinary cloudinary;

        public ChanelsService(
            IDeletableEntityRepository<Chanel> chanelRepository,
            IDeletableEntityRepository<Image> imageRepository,
            IDeletableEntityRepository<PlayZone.Data.Models.Video> videoReposiroty,
            Cloudinary cloudinary)
        {
            this.chanelRepository = chanelRepository;
            this.imageRepository = imageRepository;
            this.videoReposiroty = videoReposiroty;
            this.cloudinary = cloudinary;
        }

        public bool IsValidChanel(string title)
        {
            if (this.chanelRepository.All().Any(c => c.Title == title))
            {
                return false;
            }

            return true;
        }

        public async Task<string> CreateChanelAsync(string title, string description, ApplicationUser user)
        {
            var chanel = new Chanel
            {
                Title = title,
                Description = description,
                UserId = user.Id,
            };

            user.ChanelId = chanel.Id;

            await this.chanelRepository.AddAsync(chanel);
            await this.chanelRepository.SaveChangesAsync();

            return chanel.Id;
        }

        public async Task UploadAsync(IFormFile file, string id)
        {
            var currentImage = this.imageRepository.All().FirstOrDefault(c => c.ChanelId == id);

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

            var currentChanel = this.chanelRepository.All().Where(c => c.Id == id).FirstOrDefault();

            await this.CreateImage(imageUrl, publicId, currentChanel);
        }

        public T GetChanelById<T>(string id)
        {
            var chanel = this.chanelRepository.All().Where(c => c.Id == id).To<T>().FirstOrDefault();

            return chanel;
        }

        public async Task CreateImage(string url, string cloudinaryPublicId, Chanel currentChanel)
        {
            var image = new Image
            {
                Url = url,
                CloudinaryPublicId = cloudinaryPublicId,
                ChanelId = currentChanel.Id,
                Chanel = currentChanel,
            };

            await this.imageRepository.AddAsync(image);

            currentChanel.ImageId = image.Id;

            await this.imageRepository.SaveChangesAsync();
        }

        //public string GetChanelDescription(string id)
        //{
        //    var chanel = this.chanelRepository.All().Where(c => c.Id == id).FirstOrDefault();

        //    if (chanel != null)
        //    {
        //        return chanel.Description;
        //    }

        //    return null;
        //}

        public IEnumerable<T> GetAllVieos<T>(string id)
        {
            var videos = this.videoReposiroty.All().Where(v => v.ChanelId == id);

            return videos.To<T>().ToList();
        }
    }
}
