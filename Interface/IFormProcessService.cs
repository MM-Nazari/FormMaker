using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IFormProcessService
    {
        Task<ApiResponse<IEnumerable<FormProcessDto>>> GetAllFormProcessesAsync();
        Task<ApiResponse<FormProcessDto>> GetFormProcessByIdAsync(int formProcessId);
        Task<ApiResponse<FormProcessDto>> CreateFormProcessAsync(FormProcessCreateDto formProcessCreateDto);
        Task<ApiResponse<FormProcessDto>> UpdateFormProcessAsync(FormProcessUpdateDto formProcessUpdateDto);
        Task<ApiResponse<bool>> DeleteFormProcessAsync(int formProcessId);
    }
}
