using Application_ECommerce.App.Products.Dtos;
using Application_ECommerce.Models.Home;
using AutoMapper;

namespace Application_ECommerce.Mapping.Home
{
    public class HomeMappingProfile :Profile 
    {
        public HomeMappingProfile()
        {
            CreateMap<HomeProductViewModel, ProductDto>().ReverseMap();
        }
    }
}