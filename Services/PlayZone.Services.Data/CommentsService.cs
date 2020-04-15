namespace PlayZone.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using PlayZone.Data.Common.Repositories;
    using PlayZone.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task Create(string videoId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                VideoId = videoId,
                UserId = userId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public bool IsInVideoId(int? commentId, string videoId)
        {
            var commentVideoId = this.commentsRepository.All()
                .Where(c => c.Id == commentId)
                .Select(c => c.VideoId)
                .FirstOrDefault();

            return commentVideoId == videoId;
        }
    }
}
