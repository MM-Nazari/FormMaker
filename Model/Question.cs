namespace FormMaker.Model
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionType { get; set; }  
        public string ValidationRule { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Relations
        public ICollection<AnswerOption> AnswerOptions { get; set; }
        public ICollection<FormQuestion> FormQuestions { get; set; }
    }
}
