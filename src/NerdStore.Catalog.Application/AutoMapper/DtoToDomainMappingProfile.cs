using AutoMapper;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Application.AutoMapper;

public class DtoToDomainMappingProfile : Profile
{
    public DtoToDomainMappingProfile()
    {
        CreateMap<ProductDto, Product>()
            .ConstructUsing(p => Product.Create(
                p.Name, p.Description, p.Active,
                p.Value, p.CategoryId, p.RegisterDate,
                p.Image, new Dimensions(p.Height, p.Width, p.Depth)));
        
        CreateMap<CategoryDto, Category>()
            .ConstructUsing(c => new Category(c.Name, c.Code));
    }
}