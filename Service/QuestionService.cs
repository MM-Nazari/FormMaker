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

        public async Task<ApiResponse<QuestionDTO>> GetQuestionByIdAsync(int questionId)
        {
            var question = await _context.Questions
                .Where(q => q.QuestionID == questionId)
                .Select(q => new QuestionDTO
                {
                    QuestionID = q.QuestionID,
                    QuestionTitle = q.QuestionTitle,
                    QuestionType = q.QuestionType,
                    ValidationRule = q.ValidationRule,
                    CreatedAt = q.CreatedAt,
                    UpdatedAt = q.UpdatedAt,
                    IsDeleted = q.IsDeleted,
                    IsFrequent = q.IsFrequent
                })
                .FirstOrDefaultAsync();

            if (question == null)
            {
                return new ApiResponse<QuestionDTO>(false, ResponseMessage.QuestionNotFound, null, 404);
            }

            return new ApiResponse<QuestionDTO>(true, ResponseMessage.QuestionRetrieved, question, 200);
        }

        public async Task<ApiResponse<IEnumerable<QuestionDTO>>> GetAllQuestionsAsync()
        {
            var questions = await _context.Questions
                .Select(q => new QuestionDTO
                {
                    QuestionID = q.QuestionID,
                    QuestionTitle = q.QuestionTitle,
                    QuestionType = q.QuestionType,
                    ValidationRule = q.ValidationRule,
                    CreatedAt = q.CreatedAt,
                    UpdatedAt = q.UpdatedAt,
                    IsDeleted = q.IsDeleted,
                    IsFrequent = q.IsFrequent
                })
                .ToListAsync();

            return new ApiResponse<IEnumerable<QuestionDTO>>(true, ResponseMessage.QuestionRetrieved, questions, 200);
        }

        public async Task<ApiResponse<QuestionDTO>> CreateQuestionAsync(QuestionDTO questionDto)
        {
            var question = new Question
            {
                QuestionTitle = questionDto.QuestionTitle,
                QuestionType = questionDto.QuestionType,
                ValidationRule = questionDto.ValidationRule,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                IsFrequent = questionDto.IsFrequent
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            var createdQuestionDto = new QuestionDTO
            {
                QuestionID = question.QuestionID,
                QuestionTitle = question.QuestionTitle,
                QuestionType = question.QuestionType,
                ValidationRule = question.ValidationRule,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
                IsDeleted = question.IsDeleted,
                IsFrequent = question.IsFrequent
            };

            return new ApiResponse<QuestionDTO>(true, ResponseMessage.QuestionCreated, createdQuestionDto, 201);
        }

        public async Task<ApiResponse<QuestionDTO>> UpdateQuestionAsync(int questionId, QuestionDTO questionDto)
        {
            var question = await _context.Questions
                .FirstOrDefaultAsync(q => q.QuestionID == questionId);

            if (question == null)
            {
                return new ApiResponse<QuestionDTO>(false, ResponseMessage.QuestionNotFound, null, 404);
            }

            question.QuestionTitle = questionDto.QuestionTitle;
            question.QuestionType = questionDto.QuestionType;
            question.ValidationRule = questionDto.ValidationRule;
            question.UpdatedAt = DateTime.UtcNow;
            question.IsFrequent = questionDto.IsFrequent;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync();

            var updatedQuestionDto = new QuestionDTO
            {
                QuestionID = question.QuestionID,
                QuestionTitle = question.QuestionTitle,
                QuestionType = question.QuestionType,
                ValidationRule = question.ValidationRule,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
                IsDeleted = question.IsDeleted,
                IsFrequent = question.IsFrequent
            };

            return new ApiResponse<QuestionDTO>(true, ResponseMessage.QuestionUpdated, updatedQuestionDto, 200);
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
