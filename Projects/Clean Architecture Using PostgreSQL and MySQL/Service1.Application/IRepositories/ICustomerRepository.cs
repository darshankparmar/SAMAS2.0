using Service1.Domain.Entities;

namespace Service1.Application.IRepositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(int id);
        Task<List<Customer>> GetAllAsync();
        Task<string> AddAsync(Customer customer);
        Task<string> UpdateAsync(Customer customer);
        Task<string> DeleteAsync(int id);
    }
}