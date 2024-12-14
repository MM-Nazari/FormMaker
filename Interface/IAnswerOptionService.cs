using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IAnswerOptionService
    {
        Task<ApiResponse<IEnumerable<AnswerOptionDto>>> GetAllAnswerOptionsAsync();
        Task<ApiResponse<AnswerOptionDto>> GetAnswerOptionByIdAsync(int id);
        Task<ApiResponse<AnswerOptionDto>> CreateAnswerOptionAsync(AnswerOptionDto answerOptionDto);
        Task<ApiResponse<AnswerOptionDto>> UpdateAnswerOptionAsync(int id, AnswerOptionDto answerOptionDto);
        Task<ApiResponse<bool>> DeleteAnswerOptionAsync(int id);
    }
}
