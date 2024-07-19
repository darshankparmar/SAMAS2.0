using Service1.Domain.Entities;

namespace Service1.Application.IServices
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<string> AddCustomerAsync(Customer customer);
        Task<string> UpdateCustomerAsync(Customer customer);
        Task<string> DeleteCustomerAsync(int id);
    }
}