using Dapper;
using NerdStore.Core.Data;
using NerdStore.Sales.Domain;
using NerdStore.Sales.Infrastructure.QueriesAndCommands;

namespace NerdStore.Sales.Infrastructure.Repository;

public class OrderRepository : IOrderRepository
{
    private SqlConnectionFactory SqlConnectionFactory { get; }


    public OrderRepository(SqlConnectionFactory sqlConnectionFactory)
    {
        SqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async Task<IEnumerable<Order>> GetOrdersByClientId(Guid clientId)
    {
        await using var connection = SqlConnectionFactory.CreateConnection();
        var gridReader = await connection.QueryMultipleAsync(
            sql: OrderQueries.GetOrdersByClientQuery,
            commandTimeout: 15);

        var orders = await MultipleOrderMap(gridReader);

        return orders;
    }

    public async Task<Order?> GetSketchOrderByClientId(Guid clientId)
    {
        await using var connection = SqlConnectionFactory.CreateConnection();
        var gridReader = await connection.QueryMultipleAsync(
            sql: OrderQueries.GetOrderSketchByClientIdQuery);

        var sketchOrder = await SingleOrderMap(gridReader);
        
        return sketchOrder;
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        await using var connection = SqlConnectionFactory.CreateConnection();
        var gridReader = await connection.QueryMultipleAsync(
            sql: OrderQueries.GetAllQuery,
            commandTimeout: 15);

        var orders = await MultipleOrderMap(gridReader);

        return orders;
    }

    

    public async Task<Order?> GetById(Guid id)
    {
        await using var connection = SqlConnectionFactory.CreateConnection();
        var gridReader = await connection.QueryMultipleAsync(
            sql: OrderQueries.GetByIdQuery,
            param: new { Id = id },
            commandTimeout: 15);

        var order = await SingleOrderMap(gridReader);

        return order;
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

    public Task<IEnumerable<OrderItem>> GetItemsLinkedToOrder(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Voucher?> GetVoucherLinkedToOrder(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Voucher?> GetVoucherByCode(string code)
    {
        throw new NotImplementedException();
    }
    
    private async Task ApplyVoucherAndItemsInOrders(List<Order> orders)
    {
        foreach (var order in orders)
        {
            var itemsLinkedToThisOrder = await GetItemsLinkedToOrder(order.Id);
            foreach (var item in itemsLinkedToThisOrder) order.AddItem(item);

            var voucherLinkedToThisOrder = await GetVoucherLinkedToOrder(order.Id);
            if (voucherLinkedToThisOrder is not null) order.ApplyVoucher(voucherLinkedToThisOrder);
        }
    }
    
    private async Task<IEnumerable<Order>> MultipleOrderMap(SqlMapper.GridReader gridReader)
    {
        await using var reader = gridReader;

        var orders = await reader.ReadAsync<Order>();
        var ordersList = orders.ToList();
        
        await ApplyVoucherAndItemsInOrders(ordersList);

        return ordersList;
    }

    private static async Task<Order?> SingleOrderMap(SqlMapper.GridReader gridReader)
    {
        await using var reader = gridReader;

        var order = await reader.ReadFirstAsync<Order>();
        var voucher = await reader.ReadFirstAsync<Voucher?>();
        var orderItems = await reader.ReadAsync<OrderItem>();

        if (voucher is not null) order.ApplyVoucher(voucher);
        
        foreach (var orderItem in orderItems) order.AddItem(orderItem);

        return order;
    }
}