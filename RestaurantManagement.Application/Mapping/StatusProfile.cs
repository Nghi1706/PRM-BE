using AutoMapper;

using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Application.DTOs;
public class StatusProfile : Profile
{
    public StatusProfile()
    {
        CreateMap<Status, StatusDto>();
        CreateMap<CreateStatusDto, Status>();
        CreateMap<UpdateStatusDto, Status>();
    }
}
