using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using Microsoft.AspNetCore.Mvc;

namespace FormMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Forms")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<FormDto>>>> GetAllForms()
        {
            ApiResponse<IEnumerable<FormDto>> response = await _formService.GetAllFormsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<FormDto>>> GetFormById(int id)
        {
            ApiResponse<FormDto> response = await _formService.GetFormByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<FormDto>>> CreateForm([FromBody] FormCreateDto formCreateDto)
        {
            ApiResponse<FormDto> response = await _formService.CreateFormAsync(formCreateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<FormDto>>> UpdateForm([FromBody] FormUpdateDto formUpdateDto)
        {
            ApiResponse<FormDto> response = await _formService.UpdateFormAsync(formUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteForm(int id)
        {
            ApiResponse<bool> response = await _formService.DeleteFormAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("frequent")]
        public async Task<ActionResult<ApiResponse<IEnumerable<FormDto>>>> GetFrequentForms()
        {
            ApiResponse<IEnumerable<FormDto>> response = await _formService.GetFrequentFormsAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}
