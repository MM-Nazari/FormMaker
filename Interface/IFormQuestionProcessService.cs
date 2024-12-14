using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IFormQuestionProcessService
    {
        Task<ApiResponse<FormQuestionProcessDto>> GetFormQuestionProcessAsync(int id);
        Task<ApiResponse<List<FormQuestionProcessDto>>> GetAllFormQuestionProcessesAsync();
        Task<ApiResponse<FormQuestionProcessDto>> CreateFormQuestionProcessAsync(FormQuestionProcessDto formQuestionProcessDto);
        Task<ApiResponse<FormQuestionProcessDto>> UpdateFormQuestionProcessAsync(int id, FormQuestionProcessDto formQuestionProcessDto);
        Task<ApiResponse<bool>> DeleteFormQuestionProcessAsync(int id);
    }
}
