using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class AddItemToOrderCommand : Command
{
    public AddItemToOrderCommand(
        Guid clientId,
        Guid productId,
        string name,
        int quantity,
        decimal unitaryValue)
    {
        ClientId = clientId;
        ProductId = productId;
        Name = name;
        Quantity = quantity;
        UnitaryValue = unitaryValue;
    }

    public Guid ClientId { get; private set; }
    public Guid ProductId { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitaryValue { get; private set; }

    public override bool IsValid()
    {
        ValidationResult = new AddItemToOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class AddItemToOrderValidation : AbstractValidator<AddItemToOrderCommand>
{
    public AddItemToOrderValidation()
    {
        RuleFor(c => c.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do produto inválido");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("O nome do produto não foi informado");

        RuleFor(c => c.Quantity)
            .GreaterThan(0)
            .WithMessage("A quantidade mínima de um item deve ser maior que 0.");

        RuleFor(c => c.UnitaryValue)
            .GreaterThan(0)
            .WithMessage("O valor do item precisa ser maior que 0");
    }
}