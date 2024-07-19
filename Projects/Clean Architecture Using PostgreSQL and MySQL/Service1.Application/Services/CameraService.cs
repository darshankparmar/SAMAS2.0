using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service1.Application.IRepositories;
using Service1.Application.IServices;
using Service1.Domain.Entities;

namespace Service1.Application.Services
{
    public class CameraService : ICameraService
    {
        private readonly ICameraRepository _CameraRepository;

        public CameraService(ICameraRepository CameraRepository)
        {
            _CameraRepository = CameraRepository;
        }

        public async Task<Camera> GetCameraByIdAsync(int id)
        {
            return await _CameraRepository.GetByIdAsync(id);
        }

        public async Task<List<Camera>> GetAllCamerasAsync()
        {
            return await _CameraRepository.GetAllAsync();
        }

        public async Task<string> AddCameraAsync(Camera Camera)
        {
            return await _CameraRepository.AddAsync(Camera);
        }

        public async Task<string> UpdateCameraAsync(Camera Camera)
        {
            return await _CameraRepository.UpdateAsync(Camera);
        }

        public async Task<string> DeleteCameraAsync(int id)
        {
            return await _CameraRepository.DeleteAsync(id);
        }
    }
}