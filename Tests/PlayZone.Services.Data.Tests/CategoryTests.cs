namespace PlayZone.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using PlayZone.Data;
    using PlayZone.Data.Models;
    using PlayZone.Data.Repositories;
    using PlayZone.Services.Mapping;
    using PlayZone.Web.ViewModels.Videos;
    using Xunit;

    public class CategoryTests
    {
        [Fact]
        public async Task GetAllCategoriesTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(CategoryDropDownViewModel).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Category()
            {
                Name = "Music",
                Description = "music Description",
            });
    
            await repository.SaveChangesAsync();

            var service = new CategoriesService(repository);

            var categories = service.GetAllCategories<CategoryDropDownViewModel>();

            Assert.Single(categories);
        }
    }
}
