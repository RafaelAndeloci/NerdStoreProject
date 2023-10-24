using NerdStore.Core.Data;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Infrastructure.Repository;

public class OrderRepository : IOrderRepository
{
    public SqlConnectionFactory SqlConnectionFactory { get; }


    public OrderRepository(SqlConnectionFactory sqlConnectionFactory)
    {
        SqlConnectionFactory = sqlConnectionFactory;
    }

    public Task<Order> GetOrderByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> OrdersByClientId(Guid clientId)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetSketchOrderByClientId(Guid clientId)
    {
        throw new NotImplementedException();
    }

    public void Add(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Create(Order entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Order order)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid entity)
    {
        throw new NotImplementedException();
    }

    public Task<OrderItem> GetItemById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId)
    {
        throw new NotImplementedException();
    }

    public void AddItem(OrderItem orderItem)
    {
        throw new NotImplementedException();
    }

    public void UpdateItem(OrderItem orderItem)
    {
        throw new NotImplementedException();
    }

    public void RemoveItem(OrderItem orderItem)
    {
        throw new NotImplementedException();
    }

    public Task<Voucher> GetVoucherByCode(string code)
    {
        throw new NotImplementedException();
    }


}