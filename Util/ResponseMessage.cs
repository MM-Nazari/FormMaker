using FormMaker.Dto;

namespace FormMaker.Util
{
    public class ResponseMessage
    {
        // Question
        public const string QuestionNotFound = "Question not found";
        public const string QuestionRetrieved = "Question retrieved successfully";
        public const string QuestionCreated = "Question created successfully";
        public const string QuestionUpdated = "Question updated successfully";
        public const string QuestionDeleted = "Question deleted successfully";

        // Process
        public const string ProcessNotFound = "Process not found";
        public const string ProcessRetrieved = "Process retrieved successfully";
        public const string ProcessCreated = "Process created successfully";
        public const string ProcessUpdated = "Process updated successfully";
        public const string ProcessDeleted = "Process deleted successfully";

        // Form
        public const string FormNotFound = "Form not found";
        public const string FormRetrieved = "Form retrieved successfully";
        public const string FormCreated = "Form created successfully";
        public const string FormUpdated = "Form updated successfully";
        public const string FormDeleted = "Form deleted successfully";

        // Answer
        public const string AnswerNotFound = "Answer not found";
        public const string AnswerRetrieved = "Answer retrieved successfully";
        public const string AnswerCreated = "Answer created successfully";
        public const string AnswerUpdated = "Answer updated successfully";
        public const string AnswerDeleted = "Answer deleted successfully";

        // AnswerOption
        public const string AnswerOptionNotFound = "AnswerOption not found";
        public const string AnswerOptionRetrieved = "AnswerOption retrieved successfully";
        public const string AnswerOptionCreated = "AnswerOption created successfully";
        public const string AnswerOptionUpdated = "AnswerOption updated successfully";
        public const string AnswerOptionDeleted = "AnswerOption deleted successfully";

        // Form Process
        public const string FormProcessNotFound = "Form process not found";
        public const string FormProcessRetrieved = "Form process retrieved successfully";
        public const string FormProcessCreated = "Form process created successfully";
        public const string FormProcessUpdated = "Form process updated successfully";
        public const string FormProcessDeleted = "Form process deleted successfully";
        public const string NoFormsFoundForProcess = "There is not any Form Related to this Process";
        public const string StageDuplicateForProcess = "Stage is Duplicated";

        public const string FormLinkedProcess = "Form created and linked to process successfully";

        // Form Question
        public const string FormQuestionNotFound = "Form question not found";
        public const string FormQuestionRetrieved = "Form question retrieved successfully";
        public const string FormQuestionCreated = "Form question created successfully";
        public const string FormQuestionUpdated = "Form question updated successfully";
        public const string FormQuestionDeleted = "Form question deleted successfully";
        public const string NoQuestionsFoundForForm = "There is no Question Related to this Form";
        public const string QuestionOrderDuplicateForForm = "Question Order is Duplicated";

        public const string QuestionLinkedForn = "Question created and linked to form and process successfully";

        // FormQuestionProcess
        public const string FormQuestionProcessNotFound = "Form question process not found";
        public const string FormQuestionProcessRetrieved = "Form question process retrieved successfully";
        public const string FormQuestionProcessCreated = "Form question process created successfully";
        public const string FormQuestionProcessUpdated = "Form question process updated successfully";
        public const string FormQuestionProcessDeleted = "Form question process deleted successfully";

        // Process <-> Form <-> Question
        public const string FormWithProcessAndQuestionsCreated = "Process with Form and Questions has been created";

        // Duplicate
        public const string PriorityIsDuplicate = "An answer option with priority already exists for the question";

        // Internal Server Error 
        public const string InternalServerError = "Internal Server Error";
    }
}
