using FormMaker.Dto;
using FormMaker.Model;

namespace FormMaker.Interface
{
    public interface IProcessService
    {
        Task<ApiResponse<ProcessDto>> CreateProcessAsync(ProcessDto processDto);
        Task<ApiResponse<ProcessDto>> GetProcessByIdAsync(int processId);
        Task<ApiResponse<List<ProcessDto>>> GetAllProcessesAsync();
        Task<ApiResponse<ProcessDto>> UpdateProcessAsync(int processId, ProcessDto processDto);
        Task<ApiResponse<bool>> DeleteProcessAsync(int processId);
    }
}
