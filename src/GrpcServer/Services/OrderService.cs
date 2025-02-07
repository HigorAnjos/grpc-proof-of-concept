using Contracts.Protos;
using Grpc.Core;

namespace GrpcServer.Services;

public class OrderService : Contracts.Protos.OrderService.OrderServiceBase
{
    public override Task<OrderResponse> GetOrder(OrderRequest request, ServerCallContext context)
    {
        return Task.FromResult(new OrderResponse
        {
            OrderId = request.OrderId,
            ProductName = "Laptop Dell XPS",
            Quantity = 1,
            Price = 2499.99
        });
    }
}
