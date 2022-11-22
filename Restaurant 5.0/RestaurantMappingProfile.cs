using AutoMapper;
using Restaurant_5._0.Entities;
using Restaurant_5._0.Models;

namespace Restaurant_5._0
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(x => x.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(x => x.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(x => x.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(x => x.Address, c=>c.MapFrom(dto=>new Address()
                {
                    City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street
                }));

            CreateMap<CreateDishDto, Dish>();
        }
    }
}
