using MediatR;
using NerdStore.Core.Messages;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Application.Commands;

public class OrderCommandHandler : IRequestHandler<AddItemToOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public OrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(AddItemToOrderCommand message, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(message)) return false;

        var order = await _orderRepository.GetSketchOrderByClientId(message.ClientId);
        var orderItem = new OrderItem(message.ProductId, message.Name, message.Quantity, message.UnitaryValue);

        if (order is null)
        {
            order = Order.OrderFactory.NewOrderSketch(message.ClientId);
            order.AddItem(orderItem);
            
            _orderRepository.Add(order);
        }
        else
        {
            var existentOrderItem = order.OrderItemAlreadyExists(orderItem);
            order.AddItem(orderItem);

            if (existentOrderItem)
            {
                _orderRepository.UpdateItem(order.OrderItems.First(p => p.ProductId == orderItem.ProductId));
            }
            else
            {
                _orderRepository.AddItem(orderItem);
            }
        }

        return true;
    }

    private bool ValidateCommand(Command message)
    {
        if (message.IsValid()) return true;

        foreach (var error in message.ValidationResult.Errors)
        {
            //Lançar um evento de erro
        }
        
        return false;
    }
}