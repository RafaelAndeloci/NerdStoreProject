using Dapper;
using NerdStore.Catalog.Data.QueriesAndCommands;
using NerdStore.Catalog.Domain;
using NerdStore.Core.Data;

namespace NerdStore.Catalog.Data.Repository;

public class ProductRepository : IProductRepository
{
    private SqlConnectionFactory SqlConnectionFactory { get; }

    public ProductRepository(SqlConnectionFactory sqlConnectionFactory)
    {
        SqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        await using var connection = SqlConnectionFactory.CreateConnection();
        
        var products = connection.QueryAsync<Product, Dimensions, Category, Product>(
            sql: ProductRepositoryQueries.GetAllProductsQuery,
            splitOn: "Height, Id",
            map: (product, dimensions, category) =>
            {
                product.ChangeCategory(category);
                product.ChangeDimensions(dimensions);
                return product;
            },
            commandTimeout: 10);

        return await products;
    }
    
    public async Task<IEnumerable<Category>> GetCategories()
    {
        await using var connection = SqlConnectionFactory.CreateConnection();
        
        var categories = connection.QueryAsync<Category>(
            sql: ProductRepositoryQueries.GetAllCategoriesQuery,
            commandTimeout: 10);

        return await categories;
    }

    public async Task<Product?> GetById(Guid id)
    {
        await using var connection = SqlConnectionFactory.CreateConnection();

        var productsWithCategory = await connection.QueryMultipleAsync(
            sql: ProductRepositoryQueries.GetProductByIdQuery,
            param: new { Id = id },
            commandTimeout: 15);
        
        var product = await SingleProductMap(productsWithCategory);

        return product;
    }
    
    public async Task<IEnumerable<Product>> GetByCategoryCode(int categoryCode)
    {
        await using var connection = SqlConnectionFactory.CreateConnection();
        var products = await connection.QueryAsync<Product, Dimensions, Category, Product>(
            sql: ProductRepositoryQueries.GetAllProductsByCategoryCodeQuery,
            param: new { Code = categoryCode },
            commandTimeout: 15,
            splitOn: "Height, Id",
            map: (product, dimension, category) =>
            {
                product.ChangeCategory(category);
                product.ChangeDimensions(dimension);
                return product;
            });

        return products;
    }

    public void Create(Product entity)
    {
        using var connection = SqlConnectionFactory.CreateConnection();
        var parameters = GetParameters(entity);
        
        connection.Execute(
            sql: ProductRepositoryCommands.CreateProductCommand, 
            param: parameters,
            commandTimeout: 15);
    }
    
    public void CreateCategory(Category category)
    {
        using var connection = SqlConnectionFactory.CreateConnection();
        var parameters = GetParameters(category);
        connection.Execute(
            sql: ProductRepositoryCommands.CreateCategoryCommand, 
            param: parameters,
            commandTimeout: 15);

    }

    public void Update(Product entity)
    {
        using var connection = SqlConnectionFactory.CreateConnection();
        var parameters = GetParameters(entity);

        connection.Execute(
            sql: ProductRepositoryCommands.UpdateProductCommand, 
            param: parameters,
            commandTimeout: 15);
    }

    public void UpdateCategory(Category category)
    {
        using var connection = SqlConnectionFactory.CreateConnection();
        var parameters = GetParameters(category);
        
        connection.Execute(
            sql: ProductRepositoryCommands.UpdateCategoryCommand, 
            param: parameters,
            commandTimeout: 15);
    }
    
    public void Delete(Guid productId)
    {
        using var connection = SqlConnectionFactory.CreateConnection();

        connection.Execute(
            sql: ProductRepositoryCommands.DeleteProductCommand, 
            param: new { Id = productId },
            commandTimeout: 15);
    }
    
    public void DeleteCategory(Guid categoryId)
    {
        using var connection = SqlConnectionFactory.CreateConnection();
        
        connection.Execute(
            sql: ProductRepositoryCommands.DeleteCategoryCommand, 
            param: new{ Id = categoryId },
            commandTimeout: 15);
    }
    
    
    private static async Task<Product?> SingleProductMap(SqlMapper.GridReader gridReader)
    {
        await using var reader = gridReader;

        var product = await reader.ReadFirstAsync<Product>();
        var dimensions = await reader.ReadFirstAsync<Dimensions>();
        product.ChangeDimensions(dimensions);
        product.ChangeCategory(await reader.ReadFirstAsync<Category>());
        return product;
    }
    private static object GetParameters(Product entity)
    {
        var parameters = new
        {
            entity.Id,
            entity.CategoryId,
            entity.Name,
            entity.Description,
            entity.Active,
            entity.Value,
            entity.RegisterDate,
            entity.Image,
            entity.StockQuantity,
            entity.Dimensions.Height,
            entity.Dimensions.Width,
            entity.Dimensions.Depth
        };
        return parameters;
    }
    private static object GetParameters(Category category)
    {
        var parameters = new
        {
            category.Id,
            category.Name,
            category.Code
        };
        return parameters;
    }
}