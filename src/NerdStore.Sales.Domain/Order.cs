using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain;

public class Order : Entity, IAggregateRoot
{
    private readonly List<OrderItem> _orderItems;

    public Order(Guid clientId, bool usingVoucher, decimal discount, decimal totalValue)
    {
        ClientId = clientId;
        UsingVoucher = usingVoucher;
        Discount = discount;
        TotalValue = totalValue;
        _orderItems = new();
    }

    public int Code { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool UsingVoucher { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalValue { get; private set; }
    public DateTime RegisterDate { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    //EF Relation
    public Voucher Voucher { get; private set; }
    
    public void AddItem(OrderItem item)
    {
        if (!item.IsValid()) return;
        
        item.LinkOrder(Id);

        if (OrderItemAlreadyExists(item))
        {
            var existingItem = _orderItems.First(p => p.ProductId == item.ProductId);
            
            existingItem.AddUnities(item.Quantity);
            
            item=existingItem;
            
            _orderItems.Remove(existingItem);
        }
        item.CalculateTotal();
        _orderItems.Add(item);
        
        CalculateOrderValue();
    }
    public void UpdateItem(OrderItem item)
    {
        if (!item.IsValid()) return;
        item.LinkOrder(Id);

        var existentItem = OrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

        if (existentItem == null) throw new DomainException("O item não pertence ao pedido");

        _orderItems.Remove(existentItem);
        _orderItems.Add(item);
        
        CalculateOrderValue();
    }
    public void RemoveItem(OrderItem item)
    {
        if (!item.IsValid()) return;

        var existentItem = OrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

        if (existentItem == null) throw new DomainException("O Item não pertence ao pedido");
        _orderItems.Remove(existentItem);

        CalculateOrderValue();
    }
    public void UpdateUnities(OrderItem item, int unities)
    {
        item.UpdateUnities(unities);
        UpdateItem(item);
    }
    public bool OrderItemAlreadyExists(OrderItem orderItem) => _orderItems.Any(p => p.ProductId == orderItem.Id);
    public void ApplyVoucher(Voucher voucher)
    {
        Voucher = voucher;
        UsingVoucher = true;
        CalculateOrderValue();
    }
    public void CalculateOrderValue()
    {
        TotalValue = OrderItems.Sum(p => p.CalculateTotal());
        CalculateTotalDiscountValue();
    }
    public void CalculateTotalDiscountValue()
    {
        if(!UsingVoucher) return;
        decimal discount = 0;
        var totalValue = TotalValue;

        if (Voucher.VoucherDiscountType == VoucherDiscountType.Percentage)
        {
            if (Voucher.Percentage.HasValue)
            {
                discount = (totalValue * Voucher.Percentage.Value) / 100;
                totalValue -= discount;
            }
        }
        else
        {
            if (Voucher.DiscountValue.HasValue)
            {
                discount = Voucher.DiscountValue.Value;
                totalValue -= discount;
            }
        }

        TotalValue = totalValue < 0 ? 0 : totalValue;
        Discount = discount;
    }

    public void TurnSketch() => OrderStatus = OrderStatus.Sketch;
    public void StartOrder() => OrderStatus = OrderStatus.Started;
    public void FinishOrder() => OrderStatus = OrderStatus.Paid;
    public void CancelOrder() => OrderStatus = OrderStatus.Canceled;
    
    public static class OrderFactory
    {
        public static Order NewOrderSketch(Guid clientId)
        {
            return new Order
            {
                ClientId = clientId,
                OrderStatus = OrderStatus.Sketch
            };
        }
    }
    
    protected Order() => _orderItems = new();
    
}