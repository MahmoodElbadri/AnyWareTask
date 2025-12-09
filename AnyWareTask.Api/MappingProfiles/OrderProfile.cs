using AnyWareTask.Api.Dtos;
using AnyWareTask.Api.Models;
using AutoMapper;

namespace AnyWareTask.Api.MappingProfiles;

public class OrderProfile:Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<OrderRequestDto, Order>();
    }
}
