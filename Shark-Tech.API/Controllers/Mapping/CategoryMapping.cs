using AutoMapper;
using Shark_Tech.BL;
using Shark_Tech.DAL;

namespace Shark_Tech.API;

public class CategoryMapping: Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<UpdateCategoryDTO, Category>().ReverseMap();

    }
}
