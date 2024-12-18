using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using Microsoft.AspNetCore.Mvc;

namespace FormMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Answers")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AnswerDto>>>> GetAllAnswers()
        {
            try
            {
                ApiResponse<IEnumerable<AnswerDto>> response = await _answerService.GetAllAnswersAsync();
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                ApiResponse<string> errorResponse = new ApiResponse<string>(
                    false,
                    ResponseMessage.InternalServerError,
                    ex.Message,
                    StatusCodes.Status500InternalServerError
                );
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AnswerDto>>> GetAnswerById(int id)
        {
            try
            {
                ApiResponse<AnswerDto> response = await _answerService.GetAnswerByIdAsync(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                ApiResponse<string> errorResponse = new ApiResponse<string>(
                    false,
                    ResponseMessage.InternalServerError,
                    ex.Message,
                    StatusCodes.Status500InternalServerError
                );
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<AnswerDto>>> CreateAnswer([FromBody] AnswerCreateDto answerCreateDto)
        {
            try
            {
                ApiResponse<AnswerDto> response = await _answerService.CreateAnswerAsync(answerCreateDto);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                ApiResponse<string> errorResponse = new ApiResponse<string>(
                    false,
                    ResponseMessage.InternalServerError,
                    ex.Message,
                    StatusCodes.Status500InternalServerError
                );
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<AnswerDto>>> UpdateAnswer([FromBody] AnswerUpdateDto answerUpdateDto)
        {
            try
            {
                var response = await _answerService.UpdateAnswerAsync(answerUpdateDto);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                ApiResponse<string> errorResponse = new ApiResponse<string>(
                    false,
                    ResponseMessage.InternalServerError,
                    ex.Message,
                    StatusCodes.Status500InternalServerError
                );
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAnswer(int id)
        {
            try
            {
                ApiResponse<bool> response = await _answerService.DeleteAnswerAsync(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                ApiResponse<string> errorResponse = new ApiResponse<string>(
                    false,
                    ResponseMessage.InternalServerError,
                    ex.Message,
                    StatusCodes.Status500InternalServerError
                );
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }
    }
}
