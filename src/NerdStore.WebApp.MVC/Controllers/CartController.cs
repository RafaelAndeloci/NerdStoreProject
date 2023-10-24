using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Core.Bus;
using NerdStore.Sales.Application.Commands;

namespace NerdStore.WebApp.MVC.Controllers;

public class CartController : ControllerBase
{
    private readonly IProductAppService _productAppService;
    private readonly IMediatorHandler _mediatorHandler;

    public CartController(
        IProductAppService productAppService,
        IMediatorHandler mediatorHandler)
    {
        _productAppService = productAppService;
        _mediatorHandler = mediatorHandler;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("meu-carrinho")]
    public async Task<IActionResult> AddItem(Guid id, int quantity)
    {
        var product = await _productAppService.GetByIdAsync(id);
        if (product is null) return BadRequest();

        if (product.StockQuantity < quantity)
        {
            TempData["Erro"] = "Produto com estoque insuficiente";
            return RedirectToAction("ProductDetails", "ShopWindow", new { id });
        }

        var command = new AddItemToOrderCommand(ClientId, product.Id, product.Name, quantity, product.Value);

        await _mediatorHandler.SendCommand(command);
        
        
        TempData["Erro"] = "Produto indisponível";
        return RedirectToAction("ProductDetails", "ShopWindow", new { id });
    }
}