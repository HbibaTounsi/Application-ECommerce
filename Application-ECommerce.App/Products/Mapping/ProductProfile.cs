using Application_ECommerce.App.Products.Dtos;
using Application_ECommerce.Core.Entities.Product;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Products.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Mapping de Product vers ProductDto
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.ImageFile, opt => opt.Ignore());

            // Mapping CreateProductDto -> Product
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryID));

            // Mapping UpdateProductDto -> Product
            CreateMap<UpdateProductDto, Product>();
        }
    }
}