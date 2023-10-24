using NerdStore.Core.Data;

namespace NerdStore.Sales.Domain;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<IEnumerable<Order>> OrdersByClientId(Guid clientId);
    Task<Order?> GetSketchOrderByClientId(Guid clientId);
    void Add(Order order);
    void Update(Order order);
    
    Task<OrderItem> GetItemById(Guid id);
    Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId);
    void AddItem(OrderItem orderItem);
    void UpdateItem(OrderItem orderItem);
    void RemoveItem(OrderItem orderItem);

    Task<Voucher> GetVoucherByCode(string code);
}