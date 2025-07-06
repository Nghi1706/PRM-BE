using AutoMapper;

using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Application.DTOs;
public class StatusCategoryProfile : Profile
{
    public StatusCategoryProfile()
    {
        CreateMap<StatusCategory, StatusCategoryDto>();
        CreateMap<CreateStatusCategoryDto, StatusCategory>();
        CreateMap<UpdateStatusCategoryDto, StatusCategory>();
    }
}
