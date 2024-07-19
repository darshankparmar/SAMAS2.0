using Microsoft.EntityFrameworkCore;
using Service1.Application.IRepositories;
using Service1.Domain.Entities;
using Service1.Infrastructure.Data;

namespace Service1.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContextFactory _contextFactory;

        public ProductRepository(IDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var _dbContext = _contextFactory.ProductDbContext;
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Products.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var _dbContext = _contextFactory.ProductDbContext;
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<string> AddAsync(Product product)
        {
            try
            {
            var _dbContext = _contextFactory.ProductDbContext;
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return "Product added successfully.";
            }
            catch (Exception ex)
            {
                return $"Failed to add product: {ex.Message}";
            }
        }

        public async Task<string> UpdateAsync(Product product)
        {
            try
            {
            var _dbContext = _contextFactory.ProductDbContext;

            var existingProduct = _dbContext.Products.Find(product.Id);
            if (existingProduct != null)
            {
                _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Product not found");
            }
            return "Product updated successfully.";
            }
            catch (Exception ex)
            {
                return $"Failed to update product: {ex.Message}";
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                var _dbContext = _contextFactory.ProductDbContext;
                var product = await _dbContext.Products.FindAsync(id);
                if (product != null)
                {
                    _dbContext.Products.Remove(product);
                    await _dbContext.SaveChangesAsync();
                    return "Product deleted successfully.";
                }
                else
                {
                    return "Product not found.";
                }
            }
            catch (Exception ex)
            {
                return $"Failed to delete product: {ex.Message}";
            }
        }
    }
}