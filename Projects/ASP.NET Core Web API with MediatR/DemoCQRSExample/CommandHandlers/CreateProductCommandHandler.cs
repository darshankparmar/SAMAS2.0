using DemoCQRSExample.Commands;
using DemoCQRSExample.Models;
using MediatR;

namespace DemoCQRSExample.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        // Define a fake data source (in-memory list)
        private readonly List<Product> _products = new List<Product>();
        private int _nextProductId = 1;

        public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Simulate creating a new product
            var product = new Product
            {
                Id = _nextProductId++,
                Name = request.Name,
                Price = request.Price
            };

            // Add the product to the fake data source
            _products.Add(product);

            // Return the newly created product's ID
            return Task.FromResult(product.Id);
        }
    }
}