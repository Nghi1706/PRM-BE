using AutoMapper;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Domain.Entities;
public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<CreateRestaurantDto, Restaurant>();
        CreateMap<UpdateRestaurantDto, Restaurant>();
    }
}
