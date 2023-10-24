using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain;

public class Product : Entity, IAggregateRoot
{
    private Product(
        string name,
        string description,
        bool active,
        decimal value,
        Guid categoryId,
        DateTime registerDate,
        string image,
        Dimensions dimensions)
    {
        
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Active = active;
        Value = value;
        RegisterDate = registerDate;
        Image = image;
        Dimensions = dimensions;
        Validate();
    }

    public static Product Create(
        string name,
        string description,
        bool active, 
        decimal value, 
        Guid categoryId, 
        DateTime registerDate, 
        string image,
        Dimensions dimensions)
    {
        return new Product(name, description, active, value, categoryId, registerDate, image, dimensions);
    }

    public Guid CategoryId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool Active { get; private set; }
    public decimal Value { get; private set; }
    public DateTime RegisterDate { get; private set; }
    public string Image { get; private set; }
    public int StockQuantity { get; private set; }
    public Dimensions Dimensions { get; private set; }
    public Category Category { get; private set; }

    public void Activate() => Active = true;
    public void Deactivate() => Active = false;

    public void ChangeDimensions(Dimensions dimensions)
    {
        Dimensions = dimensions;
    }

    public void ChangeCategory(Category category)
    {
        Category = category;
        CategoryId = category.Id;
    }

    public void ChangeDescription(string description)
    {
        AssertionConcern.ValidateIfEmpty(description, "O campo Descricao do produto não pode estar vazio");
        Description = description;
    }

    public void DebitStock(int quantity)
    {
        if (quantity < 0) quantity *= -1;
        if (!HaveStock(quantity)) throw new DomainException("Estoque Insuficiente");
        
        StockQuantity -= quantity;
    }

    public void ReplenishStock(int quantity)
    {
        StockQuantity += quantity;
    }
    
    
    public bool HaveStock(int quantity) => StockQuantity >= quantity;

    public void Validate()
    {
        AssertionConcern.ValidateIfEmpty(Name, "O campo Nome do Produto não pode estar vazio.");
        AssertionConcern.ValidateIfEmpty(Description, "O campo Descricao do Produto não pode estar vazio");
        AssertionConcern.ValidateIfEqual(CategoryId, Guid.Empty, "O campo CategoriaId do Produto não pode estar vazio");
        AssertionConcern.ValidateIfLess(Value, 1, "O campo Valor do Produto não pode ser menor ou igual a 0");
        AssertionConcern.ValidateIfEmpty(Image, "O campo Imagem do Produto não pode estar vazio");
    }

    
    /// <summary>
    /// Constructor created for data purposes only
    /// </summary>
    protected Product() { }
}