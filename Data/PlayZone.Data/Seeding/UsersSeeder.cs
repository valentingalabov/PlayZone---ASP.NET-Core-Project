namespace PlayZone.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
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

            var users = new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    UserName = "admin@abv.bg",
                    PasswordHash = Sha256("admin"),
                    Email = "admin@abv.bg",
                    EmailConfirmed = true,
                    ChannelId = null,
                },

                new ApplicationUser
                {
                    UserName = "demo@abv.bg",
                    PasswordHash = Sha256("demo"),
                    Email = "demo@abv.bg",
                    EmailConfirmed = true,
                    ChannelId = null,
                },

                new ApplicationUser
                {
                    UserName = "play@abv.bg",
                    PasswordHash = Sha256("play"),
                    Email = "play@abv.bg",
                    EmailConfirmed = true,
                    ChannelId = null,
                },
            };

            await dbContext.Users.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();
        }

        public static string Sha256(string value)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}
