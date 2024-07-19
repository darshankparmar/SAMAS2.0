using Microsoft.AspNetCore.Mvc;
using Service1.Application.DTOs;
using Service1.Application.IServices;
using Service1.Domain.Entities;

namespace Service1.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CameraController : ControllerBase
{
    private readonly ICameraService _CameraService;
    private readonly ILogger<CameraController> _logger;

    public CameraController(ICameraService CameraService, ILogger<CameraController> logger)
    {
        _CameraService = CameraService ?? throw new ArgumentNullException(nameof(CameraService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Camera>>> GetAllCameraDetails()
    {
        try
        {
            var Cameras = await _CameraService.GetAllCamerasAsync();
            return Ok(Cameras);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting all Camera details.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Camera>> GetCameraById(int id)
    {
        try
        {
            var Camera = await _CameraService.GetCameraByIdAsync(id);
            if (Camera == null)
            {
                return NotFound();
            }
            return Ok(Camera);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while getting Camera details for ID {id}.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<string>> RegisterCamera([FromBody] Camera Camera)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            string message = await _CameraService.AddCameraAsync(Camera);
            return Ok(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering the Camera.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> UpdateCamera(int id, [FromBody] Camera Camera)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != Camera.Id)
        {
            return BadRequest("ID mismatch.");
        }

        try
        {
            string message = await _CameraService.UpdateCameraAsync(Camera);
            return Ok(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating the Camera with ID {id}.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteCamera(int id)
    {
        try
        {
            string message = await _CameraService.DeleteCameraAsync(id);
            return Ok(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the Camera with ID {id}.");
            return StatusCode(500, "Internal server error");
        }
    }
}