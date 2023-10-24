namespace NerdStore.Catalog.Domain;

public interface IStockService
{
    Task<bool> DebitStock(Guid productId, int quantity);
    Task<bool> ReplenishStock(Guid productId, int quantity);
}