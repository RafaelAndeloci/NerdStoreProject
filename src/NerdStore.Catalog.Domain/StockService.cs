using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Bus;

namespace NerdStore.Catalog.Domain;

public class StockService : IStockService
{
    private readonly IProductRepository _repository;
    private readonly IMediatorHandler _bus;

    public StockService(
        IProductRepository repository,
        IMediatorHandler bus)
    {
        _repository = repository;
        _bus = bus;
    }

    public async Task<bool> DebitStock(Guid productId, int quantity)
    {
        var product = await _repository.GetById(productId);


        //Verificando se o produto é nulo ou se não há estoque o suficiente para debitar.
        if (product is null || !product.HaveStock(quantity)) return false;
        
        product.DebitStock(quantity);
        
        //Mandando para o Bus uma notificação caso o estoque do produto esteja acabando.
        if (product.StockQuantity < 10)
        {
            await _bus.PublishEvent(new ProductOutOfStockEvent(product.Id, product.StockQuantity));
        }       
        
        //Fazendo a persistencia no banco.
        _repository.Update(product);
        return true;
    }

    public async Task<bool> ReplenishStock(Guid productId, int quantity)
    {
        var product = await _repository.GetById(productId);

        if (product is null) return false;
        
        product.ReplenishStock(quantity);
        
        _repository.Update(product);
        return true;
    }
}