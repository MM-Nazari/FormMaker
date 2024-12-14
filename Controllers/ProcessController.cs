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
        public async Task<IActionResult> CreateProcess([FromBody] ProcessDto processDto)
        {
            var response = await _processService.CreateProcessAsync(processDto);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProcess(int id, [FromBody] ProcessDto processDto)
        {
            var response = await _processService.UpdateProcessAsync(id, processDto);
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
