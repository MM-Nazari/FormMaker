using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using Microsoft.AspNetCore.Mvc;

namespace FormMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "FormQuestions")]
    public class FormQuestionController : ControllerBase
    {
        private readonly IFormQuestionService _formQuestionService;

        public FormQuestionController(IFormQuestionService formQuestionService)
        {
            _formQuestionService = formQuestionService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<FormQuestionDto>>>> GetAllFormQuestions()
        {
            ApiResponse<IEnumerable<FormQuestionDto>> response = await _formQuestionService.GetAllFormQuestionsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<FormQuestionDto>>> GetFormQuestionById(int id)
        {
            ApiResponse<FormQuestionDto> response = await _formQuestionService.GetFormQuestionByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<FormQuestionDto>>> CreateFormQuestion([FromBody] FormQuestionCreateDto formQuestionCreateDto)
        {
            ApiResponse<FormQuestionDto> response = await _formQuestionService.CreateFormQuestionAsync(formQuestionCreateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<FormQuestionDto>>> UpdateFormQuestion([FromBody] FormQuestionUpdateDto formQuestionUpdateDto)
        {
            ApiResponse<FormQuestionDto> response = await _formQuestionService.UpdateFormQuestionAsync(formQuestionUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormQuestion(int id)
        {
            ApiResponse<bool> response = await _formQuestionService.DeleteFormQuestionAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{formId}/questions")]
        public async Task<ActionResult<ApiResponse<IEnumerable<QuestionDto>>>> GetQuestionsByFormId(int formId)
        {
            ApiResponse<IEnumerable<QuestionDto>> response = await _formQuestionService.GetQuestionsByFormIdAsync(formId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
