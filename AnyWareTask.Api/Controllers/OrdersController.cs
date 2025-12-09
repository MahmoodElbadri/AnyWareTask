using AnyWareTask.Api.Data;
using AnyWareTask.Api.Dtos;
using AnyWareTask.Api.Interfaces;
using AnyWareTask.Api.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AnyWareTask.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(ApplicationDbContext _db, IMapper _mapper, ICacheService _cache, IOrderService _service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _service.GetAllOrdersAsync(); //here we are calling the service to get the orders from the database
        return Ok(orders);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _service.GetOrderByIdAsync(id); //here we are calling the service to get the order from the database
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] OrderRequestDto dto)
    {
        var order = await _service.AddOrderAsync(dto); //here we are calling the service to add the order to the database
        return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderRequestDto dto)
    {
        var order = await _service.UpdateOrderAsync(id, dto); //here we are calling the service to update the order in the database
        return Ok(order);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var isDeleted = await _service.DeleteOrderAsync(id); //here we are calling the service to delete the order from the database
        return (isDeleted) ? NoContent() : NotFound();
    }
}
