namespace PlayZone.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PlayZone.Data.Models;

    public class VideosSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Videos.Any())
            {
                return;
            }

            var videos = new List<Video>()
            {
                new Video
                {
                    Url = "RjHp4eD0THY",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Sunday Motorcycle Riding",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Chanel = dbContext.Chanels.FirstOrDefault(),
                },
                new Video
                {
                    Url = "5turHFDTZqs",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Black Squat Sniper highlights",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Chanel = dbContext.Chanels.FirstOrDefault(),
                },
                new Video
                {
                    Url = "fVISdcMySqo",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Fury Warrior Rbg's Cataclysm wow 4.3.4",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Chanel = dbContext.Chanels.FirstOrDefault(),
                },
            };

            foreach (var video in videos)
            {
                await dbContext.Videos.AddAsync(video);
            }
        }
    }
}
