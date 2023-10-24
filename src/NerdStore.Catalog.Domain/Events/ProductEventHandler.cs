using MediatR;

namespace NerdStore.Catalog.Domain.Events;

public class ProductEventHandler : INotificationHandler<ProductOutOfStockEvent>
{
    private readonly IProductRepository _productRepository;

    public ProductEventHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(ProductOutOfStockEvent message, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(message.AggregateId);
        
        // Tratar o jeito como irá notificar, no caso, iremos enviar um e-mail.
        
    }
}