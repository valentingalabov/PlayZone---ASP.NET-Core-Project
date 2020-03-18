namespace PlayZone.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAllCategories<T>();
    }
}
