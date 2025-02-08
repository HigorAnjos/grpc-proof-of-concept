using Contracts.Protos;
using Microsoft.AspNetCore.Mvc;

namespace WebApiClient.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly OrderService.OrderServiceClient _client;

    public OrderController(OrderService.OrderServiceClient client)
    {
        _client = client;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(string id)
    {
        var response = await _client.GetOrderAsync(new OrderRequest { OrderId = id });
        return Ok(response);
    }
}
