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
    }
}
