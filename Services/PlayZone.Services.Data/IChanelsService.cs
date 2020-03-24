﻿namespace PlayZone.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using PlayZone.Data.Models;

    public interface IChanelsService
    {
        Task<string> CreateChanelAsync(string title, string description, ApplicationUser user);

        bool IsValidChanel(string title);

        Task UploadAsync(IFormFile file, string id);

        T GetChanelById<T>(string id);

        Task CreateImage(string url, string cloudinaryPublicId, Chanel currentChanel);

        //string GetChanelDescription(string id);

        IEnumerable<T> GetAllVieos<T>(string id);

    }
}