using Contracts.Protos;
using Grpc.Net.Client;

namespace WebApiClient.GrpcClients;

public class GrpcAccountClient
{
    private readonly AccountService.AccountServiceClient _client;

    public GrpcAccountClient(IConfiguration configuration)
    {
        var channel = GrpcChannel.ForAddress(configuration["GrpcSettings:AccountServiceUrl"]);
        _client = new AccountService.AccountServiceClient(channel);
    }

    public async Task<AccountResponse> GetAccountAsync(string accountId)
    {
        return await _client.GetAccountAsync(new AccountRequest { AccountId = accountId });
    }
}
