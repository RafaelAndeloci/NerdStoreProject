using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain;

public class Voucher : Entity
{
    public string Code { get; private set; }
    public decimal? Percentage { get; private set; }
    public decimal? DiscountValue { get; private set; }
    public int Quantity { get; private set; }
    public VoucherDiscountType VoucherDiscountType { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? UseDate { get; private set; }
    public DateTime ExpireDate { get; private set; }
    public bool Active { get; private set; }
    public bool Used { get; private set; }
    
    //EF Relation
    public ICollection<Order> Orders { get; set; }
}