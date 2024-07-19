using Microsoft.EntityFrameworkCore;
using Service1.Application.IRepositories;
using Service1.Domain.Entities;
using Service1.Infrastructure.Data;

namespace Service1.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbContextFactory _contextFactory;

        public CustomerRepository(IDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var _dbContext = _contextFactory.CustomerDbContext;
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Customers.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            var _dbContext = _contextFactory.CustomerDbContext;
            return await _dbContext.Customers.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<string> AddAsync(Customer customer)
        {
            try
            {
                var _dbContext = _contextFactory.CustomerDbContext;
                await _dbContext.Customers.AddAsync(customer);
                await _dbContext.SaveChangesAsync();
                return "Customer added successfully.";
            }
            catch (Exception ex)
            {
                return $"Failed to add customer: {ex.Message}";
            }
        }

        public async Task<string> UpdateAsync(Customer customer)
        {
            try
            {
                var _dbContext = _contextFactory.CustomerDbContext;

                var existingCustomer = _dbContext.Customers.Find(customer.Id);
                if (existingCustomer != null)
                {
                    _dbContext.Entry(existingCustomer).CurrentValues.SetValues(customer);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Customer not found");
                }
                return "Customer updated successfully.";
            }
            catch (Exception ex)
            {
                return $"Failed to update customer: {ex.Message}";
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                var _dbContext = _contextFactory.CustomerDbContext;
                var customer = await _dbContext.Customers.FindAsync(id);
                if (customer != null)
                {
                    _dbContext.Customers.Remove(customer);
                    await _dbContext.SaveChangesAsync();
                    return "Customer deleted successfully.";
                }
                else
                {
                    return "Customer not found.";
                }
            }
            catch (Exception ex)
            {
                return $"Failed to delete customer: {ex.Message}";
            }
        }
    }
}