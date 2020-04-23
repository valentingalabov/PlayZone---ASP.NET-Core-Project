namespace PlayZone.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PlayZone.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categoriesToCreate = new List<string>()
            {
                "Funny", "Sport",
                " Hobby", "Motosport",
                "Movie", "Music", "Inspiration",
                "Education",
            };

            foreach (var category in categoriesToCreate)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = category,
                    Description = category,
                });
            }
        }
    }
}
