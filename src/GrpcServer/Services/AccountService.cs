using Contracts.Protos;
using Grpc.Core;

namespace GrpcServer.Services;

public class AccountService : Contracts.Protos.AccountService.AccountServiceBase
{
    public override Task<AccountResponse> GetAccount(AccountRequest request, ServerCallContext context)
    {
        return Task.FromResult(new AccountResponse
        {
            AccountId = request.AccountId,
            Name = "John Doe",
            Email = "john.doe@example.com"
        });
    }
}
