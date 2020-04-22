namespace PlayZone.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PlayZone.Data.Models;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var user = new ApplicationUser
            {
                UserName = "v_galabow@abv.bg",
                PasswordHash = "123456",
                EmailConfirmed = true,
                Email = "v_galabow@abv.bg",
                ChannelId = null,
            };

            await dbContext.Users.AddAsync(user);
        }
    }
}
