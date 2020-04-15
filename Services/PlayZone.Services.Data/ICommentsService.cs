namespace PlayZone.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task Create(string videoId, string userId, string content, int? parentId = null);

        bool IsInVideoId(int? commentId, string videoId);
    }
}
