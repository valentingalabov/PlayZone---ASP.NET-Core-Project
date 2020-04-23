namespace PlayZone.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PlayZone.Data.Models;

    public class ChannelsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Channels.Any())
            {
                return;
            }

            var user1 = dbContext.Users.FirstOrDefault(u => u.UserName == "admin@abv.bg");
            var user2 = dbContext.Users.FirstOrDefault(u => u.UserName == "demo@abv.bg");
            var user3 = dbContext.Users.FirstOrDefault(u => u.UserName == "play@abv.bg");

            var channel = new List<Channel>()
            {
                new Channel
                {
                Title = "AD Channel",
                UserId = user1.Id,
                Description = "This is channel with ad Content!",
                },

                new Channel
                {
                Title = "Motorsport",
                UserId = user2.Id,
                Description = "This is channel with Cars",
                },

                new Channel
                {
                Title = "Gaming Channel",
                UserId = user3.Id,
                Description = "This is channel with Gaming!",
                },
            };

            await dbContext.Channels.AddRangeAsync(channel);
            await dbContext.SaveChangesAsync();
        }
    }
}
