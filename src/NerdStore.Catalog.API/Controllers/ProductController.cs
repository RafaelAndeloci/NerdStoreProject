using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IProductAppService _productAppService;

    public ProductController(IProductRepository productRepository, IMapper mapper, IProductAppService productAppService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _productAppService = productAppService;
    }

    [HttpGet]
    [Route("GetAllProducts")]
    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        return await _productAppService.GetAllAsync();
    }

    [HttpGet]
    [Route("GetAllProductsByCategoryCode")]
    public async Task<IEnumerable<ProductDto>> GetAllProductsByCategoryCode(int code)
    {
        return await _productAppService.GetByCategoryCodeAsync(code);
    }

    [HttpGet]
    [Route("GetProductById")]
    public async Task<ProductDto?> GetProductById(Guid id)
    {
        return await _productAppService.GetByIdAsync(id);
    }

    [HttpPost]
    [Route("Create")]
    public void Create(ProductDto productDto)
    {
        _productRepository.Create(_mapper.Map<Product>(productDto));
    }

    [HttpPut]
    [Route("Update")]
    public void Update(ProductDto productDto)
    {
        _productRepository.Update(_mapper.Map<Product>(productDto));
    }

    [HttpPost]
    [Route("DebitStock")]
    public async Task DebitStock(Guid idProduct, int quantity)
    {
        await _productAppService.DebitStockAsync(idProduct, quantity);
    }

    [HttpPost]
    [Route("ReplenishStock")]
    public async Task ReplenishStock(Guid idProduct, int quantity)
    {
        await _productAppService.ReplenishStockAsync(idProduct, quantity);
    }

    [HttpDelete]
    [Route("Delete")]
    public void Delete(Guid id)
    {
        _productRepository.Delete(id);
    }
}