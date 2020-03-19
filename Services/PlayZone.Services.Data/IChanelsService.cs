namespace PlayZone.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using PlayZone.Data.Models;

    public interface IChanelsService
    {
        Task<string> CreateChanelAsync(string title, string description, ApplicationUser user);

        bool IsValidChanel(string title);

        Task<string> UploadAsync(IFormFile file);

        T GetChanelById<T>(string id);
    }
}
