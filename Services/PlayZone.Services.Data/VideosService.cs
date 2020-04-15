namespace PlayZone.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PlayZone.Data.Common.Repositories;
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

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
            var video = this.videosRepository.All()
                .Where(v => v.Id == id).To<T>()
                .FirstOrDefault();

            return video;
        }

        public IEnumerable<T> GetAllVideos<T>(int? take, int skip = 0)
        {
            var query = this.videosRepository.All()
                .OrderByDescending(v => v.CreatedOn)
                .Skip(skip);

            return query.Take(take.Value).To<T>().ToList();
        }

        public bool IsValidVideo(string title, string url)
        {
            if (this.videosRepository.All().Any(v => v.Title == title || v.Url == url))
            {
                return false;
            }

            return true;
        }

        public int GetAllVideosCount()
        {
            return this.videosRepository.All().Count();
        }
    }
}
