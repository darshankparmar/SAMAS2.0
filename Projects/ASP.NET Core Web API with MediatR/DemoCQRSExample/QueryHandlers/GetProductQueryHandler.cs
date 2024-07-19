using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DemoCQRSExample.Models;
using DemoCQRSExample.Queries;
using MediatR;

namespace DemoCQRSExample.QueryHandlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>, IRequestHandler<GetProductsQuery, List<Product>>
    {
        // Define a fake data source (in-memory list)
        private readonly List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Product A", Price = 10.99m },
            new Product { Id = 2, Name = "Product B", Price = 19.99m },
            // Add more fake products as needed
        };

        public Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_products);
        }

        public Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            // Simulate retrieving a product by ID from the fake data source
            // You can take the records from database also.
        
            var product = _products.FirstOrDefault(p => p.Id == request.Id);

            if (product == null)
            {
                // If the product is not found, you can throw an exception or return null
                throw new Exception($"Product with ID {request.Id} not found.");
            }

            return Task.FromResult(product);
        }
    }
}