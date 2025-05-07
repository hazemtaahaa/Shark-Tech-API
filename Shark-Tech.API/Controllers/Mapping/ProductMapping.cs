using AutoMapper;
using Shark_Tech.BL;
using Shark_Tech.DAL;

namespace Shark_Tech.API.Controllers.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
            CreateMap<ProductImage, ProductImageDTO>().ReverseMap();

        }
    }
    
}
