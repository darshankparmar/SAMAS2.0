using Grpc.Core;
using InventoryService.Protos;

namespace InventoryService.Services
{
    public class InventoryServiceImpl : Inventory.InventoryBase
    {
        public static readonly List<Product> Products = new List<Product>
        {
            new Product { Id = "1", Name = "Product A", Quantity = 10 },
            new Product { Id = "2", Name = "Product B", Quantity = 0 }
        };

        public override Task<ProductList> GetProductList(Empty request, ServerCallContext context)
        {
            var response = new ProductList();
            response.Products.AddRange(Products);
            return Task.FromResult(response);
        }
    }
}
