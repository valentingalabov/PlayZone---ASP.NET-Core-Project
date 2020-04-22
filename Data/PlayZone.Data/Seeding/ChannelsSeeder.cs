namespace PlayZone.Data.Seeding
{
    using System;
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

            var videos = dbContext.Videos.ToList();
            var user = dbContext.Users.FirstOrDefault();
            var channel = new Channel
            {
                Title = "EnduroVideochannel",
                UserId = user.Id,
                Description = "This is channel with Enduro Content!",
                Videos = videos,
            };

            await dbContext.Channels.AddAsync(channel);
            user.ChannelId = channel.Id;
        }
    }
}
