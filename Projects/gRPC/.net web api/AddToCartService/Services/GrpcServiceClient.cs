using Grpc.Net.Client;
using InventoryService.Protos;

namespace AddToCartService.Services;

public class GrpcServiceClient
{
    private readonly Inventory.InventoryClient _client;

    public GrpcServiceClient(GrpcChannel channel)
    {
        _client = new Inventory.InventoryClient(channel);
    }

    public async Task<ProductList> GetProductListAsync()
    {
        return await _client.GetProductListAsync(new Empty());
    }
}
