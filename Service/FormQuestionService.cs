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
                    CreatedAt = fq.CreatedAt,
                    UpdatedAt = fq.UpdatedAt
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
                    CreatedAt = fq.CreatedAt,
                    UpdatedAt = fq.UpdatedAt
                })
            .FirstOrDefaultAsync();

            if (formQuestion == null)
            {
                return new ApiResponse<FormQuestionDto>(false, ResponseMessage.FormQuestionNotFound, null, 404);
            }

            return new ApiResponse<FormQuestionDto>(true, ResponseMessage.FormQuestionRetrieved, formQuestion, 200);
        }

        public async Task<ApiResponse<FormQuestionDto>> CreateFormQuestionAsync(FormQuestionDto formQuestionDto)
        {
            var formQuestion = new FormQuestion
            {
                FormID = formQuestionDto.FormID,
                QuestionID = formQuestionDto.QuestionID,
                QuestionOrder = formQuestionDto.QuestionOrder,
                IsRequired = formQuestionDto.IsRequired,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.FormQuestions.Add(formQuestion);
            await _context.SaveChangesAsync();

            formQuestionDto.FormQuestionID = formQuestion.FormQuestionID;
            return new ApiResponse<FormQuestionDto>(true, ResponseMessage.FormQuestionCreated, formQuestionDto, 201);
        }

        public async Task<ApiResponse<FormQuestionDto>> UpdateFormQuestionAsync(int formQuestionId, FormQuestionDto formQuestionDto)
        {
            var formQuestion = await _context.FormQuestions
                .FirstOrDefaultAsync(fq => fq.FormQuestionID == formQuestionId && !fq.IsDeleted);

            if (formQuestion == null)
            {
                return new ApiResponse<FormQuestionDto>(false, ResponseMessage.FormQuestionNotFound, null, 404);
            }

            formQuestion.FormID = formQuestionDto.FormID;
            formQuestion.QuestionID = formQuestionDto.QuestionID;
            formQuestion.QuestionOrder = formQuestionDto.QuestionOrder;
            formQuestion.IsRequired = formQuestionDto.IsRequired;
            formQuestion.UpdatedAt = DateTime.UtcNow;

            _context.FormQuestions.Update(formQuestion);
            await _context.SaveChangesAsync();

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
    }
}
