using FormMaker.Data.Context;
using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace FormMaker.Service
{
    public class FormQuestionProcessService : IFormQuestionProcessService
    {
        private readonly FormMakerDbContext _context;

        public FormQuestionProcessService(FormMakerDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<FormQuestionProcessDto>>> GetAllFormQuestionProcessesAsync()
        {
            var formQuestionProcesses = await _context.FormQuestionProcesses
                .Where(fqp => !fqp.IsDeleted)
                .Select(fqp => new FormQuestionProcessDto
                {
                    FormQuestionProcessID = fqp.FormQuestionProcessID,
                    FormQuestionID = fqp.FormQuestionID,
                    FormProcessID = fqp.FormProcessID,
                    CreatedAtJalali = Jalali.ToJalali(fqp.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(fqp.UpdatedAt),
                    IsDeleted = fqp.IsDeleted
                })
                .ToListAsync();

            return new ApiResponse<List<FormQuestionProcessDto>>(true, ResponseMessage.FormQuestionProcessRetrieved, formQuestionProcesses, 200);
        }

        public async Task<ApiResponse<FormQuestionProcessDto>> GetFormQuestionProcessAsync(int formQuestionProcessId)
        {
            var formQuestionProcess = await _context.FormQuestionProcesses
                .Where(fqp => fqp.FormQuestionProcessID == formQuestionProcessId && !fqp.IsDeleted)
                .Select(fqp => new FormQuestionProcessDto
                {
                    FormQuestionProcessID = fqp.FormQuestionProcessID,
                    FormQuestionID = fqp.FormQuestionID,
                    FormProcessID = fqp.FormProcessID,
                    CreatedAtJalali = Jalali.ToJalali(fqp.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(fqp.UpdatedAt),
                    IsDeleted = fqp.IsDeleted
                })
                .FirstOrDefaultAsync();

            if (formQuestionProcess == null)
            {
                return new ApiResponse<FormQuestionProcessDto>(false, ResponseMessage.FormQuestionProcessNotFound, null, 404);
            }

            return new ApiResponse<FormQuestionProcessDto>(true, ResponseMessage.FormQuestionProcessRetrieved, formQuestionProcess, 200);
        }

        public async Task<ApiResponse<FormQuestionProcessDto>> CreateFormQuestionProcessAsync(FormQuestionProcessCreateDto formQuestionProcessCreateDto)
        {
            var formQuestionProcess = new FormQuestionProcess
            {
                FormQuestionID = formQuestionProcessCreateDto.FormQuestionID,
                FormProcessID = formQuestionProcessCreateDto.FormProcessID,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.FormQuestionProcesses.Add(formQuestionProcess);
            await _context.SaveChangesAsync();

            var formQuestionProcessDto = new FormQuestionProcessDto
            {
                FormQuestionProcessID = formQuestionProcess.FormQuestionProcessID,
                FormQuestionID = formQuestionProcess.FormQuestionID,
                FormProcessID = formQuestionProcess.FormProcessID,
                CreatedAtJalali = Jalali.ToJalali(formQuestionProcess.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(formQuestionProcess.UpdatedAt),
                IsDeleted = formQuestionProcess.IsDeleted
            };

            return new ApiResponse<FormQuestionProcessDto>(true, ResponseMessage.FormQuestionProcessCreated, formQuestionProcessDto, 201);
        }

        public async Task<ApiResponse<FormQuestionProcessDto>> UpdateFormQuestionProcessAsync(FormQuestionProcessUpdateDto formQuestionProcessUpdateDto)
        {
            var formQuestionProcess = await _context.FormQuestionProcesses
                .FirstOrDefaultAsync(fqp => fqp.FormQuestionProcessID == formQuestionProcessUpdateDto.FormQuestionProcessID && !fqp.IsDeleted);

            if (formQuestionProcess == null)
            {
                return new ApiResponse<FormQuestionProcessDto>(false, ResponseMessage.FormQuestionProcessNotFound, null, 404);
            }

            formQuestionProcess.FormQuestionID = formQuestionProcessUpdateDto.FormQuestionID;
            formQuestionProcess.FormProcessID = formQuestionProcessUpdateDto.FormProcessID;
            formQuestionProcess.UpdatedAt = DateTime.UtcNow;

            _context.FormQuestionProcesses.Update(formQuestionProcess);
            await _context.SaveChangesAsync();

            var formQuestionProcessDto = new FormQuestionProcessDto
            {
                FormQuestionProcessID = formQuestionProcess.FormQuestionProcessID,
                FormQuestionID = formQuestionProcess.FormQuestionID,
                FormProcessID = formQuestionProcess.FormProcessID,
                CreatedAtJalali = Jalali.ToJalali(formQuestionProcess.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(formQuestionProcess.UpdatedAt),
                IsDeleted = formQuestionProcess.IsDeleted
            };

            return new ApiResponse<FormQuestionProcessDto>(true, ResponseMessage.FormQuestionProcessUpdated, formQuestionProcessDto, 200);
        }

        public async Task<ApiResponse<bool>> DeleteFormQuestionProcessAsync(int formQuestionProcessId)
        {
            var formQuestionProcess = await _context.FormQuestionProcesses
                .FirstOrDefaultAsync(fqp => fqp.FormQuestionProcessID == formQuestionProcessId && !fqp.IsDeleted);

            if (formQuestionProcess == null)
            {
                return new ApiResponse<bool>(false, ResponseMessage.FormQuestionProcessNotFound, false, 404);
            }

            formQuestionProcess.IsDeleted = true;
            formQuestionProcess.UpdatedAt = DateTime.UtcNow;

            _context.FormQuestionProcesses.Update(formQuestionProcess);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessage.FormQuestionProcessDeleted, true, 200);
        }

        public async Task<ApiResponse<IEnumerable<AnswerDto>>> GetAnswersByProcessIdAsync(int processId)
        {
            // Retrieve answers for the given process ID
            var answers = await _context.FormQuestionProcesses
                .Where(fpq => fpq.FormProcess.ProcessID == processId && !fpq.IsDeleted)
                .SelectMany(fpq => _context.Answers
                    .Where(a => a.FormQuestionProcessID == fpq.FormQuestionProcessID && !a.IsDeleted)
                    .Select(a => new AnswerDto
                    {
                        AnswerID = a.AnswerID,
                        FormQuestionProcessID = a.FormQuestionProcessID,
                        AnswerText = a.AnswerText,
                        AnswerOptionID = a.AnswerOptionID,
                        FilePath = a.FilePath,
                        IsCaptchaSolved = a.IsCaptchaSolved,
                        CaptchaAnswer = a.CaptchaAnswer,
                        CreatedAtJalali = Jalali.ToJalali(a.CreatedAt),
                        UpdatedAtJalali = Jalali.ToJalali(a.UpdatedAt),
                        IsDeleted = a.IsDeleted
                    })
                )
                .ToListAsync();

            if (!answers.Any())
            {
                return new ApiResponse<IEnumerable<AnswerDto>>(false, ResponseMessage.AnswerNotFound, null, 404);
            }

            return new ApiResponse<IEnumerable<AnswerDto>>(true, ResponseMessage.AnswerRetrieved, answers, 200);
        }
    }
}
