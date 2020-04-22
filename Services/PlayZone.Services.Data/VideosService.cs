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
            var video = new Video
            {
                Title = title,
                Url = this.GetShortUrl(url),
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
            if (this.videosRepository.All()
                .Any(v => v.Title == title || v.Url == this.GetShortUrl(url)))
            {
                return false;
            }

            return true;
        }

        public int GetAllVideosCount()
        {
            return this.videosRepository.All().Count();
        }

        public bool IsOwner(string videoId, string userId)
        {
            var video = this.videosRepository.All().FirstOrDefault(v => v.Id == videoId);

            if (video.UserId == userId)
            {
                return true;
            }

            return false;
        }

        public async Task EditVideoAsync(string videoId, string title, string url, string description, int categoryId)
        {
            var videoToEdit = this.videosRepository.All().Where(v => v.Id == videoId).FirstOrDefault();

            if (videoToEdit != null)
            {
                videoToEdit.Title = title;
                videoToEdit.Url = this.GetShortUrl(url);
                videoToEdit.Description = description;
                videoToEdit.CategoryId = categoryId;

                await this.videosRepository.SaveChangesAsync();
            }
        }


        public string GetShortUrl(string url)
        {
            return url.Replace("https://www.youtube.com/watch?v=", string.Empty);
        }

        public async Task Delete(string videoId)
        {
            var videoToDelete = this.videosRepository.All().FirstOrDefault(v => v.Id == videoId);

            if (videoToDelete != null)
            {
                this.videosRepository.Delete(videoToDelete);
            }

            await this.videosRepository.SaveChangesAsync();
        }

        public bool IsValidUrlAfterEdit(string id, string url)
        {
            var videos = this.videosRepository.All().Where(v => v.Id != id).ToList();

            if (videos.Any(v => v.Url == this.GetShortUrl(url)))
            {
                return false;
            }

            return true;
        }

        public bool IsValidTitleAfterEdit(string id, string title)
        {
            var videos = this.videosRepository.All().Where(v => v.Id != id).ToList();

            if (videos.Any(v => v.Title == title))
            {
                return false;
            }

            return true;
        }

        public bool IsValidTitle(string title)
        {
            if (this.videosRepository.All()
                  .Any(v => v.Title == title))
            {
                return false;
            }

            return true;
        }

        public bool IsValidUrl(string url)
        {
            if (this.videosRepository.All()
                 .Any(v => v.Url == this.GetShortUrl(url)))
            {
                return false;
            }

            return true;
        }
    }
}
