using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IFormQuestionProcessService
    {
        Task<ApiResponse<FormQuestionProcessDto>> GetFormQuestionProcessAsync(int id);
        Task<ApiResponse<List<FormQuestionProcessDto>>> GetAllFormQuestionProcessesAsync();
        Task<ApiResponse<FormQuestionProcessDto>> CreateFormQuestionProcessAsync(FormQuestionProcessCreateDto formQuestionProcessCreateDto);
        Task<ApiResponse<FormQuestionProcessDto>> UpdateFormQuestionProcessAsync(FormQuestionProcessUpdateDto formQuestionProcessUpdateDto);
        Task<ApiResponse<bool>> DeleteFormQuestionProcessAsync(int id);
        Task<ApiResponse<IEnumerable<AnswerDto>>> GetAnswersByProcessIdAsync(int processId);
    }
}
