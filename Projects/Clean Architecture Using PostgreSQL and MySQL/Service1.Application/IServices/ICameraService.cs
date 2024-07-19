using Service1.Domain.Entities;

namespace Service1.Application.IServices
{
    public interface ICameraService
    {
        Task<Camera> GetCameraByIdAsync(int id);
        Task<List<Camera>> GetAllCamerasAsync();
        Task<string> AddCameraAsync(Camera Camera);
        Task<string> UpdateCameraAsync(Camera Camera);
        Task<string> DeleteCameraAsync(int id);
    }
}