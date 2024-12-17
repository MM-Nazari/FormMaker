using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IFormProcessService
    {
        Task<ApiResponse<IEnumerable<FormProcessDTO>>> GetAllFormProcessesAsync();
        Task<ApiResponse<FormProcessDTO>> GetFormProcessByIdAsync(int formProcessId);
        Task<ApiResponse<FormProcessDTO>> CreateFormProcessAsync(FormProcessCreateDto formProcessCreateDto);
        Task<ApiResponse<FormProcessDTO>> UpdateFormProcessAsync(FormProcessUpdateDto formProcessUpdateDto);
        Task<ApiResponse<bool>> DeleteFormProcessAsync(int formProcessId);
    }
}
