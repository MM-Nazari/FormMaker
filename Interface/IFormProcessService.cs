using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IFormProcessService
    {
        Task<ApiResponse<IEnumerable<FormProcessDTO>>> GetAllFormProcessesAsync();
        Task<ApiResponse<FormProcessDTO>> GetFormProcessByIdAsync(int formProcessId);
        Task<ApiResponse<FormProcessDTO>> CreateFormProcessAsync(FormProcessDTO formProcessDto);
        Task<ApiResponse<FormProcessDTO>> UpdateFormProcessAsync(int formProcessId, FormProcessDTO formProcessDto);
        Task<ApiResponse<bool>> DeleteFormProcessAsync(int formProcessId);
    }
}
