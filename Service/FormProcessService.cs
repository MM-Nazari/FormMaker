using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using FormMaker.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Service
{
    public class FormProcessService : IFormProcessService
    {
        private readonly FormMakerDbContext _context;

        public FormProcessService(FormMakerDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<FormProcessDto>>> GetAllFormProcessesAsync()
        {
            var formProcesses = await _context.FormProcesses
                .Where(fp => !fp.IsDeleted)
                .Select(fp => new FormProcessDto
                {
                    FormProcessID = fp.FormProcessID,
                    FormID = fp.FormID,
                    ProcessID = fp.ProcessID,
                    Stage = fp.Stage,
                    CreatedAtJalali = Jalali.ToJalali(fp.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(fp.UpdatedAt),
                    IsDeleted = fp.IsDeleted
                })
                .ToListAsync();

            return new ApiResponse<IEnumerable<FormProcessDto>>(true, ResponseMessage.FormProcessRetrieved, formProcesses, 200);
        }

        public async Task<ApiResponse<FormProcessDto>> GetFormProcessByIdAsync(int formProcessId)
        {
            var formProcess = await _context.FormProcesses
                .Where(fp => fp.FormProcessID == formProcessId && !fp.IsDeleted)
                .Select(fp => new FormProcessDto
                {
                    FormProcessID = fp.FormProcessID,
                    FormID = fp.FormID,
                    ProcessID = fp.ProcessID,
                    Stage = fp.Stage,
                    CreatedAtJalali = Jalali.ToJalali(fp.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(fp.UpdatedAt),
                    IsDeleted = fp.IsDeleted
                })
                .FirstOrDefaultAsync();

            if (formProcess == null)
            {
                return new ApiResponse<FormProcessDto>(false, ResponseMessage.FormProcessNotFound, null, 404);
            }

            return new ApiResponse<FormProcessDto>(true, ResponseMessage.FormProcessRetrieved, formProcess, 200);
        }

        public async Task<ApiResponse<FormProcessDto>> CreateFormProcessAsync(FormProcessCreateDto formProcessCreateDto)
        {
            var formProcess = new FormProcess
            {
                FormID = formProcessCreateDto.FormID,
                ProcessID = formProcessCreateDto.ProcessID,
                Stage = formProcessCreateDto.Stage,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.FormProcesses.Add(formProcess);
            await _context.SaveChangesAsync();

            var formProcessDto = new FormProcessDto
            {
                FormProcessID = formProcess.FormID,
                FormID = formProcess.FormID,
                ProcessID = formProcess.ProcessID,
                Stage = formProcess.Stage,
                CreatedAtJalali = Jalali.ToJalali(formProcess.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(formProcess.UpdatedAt),
                IsDeleted = formProcess.IsDeleted
            };

            return new ApiResponse<FormProcessDto>(true, ResponseMessage.FormProcessCreated, formProcessDto, 201);
        }

        public async Task<ApiResponse<FormProcessDto>> UpdateFormProcessAsync(FormProcessUpdateDto formProcessUpdateDto)
        {
            var formProcess = await _context.FormProcesses
                .FirstOrDefaultAsync(fp => fp.FormProcessID == formProcessUpdateDto.FormProcessID && !fp.IsDeleted);

            if (formProcess == null)
            {
                return new ApiResponse<FormProcessDto>(false, ResponseMessage.FormProcessNotFound, null, 404);
            }

            formProcess.FormID = formProcessUpdateDto.FormID;
            formProcess.ProcessID = formProcessUpdateDto.ProcessID;
            formProcess.Stage = formProcessUpdateDto.Stage;
            formProcess.UpdatedAt = DateTime.UtcNow;

            _context.FormProcesses.Update(formProcess);
            await _context.SaveChangesAsync();

            var formProcessDto = new FormProcessDto
            {
                FormProcessID = formProcess.FormID,
                FormID = formProcess.FormID,
                ProcessID = formProcess.ProcessID,
                Stage = formProcess.Stage,
                CreatedAtJalali = Jalali.ToJalali(formProcess.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(formProcess.UpdatedAt),
                IsDeleted = formProcess.IsDeleted
            };

            return new ApiResponse<FormProcessDto>(true, ResponseMessage.FormProcessUpdated, formProcessDto, 200);
        }

        public async Task<ApiResponse<bool>> DeleteFormProcessAsync(int formProcessId)
        {
            var formProcess = await _context.FormProcesses
                .FirstOrDefaultAsync(fp => fp.FormProcessID == formProcessId && !fp.IsDeleted);

            if (formProcess == null)
            {
                return new ApiResponse<bool>(false, ResponseMessage.FormProcessNotFound, false, 404);
            }

            formProcess.IsDeleted = true;
            formProcess.UpdatedAt = DateTime.UtcNow;

            _context.FormProcesses.Update(formProcess);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessage.FormProcessDeleted, true, 200);
        }

        public async Task<ApiResponse<IEnumerable<FormDto>>> GetFormsByProcessIdAsync(int processId)
        {
            var forms = await _context.FormProcesses
                .Where(fp => fp.ProcessID == processId && !fp.IsDeleted)
                .Select(fp => new FormDto
                {
                    FormID = fp.Form.FormID,
                    FormTitle = fp.Form.FormTitle,
                    FormDescription = fp.Form.FormDescription,
                    IsFrequent = fp.Form.IsFrequent,
                    CreatedAtJalali = Jalali.ToJalali(fp.Form.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(fp.Form.UpdatedAt),
                    IsDeleted = fp.Form.IsDeleted
                })
                .ToListAsync();

            if (!forms.Any())
            {
                return new ApiResponse<IEnumerable<FormDto>>(false, ResponseMessage.FormNotFound, null, 404);
            }

            return new ApiResponse<IEnumerable<FormDto>>(true, ResponseMessage.FormRetrieved, forms, 200);
        }
    }
}
