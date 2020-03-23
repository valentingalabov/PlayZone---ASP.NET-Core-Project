namespace PlayZone.Services.Data
{
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
        private readonly Cloudinary cloudinary;

        public ChanelsService(IDeletableEntityRepository<Chanel> chanelRepository, IDeletableEntityRepository<Image> imageRepository, Cloudinary cloudinary)
        {
            this.chanelRepository = chanelRepository;
            this.imageRepository = imageRepository;
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
            var currentChanel = this.chanelRepository.All().Where(c => c.Id == id).FirstOrDefault();

            if (currentChanel.Image != null)
            {
                this.cloudinary.DeleteResources(currentChanel.Image.Id);
                currentChanel.Image = null;
            }

            var imageToDelete = this.imageRepository.All().Where(i => i.ChanelId == id).FirstOrDefault();
            this.imageRepository.HardDelete(imageToDelete);
            await this.imageRepository.SaveChangesAsync();

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

            var imageId = this.CreateImage(imageUrl, publicId, currentChanel);
        }

        public T GetChanelById<T>(string id)
        {
            var chanel = this.chanelRepository.All().Where(c => c.Id == id).To<T>().FirstOrDefault();

            return chanel;
        }

        public async Task<string> CreateImage(string url, string cloudinaryPublicId, Chanel currentChanel)
        {
            var image = new Image
            {
                Url = url,
                CloudinaryPublicId = cloudinaryPublicId,
                ChanelId = currentChanel.Id,
                Chanel = currentChanel,
            };

            await this.imageRepository.AddAsync(image);
            currentChanel.Image = image;

            await this.imageRepository.SaveChangesAsync();

            return image.Id;
        }
    }
}
