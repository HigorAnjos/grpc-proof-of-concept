using Contracts.Protos;
using CorrelationId.HttpClient;
using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.Options;
using WebApiClient.DependencyInjection.Options;

namespace WebApiClient.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registra todos os clientes gRPC e configura as opções correspondentes.
        /// </summary>
        public static void AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
        {
            // Configura as opções dos clientes gRPC
            services.ConfigureOrderGrpcClientOptions(configuration.GetSection(nameof(OrderGrpcClientOptions)));

            // Registra os clientes gRPC
            services.AddOrderGrpcClient();
        }

        /// <summary>
        /// Registra o cliente gRPC para o serviço de pedidos (OrderService).
        /// </summary>
        public static void AddOrderGrpcClient(this IServiceCollection services)
            => services.AddGrpcClient<OrderService.OrderServiceClient, OrderGrpcClientOptions>();

        /// <summary>
        /// Configura as opções de OrderGrpcClientOptions a partir da configuração da aplicação.
        /// </summary>
        public static OptionsBuilder<OrderGrpcClientOptions> ConfigureOrderGrpcClientOptions(this IServiceCollection services, IConfigurationSection section)
            => services
                .AddOptions<OrderGrpcClientOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart();

        /// <summary>
        /// Método genérico para registrar um cliente gRPC na coleção de serviços.
        /// </summary>
        private static void AddGrpcClient<TClient, TOptions>(this IServiceCollection services)
            where TClient : ClientBase  // Restringe o tipo do cliente a derivar de ClientBase (classe base do gRPC)
            where TOptions : class  // Restringe as opções a serem uma classe
            => services.AddGrpcClient<TClient>((provider, client) =>
            {
                // Obtém as configurações do cliente gRPC a partir do serviço de configuração da aplicação.
                var options = provider.GetRequiredService<IOptionsMonitor<TOptions>>().CurrentValue as dynamic;
                client.Address = new(options.BaseAddress); // Define o endereço do servidor gRPC.
            })
                .AddCorrelationIdForwarding() // Encaminha o ID de correlação para rastreamento de requisições.
                .ConfigureChannel(options =>
                {
                    options.Credentials = ChannelCredentials.Insecure; // Define credenciais inseguras (sem TLS).
                    options.ServiceConfig = new()
                    {
                        LoadBalancingConfigs = { new RoundRobinConfig() } // Configura balanceamento de carga round-robin.
                    };
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                    new SocketsHttpHandler
                    {
                        PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan, // Conexões inativas não expiram.
                        KeepAlivePingDelay = TimeSpan.FromSeconds(60), // Intervalo entre pings de keep-alive.
                        KeepAlivePingTimeout = TimeSpan.FromSeconds(30), // Tempo limite para resposta do keep-alive.
                        EnableMultipleHttp2Connections = true // Habilita múltiplas conexões HTTP/2 para o mesmo host.
                    })
                .EnableCallContextPropagation(options
                    => options.SuppressContextNotFoundErrors = true); // Suprime erros de contexto não encontrado.
    }
}
