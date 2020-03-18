namespace PlayZone.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PlayZone.Data.Common.Repositories;
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<T> GetAllCategories<T>()
        {
            return this.categoryRepository.All().OrderBy(x => x.Name).To<T>().ToList();
        }
    }
}
