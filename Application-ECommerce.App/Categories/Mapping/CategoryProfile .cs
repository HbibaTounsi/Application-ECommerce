using Application_ECommerce.App.Categories.Dtos;
using AutoMapper;
using Application_ECommerce.Core.Entities.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Categories.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Mapping between entity and DTO
            CreateMap<Category, CategoryDto>().ReverseMap();

            // Mapping for creation operation
            CreateMap<CreateCategoryDto, Category>();

            // Mapping for update operation
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
