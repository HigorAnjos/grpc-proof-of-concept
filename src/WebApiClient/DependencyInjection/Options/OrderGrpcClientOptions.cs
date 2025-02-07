using System.ComponentModel.DataAnnotations;

namespace WebApiClient.DependencyInjection.Options
{
    public record OrderGrpcClientOptions
    {
        [Required, Url] public required string BaseAddress { get; init; }
    }
}
