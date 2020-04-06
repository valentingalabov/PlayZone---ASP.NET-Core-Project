namespace PlayZone.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PlayZone.Data.Models;

    public class ChanelsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Chanels.Any())
            {
                return;
            }

            var videos = dbContext.Videos.ToList();
            var user = dbContext.Users.FirstOrDefault();
            var chanel = new Chanel
            {
                Title = "EnduroVideoChanel",
                UserId = user.Id,
                Description = "This is Chanel with Enduro Content!",
                Videos = videos,
            };

            await dbContext.Chanels.AddAsync(chanel);
            user.ChanelId = chanel.Id;
        }
    }
}
