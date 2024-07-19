using Microsoft.EntityFrameworkCore;
using Service1.Application.IRepositories;
using Service1.Domain.Entities;
using Service1.Infrastructure.Data;

namespace Service1.Infrastructure.Repositories
{
    public class CameraRepository : ICameraRepository
    {
        private readonly IDbContextFactory _contextFactory;

        public CameraRepository(IDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Camera> GetByIdAsync(int id)
        {
            var _dbContext = _contextFactory.CameraDbContext;
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Cameras.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<Camera>> GetAllAsync()
        {
            var _dbContext = _contextFactory.CameraDbContext;
            return await _dbContext.Cameras.ToListAsync();
        }

        public async Task<string> AddAsync(Camera Camera)
        {
            try
            {
            var _dbContext = _contextFactory.CameraDbContext;
            await _dbContext.Cameras.AddAsync(Camera);
            await _dbContext.SaveChangesAsync();
            return "Camera added successfully.";
            }
            catch (Exception ex)
            {
                return $"Failed to add Camera: {ex.Message}";
            }
        }

        public async Task<string> UpdateAsync(Camera Camera)
        {
            try
            {
            var _dbContext = _contextFactory.CameraDbContext;

            var existingCamera = _dbContext.Cameras.Find(Camera.Id);
            if (existingCamera != null)
            {
                _dbContext.Entry(existingCamera).CurrentValues.SetValues(Camera);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Camera not found");
            }
            return "Camera updated successfully.";
            }
            catch (Exception ex)
            {
                return $"Failed to update Camera: {ex.Message}";
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                var _dbContext = _contextFactory.CameraDbContext;
                var Camera = await _dbContext.Cameras.FindAsync(id);
                if (Camera != null)
                {
                    _dbContext.Cameras.Remove(Camera);
                    await _dbContext.SaveChangesAsync();
                    return "Camera deleted successfully.";
                }
                else
                {
                    return "Camera not found.";
                }
            }
            catch (Exception ex)
            {
                return $"Failed to delete Camera: {ex.Message}";
            }
        }
    }
}