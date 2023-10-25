using NerdStore.Core.Data;

namespace NerdStore.Sales.Domain;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByClientId(Guid clientId);
    Task<Order?> GetSketchOrderByClientId(Guid clientId);

    Task<OrderItem> GetItemById(Guid id);
    Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId);
    void AddItem(OrderItem orderItem);
    void UpdateItem(OrderItem orderItem);
    void RemoveItem(OrderItem orderItem);

    Task<IEnumerable<OrderItem>> GetItemsLinkedToOrder(Guid orderId);
    Task<Voucher?> GetVoucherLinkedToOrder(Guid orderId);
    Task<Voucher?> GetVoucherByCode(string code);
}