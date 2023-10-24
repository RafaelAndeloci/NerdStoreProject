using MediatR;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data.Repository;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Bus;
using NerdStore.Core.Data;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Domain;
using NerdStore.Sales.Infrastructure.Repository;

namespace NerdStore.WebApp.MVC.Setup;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        //Data dependencies
        services.AddSingleton(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                   throw new ApplicationException("The connection string is null");

            return new SqlConnectionFactory(connectionString);
        });

        //Domain Bus (Mediator)
        services.AddScoped<IMediatorHandler, MediatrHandler>();
        
        // Notifications
        
        // Catalog
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<IStockService, StockService>();

        services.AddScoped<INotificationHandler<ProductOutOfStockEvent>, ProductEventHandler>();
        
        //Sales
        services.AddScoped<IRequestHandler<AddItemToOrderCommand, bool>, OrderCommandHandler>();

        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}