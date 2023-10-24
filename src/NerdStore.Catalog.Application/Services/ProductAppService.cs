using AutoMapper;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Domain;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Application.Services;

public class ProductAppService : IProductAppService
{
    private readonly IProductRepository _productRepository;
    private readonly IStockService _stockService;
    private readonly IMapper _mapper;
    
    public ProductAppService(
        IProductRepository productRepository,
        IMapper mapper,
        IStockService stockService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _stockService = stockService;
    }


    public async Task<IEnumerable<ProductDto>> GetByCategoryCodeAsync(int code)
    {
        var products = _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetByCategoryCode(code));
        return products;
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = _mapper.Map<ProductDto>(await _productRepository.GetById(id));
        return product;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAll());
        return productDtos;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(await _productRepository.GetCategories());
        return categoryDtos;
    }

    public Task AddProductAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _productRepository.Create(product);
        return Task.CompletedTask;
    }

    public Task UpdateProductAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto); 
        _productRepository.Update(product);
        return Task.CompletedTask;
    }

    public async Task<ProductDto> DebitStockAsync(Guid id, int quantity)
    {
        if (!_stockService.DebitStock(id, quantity).Result) 
            throw new DomainException("Falha ao debitar estoque");
        var productDto = _mapper.Map<ProductDto>(await _productRepository.GetById(id));
        return productDto;
    }

    public async Task<ProductDto> ReplenishStockAsync(Guid id, int quantity)
    {
        if (!_stockService.ReplenishStock(id, quantity).Result)
            throw new DomainException("Falha ao repor o estoque");
        var productDto = _mapper.Map<ProductDto>(await _productRepository.GetById(id));
        return productDto;
    }
}