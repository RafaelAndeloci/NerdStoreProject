using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain;

public class OrderItem : Entity
{
    public OrderItem(Guid productId, string productName, int quantity, decimal unitaryValue)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitaryValue = unitaryValue;
    }

    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitaryValue { get; private set; }
    
    //EF Relation
    public Order Order { get; set; }

    internal void LinkOrder(Guid orderId) => OrderId = orderId;
    public decimal CalculateTotal() => Quantity * UnitaryValue;
    internal void AddUnities(int unity) => Quantity += unity;
    internal void UpdateUnities(int unities) => Quantity = unities;

    protected OrderItem() { }

    public override bool IsValid()
    {
        return true;
    }
}