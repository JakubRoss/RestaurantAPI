using AutoMapper;
using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(r => r.City, d => d.MapFrom(s => s.Adress.City))
                .ForMember(r => r.Street, d => d.MapFrom(s => s.Adress.Street))
                .ForMember(r => r.PostalCode, d => d.MapFrom(s => s.Adress.PostalCode));
            CreateMap<Dish, DishDto>();

        }
    }
}