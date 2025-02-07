using Microsoft.AspNetCore.Mvc;
using WebApiClient.GrpcClients;

namespace WebApiClient.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly GrpcAccountClient _grpcClient;

    public AccountController(GrpcAccountClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount(string id)
    {
        var account = await _grpcClient.GetAccountAsync(id);
        return Ok(account);
    }
}
