using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System.Net;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, NamesDto>();

            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(r => r.City, d => d.MapFrom(s => s.Address.City))
                .ForMember(r => r.Street, d => d.MapFrom(s => s.Address.Street))
                .ForMember(r => r.PostalCode, d => d.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address,c => c.MapFrom(dto => new Address()
                {
                    City= dto.City,
                    Street= dto.Street,
                    PostalCode= dto.PostalCode,
                }));

        }
    }
}