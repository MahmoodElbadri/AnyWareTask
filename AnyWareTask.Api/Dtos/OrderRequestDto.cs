namespace AnyWareTask.Api.Dtos;

public class OrderRequestDto
{
    public string CustomerName { get; set; }
    public string Product { get; set; }
    public decimal Amount { get; set; }
}
