using System.Reflection;
using MediatR;
using NerdStore.Catalog.Application.AutoMapper;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data.Repository;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Bus;
using NerdStore.Core.Data;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Domain;
using NerdStore.Sales.Infrastructure.Repository;

namespace NerdStore.Catalog.API;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddSingleton(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                   throw new ApplicationException("The connection string is null");

            return new SqlConnectionFactory(connectionString);
        });

        services.AddAutoMapper(typeof(DomainToDtoMappingProfile), typeof(DtoToDomainMappingProfile));
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        //Domain Bus (Mediator)
        services.AddTransient<IMediatorHandler, MediatrHandler>();
        
        // Notifications
        
        // Catalog
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<IStockService, StockService>();

        services.AddScoped<INotificationHandler<ProductOutOfStockEvent>, ProductEventHandler>();
        
        services.AddScoped<IRequestHandler<AddItemToOrderCommand, bool>, OrderCommandHandler>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}