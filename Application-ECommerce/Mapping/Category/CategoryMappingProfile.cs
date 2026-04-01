using Application_ECommerce.App.Categories.Dtos;
using Application_ECommerce.Models.Category;
using AutoMapper;

namespace Application_ECommerce.Mapping.Category
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // Mapping pour CategoryViewModel <-> CategoryDto
            CreateMap<CategoryViewModel, CategoryDto>().ReverseMap();

            CreateMap<DeleteCategoryViewModel, CategoryDto>().ReverseMap();

            CreateMap<CreateCategoryViewModel, CreateCategoryDto>();
            CreateMap<EditCatgoryViewModel, UpdateCategoryDto>().ReverseMap();
        }
    }
}
