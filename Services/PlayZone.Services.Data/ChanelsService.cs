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
        private readonly Cloudinary cloudinary;

        public ChanelsService(IDeletableEntityRepository<Chanel> chanelRepository, Cloudinary cloudinary)
        {
            this.chanelRepository = chanelRepository;
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

        public async Task<string> UploadAsync(IFormFile file)
        {
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

            var imageUrl = result.Uri.AbsoluteUri.Replace("https://res.cloudinary.com/dqh6dvohu/image/upload/", string.Empty);
            return imageUrl;
        }

        public T GetChanelById<T>(string id)
        {
            var chanel = this.chanelRepository.All().Where(c => c.Id == id).To<T>().FirstOrDefault();

            return chanel;
        }
    }
}
