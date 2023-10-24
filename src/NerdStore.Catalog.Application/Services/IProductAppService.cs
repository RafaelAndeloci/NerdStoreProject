using NerdStore.Catalog.Application.DTOs;

namespace NerdStore.Catalog.Application.Services;

public interface IProductAppService
{
    Task<IEnumerable<ProductDto>> GetByCategoryCodeAsync(int code);
    Task<ProductDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task AddProductAsync(ProductDto productDto);
    Task UpdateProductAsync(ProductDto productDto); 
    Task<ProductDto> DebitStockAsync(Guid id, int quantity);
    Task<ProductDto> ReplenishStockAsync(Guid id, int quantity);
}