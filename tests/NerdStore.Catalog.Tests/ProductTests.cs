using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void Product_Validate_ValidationsShouldReturnExceptions()
    {
        var ex = Assert.Throws<DomainException>(() =>
            Product.Create(string.Empty, "Descricao", false, 100,
                Guid.NewGuid(), DateTime.Now,
                "Imagem", new Dimensions(1, 1, 1)));
        
        Assert.Equal("O campo Nome do Produto não pode estar vazio.", ex.Message);

        ex = Assert.Throws<DomainException>(() => 
                Product.Create("Nome", string.Empty, false, 100,
                    Guid.NewGuid(), DateTime.Now, "Imagem",
                    new Dimensions(1, 1, 1)));
        
        Assert.Equal("O campo Descricao do Produto não pode estar vazio", ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            Product.Create("Nome", "Descricao", false, 0,
                Guid.NewGuid(), DateTime.Now, "Imagem",
                new Dimensions(1, 1,1)));
        
        Assert.Equal("O campo Valor do Produto não pode ser menor ou igual a 0", ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            Product.Create("Nome", "Descricao", false, 100,
                Guid.Empty, DateTime.Now, "Imagem",
                new Dimensions(1, 1, 1)));
        
        Assert.Equal("O campo CategoriaId do Produto não pode estar vazio", ex.Message);
        
        ex = Assert.Throws<DomainException>(() =>
            Product.Create("Nome", "Descricao", false, 100,
                Guid.NewGuid(), DateTime.Now, "Imagem",
                new Dimensions(0, 1, 1)));
        
        Assert.Equal("O campo Altura não pode ser menor ou igual a 0", ex.Message);
    }
}