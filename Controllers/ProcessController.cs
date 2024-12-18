using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using Microsoft.AspNetCore.Mvc;

namespace FormMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Processes")]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessService _processService;

        public ProcessController(IProcessService processService)
        {
            _processService = processService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProcessDto>>> CreateProcess([FromBody] ProcessCreateDto processCreateDto)
        {
            ApiResponse<ProcessDto> response = await _processService.CreateProcessAsync(processCreateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProcessDto>>> GetProcessById(int id)
        {
            ApiResponse<ProcessDto> response = await _processService.GetProcessByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ProcessDto>>>> GetAllProcesses()
        {
            ApiResponse<List<ProcessDto>> response = await _processService.GetAllProcessesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<ProcessDto>>> UpdateProcess([FromBody] ProcessUpdateDto processUpdateDto)
        {
            ApiResponse<ProcessDto> response = await _processService.UpdateProcessAsync(processUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProcess(int id)
        {
            ApiResponse<bool> response = await _processService.DeleteProcessAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
