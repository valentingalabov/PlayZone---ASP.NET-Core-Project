namespace PlayZone.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using PlayZone.Data.Models;

    public interface IChannelsService
    {
        Task<string> CreateChannelAsync(string title, string description, ApplicationUser user);

        bool IsValidChannel(string title);

        bool IsValidChaneAfterEdit(string channelId, string title);

        Task UploadAsync(IFormFile file, string id);

        T GetChannelById<T>(string id);

        int GetAllVideosByChannelCount(string id);

        bool IsOwner(string channelId, string userchannelId);

        Task CreateImage(string url, string cloudinaryPublicId, Channel currentchannel);

        IEnumerable<T> GetVieosByChannel<T>(string id, int? take, int skip = 0);

        Task EditChannelAsync(string id, string title, string description);
    }
}
