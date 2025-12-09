using AnyWareTask.Api.Dtos;

namespace AnyWareTask.Api.Interfaces;

public interface IOrderService
{
    public Task<List<OrderDto>> GetAllOrdersAsync();
    public Task<OrderDto> GetOrderByIdAsync(Guid id);
    public Task<OrderDto> AddOrderAsync(OrderRequestDto dto);
    public Task<OrderDto> UpdateOrderAsync(Guid id, OrderRequestDto dto);
    public Task<bool> DeleteOrderAsync(Guid id);
}
