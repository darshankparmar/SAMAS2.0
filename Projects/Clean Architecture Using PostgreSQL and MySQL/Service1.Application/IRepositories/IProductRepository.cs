using Service1.Domain.Entities;

namespace Service1.Application.IRepositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<string> AddAsync(Product product);
        Task<string> UpdateAsync(Product product);
        Task<string> DeleteAsync(int id);
    }
}