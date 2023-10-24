using NerdStore.Core.Data;

namespace NerdStore.Catalog.Domain;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryCode(int categoryCode);
    Task<IEnumerable<Category>> GetCategories();
    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(Guid categoryId);
}