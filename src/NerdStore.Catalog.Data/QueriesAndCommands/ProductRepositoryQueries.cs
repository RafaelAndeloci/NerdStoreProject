namespace NerdStore.Catalog.Data.QueriesAndCommands;

public abstract class ProductRepositoryQueries
{
    public const string GetAllProductsQuery = @"
            SELECT
                p.[Id],
                p.[CategoryId],
                p.[Name],
                p.[Description],
                p.[Active],
                p.[Value],
                p.[RegisterDate],
                p.[Image],
                p.[StockQuantity],
                
                p.[Height],
                p.[Width],
                p.[Depth],
                
                c.[Id],
                c.[Name],
                c.[Code]
            FROM [Products] p
            INNER JOIN Categories c
                ON (p.CategoryId = c.Id)";

    public const string GetAllCategoriesQuery = @"
            SELECT
                [Id],
                [Name],
                [Code]
            FROM [Categories]";

    public const string GetProductByIdQuery = @"
            SELECT
                p.[Id],
                p.[CategoryId],
                p.[Name],
                p.[Description],
                p.[Active],
                p.[Value],
                p.[RegisterDate],
                p.[Image],
                p.[StockQuantity]
            FROM [Products] p
            WHERE p.[Id] = @Id;
            
            SELECT
                p.[Height],
                p.[Width],
                p.[Depth]
            FROM [Products] p
            WHERE p.[Id] = @Id;
            
            SELECT 
                c.Id,
                c.Name,
                c.Code
            FROM [Categories] c
            INNER JOIN [Products] p
                ON (c.Id = p.CategoryId)
            WHERE p.[Id] = @Id;";

    public const string GetAllProductsByCategoryCodeQuery = @"
            SELECT
                p.[Id],
                p.[CategoryId],
                p.[Name],
                p.[Description],
                p.[Active],
                p.[Value],
                p.[RegisterDate],
                p.[Image],
                p.[StockQuantity],
                p.[Height],
                p.[Width],
                p.[Depth],
                
                c.[Id],
                c.[Name],
                c.[Code]
            FROM [Products] p
            INNER JOIN Categories c
                ON (p.CategoryId = c.Id)
            WHERE c.[Code] = @Code";
}