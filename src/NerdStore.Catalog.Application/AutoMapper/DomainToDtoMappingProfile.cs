using AutoMapper;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Application.AutoMapper;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        
        CreateMap<Product, ProductDto>()
            //Mapping properties from Product.Dimensions
            .ForMember(d => d.Width,
                o => 
                    o.MapFrom(s => s.Dimensions.Width))
            
            .ForMember(d => d.Height,
                o => 
                    o.MapFrom(s => s.Dimensions.Height))
            
            .ForMember(d => d.Depth,
                o =>
                    o.MapFrom(s => s.Dimensions.Depth));
    }
}