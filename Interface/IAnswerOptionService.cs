using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IAnswerOptionService
    {
        Task<ApiResponse<IEnumerable<AnswerOptionDto>>> GetAllAnswerOptionsAsync();
        Task<ApiResponse<AnswerOptionDto>> GetAnswerOptionByIdAsync(int id);
        Task<ApiResponse<AnswerOptionDto>> CreateAnswerOptionAsync(AnswerOptionCreateDto answerOptionCreateDto);
        Task<ApiResponse<AnswerOptionDto>> UpdateAnswerOptionAsync(int id, AnswerOptionUpdateDto answerOptionUpdateDto);
        Task<ApiResponse<bool>> DeleteAnswerOptionAsync(int id);
    }
}
