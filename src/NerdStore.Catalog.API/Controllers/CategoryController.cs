using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IProductAppService _productAppService;
    private readonly IMapper _mapper;

    public CategoryController(IProductRepository productRepository, IProductAppService productAppService, IMapper mapper)
    {
        _productRepository = productRepository;
        _productAppService = productAppService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        return await _productAppService.GetCategoriesAsync();
    }

    [HttpPost]
    [Route("Create")]
    public void Create(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        _productRepository.CreateCategory(category);
    }

    [HttpPut]
    [Route("Update")]
    public void Update(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        _productRepository.UpdateCategory(category);
    }

    [HttpDelete]
    [Route("Delete")]
    public void Delete(Guid id)
    {
        _productRepository.DeleteCategory(id);
    }
}