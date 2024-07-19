using Service1.Domain.Entities;

namespace Service1.Application.IRepositories
{
    public interface ICameraRepository
    {
        Task<Camera> GetByIdAsync(int id);
        Task<List<Camera>> GetAllAsync();
        Task<string> AddAsync(Camera Camera);
        Task<string> UpdateAsync(Camera Camera);
        Task<string> DeleteAsync(int id);
    }
}