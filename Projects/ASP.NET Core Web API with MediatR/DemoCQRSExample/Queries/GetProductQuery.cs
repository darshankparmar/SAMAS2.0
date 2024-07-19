using DemoCQRSExample.Models;
using MediatR;

namespace DemoCQRSExample.Queries
{
    public class GetProductQuery : IRequest<Product>
    {
        public int Id { get; set; }
    }

    public record GetProductsQuery() : IRequest<List<Product>>
    {
        
    }
}