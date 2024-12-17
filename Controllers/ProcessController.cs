using FormMaker.Dto;
using FormMaker.Interface;
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
        public async Task<IActionResult> CreateProcess([FromBody] ProcessCreateDto processCreateDto)
        {
            var response = await _processService.CreateProcessAsync(processCreateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcessById(int id)
        {
            var response = await _processService.GetProcessByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProcesses()
        {
            var response = await _processService.GetAllProcessesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProcess([FromBody] ProcessUpdateDto processUpdateDto)
        {
            var response = await _processService.UpdateProcessAsync(processUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcess(int id)
        {
            var response = await _processService.DeleteProcessAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
