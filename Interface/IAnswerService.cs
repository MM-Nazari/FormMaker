using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IAnswerService
    {
        Task<ApiResponse<IEnumerable<AnswerDto>>> GetAllAnswersAsync();
        Task<ApiResponse<AnswerDto>> GetAnswerByIdAsync(int id);
        Task<ApiResponse<AnswerDto>> CreateAnswerAsync(AnswerDto answerDto);
        Task<ApiResponse<AnswerDto>> UpdateAnswerAsync(int id, AnswerDto answerDto);
        Task<ApiResponse<bool>> DeleteAnswerAsync(int id);
    }
}
