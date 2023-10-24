using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Application.Services;

namespace NerdStore.WebApp.MVC.Controllers;

public class AdminProductsController : Controller
{
    private readonly IProductAppService _productAppService;

    public AdminProductsController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _productAppService.GetAllAsync());
    }

    public async Task<IActionResult> NewProduct()
    {
        return View(await FillCategories(new ProductDto()));
    }

    [HttpPost]
    public async Task<IActionResult> NewProduct(ProductDto productDto)
    {
        if (!ModelState.IsValid) return View(await FillCategories(productDto));

        await _productAppService.AddProductAsync(productDto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(Guid id)
    {
        return View(await FillCategories(await _productAppService.GetByIdAsync(id)));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(Guid id, ProductDto productDto)
    {
        var product = await _productAppService.GetByIdAsync(id);
        
        productDto.StockQuantity = product.StockQuantity;

        ModelState.Remove("StockQuantity");
        if (!ModelState.IsValid) return View(await FillCategories(productDto));

        await _productAppService.UpdateProductAsync(productDto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> UpdateStock(Guid id)
    {
        return View("Stock", await _productAppService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStock(Guid id, int quantity)
    {
        if (quantity > 0) await _productAppService.ReplenishStockAsync(id, quantity);
        
        else await _productAppService.DebitStockAsync(id, quantity);

        return View("Index", await _productAppService.GetAllAsync());
    }

    private async Task<ProductDto> FillCategories(ProductDto productDto)
    {
        productDto.Categories = await _productAppService.GetCategoriesAsync();
        return productDto;
    }
}