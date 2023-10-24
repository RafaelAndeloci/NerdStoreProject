using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Events;

public class ProductOutOfStockEvent : DomainEvent
{
    public int RemainingQuantity { get; set; }

    public ProductOutOfStockEvent(Guid aggregateId, int remainingQuantity) : base(aggregateId)
    {
        RemainingQuantity = remainingQuantity;
    }
    
    
    
}