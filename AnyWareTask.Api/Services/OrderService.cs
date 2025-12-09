using AnyWareTask.Api.Data;
using AnyWareTask.Api.Dtos;
using AnyWareTask.Api.Exceptions;
using AnyWareTask.Api.Interfaces;
using AnyWareTask.Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AnyWareTask.Api.Services;

public class OrderService(ApplicationDbContext db, IMapper mapper, ICacheService cache) : IOrderService
{
    public async Task<OrderDto> AddOrderAsync(OrderRequestDto dto)
    {
        var order = mapper.Map<Order>(dto);
        db.Orders.Add(order);
        await db.SaveChangesAsync();
        await cache.DeleteAsync("V2-ordersList"); //here we are deleting the cache cause we added a new order and the cache is not up to date
        return mapper.Map<OrderDto>(order);
    }

    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        var order = await db.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        if (order is null)
        {
            throw new NotFoundException(nameof(Order), id.ToString());
        }
        db.Orders.Remove(order);
        await db.SaveChangesAsync();
        await cache.DeleteAsync("V2-ordersList");
        await cache.DeleteAsync($"V2-order-{id}");
        return true;
    }

    public async Task<List<OrderDto>> GetAllOrdersAsync()
    {
        var cacheKey = "V2-ordersList"; //here we are creating the key for the cache
        var cacheOrders = await cache.GetAsync<List<OrderDto>>(cacheKey); //here we are getting the value from the cache
        if (cacheOrders is not null)
        {
            return (cacheOrders);
        }
        var orders = await db.Orders.ToListAsync();
        var orderDto = mapper.Map<List<OrderDto>>(orders);
        await cache.SetAsync(cacheKey, orderDto, TimeSpan.FromMinutes(5));
        return (orderDto);
    }

    public async Task<OrderDto> GetOrderByIdAsync(Guid id)
    {
        var cacheKey = $"V2-order-{id}"; //here we are creating the key for the cache
        var cacheOrder = await cache.GetAsync<OrderDto>(cacheKey); //here we are getting the value from the cache
        if (cacheOrder is not null)
        {
            return (cacheOrder); //here we are returning the value from the cache if miss it will go to the database
        }
        var order = await db.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        if (order is null)
        {
            throw new NotFoundException(nameof(Order), id.ToString());
        }
        var orderDto = mapper.Map<OrderDto>(order);
        await cache.SetAsync(cacheKey, orderDto, TimeSpan.FromMinutes(5)); //here we are setting the value in the cache to get it from the cache next time
        return (orderDto);
    }

    public async Task<OrderDto> UpdateOrderAsync(Guid id, OrderRequestDto dto)
    {
        var order = await db.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        if (order is null) {
            throw new NotFoundException(nameof(Order), id.ToString());
        }
        order.Product = dto.Product;
        order.CustomerName = dto.CustomerName;
        order.Amount = dto.Amount;
        await cache.DeleteAsync("V2-ordersList");
        await db.SaveChangesAsync();
        var orderDto = mapper.Map<OrderDto>(order);
        return (orderDto);
    }
}


