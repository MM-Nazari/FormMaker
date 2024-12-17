using FormMaker.Data.Context;
using FormMaker.Dto;
using FormMaker.Interface;
using FormMaker.Model;
using FormMaker.Util;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly FormMakerDbContext _context;

        public QuestionService(FormMakerDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<QuestionDto>> GetQuestionByIdAsync(int questionId)
        {
            var question = await _context.Questions
                .Where(q => q.QuestionID == questionId)
                .Select(q => new QuestionDto
                {
                    QuestionID = q.QuestionID,
                    QuestionTitle = q.QuestionTitle,
                    QuestionType = q.QuestionType,
                    ValidationRule = q.ValidationRule,
                    IsFrequent = q.IsFrequent,
                    CreatedAtJalali = Jalali.ToJalali(q.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(q.UpdatedAt),
                    IsDeleted = q.IsDeleted
                })
                .FirstOrDefaultAsync();

            if (question == null)
            {
                return new ApiResponse<QuestionDto>(false, ResponseMessage.QuestionNotFound, null, 404);
            }

            return new ApiResponse<QuestionDto>(true, ResponseMessage.QuestionRetrieved, question, 200);
        }

        public async Task<ApiResponse<IEnumerable<QuestionDto>>> GetAllQuestionsAsync()
        {
            var questions = await _context.Questions
                .Select(q => new QuestionDto
                {
                    QuestionID = q.QuestionID,
                    QuestionTitle = q.QuestionTitle,
                    QuestionType = q.QuestionType,
                    ValidationRule = q.ValidationRule,
                    IsFrequent = q.IsFrequent,
                    CreatedAtJalali = Jalali.ToJalali(q.CreatedAt),
                    UpdatedAtJalali = Jalali.ToJalali(q.UpdatedAt),
                    IsDeleted = q.IsDeleted
                })
                .ToListAsync();

            return new ApiResponse<IEnumerable<QuestionDto>>(true, ResponseMessage.QuestionRetrieved, questions, 200);
        }

        public async Task<ApiResponse<QuestionDto>> CreateQuestionAsync(QuestionCreateDto questionCreateDto)
        {
            var question = new Question
            {
                QuestionTitle = questionCreateDto.QuestionTitle,
                QuestionType = questionCreateDto.QuestionType,
                ValidationRule = questionCreateDto.ValidationRule,
                IsFrequent = questionCreateDto.IsFrequent,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            var questionDto = new QuestionDto
            {
                QuestionID = question.QuestionID,
                QuestionTitle = question.QuestionTitle,
                QuestionType = question.QuestionType,
                ValidationRule = question.ValidationRule,
                IsFrequent = question.IsFrequent,
                CreatedAtJalali = Jalali.ToJalali(question.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(question.UpdatedAt),
                IsDeleted = question.IsDeleted
            };

            return new ApiResponse<QuestionDto>(true, ResponseMessage.QuestionCreated, questionDto, 201);
        }

        public async Task<ApiResponse<QuestionDto>> UpdateQuestionAsync(QuestionUpdateDto questionUpdateDto)
        {
            var question = await _context.Questions
                .FirstOrDefaultAsync(q => q.QuestionID == questionUpdateDto.QuestionID);

            if (question == null)
            {
                return new ApiResponse<QuestionDto>(false, ResponseMessage.QuestionNotFound, null, 404);
            }

            question.QuestionTitle = questionUpdateDto.QuestionTitle;
            question.QuestionType = questionUpdateDto.QuestionType;
            question.ValidationRule = questionUpdateDto.ValidationRule;
            question.IsFrequent = questionUpdateDto.IsFrequent;
            question.UpdatedAt = DateTime.UtcNow;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync();

            var questionDto = new QuestionDto
            {
                QuestionID = question.QuestionID,
                QuestionTitle = question.QuestionTitle,
                QuestionType = question.QuestionType,
                ValidationRule = question.ValidationRule,
                IsFrequent = question.IsFrequent,
                CreatedAtJalali = Jalali.ToJalali(question.CreatedAt),
                UpdatedAtJalali = Jalali.ToJalali(question.UpdatedAt),
                IsDeleted = question.IsDeleted
            };

            return new ApiResponse<QuestionDto>(true, ResponseMessage.QuestionUpdated, questionDto, 200);
        }

        public async Task<ApiResponse<bool>> DeleteQuestionAsync(int questionId)
        {
            var question = await _context.Questions
                .FirstOrDefaultAsync(q => q.QuestionID == questionId);

            if (question == null)
            {
                return new ApiResponse<bool>(false, ResponseMessage.QuestionNotFound, false, 404);
            }

            question.IsDeleted = true;
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessage.QuestionDeleted, true, 200);
        }
    }
}
