using AutoMapper;
using Service1.Application.DTOs;
using Service1.Domain.Entities;

namespace Service1.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}