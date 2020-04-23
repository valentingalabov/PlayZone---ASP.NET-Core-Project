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
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "admin@abv.bg"),
                },
                new Video
                {
                    Url = "5turHFDTZqs",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Black Squat Sniper highlights",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "admin@abv.bg"),
                },
                new Video
                {
                    Url = "fVISdcMySqo",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Fury Warrior Rbg's Cataclysm wow 4.3.4",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "admin@abv.bg"),
                },
                new Video
                {
                    Url = "7L87c7Jza5I&t=2s",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Eco rado",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "demo@abv.bg"),
                },
                new Video
                {
                    Url = "PQf0FVXFzdk",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Chery",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "demo@abv.bg"),
                },

                new Video
                {
                    Url = "Ok3du5hI1M0",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Chery foood",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "demo@abv.bg"),
                },

                new Video
                {
                    Url = "UzcJrhldVLw",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Hair Cut",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "demo@abv.bg"),
                },

                new Video
                {
                    Url = "qYwrNUw_mHc",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Sell your building",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "play@abv.bg"),
                },

                new Video
                {
                    Url = "fZsOcGSC3qQ",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Shipka",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "play@abv.bg"),
                },

                new Video
                {
                    Url = "3yXjAwTTXfI",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Motivation",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "play@abv.bg"),
                },

                new Video
                {
                    Url = "GNQhschvxlo",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Boyana guest house",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "play@abv.bg"),
                },

                new Video
                {
                    Url = "H40UM8oea3g",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Volkswagen advertising",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "play@abv.bg"),
                },

                new Video
                {
                    Url = "_BfjnOGr-1w",
                    CategoryId = dbContext.Categories.Select(x => x.Id).FirstOrDefault(),
                    Title = "Golf",
                    Description = "Add Descriotion Here",
                    UserId = dbContext.Users.Select(u => u.Id).FirstOrDefault(),
                    Channel = dbContext.Channels.FirstOrDefault(c => c.User.UserName == "play@abv.bg"),
                },
            };

            await dbContext.Videos.AddRangeAsync(videos);

            await dbContext.SaveChangesAsync();
        }
    }
}
