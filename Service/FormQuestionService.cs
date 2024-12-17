using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using FormMaker.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Service
{
    public class FormQuestionService : IFormQuestionService
    {
        private readonly FormMakerDbContext _context;

        public FormQuestionService(FormMakerDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<FormQuestionDto>>> GetAllFormQuestionsAsync()
        {
            var formQuestions = await _context.FormQuestions
                .Where(fq => !fq.IsDeleted)
                .Select(fq => new FormQuestionDto
                {
                    FormQuestionID = fq.FormQuestionID,
                    FormID = fq.FormID,
                    QuestionID = fq.QuestionID,
                    QuestionOrder = fq.QuestionOrder,
                    IsRequired = fq.IsRequired,
                    CreatedAtJalali = Jalali.ToJalali(fq.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(fq.UpdatedAt),
                    IsDeleted = fq.IsDeleted
                })
            .ToListAsync();

            return new ApiResponse<IEnumerable<FormQuestionDto>>(true, ResponseMessage.FormQuestionRetrieved, formQuestions, 200);
        }

        public async Task<ApiResponse<FormQuestionDto>> GetFormQuestionByIdAsync(int formQuestionId)
        {
            var formQuestion = await _context.FormQuestions
                .Where(fq => fq.FormQuestionID == formQuestionId && !fq.IsDeleted)
                .Select(fq => new FormQuestionDto
                {
                    FormQuestionID = fq.FormQuestionID,
                    FormID = fq.FormID,
                    QuestionID = fq.QuestionID,
                    QuestionOrder = fq.QuestionOrder,
                    IsRequired = fq.IsRequired,
                    CreatedAtJalali = Jalali.ToJalali(fq.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(fq.UpdatedAt),
                    IsDeleted = fq.IsDeleted
                })
            .FirstOrDefaultAsync();

            if (formQuestion == null)
            {
                return new ApiResponse<FormQuestionDto>(false, ResponseMessage.FormQuestionNotFound, null, 404);
            }

            return new ApiResponse<FormQuestionDto>(true, ResponseMessage.FormQuestionRetrieved, formQuestion, 200);
        }

        public async Task<ApiResponse<FormQuestionDto>> CreateFormQuestionAsync(FormQuestionCreateDto formQuestionCreateDto)
        {
            var formQuestion = new FormQuestion
            {
                FormID = formQuestionCreateDto.FormID,
                QuestionID = formQuestionCreateDto.QuestionID,
                QuestionOrder = formQuestionCreateDto.QuestionOrder,
                IsRequired = formQuestionCreateDto.IsRequired,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.FormQuestions.Add(formQuestion);
            await _context.SaveChangesAsync();

            var formQuestionDto = new FormQuestionDto
            {
                FormQuestionID = formQuestion.FormQuestionID,
                FormID = formQuestion.FormID,
                QuestionID = formQuestion.QuestionID,
                QuestionOrder = formQuestion.QuestionOrder,
                IsRequired = formQuestion.IsRequired,
                CreatedAtJalali = Jalali.ToJalali(formQuestion.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(formQuestion.UpdatedAt),
                IsDeleted = formQuestion.IsDeleted
            };

            return new ApiResponse<FormQuestionDto>(true, ResponseMessage.FormQuestionCreated, formQuestionDto, 201);
        }

        public async Task<ApiResponse<FormQuestionDto>> UpdateFormQuestionAsync(FormQuestionUpdateDto formQuestionUpdateDto)
        {
            var formQuestion = await _context.FormQuestions
                .FirstOrDefaultAsync(fq => fq.FormQuestionID == formQuestionUpdateDto.FormQuestionID && !fq.IsDeleted);

            if (formQuestion == null)
            {
                return new ApiResponse<FormQuestionDto>(false, ResponseMessage.FormQuestionNotFound, null, 404);
            }

            formQuestion.FormID = formQuestionUpdateDto.FormID;
            formQuestion.QuestionID = formQuestionUpdateDto.QuestionID;
            formQuestion.QuestionOrder = formQuestionUpdateDto.QuestionOrder;
            formQuestion.IsRequired = formQuestionUpdateDto.IsRequired;
            formQuestion.UpdatedAt = DateTime.UtcNow;

            _context.FormQuestions.Update(formQuestion);
            await _context.SaveChangesAsync();

            var formQuestionDto = new FormQuestionDto
            {
                FormQuestionID = formQuestion.FormQuestionID,
                FormID = formQuestion.FormID,
                QuestionID = formQuestion.QuestionID,
                QuestionOrder = formQuestion.QuestionOrder,
                IsRequired = formQuestion.IsRequired,
                CreatedAtJalali = Jalali.ToJalali(formQuestion.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(formQuestion.UpdatedAt),
                IsDeleted = formQuestion.IsDeleted
            };

            return new ApiResponse<FormQuestionDto>(true, ResponseMessage.FormQuestionUpdated, formQuestionDto, 200);
        }

        public async Task<ApiResponse<bool>> DeleteFormQuestionAsync(int formQuestionId)
        {
            var formQuestion = await _context.FormQuestions
                .FirstOrDefaultAsync(fq => fq.FormQuestionID == formQuestionId && !fq.IsDeleted);

            if (formQuestion == null)
            {
                return new ApiResponse<bool>(false, ResponseMessage.FormQuestionNotFound, false, 404);
            }

            formQuestion.IsDeleted = true;
            formQuestion.UpdatedAt = DateTime.UtcNow;

            _context.FormQuestions.Update(formQuestion);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessage.FormQuestionDeleted, true, 200);
        }

        public async Task<ApiResponse<IEnumerable<QuestionDto>>> GetQuestionsByFormIdAsync(int formId)
        {
            var questions = await _context.FormQuestions
                .Where(fq => fq.FormID == formId && !fq.IsDeleted)
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
                return new ApiResponse<IEnumerable<QuestionDto>>(false, ResponseMessage.QuestionNotFound, null, 404);
            }

            return new ApiResponse<IEnumerable<QuestionDto>>(true, ResponseMessage.QuestionRetrieved, questions, 200);
        }
    }
}
