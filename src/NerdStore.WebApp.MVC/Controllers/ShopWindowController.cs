using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;

namespace NerdStore.WebApp.MVC.Controllers;

public class ShopWindowController : Controller
{
    private readonly IProductAppService _productAppService;

    public ShopWindowController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }

    [HttpGet]
    [Route("vitrine")]
    public async Task<IActionResult> Index()
    {
        return View(await _productAppService.GetAllAsync());
    }

    [HttpGet]
    [Route("produto-detalhe/{id}")]
    public async Task<IActionResult> ProductDetails(Guid id)
    {
        return View(await _productAppService.GetByIdAsync(id));
    }
}