namespace PlayZone.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PlayZone.Data.Common.Repositories;
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;
    using PlayZone.Web.ViewModels.Videos;

    public class VideosService : IVideosService
    {
        private readonly IDeletableEntityRepository<Video> videosRepository;

        public VideosService(IDeletableEntityRepository<Video> videosRepository)
        {
            this.videosRepository = videosRepository;
        }

        public async Task<string> CreateVideoAsync(string title, string url, string description, int categoryId, string userId, string chanelId)
        {
            var urlToAdd = url.Replace("https://www.youtube.com/watch?v=", string.Empty);

            var video = new Video
            {
                Title = title,
                Url = urlToAdd,
                CategoryId = categoryId,
                Description = description,
                UserId = userId,
                ChanelId = chanelId,
            };

            await this.videosRepository.AddAsync(video);

            await this.videosRepository.SaveChangesAsync();

            return video.Id;
        }

        public T GetVideoById<T>(string id)
        {
            var video = this.videosRepository.All().Where(v => v.Id == id).To<T>().FirstOrDefault();

            return video;
        }

        public IEnumerable<T> GetAllVieos<T>()
        {
            return this.videosRepository.All().To<T>().ToList();
        }

        public bool IsValidVideo(string title, string url)
        {
            if (this.videosRepository.All().Any(v => v.Title == title || v.Url == url))
            {
                return false;
            }

            return true;
        }
    }
}
