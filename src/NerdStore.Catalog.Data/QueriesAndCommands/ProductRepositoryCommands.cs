namespace NerdStore.Catalog.Data.QueriesAndCommands;

public static class ProductRepositoryCommands
{
    public const string CreateProductCommand = @"INSERT INTO Products
        ([Id],
         [CategoryId],
         [Name],
         [Description],
         [Active],
         [Value],
         [RegisterDate],
         [Image],
         [StockQuantity],
         [Height],
         [Width],
         [Depth])
         VALUES 
             (@Id,
              @CategoryId,
              @Name,
              @Description,
              @Active,
              @Value,
              @RegisterDate,
              @Image,
              @StockQuantity,
              @Height,
              @Width,
              @Depth)";
    
    public const string UpdateProductCommand = @"UPDATE Products
        SET
            [Id] = @Id,
            [CategoryId] = @CategoryId,
            [Name] = @Name,
            [Description] = @Description,
            [Active] = @Active,
            [Value] = @Value,
            [RegisterDate] = @RegisterDate,
            [Image] = @Image,
            [StockQuantity] = @StockQuantity,
            [Height] = @Height,
            [Width] = @Width,
            [Depth] = @Depth
        WHERE Id = @Id";
    
    public const string DeleteProductCommand = @"DELETE FROM Products WHERE Id = @Id";
    
    
    public const string CreateCategoryCommand = @"INSERT INTO Categories ([Id], [Name], [Code])
            VALUES 
                (@Id,
                 @Name,
                 @Code)";
    public const string UpdateCategoryCommand = @"UPDATE [Categories]
            SET
                [Id] = @Id,
                [Name] = @Name,
                [Code] = @Code
            WHERE [Id] = @Id";
    public const string DeleteCategoryCommand = @"DELETE FROM [Categories] WHERE [Id] = @Id";
}