using FormMaker.Data.Context;
using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using Microsoft.EntityFrameworkCore;
using System;

namespace FormMaker.Service
{
    public class FormService : IFormService
    {
        private readonly FormMakerDbContext _context;

        public FormService(FormMakerDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<FormDto>>> GetAllFormsAsync()
        {
            var forms = await _context.Forms
                .Where(f => !f.IsDeleted)
                .Select(f => new FormDto
                {
                    ProcessIDs = f.FormProcesses
                        .Where(fp => !fp.IsDeleted)
                        .Select(fp => fp.ProcessID)
                        .Distinct()
                        .ToList(),
                    FormID = f.FormID,
                    FormTitle = f.FormTitle,
                    FormDescription = f.FormDescription,
                    IsFrequent = f.IsFrequent,
                    CreatedAtJalali = Jalali.ToJalali(f.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(f.UpdatedAt),
                    IsDeleted = f.IsDeleted
                })
                .ToListAsync();

            return new ApiResponse<IEnumerable<FormDto>>(true, ResponseMessage.FormRetrieved, forms, 200);
        }

        public async Task<ApiResponse<FormDto>> GetFormByIdAsync(int formId)
        {
            var form = await _context.Forms
                .Where(f => !f.IsDeleted && f.FormID == formId)
                .Select(f => new FormDto
                {
                    ProcessIDs = f.FormProcesses
                        .Where(fp => !fp.IsDeleted)
                        .Select(fp => fp.ProcessID)
                        .Distinct()
                        .ToList(),
                    FormID = f.FormID,
                    FormTitle = f.FormTitle,
                    FormDescription = f.FormDescription,
                    IsFrequent = f.IsFrequent,
                    CreatedAtJalali = Jalali.ToJalali(f.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(f.UpdatedAt),
                    IsDeleted = f.IsDeleted
                })
            .FirstOrDefaultAsync();

            if (form == null)
            {
                return new ApiResponse<FormDto>(false, ResponseMessage.FormNotFound, null, 404);
            }
            return new ApiResponse<FormDto>(true, ResponseMessage.FormRetrieved, form, 200);
        }

        public async Task<ApiResponse<FormDto>> CreateFormAsync(FormCreateDto formCraeteDto)
        {
            var form = new Form
            {
                FormTitle = formCraeteDto.FormTitle,
                FormDescription = formCraeteDto.FormDescription,
                IsFrequent = formCraeteDto.IsFrequent,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Forms.Add(form);
            await _context.SaveChangesAsync();

            var formDto = new FormDto
            {
                ProcessIDs = form.FormProcesses
                        .Where(fp => !fp.IsDeleted)
                        .Select(fp => fp.ProcessID)
                        .Distinct()
                        .ToList(),
                FormID = form.FormID,
                FormTitle = form.FormTitle,
                FormDescription = form.FormDescription,
                IsFrequent = form.IsFrequent,
                CreatedAtJalali = Jalali.ToJalali(form.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(form.UpdatedAt),
                IsDeleted = form.IsDeleted
            };

            return new ApiResponse<FormDto>(true, ResponseMessage.FormCreated, formDto, 201);
        }

        public async Task<ApiResponse<FormDto>> UpdateFormAsync(FormUpdateDto formUpdateDto)
        {
            var form = await _context.Forms.FirstOrDefaultAsync(f => f.FormID == formUpdateDto.FormID && !f.IsDeleted);

            if (form == null)
            {
                return new ApiResponse<FormDto>(false, ResponseMessage.FormNotFound, null, 404);
            }

            form.FormTitle = formUpdateDto.FormTitle;
            form.FormDescription = formUpdateDto.FormDescription;
            form.UpdatedAt = DateTime.UtcNow;
            form.IsFrequent = formUpdateDto.IsFrequent;

            _context.Forms.Update(form);
            await _context.SaveChangesAsync();

            var formDto = new FormDto
            {
                ProcessIDs = form.FormProcesses.Where(fp => !fp.IsDeleted).Select(fp => fp.ProcessID).Distinct().ToList(),
                FormID = form.FormID,
                FormTitle = form.FormTitle,
                FormDescription = form.FormDescription,
                IsFrequent = form.IsFrequent,
                CreatedAtJalali = Jalali.ToJalali(form.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(form.UpdatedAt),
                IsDeleted = form.IsDeleted
            };

            return new ApiResponse<FormDto>(true, ResponseMessage.FormUpdated, formDto, 200);
        }

        public async Task<ApiResponse<bool>> DeleteFormAsync(int formId)
        {
            var form = await _context.Forms.FirstOrDefaultAsync(f => f.FormID == formId && !f.IsDeleted);

            if (form == null)
            {
                return new ApiResponse<bool>(false, ResponseMessage.FormNotFound, false, 404);
            }

            form.IsDeleted = true;
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessage.FormDeleted, true, 200);
        }

        public async Task<ApiResponse<IEnumerable<FormDto>>> GetFrequentFormsAsync()
        {
            var frequentForms = await _context.Forms
                .Where(f => f.IsFrequent && !f.IsDeleted)
                .Select(f => new FormDto
                {
                    ProcessIDs = f.FormProcesses.Where(fp => !fp.IsDeleted).Select(fp => fp.ProcessID).Distinct().ToList(),
                    FormID = f.FormID,
                    FormTitle = f.FormTitle,
                    FormDescription = f.FormDescription,
                    IsFrequent = f.IsFrequent,
                    CreatedAtJalali = Jalali.ToJalali(f.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(f.UpdatedAt),
                    IsDeleted = f.IsDeleted
                })
                .ToListAsync();

            return new ApiResponse<IEnumerable<FormDto>>(true, ResponseMessage.FormRetrieved, frequentForms, 200);
        }

        public async Task<ApiResponse<FormWithProcessDto>> CreateFormWithProcessAsync(FormWithProcessCreateDto formWithProcessCreateDto)
        {
                // Step 1: Check if the process exists
                var process = await _context.Processes.FindAsync(formWithProcessCreateDto.ProcessID);
                if (process == null)
                {
                    return new ApiResponse<FormWithProcessDto>(false, ResponseMessage.ProcessNotFound, null, StatusCodes.Status404NotFound);
                }

                // Step 2: Create the form
                var form = new Form
                {
                    FormTitle = formWithProcessCreateDto.FormTitle,
                    FormDescription = formWithProcessCreateDto.FormDescription,
                    IsFrequent = formWithProcessCreateDto.IsFrequent,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                _context.Forms.Add(form);
                await _context.SaveChangesAsync();

            var stageExists = await _context.FormProcesses
                .AnyAsync(fp => fp.ProcessID == formWithProcessCreateDto.ProcessID && fp.Stage == formWithProcessCreateDto.Stage && !fp.IsDeleted);

            if (stageExists)
            {
                return new ApiResponse<FormWithProcessDto>(
                    false,
                    ResponseMessage.StageDuplicateForProcess,
                    null,
                    StatusCodes.Status400BadRequest
                );
            }

            // Step 3: Create the FormProcess and link it to the form
            var formProcess = new FormProcess
                {
                    FormID = form.FormID,
                    ProcessID = formWithProcessCreateDto.ProcessID,
                    Stage = formWithProcessCreateDto.Stage, 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                _context.FormProcesses.Add(formProcess);
                await _context.SaveChangesAsync();

                // Step 4: Prepare the response DTO
                var formWithProcessDto = new FormWithProcessDto
                {
                    FormID = form.FormID,
                    FormTitle = form.FormTitle,
                    FormDescription = form.FormDescription,
                    IsFrequent = form.IsFrequent,
                    CreatedAtJalali = Jalali.ToJalali(form.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(form.UpdatedAt),
                    IsDeleted = form.IsDeleted,
                    ProcessID = formProcess.ProcessID,
                    Stage = formProcess.Stage
                };

                return new ApiResponse<FormWithProcessDto>(true, ResponseMessage.FormLinkedProcess, formWithProcessDto, 201);

        }

        public async Task<ApiResponse<IEnumerable<QuestionDto>>> GetQuestionsByFormIdAsync(int formId)
        {
            // Check if the form exists and is not deleted
            var formExists = await _context.Forms.AnyAsync(f => f.FormID == formId && !f.IsDeleted);
            if (!formExists)
            {
                return new ApiResponse<IEnumerable<QuestionDto>>(false, ResponseMessage.FormNotFound, null, 404);
            }

            // Fetch questions associated with the form
            var questions = await _context.FormQuestions
                .Where(fq => fq.FormID == formId && !fq.IsDeleted && !fq.Question.IsDeleted)
                .OrderBy(fq => fq.QuestionOrder)
                .Select(fq => new QuestionDto
                {
                    QuestionID = fq.Question.QuestionID,
                    QuestionTitle = fq.Question.QuestionTitle,
                    QuestionType = fq.Question.QuestionType,
                    ValidationRule = fq.Question.ValidationRule,
                    IsFrequent = fq.Question.IsFrequent,
                    CreatedAtJalali = Jalali.ToJalali(fq.Question.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(fq.Question.UpdatedAt),
                    IsDeleted = fq.Question.IsDeleted
                })
                .ToListAsync();

            if (!questions.Any())
            {
                return new ApiResponse<IEnumerable<QuestionDto>>(false, ResponseMessage.NoQuestionsFoundForForm, null, 404);
            }

            return new ApiResponse<IEnumerable<QuestionDto>>(true, ResponseMessage.QuestionRetrieved, questions, 200);
        }

    }
}
